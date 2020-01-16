﻿using NicoPlayerHohoema.Models;
using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI;

namespace NicoPlayerHohoema.ViewModels.PlayerSidePaneContent
{
    public class SettingsSidePaneContentViewModel : SidePaneContentViewModelBase
    {
        public SettingsSidePaneContentViewModel(
            NGSettings ngSettings, 
            PlayerSettings playerSettings,
            IScheduler scheduler
            )
        {
            _playerSettings = playerSettings;
            _scheduler = scheduler;
            VideoPlayingQuality = _playerSettings.ToReactivePropertyAsSynchronized(x => x.DefaultQuality,
                convert: x => VideoPlayingQualityList.First(y => y.Value == x),
                convertBack: x => x.Value,
                raiseEventScheduler: _scheduler,
                mode: ReactivePropertyMode.DistinctUntilChanged
                )
            .AddTo(_CompositeDisposable);
            VideoPlayingQuality.Subscribe(x =>
            {
                VideoQualityChanged?.Invoke(this, x.Value);
            })
            .AddTo(_CompositeDisposable);


            VideoPlaybackRate = _playerSettings.ToReactivePropertyAsSynchronized(x => x.PlaybackRate);
            SetPlaybackRateCommand = VideoPlaybackRate.Select(
                rate => rate != 1.0
                )
                .ToReactiveCommand<double?>(_scheduler)
            .AddTo(_CompositeDisposable);

            SetPlaybackRateCommand.Subscribe(
                (rate) => VideoPlaybackRate.Value = rate.HasValue ? rate.Value : 1.0
                )
            .AddTo(_CompositeDisposable);



            LiveVideoPlayingQuality = _playerSettings.ToReactivePropertyAsSynchronized(x => x.DefaultLiveQuality,
                convert: x => LivePlayingQualityList.FirstOrDefault(y => y.Value == x),
                convertBack: x => x.Value,
                raiseEventScheduler: _scheduler,
                mode: ReactivePropertyMode.DistinctUntilChanged
                )
            .AddTo(_CompositeDisposable);

            IsLowLatency = _playerSettings.ToReactivePropertyAsSynchronized(x => x.LiveWatchWithLowLatency, _scheduler, mode: ReactivePropertyMode.DistinctUntilChanged)
            .AddTo(_CompositeDisposable);

            IsKeepDisplayInPlayback = _playerSettings.ToReactivePropertyAsSynchronized(x => x.IsKeepDisplayInPlayback, _scheduler)
            .AddTo(_CompositeDisposable);
            ScrollVolumeFrequency = _playerSettings.ToReactivePropertyAsSynchronized(x => x.SoundVolumeChangeFrequency, _scheduler)
            .AddTo(_CompositeDisposable);
            IsForceLandscapeDefault = _playerSettings.ToReactivePropertyAsSynchronized(x => x.IsForceLandscape, _scheduler)
            .AddTo(_CompositeDisposable);

            AutoHideDelayTime = _playerSettings.ToReactivePropertyAsSynchronized(x =>
                x.AutoHidePlayerControlUIPreventTime
                , x => x.TotalSeconds
                , x => TimeSpan.FromSeconds(x)
                , _scheduler
                )
            .AddTo(_CompositeDisposable);

            PlaylistEndAction = _playerSettings.ToReactivePropertyAsSynchronized(x => x.PlaylistEndAction, _scheduler)
            .AddTo(_CompositeDisposable);

            AutoMoveNextVideoOnPlaylistEmpty = _playerSettings.ToReactivePropertyAsSynchronized(x => x.AutoMoveNextVideoOnPlaylistEmpty, _scheduler)
            .AddTo(_CompositeDisposable);


            // NG Comment User Id



            // Comment Display 
            CommentColor = _playerSettings.ToReactivePropertyAsSynchronized(x => x.CommentColor, _scheduler)
            .AddTo(_CompositeDisposable);
            IsPauseWithCommentWriting = _playerSettings.ToReactivePropertyAsSynchronized(x => x.PauseWithCommentWriting, _scheduler)
            .AddTo(_CompositeDisposable);
            CommentRenderingFPS = _playerSettings.ToReactivePropertyAsSynchronized(x => x.CommentRenderingFPS, _scheduler)
            .AddTo(_CompositeDisposable);
            CommentDisplayDuration = _playerSettings.ToReactivePropertyAsSynchronized(x => x.CommentDisplayDuration, x => x.TotalSeconds, x => TimeSpan.FromSeconds(x), _scheduler)
            .AddTo(_CompositeDisposable);
            CommentFontScale = _playerSettings.ToReactivePropertyAsSynchronized(x => x.DefaultCommentFontScale, _scheduler)
            .AddTo(_CompositeDisposable);
            IsDefaultCommentWithAnonymous = _playerSettings.ToReactivePropertyAsSynchronized(x => x.IsDefaultCommentWithAnonymous, _scheduler)
            .AddTo(_CompositeDisposable);
            CommentOpacity = _playerSettings.ToReactivePropertyAsSynchronized(x => x.CommentOpacity, _scheduler)
            .AddTo(_CompositeDisposable);

            NicoScript_Default_Enabled = _playerSettings.ToReactivePropertyAsSynchronized(x => x.NicoScript_Default_Enabled, raiseEventScheduler: _scheduler)
                .AddTo(_CompositeDisposable);
            NicoScript_DisallowSeek_Enabled = _playerSettings.ToReactivePropertyAsSynchronized(x => x.NicoScript_DisallowSeek_Enabled, raiseEventScheduler: _scheduler)
                .AddTo(_CompositeDisposable);
            NicoScript_DisallowComment_Enabled = _playerSettings.ToReactivePropertyAsSynchronized(x => x.NicoScript_DisallowComment_Enabled, raiseEventScheduler: _scheduler)
                .AddTo(_CompositeDisposable);
            NicoScript_Jump_Enabled = _playerSettings.ToReactivePropertyAsSynchronized(x => x.NicoScript_Jump_Enabled, raiseEventScheduler: _scheduler)
                .AddTo(_CompositeDisposable);
            NicoScript_Replace_Enabled = _playerSettings.ToReactivePropertyAsSynchronized(x => x.NicoScript_Replace_Enabled, raiseEventScheduler: _scheduler)
                .AddTo(_CompositeDisposable);


            // NG Comment

            NGCommentUserIdEnable = _playerSettings.ToReactivePropertyAsSynchronized(x => x.NGCommentUserIdEnable, _scheduler)
            .AddTo(_CompositeDisposable);
            NGCommentUserIds = _playerSettings.NGCommentUserIds
                .ToReadOnlyReactiveCollection(x =>
                    RemovableSettingsListItemHelper.UserIdInfoToRemovableListItemVM(x, OnRemoveNGCommentUserIdFromList),
                    _scheduler
                    )
            .AddTo(_CompositeDisposable);

            NGCommentKeywordEnable = _playerSettings.ToReactivePropertyAsSynchronized(x => x.NGCommentKeywordEnable, _scheduler)
            .AddTo(_CompositeDisposable);
            NGCommentKeywords = new ReactiveProperty<string>(_scheduler, string.Empty)
            .AddTo(_CompositeDisposable);

            NGCommentKeywordError = NGCommentKeywords
                .Select(x =>
                {
                    var keywords = x.Split('\r');
                    var invalidRegex = keywords.FirstOrDefault(keyword =>
                    {
                        Regex regex = null;
                        try
                        {
                            regex = new Regex(keyword);
                        }
                        catch { }
                        return regex == null;
                    });

                    if (invalidRegex == null)
                    {
                        return string.Empty;
                    }
                    else
                    {
                        return $"Error in \"{invalidRegex}\"";
                    }
                })
                .ToReadOnlyReactiveProperty(eventScheduler: _scheduler)
            .AddTo(_CompositeDisposable);

            SelectedNGCommentScore = _playerSettings.ToReactivePropertyAsSynchronized(x => x.NGCommentScore, _scheduler)
            .AddTo(_CompositeDisposable);



            CommentGlassMowerEnable = _playerSettings
                .ToReactivePropertyAsSynchronized(x => x.CommentGlassMowerEnable, _scheduler)
            .AddTo(_CompositeDisposable);





            NGCommentKeywords.Value = string.Join("\r", _playerSettings.NGCommentKeywords.Select(x => x.Keyword)) + "\r";
            NGCommentKeywords.Throttle(TimeSpan.FromSeconds(3))
                .Subscribe(_ =>
                {
                    _playerSettings.NGCommentKeywords.Clear();
                    foreach (var ngKeyword in NGCommentKeywords.Value.Split('\r'))
                    {
                        if (!string.IsNullOrWhiteSpace(ngKeyword))
                        {
                            _playerSettings.NGCommentKeywords.Add(new NGKeyword() { Keyword = ngKeyword });
                        }
                    }
                })
                .AddTo(_CompositeDisposable);
        }


        public event EventHandler<NicoVideoQuality> VideoQualityChanged;

        // Video Settings
        public static List<ValueWithAvairability<NicoVideoQuality>> VideoPlayingQualityList { get; } = new []
        {
            new ValueWithAvairability<NicoVideoQuality>(NicoVideoQuality.Dmc_SuperHigh),
            new ValueWithAvairability<NicoVideoQuality>(NicoVideoQuality.Dmc_High),
            new ValueWithAvairability<NicoVideoQuality>(NicoVideoQuality.Dmc_Midium),
            new ValueWithAvairability<NicoVideoQuality>(NicoVideoQuality.Dmc_Low),
            new ValueWithAvairability<NicoVideoQuality>(NicoVideoQuality.Dmc_Mobile),
        }.ToList();

        public ReactiveProperty<ValueWithAvairability<NicoVideoQuality>> VideoPlayingQuality { get; private set; }
        public ReactiveProperty<bool> IsLowLatency { get; private set; }

        public ReactiveProperty<double> VideoPlaybackRate { get; private set; }
        public ReactiveCommand<double?> SetPlaybackRateCommand { get; private set; }

        public static List<double> VideoPlaybackRateList { get; }

        // Live Settings
        public static List<ValueWithAvairability<string>> LivePlayingQualityList { get; } = new[]
        {
            new ValueWithAvairability<string>("super_high"),
            new ValueWithAvairability<string>("high"),
            new ValueWithAvairability<string>("normal"),
            new ValueWithAvairability<string>("low"),
            new ValueWithAvairability<string>("super_low"),
        }.ToList();
        public ReactiveProperty<ValueWithAvairability<string>> LiveVideoPlayingQuality { get; private set; }
        private bool _IsLeoPlayerLive;
        public bool IsLeoPlayerLive
        {
            get { return _IsLeoPlayerLive; }
            set { SetProperty(ref _IsLeoPlayerLive, value); }
        }

        // Player Settings
        public ReactiveProperty<bool> IsForceLandscapeDefault { get; private set; }

        public ReactiveProperty<bool> IsKeepDisplayInPlayback { get; private set; }
        public ReactiveProperty<double> ScrollVolumeFrequency { get; private set; }

        public ReactiveProperty<double> AutoHideDelayTime { get; private set; }

        public DelegateCommand ResetDefaultPlaybackRateCommand { get; private set; }




        public ReactiveProperty<bool> IsDefaultCommentWithAnonymous { get; private set; }
        public ReactiveProperty<uint> CommentRenderingFPS { get; private set; }
        public ReactiveProperty<double> CommentDisplayDuration { get; private set; }
        public ReactiveProperty<double> CommentFontScale { get; private set; }
        public ReactiveProperty<Color> CommentColor { get; private set; }
        public ReactiveProperty<bool> IsPauseWithCommentWriting { get; private set; }

        public ReactiveProperty<double> CommentOpacity { get; private set; }


        public static List<Color> CommentColorList { get; private set; }
        public static List<uint> CommentRenderringFPSList { get; private set; }


        public ReactiveProperty<bool> IsEnableOwnerCommentCommand { get; private set; }
        public ReactiveProperty<bool> IsEnableUserCommentCommand { get; private set; }
        public ReactiveProperty<bool> IsEnableAnonymousCommentCommand { get; private set; }

        public ReactiveProperty<bool> NicoScript_Default_Enabled { get; private set; }
        public ReactiveProperty<bool> NicoScript_DisallowSeek_Enabled { get; private set; }
        public ReactiveProperty<bool> NicoScript_DisallowComment_Enabled { get; private set; }
        public ReactiveProperty<bool> NicoScript_Jump_Enabled { get; private set; }
        public ReactiveProperty<bool> NicoScript_Replace_Enabled { get; private set; }

        public static List<PlaylistEndAction> PlaylistEndActionList { get; private set; }
        public ReactiveProperty<PlaylistEndAction> PlaylistEndAction { get; private set; }

        public ReactiveProperty<bool> AutoMoveNextVideoOnPlaylistEmpty { get; private set; }

        // NG Comments

        public ReactiveProperty<bool> NGCommentUserIdEnable { get; private set; }
        public ReadOnlyReactiveCollection<RemovableListItem<string>> NGCommentUserIds { get; private set; }

        public ReactiveProperty<bool> NGCommentKeywordEnable { get; private set; }
        public ReactiveProperty<string> NGCommentKeywords { get; private set; }
        public ReadOnlyReactiveProperty<string> NGCommentKeywordError { get; private set; }

        public ReactiveProperty<int> SelectedNGCommentScore { get; private set; }


        public ReactiveProperty<bool> CommentGlassMowerEnable { get; private set; }



        private PlayerSettings _playerSettings;
        private readonly IScheduler _scheduler;

        static SettingsSidePaneContentViewModel()
        {
            CommentRenderringFPSList = new List<uint>()
            {
                5, 10, 15, 24, 30, 45, 60, 75, 90, 120
            };

            CommentColorList = new List<Color>()
            {
                Colors.WhiteSmoke,
                Colors.Black,
            };

            PlaylistEndActionList = new List<Models.PlaylistEndAction>()
            {
                Models.PlaylistEndAction.NothingDo,
                Models.PlaylistEndAction.ChangeIntoSplit,
                Models.PlaylistEndAction.CloseIfPlayWithCurrentWindow
            };

            VideoPlaybackRateList = new List<double>()
            {
                2.0,
                1.75,
                1.5,
                1.25,
                1.0,
                .75,
                .5,
                .25,
                .05
            };
        }

        
        
        public void SetupAvairableLiveQualities(IList<string> qualities)
        {
            if (qualities == null) { return; }

            foreach (var i in LivePlayingQualityList)
            {
                i.IsAvairable = qualities.Any(x => x == i.Value);
            }
        }

        protected override void OnDispose()
        {
            base.OnDispose();
        }

        private void OnRemoveNGCommentUserIdFromList(string userId)
        {
            var removeTarget = _playerSettings.NGCommentUserIds.First(x => x.UserId == userId);
            _playerSettings.NGCommentUserIds.Remove(removeTarget);
        }

    }


    public class ValueWithAvairability<T> : BindableBase
    {
        public ValueWithAvairability(T value, bool isAvairable = true)
        {
            Value = value;
            IsAvairable = isAvairable;
        }
        public T Value { get; set; }

        private bool _IsAvairable;
        public bool IsAvairable
        {
            get { return _IsAvairable; }
            set { SetProperty(ref _IsAvairable, value); }
        }
    }

}
