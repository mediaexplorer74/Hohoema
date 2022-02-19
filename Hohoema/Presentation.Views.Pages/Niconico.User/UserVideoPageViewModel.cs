﻿using Hohoema.Models.Domain.Niconico.User;
using Hohoema.Models.Domain.PageNavigation;
using Hohoema.Models.Domain.Pins;
using Hohoema.Models.Domain.Player.Video.Cache;
using Hohoema.Models.Domain.Subscriptions;
using Hohoema.Models.Helpers;
using Hohoema.Models.UseCase;
using Hohoema.Models.UseCase.Playlist;
using Hohoema.Models.UseCase.PageNavigation;
using Hohoema.Presentation.ViewModels.Niconico.User;
using Hohoema.Presentation.ViewModels.Niconico.Video.Commands;
using Hohoema.Presentation.ViewModels.Subscriptions;
using Hohoema.Presentation.ViewModels.VideoListPage;
using Microsoft.Toolkit.Collections;
using NiconicoToolkit.User;
using Prism.Commands;
using Prism.Navigation;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Hohoema.Models.Domain.User;
using Hohoema.Models.Domain.Playlist;
using System.Reactive.Linq;
using Hohoema.Models.Domain.Video;
using Microsoft.Extensions.Logging;
using ZLogger;

namespace Hohoema.Presentation.ViewModels.Pages.Niconico.User
{
    public class UserVideoPageViewModel : HohoemaListingPageViewModelBase<VideoListItemControlViewModel>, INavigatedAwareAsync, IPinablePage, ITitleUpdatablePage
    {
        HohoemaPin IPinablePage.GetPin()
        {
            return new HohoemaPin()
            {
                Label = UserName,
                PageType = HohoemaPageType.UserVideo,
                Parameter = $"id={UserId}"
            };
        }

        IObservable<string> ITitleUpdatablePage.GetTitleObservable()
        {
            return this.ObserveProperty(x => x.UserName);
        }

        public UserVideoPageViewModel(
            ILoggerFactory loggerFactory,
            ApplicationLayoutManager applicationLayoutManager,
            UserProvider userProvider,
            SubscriptionManager subscriptionManager,
            PageManager pageManager,
            VideoPlayWithQueueCommand videoPlayWithQueueCommand,
            PlaylistPlayAllCommand playlistPlayAllCommand,
            AddSubscriptionCommand addSubscriptionCommand,
            SelectionModeToggleCommand selectionModeToggleCommand
            )
            : base(loggerFactory.CreateLogger<UserVideoPageViewModel>())
        {
            SubscriptionManager = subscriptionManager;
            ApplicationLayoutManager = applicationLayoutManager;
            UserProvider = userProvider;
            PageManager = pageManager;
            VideoPlayWithQueueCommand = videoPlayWithQueueCommand;
            PlaylistPlayAllCommand = playlistPlayAllCommand;
            AddSubscriptionCommand = addSubscriptionCommand;
            SelectionModeToggleCommand = selectionModeToggleCommand;
            UserInfo = new ReactiveProperty<UserInfoViewModel>();

            CurrentPlaylistToken = Observable.CombineLatest(
                this.ObserveProperty(x => x.UserVideoPlaylist),
                this.ObserveProperty(x => x.SelectedSortOption),
                (x, y) => new PlaylistToken(x, y)
                )
                .ToReadOnlyReactivePropertySlim()
                .AddTo(_CompositeDisposable);
            
            SelectedSortOption = UserVideoPlaylist.DefaultSortOption;
        }


        public SubscriptionManager SubscriptionManager { get; }
        public ApplicationLayoutManager ApplicationLayoutManager { get; }
        public UserProvider UserProvider { get; }
        public PageManager PageManager { get; }
        public VideoPlayWithQueueCommand VideoPlayWithQueueCommand { get; }
        public PlaylistPlayAllCommand PlaylistPlayAllCommand { get; }
        public AddSubscriptionCommand AddSubscriptionCommand { get; }
        public SelectionModeToggleCommand SelectionModeToggleCommand { get; }
        public ReactiveProperty<UserInfoViewModel> UserInfo { get; }

        private UserVideoPlaylist _UserVideoPlaylist;
        public UserVideoPlaylist UserVideoPlaylist
        {
            get { return _UserVideoPlaylist; }
            set { SetProperty(ref _UserVideoPlaylist, value); }
        }

        public UserVideoPlaylistSortOption[] SortOptions => UserVideoPlaylist.SortOptions;


        private UserVideoPlaylistSortOption _selectedSortOption;
        public UserVideoPlaylistSortOption SelectedSortOption
        {
            get { return _selectedSortOption; }
            set { SetProperty(ref _selectedSortOption, value); }
        }


        public ReadOnlyReactivePropertySlim<PlaylistToken> CurrentPlaylistToken { get; }



        public override async Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            UserId? userId = null;

            if (parameters.TryGetValue("id", out string id))
            {
                userId = id;
            }
            else if (parameters.TryGetValue("id", out UserId justUserId))
            {
                userId = justUserId;
            }

            if (userId is null || userId == UserId)
            {
                UserId = null;
                UserInfo.Value = null;
                UserName = null;
                return;
            }

            UserId = userId;

            var res = await UserProvider.GetUserDetailAsync(UserId.Value);
            if (res.IsSuccess)
            {
                User = res.Data.User;

                UserInfo.Value = new UserInfoViewModel(User.Nickname, User.Id.ToString(), User.Icons.Small.OriginalString);
                UserName = User.Nickname;


                UserVideoPlaylist = new UserVideoPlaylist(User.Id, new PlaylistId() { Id = User.Id, Origin = PlaylistItemsSourceOrigin.UserVideos }, User.Nickname, UserProvider);
                SelectedSortOption = UserVideoPlaylist.DefaultSortOption;

                this.ObserveProperty(x => x.SelectedSortOption)
                    .Where(x => x is not null)
                    .Subscribe(_ => ResetList())
                    .AddTo(_navigationDisposables);
            }

            await base.OnNavigatedToAsync(parameters);
        }



		protected override (int, IIncrementalSource<VideoListItemControlViewModel>) GenerateIncrementalSource()
		{
            if (_selectedSortOption is null)
            {
                SelectedSortOption = UserVideoPlaylist.DefaultSortOption;
            }

            return (UserVideoIncrementalSource.OneTimeLoadCount, new UserVideoIncrementalSource(UserId, User, UserProvider, UserVideoPlaylist, SelectedSortOption, _logger));
		}


        private DelegateCommand _OpenVideoOwnerUserPageCommand;
		public DelegateCommand OpenVideoOwnerUserPageCommand
		{
			get
			{
				return _OpenVideoOwnerUserPageCommand
					?? (_OpenVideoOwnerUserPageCommand = new DelegateCommand(() => 
					{
						PageManager.OpenPageWithId(HohoemaPageType.UserInfo, UserId);
					}));
			}
		}


		private string _UserName;
		public string UserName
		{
			get { return _UserName; }
			set { SetProperty(ref _UserName, value); }
		}

        private bool _IsOwnerVideoPrivate;
        public bool IsOwnerVideoPrivate
        {
            get { return _IsOwnerVideoPrivate; }
            set { SetProperty(ref _IsOwnerVideoPrivate, value); }
        }

        public UserDetail User { get; private set; }

		
		public UserId? UserId { get; private set; }
	}


	public class UserVideoIncrementalSource : IIncrementalSource<VideoListItemControlViewModel>
	{

        public const int OneTimeLoadCount = 25;
        private readonly UserVideoPlaylist _userVideoPlaylist;
        private readonly UserVideoPlaylistSortOption _selectedSortOption;
        private readonly ILogger _logger;

        public uint UserId { get; }
		public UserProvider UserProvider { get; }
        public UserDetail User { get; private set;}

		public UserVideoIncrementalSource(
            string userId, 
            UserDetail userDetail,
            UserProvider userProvider, 
            UserVideoPlaylist userVideoPlaylist, 
            UserVideoPlaylistSortOption selectedSortOption,
            ILogger logger
            )
		{
			UserId = uint.Parse(userId);
			User = userDetail;
            UserProvider = userProvider;
            _userVideoPlaylist = userVideoPlaylist;
            _selectedSortOption = selectedSortOption;
            _logger = logger;
        }

        bool _isEnd = false;
        int _count = 0;


        async Task<IEnumerable<VideoListItemControlViewModel>> IIncrementalSource<VideoListItemControlViewModel>.GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken ct)
        {
            if (_isEnd) { return Enumerable.Empty<VideoListItemControlViewModel>(); }

            var res = await UserProvider.GetUserVideosAsync(UserId, pageIndex, pageSize, _selectedSortOption.SortKey, _selectedSortOption.SortOrder);

            ct.ThrowIfCancellationRequested();

            try
            {
                var head = pageIndex * pageSize;
                var items = res.Data.Items;
                _count += items.Length;
                _isEnd = _count >= res.Data.TotalCount;
                return items.Select((item, i) => 
                new VideoListItemControlViewModel(item.Essential) 
                {
                    PlaylistItemToken = new PlaylistItemToken(_userVideoPlaylist, _selectedSortOption, new NvapiVideoContent(item.Essential))
                }
                );
            }
            catch (Exception e)
            {
                _logger.ZLogErrorWithPayload(exception: e, UserId, "User videos loading failed");
                return Enumerable.Empty<VideoListItemControlViewModel>();
            }
        }
    }
}
