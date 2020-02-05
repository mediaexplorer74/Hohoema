﻿using I18NPortable;
using Mntone.Nico2;
using Mntone.Nico2.Embed.Ichiba;
using Mntone.Nico2.Live;
using Mntone.Nico2.Live.Recommend;
using Mntone.Nico2.Live.Video;
using NicoPlayerHohoema.Interfaces;
using NicoPlayerHohoema.Models;
using NicoPlayerHohoema.Models.Helpers;
using NicoPlayerHohoema.Models.Provider;
using NicoPlayerHohoema.Services;
using NicoPlayerHohoema.Services.Page;
using NicoPlayerHohoema.UseCase;
using NicoPlayerHohoema.UseCase.Playlist;
using Prism.Commands;
using Prism.Navigation;
using Prism.Unity;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Unity;
using Windows.System;
using Windows.UI.Xaml;

namespace NicoPlayerHohoema.ViewModels
{
    public class LiveCommunityInfo : Interfaces.ICommunity
    {
        public string Id { get; set; }

        public string Label { get; set; }
    }

    public sealed class LiveInfomationPageViewModel : HohoemaViewModelBase, Interfaces.ILiveContent, INavigatedAwareAsync, IPinablePage, ITitleUpdatablePage
    {
        HohoemaPin IPinablePage.GetPin()
        {
            return new HohoemaPin()
            {
                Label = LiveInfo.Video.Title,
                PageType = HohoemaPageType.LiveInfomation,
                Parameter = $"id={LiveId}"
            };
        }

        IObservable<string> ITitleUpdatablePage.GetTitleObservable()
        {
            return this.ObserveProperty(x => x.LiveInfo).Select(x => x?.Video.Title);
        }

        // TODO: 視聴開始（会場後のみ、チャンネル会員限定やチケット必要な場合あり）
        // TODO: タイムシフト予約（tsがある場合のみ、会場前のみ、プレミアムの場合は会場後でも可）
        // TODO: 後からタイムシフト予約（プレミアムの場合のみ）
        // TODO: 配信説明
        // TODO: タグ
        // TODO: 配信者（チャンネルやコミュニティ）の概要説明
        // TODO: 配信者（チャンネルやコミュニティ）のフォロー
        // TODO: オススメ生放送（放送中、放送予定を明示）
        // TODO: ニコニコ市場
        // TODO: SNS共有


        // gateとwatchをどちらも扱った上でその差を意識させないように表現する

        // LiveInfo.Video.CurrentStatusは開演前、放送中は時間の経過によって変化する可能性がある

        public LiveInfomationPageViewModel(
            ApplicationLayoutManager applicationLayoutManager,
            AppearanceSettings appearanceSettings,
            PageManager pageManager,
            Models.NiconicoSession niconicoSession,
            NicoLiveProvider nicoLiveProvider,
            DialogService dialogService,
            ExternalAccessService externalAccessService
            )
        {
            ApplicationLayoutManager = applicationLayoutManager;
            _appearanceSettings = appearanceSettings;
            PageManager = pageManager;
            NiconicoSession = niconicoSession;
            NicoLiveProvider = nicoLiveProvider;
            HohoemaDialogService = dialogService;
            ExternalAccessService = externalAccessService;
            IsLoadFailed = new ReactiveProperty<bool>(false)
               .AddTo(_CompositeDisposable);
            LoadFailedMessage = new ReactiveProperty<string>()
                .AddTo(_CompositeDisposable);



            IsLiveIdAvairable = this.ObserveProperty(x => x.LiveId)
                .Select(x => x != null ? NiconicoRegex.IsLiveId(x) : false)
                .ToReadOnlyReactiveProperty()
                .AddTo(_CompositeDisposable);



            IsLoggedIn = NiconicoSession.ObserveProperty(x => x.IsLoggedIn)
                .ToReadOnlyReactiveProperty()
                .AddTo(_CompositeDisposable);

            IsPremiumAccount = NiconicoSession.ObserveProperty(x => x.IsPremiumAccount)
                .ToReadOnlyReactiveProperty()
                .AddTo(_CompositeDisposable);



            _IsTsPreserved = new ReactiveProperty<bool>(false)
                .AddTo(_CompositeDisposable);

            LiveTags = new ReadOnlyObservableCollection<LiveTagViewModel>(_LiveTags);

            IchibaItems = new ReadOnlyObservableCollection<IchibaItem>(_IchibaItems);

            ReccomendItems = _ReccomendItems.ToReadOnlyReactiveCollection(x =>
            {
                var liveId = "lv" + x.ProgramId;
                var liveInfoVM = new LiveInfoListItemViewModel(liveId);
                liveInfoVM.Setup(x);

                var reserve = _Reservations?.ReservedProgram.FirstOrDefault(reservation => liveId == reservation.Id);
                if (reserve != null)
                {
                    liveInfoVM.SetReservation(reserve);
                }

                return liveInfoVM;
            })
                .AddTo(_CompositeDisposable);

            IsShowOpenLiveContentButton = this.ObserveProperty(x => LiveInfo)
                .Select(x =>
                {
                    if (LiveInfo == null) { return false; }

                    if (NiconicoSession.IsPremiumAccount)
                    {
                        if (LiveInfo.Video.OpenTime > DateTime.Now) { return false; }

                        return LiveInfo.Video.TimeshiftEnabled;
                    }
                    else
                    {
                        // 一般アカウントは放送中のみ
                        if (LiveInfo.Video.OpenTime > DateTime.Now) { return false; }

                        if (_IsTsPreserved.Value) { return true; }

                        if (LiveInfo.Video.EndTime < DateTime.Now) { return false; }

                        return true;
                    }
                })
                .ToReadOnlyReactiveProperty()
                .AddTo(_CompositeDisposable);

            IsShowAddTimeshiftButton = this.ObserveProperty(x => LiveInfo)
                .Select(x =>
                {
                    if (LiveInfo == null) { return false; }
                    if (!LiveInfo.Video.TimeshiftEnabled) { return false; }
                    if (!NiconicoSession.IsLoggedIn) { return false; }

                    if (!niconicoSession.IsPremiumAccount)
                    {
                        // 一般アカウントは放送開始の30分前からタイムシフトの登録はできなくなる
                        if ((LiveInfo.Video.StartTime - TimeSpan.FromMinutes(30)) < DateTime.Now) { return false; }
                    }

                    if (LiveInfo.Video.TsArchiveEndTime != null
                        && LiveInfo.Video.TsArchiveEndTime > DateTime.Now) { return false; }

                    if (_IsTsPreserved.Value) { return false; }

                    return true;
                })
                .ToReadOnlyReactiveProperty()
                .AddTo(_CompositeDisposable);

            IsShowDeleteTimeshiftButton = _IsTsPreserved;

        }


        public DialogService HohoemaDialogService { get; }
        public ExternalAccessService ExternalAccessService { get; }
        



        #region Interfaces.ILiveContent

        string Interfaces.ILiveContent.ProviderId => LiveInfo.Community?.GlobalId;
        string Interfaces.ILiveContent.ProviderName => LiveInfo.Community?.Name;
        CommunityType Interfaces.ILiveContent.ProviderType => LiveInfo.Video.ProviderType;


        string Interfaces.INiconicoObject.Id => LiveInfo.Video.Id;

        string Interfaces.INiconicoObject.Label => LiveInfo.Video.Title;

        #endregion

        public ReactiveProperty<bool> IsLoadFailed { get; }
        public ReactiveProperty<string> LoadFailedMessage { get; }

        private string _LiveId;
        public string LiveId
        {
            get { return _LiveId; }
            private set { SetProperty(ref _LiveId, value); }
        }

        public IReadOnlyReactiveProperty<bool> IsLiveIdAvairable { get; }

        private VideoInfo _LiveInfo;
        public VideoInfo LiveInfo
        {
            get { return _LiveInfo; }
            private set
            {
                if (SetProperty(ref _LiveInfo, value))
                {
                    LivePageUrl = _LiveInfo != null ? NiconicoUrls.LiveWatchPageUrl + LiveId : null;
                }
            }
        }

        // 放送説明
        private Uri _HtmlDescription;
        public Uri HtmlDescription
        {
            get { return _HtmlDescription; }
            private set { SetProperty(ref _HtmlDescription, value); }
        }


        // コミュニティ放送者情報
        private LiveCommunityInfo _Community;
        public LiveCommunityInfo Community
        {
            get { return _Community; }
            private set { SetProperty(ref _Community, value); }
        }

        private DateTime? _ExpiredTime;
        public DateTime? ExpiredTime
        {
            get { return _ExpiredTime; }
            private set { SetProperty(ref _ExpiredTime, value); }
        }


        private string _LivePageUrl;
        public string LivePageUrl
        {
            get { return _LivePageUrl; }
            private set { SetProperty(ref _LivePageUrl, value); }
        }

        private DelegateCommand<object> _ScriptNotifyCommand;
        public DelegateCommand<object> ScriptNotifyCommand
        {
            get
            {
                return _ScriptNotifyCommand
                    ?? (_ScriptNotifyCommand = new DelegateCommand<object>(async (parameter) =>
                    {
                        Uri url = parameter as Uri ?? (parameter as HyperlinkItem)?.Url;
                        if (url != null)
                        {
                            System.Diagnostics.Debug.WriteLine($"script notified: {url}");

                            if (false == PageManager.OpenPage(url))
                            {
                                await Launcher.LaunchUriAsync(url);
                            }
                        }
                    }));
            }
        }

        // ログイン状態
        public IReadOnlyReactiveProperty<bool> IsPremiumAccount { get; }
        public IReadOnlyReactiveProperty<bool> IsLoggedIn { get; }


        // タイムシフト
        private ReactiveProperty<bool> _IsTsPreserved { get; }
        public IReadOnlyReactiveProperty<bool> IsTsPreserved => _IsTsPreserved;



        public IReadOnlyReactiveProperty<bool> IsShowOpenLiveContentButton { get; }
        public IReadOnlyReactiveProperty<bool> IsShowAddTimeshiftButton { get; }
        public IReadOnlyReactiveProperty<bool> IsShowDeleteTimeshiftButton { get; }


        private ObservableCollection<LiveTagViewModel> _LiveTags { get; } = new ObservableCollection<LiveTagViewModel>();
        public ReadOnlyObservableCollection<LiveTagViewModel> LiveTags { get; }


        private ObservableCollection<IchibaItem> _IchibaItems = new ObservableCollection<IchibaItem>();
        public ReadOnlyObservableCollection<IchibaItem> IchibaItems { get; }


        private ObservableCollection<LiveRecommendData> _ReccomendItems = new ObservableCollection<LiveRecommendData>();
        public ReadOnlyReactiveCollection<LiveInfoListItemViewModel> ReccomendItems { get; }


        #region Commands

        private DelegateCommand _TogglePreserveTimeshift;
        public DelegateCommand TogglePreserveTimeshift
        {
            get
            {
                return _TogglePreserveTimeshift
                    ?? (_TogglePreserveTimeshift = new DelegateCommand(async () => 
                    {
                        if (!NiconicoSession.IsLoggedIn) { return; }

                        var reservations = await NiconicoSession.Context.Live.GetReservationsAsync();
                        
                        if (reservations.Any(x => LiveId.EndsWith(x)))
                        {
                            var result = await DeleteReservation(LiveId, LiveInfo.Video.Title);
                            if (result)
                            {
                                await RefreshLiveInfoAsync();
                            }
                        }
                        else
                        {
                            var result = await AddReservation(LiveId, LiveInfo.Video.Title);
                            if (result)
                            {
                                await RefreshLiveInfoAsync();
                            }
                        }

                    }));
            }
        }

        private async Task<bool> DeleteReservation(string liveId, string liveTitle)
        {
            if (string.IsNullOrEmpty(liveId)) { throw new ArgumentException(nameof(liveId)); }

            bool isDeleted = false;

            var token = await NiconicoSession.Context.Live.GetReservationTokenAsync();

            if (token == null) { return isDeleted; }

            if (await HohoemaDialogService.ShowMessageDialog(
                $"{liveTitle}",
                "ConfirmDeleteTimeshift".Translate()
                , "DeleteTimeshift".Translate()
                , "Cancel".Translate()
                )
                )
            {
                await NiconicoSession.Context.Live.DeleteReservationAsync(liveId, token);

                var deleteAfterReservations = await NiconicoSession.Context.Live.GetReservationsAsync();

                isDeleted = !deleteAfterReservations.Any(x => liveId.EndsWith(x));
                if (isDeleted)
                {
                    // 削除成功
                    var notificationService = (App.Current as App).Container.Resolve<Services.NotificationService>();
                    notificationService.ShowInAppNotification(new InAppNotificationPayload()
                    {
                        Content = "InAppNotification_DeletedTimeshift".Translate(),
                        IsShowDismissButton = true,
                    });
                }
                else
                {
                    // まだ存在するゾイ
                    var notificationService = App.Current.Container.Resolve<Services.NotificationService>();
                    notificationService.ShowInAppNotification(new InAppNotificationPayload()
                    {
                        Content = "InAppNotification_FailedDeleteTimeshift".Translate(),
                        IsShowDismissButton = true,
                    });

                    Debug.Fail("タイムシフト削除に失敗しました: " + liveId);
                }
            }

            return isDeleted;

        }

        private async Task<bool> AddReservation(string liveId, string liveTitle)
        {
            var result = await NiconicoSession.Context.Live.ReservationAsync(liveId);

            bool isAdded = false;
            if (result.IsCanOverwrite)
            {
                // 予約数が上限到達、他のタイムシフトを削除すれば予約可能
                // いずれかの予約を削除するよう選択してもらう
                if (await HohoemaDialogService.ShowMessageDialog(
                       "Dialog_ConfirmTimeshiftReservationiOverwrite".Translate(result.Data.Overwrite.Title, liveTitle),
                       "DialogTitle_ConfirmTimeshiftReservationiOverwrite".Translate(),
                       "Overwrite".Translate(),
                       "Cancel".Translate()
                    ))
                {
                    result = await NiconicoSession.Context.Live.ReservationAsync(liveId, isOverwrite: true);
                }
            }

            if (result.IsOK)
            {
                // 予約できてるはず
                // LiveInfoのタイムシフト周りの情報と共に通知
                var notificationService = (App.Current as App).Container.Resolve<Services.NotificationService>();
                notificationService.ShowInAppNotification(new InAppNotificationPayload()
                {
                    Content = "InAppNotification_AddedTimeshiftWithTitle".Translate(liveTitle),
                });

                isAdded = true;
            }
            else if (result.IsCanOverwrite)
            {
                // 一つ前のダイアログで明示的的にキャンセルしてるはずなので特に通知を表示しない
            }
            else if (result.IsReservationDeuplicated)
            {
                var notificationService = (App.Current as App).Container.Resolve<Services.NotificationService>();
                notificationService.ShowInAppNotification(new InAppNotificationPayload()
                {
                    Content = "InAppNotification_ExistTimeshift".Translate(),
                });
            }
            else if (result.IsReservationExpired)
            {
                var notificationService = (App.Current as App).Container.Resolve<Services.NotificationService>();
                notificationService.ShowInAppNotification(new InAppNotificationPayload()
                {
                    Content = "InAppNotification_TimeshiftExpired".Translate(),
                });
            }

            return isAdded;
        }

        #endregion


        
        Regex GeneralUrlRegex = new Regex(@"https?:\/\/([a-zA-Z0-9.\/?=_-]*)");
        public ObservableCollection<HyperlinkItem> DescriptionHyperlinkItems { get; } = new ObservableCollection<HyperlinkItem>();


        public async Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            LiveId = parameters.GetValue<string>("id");

            await RefreshLiveInfoAsync();

            string htmlDescription = null;
            if (htmlDescription == null)
            {
                var programInfo = await NiconicoSession.Context.Live.GetProgramInfoAsync(LiveId);
                if (programInfo.IsOK)
                {
                    htmlDescription = programInfo.Data.Description;
                    Debug.WriteLine("programInfoから放送説明HTML取得：Success");
                }
                else
                {
                    Debug.WriteLine("programInfoから放送説明HTML取得：Failed");
                }
            }

            if (htmlDescription != null)
            {
                ApplicationTheme appTheme;
                if (_appearanceSettings.Theme == ElementTheme.Dark)
                {
                    appTheme = ApplicationTheme.Dark;
                }
                else if (_appearanceSettings.Theme == ElementTheme.Light)
                {
                    appTheme = ApplicationTheme.Light;
                }
                else
                {
                    appTheme = Views.Helpers.SystemThemeHelper.GetSystemTheme();
                }

                HtmlDescription = await Models.Helpers.HtmlFileHelper.PartHtmlOutputToCompletlyHtml(LiveId, htmlDescription, appTheme);

                try
                {
                    var htmlDocument = new HtmlAgilityPack.HtmlDocument();
                    htmlDocument.LoadHtml(htmlDescription);
                    var root = htmlDocument.DocumentNode;
                    var anchorNodes = root.Descendants("a");

                    foreach (var anchor in anchorNodes)
                    {
                        var url = new Uri(anchor.Attributes["href"].Value);
                        string label = null;
                        var text = anchor.InnerText;
                        if (string.IsNullOrWhiteSpace(text) || text.Contains('\n') || text.Contains('\r'))
                        {
                            label = url.OriginalString;
                        }
                        else
                        {
                            label = new string(anchor.InnerText.TrimStart(' ', '\n', '\r').TakeWhile(c => c != ' ' || c != '　').ToArray());
                        }

                        DescriptionHyperlinkItems.Add(new HyperlinkItem()
                        {
                            Label = label,
                            Url = url
                        });

                        Debug.WriteLine($"{anchor.InnerText} : {anchor.Attributes["href"].Value}");
                    }

                    /*
                    var matches = GeneralUrlRegex.Matches(HtmlDescription);
                    foreach (var match in matches.Cast<Match>())
                    {
                        if (!VideoDescriptionHyperlinkItems.Any(x => x.Url.OriginalString == match.Value))
                        {
                            VideoDescriptionHyperlinkItems.Add(new HyperlinkItem()
                            {
                                Label = match.Value,
                                Url = new Uri(match.Value)
                            });

                            Debug.WriteLine($"{match.Value} : {match.Value}");
                        }
                    }
                    */

                    RaisePropertyChanged(nameof(DescriptionHyperlinkItems));

                }
                catch
                {
                    Debug.WriteLine("動画説明からリンクを抜き出す処理に失敗");
                }

            }
        }


        public ReactiveProperty<bool> IsLiveInfoLoaded { get; } = new ReactiveProperty<bool>(false);
        private async Task RefreshLiveInfoAsync()
        {
            IsLoadFailed.Value = false;
            LoadFailedMessage.Value = string.Empty;

            IsLiveInfoLoaded.Value = false;
            try
            {
                if (LiveId == null) { throw new Exception("Require LiveId in LiveInfomationPage navigation with (e.Parameter as string)"); }

                var liveInfoResponse = await NiconicoSession.Context.Live.GetLiveVideoInfoAsync(LiveId);

                if (!liveInfoResponse.IsOK)
                {
                    throw new Exception("Live not found. LiveId is " + LiveId);
                }

                var liveInfo = liveInfoResponse.VideoInfo;
                {
                    _LiveTags.Clear();

                    Func<string, LiveTagType, LiveTagViewModel> ConvertToLiveTagVM =
                        (x, type) => new LiveTagViewModel() { Tag = x, Type = type };

                    var tags = new[] {
                        liveInfo.Livetags.Category?.Tags.Select(x => ConvertToLiveTagVM(x, LiveTagType.Category)),
                        liveInfo.Livetags.Locked?.Tags.Select(x => ConvertToLiveTagVM(x, LiveTagType.Locked)),
                        liveInfo.Livetags.Free?.Tags.Select(x => ConvertToLiveTagVM(x, LiveTagType.Free)),
                    }
                    .SelectMany(x => x ?? Enumerable.Empty<LiveTagViewModel>());

                    foreach (var tag in tags)
                    {
                        _LiveTags.Add(tag);
                    }

                    RaisePropertyChanged(nameof(LiveTags));
                }

                var reseevations = await NiconicoSession.Context.Live.GetReservationsInDetailAsync();
                var thisLiveReservation = reseevations.ReservedProgram.FirstOrDefault(x => LiveId.EndsWith(x.Id));
                if (thisLiveReservation != null)
                {
                    var timeshiftList = await NiconicoSession.Context.Live.GetMyTimeshiftListAsync();
                    ExpiredTime = (timeshiftList.Items.FirstOrDefault(x => x.Id == LiveId)?.WatchTimeLimit ?? thisLiveReservation.ExpiredAt).LocalDateTime;
                }

                _IsTsPreserved.Value = thisLiveReservation != null;

                // タイムシフト視聴開始の判定処理のため_IsTsPreservedより後にLiveInfoを代入する
                LiveInfo = liveInfo;

                Community = LiveInfo.Community != null ? new LiveCommunityInfo() { Id = LiveInfo.Community.GlobalId, Label = LiveInfo.Community.Name } : null;

            }
            catch (Exception ex)
            {
                IsLoadFailed.Value = true;
                LoadFailedMessage.Value = ex.Message;
            }
            finally
            {
                IsLiveInfoLoaded.Value = true;
            }



        }

        AsyncLock _IchibaUpdateLock = new AsyncLock();
        public bool IsIchibaInitialized { get; private set; } = false;
        public bool IsEmptyIchibaItems { get; private set; } = true;
        public async void InitializeIchibaItems()
        {
            using (var releaser = await _IchibaUpdateLock.LockAsync())
            {
                if (LiveInfo == null) { return; }

                if (IsIchibaInitialized) { return; }

                try
                {
                    var ichibaResponse = await NiconicoSession.Context.Embed.GetIchiba(LiveInfo.Video.Id);
                    if (ichibaResponse != null)
                    {
                        foreach (var ichibaItem in ichibaResponse.GetMainIchibaItems() ?? Enumerable.Empty<IchibaItem>())
                        {
                            _IchibaItems.Add(ichibaItem);
                        }

                        foreach (var ichibaItem in ichibaResponse.GetPickupIchibaItems() ?? Enumerable.Empty<IchibaItem>())
                        {
                            _IchibaItems.Add(ichibaItem);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
                finally
                {
                    IsEmptyIchibaItems = !_IchibaItems.Any();
                    RaisePropertyChanged(nameof(IsEmptyIchibaItems));

                    IsIchibaInitialized = true;
                    RaisePropertyChanged(nameof(IsIchibaInitialized));
                }
            }
        }

        Mntone.Nico2.Live.ReservationsInDetail.ReservationsInDetailResponse _Reservations;

        AsyncLock _LiveRecommendLock = new AsyncLock();
        private readonly AppearanceSettings _appearanceSettings;

        public bool IsLiveRecommendInitialized { get; private set; } = false;
        public bool IsEmptyLiveRecommendItems { get; private set; } = false;
        public ApplicationLayoutManager ApplicationLayoutManager { get; }
        public PageManager PageManager { get; }
        public Models.NiconicoSession NiconicoSession { get; }
        public NicoLiveProvider NicoLiveProvider { get; }

        public async void InitializeLiveRecommend()
        {
            using (var releaser = await _LiveRecommendLock.LockAsync())
            {
                if (LiveInfo == null) { return; }

                if (IsLiveRecommendInitialized) { return; }

                try
                {
                    if (NiconicoSession.IsLoggedIn)
                    {
                        _Reservations = await NiconicoSession.Context.Live.GetReservationsInDetailAsync();
                    }
                }
                catch { }

                try
                {
                    LiveRecommendResponse recommendResponse = null;
                    if (LiveInfo.Community?.GlobalId.StartsWith("co") ?? false)
                    {
                        recommendResponse = await NiconicoSession.Context.Live.GetCommunityRecommendAsync(LiveInfo.Video.Id, LiveInfo.Community.GlobalId);
                    }
                    else
                    {
                        recommendResponse = await NiconicoSession.Context.Live.GetOfficialOrChannelLiveRecommendAsync(LiveInfo.Video.Id);
                    }

                    foreach (var recommendItem in recommendResponse.RecommendItems)
                    {
                        _ReccomendItems.Add(recommendItem);
                    }

                    
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
                finally
                {
                    IsLiveRecommendInitialized = true;
                    RaisePropertyChanged(nameof(IsLiveRecommendInitialized));

                    IsEmptyLiveRecommendItems = !_ReccomendItems.Any();
                    RaisePropertyChanged(nameof(IsEmptyLiveRecommendItems));
                }
            }
        }
    }

    public enum LiveTagType
    {
        Category,
        Locked,
        Free,
    }

    public sealed class LiveTagViewModel
    {
        public string Tag { get; set; }
        public LiveTagType Type { get; set; }

        private static DelegateCommand<LiveTagViewModel> _SearchLiveTagCommand;
        public DelegateCommand<LiveTagViewModel> SearchLiveTagCommand
        {
            get
            {
                return _SearchLiveTagCommand
                    ?? (_SearchLiveTagCommand = new DelegateCommand<LiveTagViewModel>((tagVM) => 
                    {
                        var pageManager = App.Current.Container.Resolve<PageManager>();
                        pageManager.Search(SearchTarget.Niconama, tagVM.Tag);
                    }));
            }
        }
    }
}
