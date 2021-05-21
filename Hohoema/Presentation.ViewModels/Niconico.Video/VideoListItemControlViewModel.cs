﻿using Hohoema.Models.Domain;
using Hohoema.Models.Domain.Niconico.Channel;
using Hohoema.Models.Domain.Niconico.Mylist;
using Hohoema.Models.Domain.Niconico.User;
using Hohoema.Models.Domain.Niconico.Video;
using Hohoema.Models.Domain.Niconico.Video.WatchHistory.LoginUser;
using Hohoema.Models.Domain.Player.Video.Cache;
using Hohoema.Models.Domain.VideoCache;
using Hohoema.Models.UseCase.NicoVideos;
using Hohoema.Models.UseCase.NicoVideos.Events;
using Hohoema.Models.UseCase.PageNavigation;
using Hohoema.Models.UseCase.VideoCache.Events;
using Hohoema.Presentation.ViewModels.Niconico.Video.Commands;
using Hohoema.Presentation.ViewModels.Pages.VideoListPage.Commands;
using Microsoft.Toolkit.Mvvm.Messaging;
using Mntone.Nico2;
using Mntone.Nico2.Mylist;
using Mntone.Nico2.Searches.Video;
using NiconicoLiveToolkit.Video;
using Prism.Commands;
using Prism.Unity;
using Reactive.Bindings.Extensions;
using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;

namespace Hohoema.Presentation.ViewModels.VideoListPage
{
    public class VideoItemViewModel : FixPrism.BindableBase, IVideoContent, IDisposable,
        IRecipient<VideoPlayedMessage>,
        IRecipient<QueueItemAddedMessage>,
        IRecipient<QueueItemRemovedMessage>,
        IRecipient<QueueItemIndexUpdateMessage>,
        IRecipient<VideoCacheStatusChangedMessage>
    {
        private static readonly HohoemaPlaylist _hohoemaPlaylist;
        private static readonly VideoPlayedHistoryRepository _videoPlayedHistoryRepository;
        private static readonly VideoCacheManager _cacheManager;
        protected static readonly IScheduler _scheduler;

        static VideoItemViewModel()
        {
            _hohoemaPlaylist = App.Current.Container.Resolve<HohoemaPlaylist>();
            _cacheManager = App.Current.Container.Resolve<VideoCacheManager>();
            _scheduler = App.Current.Container.Resolve<IScheduler>();
            _videoPlayedHistoryRepository = App.Current.Container.Resolve<VideoPlayedHistoryRepository>();
            _addWatchAfterCommand = App.Current.Container.Resolve<QueueAddItemCommand>();
            _removeWatchAfterCommand = App.Current.Container.Resolve<QueueRemoveItemCommand>();
        }

        protected void SetLength(TimeSpan length)
        {
            Length = length;
            RaisePropertyChanged(nameof(Length));
        }


        public string RawVideoId { get; }

        public TimeSpan Length { get; private set; }

        public string ThumbnailUrl { get; }

        public Uri ThumbnailUri => !string.IsNullOrWhiteSpace(ThumbnailUrl) ? new Uri(ThumbnailUrl) : null;

        public string Title { get; }

        public string Id => RawVideoId;
        
        public string Label => Title;

        bool IEquatable<IVideoContent>.Equals(IVideoContent other)
        {
            return this.RawVideoId == other.Id;
        }

        public VideoItemViewModel(
            string rawVideoId, string title, string thumbnailUrl, TimeSpan videoLength
            )
        {
            RawVideoId = rawVideoId;
            Title = title;
            ThumbnailUrl = thumbnailUrl;
            Length = videoLength;

            SubscribeAll(rawVideoId);
        }

        string _subsribedVideoId;
        protected void SubscribeAll(string videoId)
        {
            if (_subsribedVideoId != null)
            {
                WeakReferenceMessenger.Default.Unregister<VideoPlayedMessage, string>(this, _subsribedVideoId);
                WeakReferenceMessenger.Default.Unregister<QueueItemAddedMessage, string>(this, _subsribedVideoId);
                WeakReferenceMessenger.Default.Unregister<QueueItemRemovedMessage, string>(this, _subsribedVideoId);
                WeakReferenceMessenger.Default.Unregister<QueueItemIndexUpdateMessage, string>(this, _subsribedVideoId);
                WeakReferenceMessenger.Default.Unregister<VideoCacheStatusChangedMessage, string>(this, _subsribedVideoId);
            }

            WeakReferenceMessenger.Default.Register<VideoPlayedMessage, string>(this, videoId);
            WeakReferenceMessenger.Default.Register<QueueItemAddedMessage, string>(this, videoId);
            WeakReferenceMessenger.Default.Register<QueueItemRemovedMessage, string>(this, videoId);
            WeakReferenceMessenger.Default.Register<QueueItemIndexUpdateMessage, string>(this, videoId);
            WeakReferenceMessenger.Default.Register<VideoCacheStatusChangedMessage, string>(this, videoId);

            (IsQueueItem, QueueItemIndex) = _hohoemaPlaylist.IsQueuePlaylistItem(videoId);
            var cacheRequest = _cacheManager.GetVideoCache(videoId);
            RefreshCacheStatus(cacheRequest?.Status, cacheRequest);
            SubscriptionWatchedIfNotWatch(videoId);

            _subsribedVideoId = videoId;
        }

        public override void Dispose()
        {
            base.Dispose();

            WeakReferenceMessenger.Default.Unregister<VideoPlayedMessage, string>(this, _subsribedVideoId);
            WeakReferenceMessenger.Default.Unregister<QueueItemAddedMessage, string>(this, _subsribedVideoId);
            WeakReferenceMessenger.Default.Unregister<QueueItemRemovedMessage, string>(this, _subsribedVideoId);
            WeakReferenceMessenger.Default.Unregister<QueueItemIndexUpdateMessage, string>(this, _subsribedVideoId);
            WeakReferenceMessenger.Default.Unregister<VideoCacheStatusChangedMessage, string>(this, _subsribedVideoId);
        }

        #region Watched

        private bool _IsWatched;
        public bool IsWatched
        {
            get { return _IsWatched; }
            set { SetProperty(ref _IsWatched, value); }
        }

        private double _LastWatchedPositionInterpolation;
        public double LastWatchedPositionInterpolation
        {
            get { return _LastWatchedPositionInterpolation; }
            set { SetProperty(ref _LastWatchedPositionInterpolation, value); }
        }


        void IRecipient<VideoPlayedMessage>.Receive(VideoPlayedMessage message)
        {
            Watched(message.Value);
        }

        void Watched(VideoPlayedMessage.VideoPlayedEventArgs args)
        {
            IsWatched = true;
            LastWatchedPositionInterpolation = Math.Clamp(args.PlayedPosition.TotalSeconds / Length.TotalSeconds, 0.0, 1.0);
        }

        void SubscriptionWatchedIfNotWatch(string videoId)
        {
            UnsubscriptionWatched(_subsribedVideoId);

            var watched = _videoPlayedHistoryRepository.IsVideoPlayed(videoId, out var hisotory);
            IsWatched = watched;
            if (!watched)
            {
                StrongReferenceMessenger.Default.Register<VideoPlayedMessage, string>(this, videoId);
            }
            else
            {
                LastWatchedPositionInterpolation = hisotory.LastPlayedPosition != TimeSpan.Zero
                    ? Math.Clamp(hisotory.LastPlayedPosition.TotalSeconds / Length.TotalSeconds, 0.0, 1.0)
                    : 1.0
                    ;
            }
        }

        void UnsubscriptionWatched(string videoId)
        {
            if (videoId != null)
            {
                StrongReferenceMessenger.Default.Unregister<VideoPlayedMessage, string>(this, videoId);
            }
        }




        #endregion

        #region Queue Item


        private static readonly QueueAddItemCommand _addWatchAfterCommand;
        public QueueAddItemCommand AddWatchAfterCommand => _addWatchAfterCommand;

        private static readonly QueueRemoveItemCommand _removeWatchAfterCommand;
        public QueueRemoveItemCommand RemoveWatchAfterCommand => _removeWatchAfterCommand;


        private DelegateCommand<object> _toggleWatchAfterCommand;
        public DelegateCommand<object> ToggleWatchAfterCommand => _toggleWatchAfterCommand
            ??= new DelegateCommand<object>(parameter => 
            {
                if (IsQueueItem)
                {
                    (_removeWatchAfterCommand as ICommand).Execute(parameter);
                }
                else
                {
                    (_addWatchAfterCommand as ICommand).Execute(parameter);
                }
            });


        private bool _IsQueueItem;
        public bool IsQueueItem
        {
            get { return _IsQueueItem; }
            set { SetProperty(ref _IsQueueItem, value); }
        }

        private int _QueueItemIndex;
        public int QueueItemIndex
        {
            get { return _QueueItemIndex; }
            set { SetProperty(ref _QueueItemIndex, value + 1); }
        }

        void IRecipient<QueueItemAddedMessage>.Receive(QueueItemAddedMessage message)
        {
            IsQueueItem = true;
        }

        void IRecipient<QueueItemRemovedMessage>.Receive(QueueItemRemovedMessage message)
        {
            IsQueueItem = false;
            QueueItemIndex = -1;
        }


        void IRecipient<QueueItemIndexUpdateMessage>.Receive(QueueItemIndexUpdateMessage message)
        {
            QueueItemIndex = message.Value.Index;
        }


        #endregion



        #region VideoCache

        private NicoVideoQuality? _CacheRequestedQuality;
        public NicoVideoQuality? CacheRequestedQuality
        {
            get { return _CacheRequestedQuality; }
            set { SetProperty(ref _CacheRequestedQuality, value); }
        }

        private VideoCacheStatus? _CacheStatus;
        public VideoCacheStatus? CacheStatus
        {
            get { return _CacheStatus; }
            set { SetProperty(ref _CacheStatus, value); }
        }

        void IRecipient<VideoCacheStatusChangedMessage>.Receive(VideoCacheStatusChangedMessage message)
        {
            _scheduler.Schedule(() =>
            {
                RefreshCacheStatus(message.Value.CacheStatus, message.Value.Item);
            });
        }

        void RefreshCacheStatus(VideoCacheStatus? status, VideoCacheItem item)
        {
            CacheStatus = status;
            if (item?.DownloadedVideoQuality is not null and not NicoVideoQuality.Unknown and var quality)
            {
                CacheRequestedQuality = quality;
            }
            else
            {
                CacheRequestedQuality = item?.RequestedVideoQuality;
            }
        }

        private void UnsubscribeCacheState()
        {
            WeakReferenceMessenger.Default.Unregister<VideoCacheStatusChangedMessage, string>(this, RawVideoId);
        }

        #endregion


        
    }






    public class VideoListItemControlViewModel : VideoItemViewModel, IVideoDetail, IDisposable,
        IRecipient<VideoOwnerFilteringAddedMessage>,
        IRecipient<VideoOwnerFilteringRemovedMessage>
    {
        static VideoListItemControlViewModel()
        {
            _nicoVideoProvider = App.Current.Container.Resolve<NicoVideoProvider>();
            _ngSettings = App.Current.Container.Resolve<VideoFilteringSettings>();
            _nicoVideoRepository = App.Current.Container.Resolve<NicoVideoCacheRepository>();
            _openVideoOwnerPageCommand = App.Current.Container.Resolve<OpenVideoOwnerPageCommand>();
            _messenger = App.Current.Container.Resolve<IMessenger>();
        }


        public VideoListItemControlViewModel(
            string rawVideoId, string title, string thumbnailUrl, TimeSpan videoLength
            )
            : base(rawVideoId, title, thumbnailUrl, videoLength)
        {
        }

        public VideoListItemControlViewModel(NicoVideo data)            
            : this(data.RawVideoId, data.Title, data.ThumbnailUrl, data.Length)
        {
            Data = data;

            _VideoId = data.VideoId;
            _PostedAt = data.PostedAt;
            _ViewCount = data.ViewCount;
            _MylistCount = data.MylistCount;
            _CommentCount = data.CommentCount;
            _IsDeleted = data.IsDeleted;
            _PrivateReason = Data.PrivateReasonType;
            _Description = Data.Description;
            Permission = Data.Permission;

            if (data.Owner != null)
            {
                _ProviderId = data.Owner.OwnerId;
                _ProviderName = data.Owner.ScreenName;
                ProviderType = data.Owner.UserType;

                RegisterVideoOwnerFilteringMessageReceiver(_ProviderId, null);
            }

            UpdateIsHidenVideoOwner(data);

            if (VideoId != RawVideoId && VideoId != null)
            {
                SubscribeAll(VideoId);
            }
        }


        public bool Equals(IVideoContent other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }


        private static readonly NicoVideoProvider _nicoVideoProvider;
        private static readonly NicoVideoCacheRepository _nicoVideoRepository;
        private static readonly VideoFilteringSettings _ngSettings;

        private static readonly OpenVideoOwnerPageCommand _openVideoOwnerPageCommand;
        private static readonly IMessenger _messenger;

        public OpenVideoOwnerPageCommand OpenVideoOwnerPageCommand => _openVideoOwnerPageCommand;


        public NicoVideo Data { get; private set; }

        private string _VideoId;
        public string VideoId
        {
            get { return _VideoId; }
            set { SetProperty(ref _VideoId, value); }
        }


        private string _ProviderId;
        public string ProviderId
        {
            get => _ProviderId;
            set
            {
                var oldProviderId = _ProviderId;
                if (SetProperty(ref _ProviderId, value))
                {
                    RegisterVideoOwnerFilteringMessageReceiver(_ProviderId, oldProviderId);
                }
            }
        }
        
        private string _ProviderName;
        public string ProviderName
        {
            get { return _ProviderName; }
            set { SetProperty(ref _ProviderName, value); }
        }


        public NicoVideoUserType ProviderType { get; set; }

        public IMylist OnwerPlaylist { get; }

        public VideoStatus VideoStatus { get; private set; }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { SetProperty(ref _Description, value); }
        }

        private DateTime _PostedAt;
        public DateTime PostedAt
        {
            get { return _PostedAt; }
            set { SetProperty(ref _PostedAt, value); }
        }


        private int _ViewCount;
        public int ViewCount
        {
            get { return _ViewCount; }
            set { SetProperty(ref _ViewCount, value); }
        }


        private int _MylistCount;
        public int MylistCount
        {
            get { return _MylistCount; }
            set { SetProperty(ref _MylistCount, value); }
        }

        private int _CommentCount;
        public int CommentCount
        {
            get { return _CommentCount; }
            set { SetProperty(ref _CommentCount, value); }
        }

        private bool _IsDeleted;
        public bool IsDeleted
        {
            get { return _IsDeleted; }
            set { SetProperty(ref _IsDeleted, value); }
        }
      
        private VideoPermission _permission;
        public VideoPermission Permission
        {
            get { return _permission; }
            set { SetProperty(ref _permission, value); }
        }


        private PrivateReasonType? _PrivateReason;
        public PrivateReasonType? PrivateReason
        {
            get { return _PrivateReason; }
            set { SetProperty(ref _PrivateReason, value); }
        }

        #region NG 

        void RegisterVideoOwnerFilteringMessageReceiver(string currentProviderId, string oldProviderId)
        {
            if (oldProviderId is not null)
            {
                _messenger.Unregister<VideoOwnerFilteringAddedMessage, string>(this, oldProviderId);
                _messenger.Unregister<VideoOwnerFilteringRemovedMessage, string>(this, oldProviderId);
            }

            if (currentProviderId is not null)
            {
                _messenger.Register<VideoOwnerFilteringAddedMessage, string>(this, currentProviderId);
                _messenger.Register<VideoOwnerFilteringRemovedMessage, string>(this, currentProviderId);
            }
        }

        void IRecipient<VideoOwnerFilteringAddedMessage>.Receive(VideoOwnerFilteringAddedMessage message)
        {
            UpdateIsHidenVideoOwner(Data);

        }

        void IRecipient<VideoOwnerFilteringRemovedMessage>.Receive(VideoOwnerFilteringRemovedMessage message)
        {
            UpdateIsHidenVideoOwner(Data);
        }


        private FilteredResult _VideoHiddenInfo;
        public FilteredResult VideoHiddenInfo
        {
            get { return _VideoHiddenInfo; }
            set { SetProperty(ref _VideoHiddenInfo, value); }
        }


        void UpdateIsHidenVideoOwner(IVideoContent video)
        {
            if (video != null)
            {
                _ngSettings.TryGetHiddenReason(video, out var result);
                VideoHiddenInfo = result;
            }
            else
            {
                VideoHiddenInfo = null;
            }
        }

        private DelegateCommand _UnregistrationHiddenVideoOwnerCommand;
        public DelegateCommand UnregistrationHiddenVideoOwnerCommand =>
            _UnregistrationHiddenVideoOwnerCommand ?? (_UnregistrationHiddenVideoOwnerCommand = new DelegateCommand(ExecuteUnregistrationHiddenVideoOwnerCommand));

        void ExecuteUnregistrationHiddenVideoOwnerCommand()
        {
            if (Data != null)
            {
                _ngSettings.RemoveHiddenVideoOwnerId(Data.ProviderId);
            }

        }



        #endregion



        public override void Dispose()
        {
            base.Dispose();

            RegisterVideoOwnerFilteringMessageReceiver(null, _ProviderId);
        }




        public async ValueTask InitializeAsync(CancellationToken ct)
        {
            if (string.IsNullOrEmpty(Label))
            {
                var data = await _nicoVideoProvider.GetNicoVideoInfo(RawVideoId, true);

                ct.ThrowIfCancellationRequested();

                Data = data;
            }

            if (ProviderId is null)
            {
                if (Data?.ProviderId is null)
                {
                    Data = _nicoVideoRepository.Get(RawVideoId);
                }

                if (Data?.ProviderId is null)
                {
                    Data = await _nicoVideoProvider.GetNicoVideoInfo(RawVideoId, requireLatest: true);
                }

                if (Data?.ProviderId is not null)
                {
                    ProviderId = Data.ProviderId;
                    ProviderType = Data.ProviderType;
                }
            }

            if (Data != null)
            {
                this.Setup(Data);
            }

            UpdateIsHidenVideoOwner(this);

            OnInitialized();
        }


        protected virtual void OnInitialized() { }

        protected virtual VideoPlayPayload MakeVideoPlayPayload()
		{
			return new VideoPlayPayload()
			{
				VideoId = RawVideoId,
				Quality = null,
			};
		}

    }

    public static class VideoListItemControlViewModelExtesnsion
    {


        public static void Setup(this VideoListItemControlViewModel vm, NicoVideo data)
        {
            vm.VideoId = data.VideoId;
            vm.PostedAt = data.PostedAt;
            vm.ViewCount = data.ViewCount;
            vm.MylistCount = data.MylistCount;
            vm.CommentCount = data.CommentCount;
            vm.IsDeleted = data.IsDeleted;
            vm.PrivateReason = data.PrivateReasonType;
            vm.Description = data.Description;
            vm.Permission = data.Permission;

            if (data.Owner != null)
            {
                vm.ProviderId = data.Owner.OwnerId;
                vm.ProviderType = data.Owner.UserType;
            }
        }

        public static void Setup(this VideoListItemControlViewModel vm, Mntone.Nico2.Users.Video.VideoData data)
        {
            if (vm.RawVideoId != data.VideoId) { throw new Models.Infrastructure.HohoemaExpception(); }

            vm.PostedAt = data.SubmitTime;
            //vm.Length = data.Length;
        }


        // とりあえずマイリストから取得したデータによる初期化
        public static void Setup(this VideoListItemControlViewModel vm, MylistData data)
        {
            if (data.WatchId != vm.RawVideoId) { throw new Models.Infrastructure.HohoemaExpception(); }

            // vm.VideoId = data.id
            vm.PostedAt = data.CreateTime;

            vm.ViewCount = (int)data.ViewCount;
            vm.CommentCount = (int)data.CommentCount;
            vm.MylistCount = (int)data.MylistCount;            
        }


        // 個別マイリストから取得したデータによる初期化
        public static void Setup(this VideoListItemControlViewModel vm, VideoInfo data)
        {
            if (vm.RawVideoId != data.Video.Id) { throw new Models.Infrastructure.HohoemaExpception(); }

            //vm.VideoId = data.Video.Id;
            vm.PostedAt = data.Video.FirstRetrieve;
            vm.ViewCount = (int)data.Video.ViewCount;
            vm.CommentCount = (int)data.Thread.GetCommentCount();
            vm.MylistCount = (int)data.Video.MylistCount;

            vm.ProviderType = data.Video.ProviderType == "channel" ? NicoVideoUserType.Channel : NicoVideoUserType.User;
            vm.ProviderId = vm.ProviderType == NicoVideoUserType.Channel ? data.Video.CommunityId : data.Video.UserId;
        }
    }




    [Flags]
    public enum VideoStatus
    {
        Watched = 0x0001,
        Filtered = 0x1000,
    }
}
