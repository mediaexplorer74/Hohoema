﻿using Hohoema.Models.Domain.Niconico;
using Hohoema.Models.Domain.Niconico.Mylist;
using Hohoema.Models.Domain.Niconico.User;
using Hohoema.Models.Domain.PageNavigation;
using Hohoema.Models.Domain.Pins;
using Hohoema.Models.Domain.Playlist;
using Hohoema.Models.Helpers;
using Hohoema.Models.UseCase;
using Hohoema.Models.UseCase.NicoVideos;
using Hohoema.Models.UseCase.PageNavigation;
using Microsoft.Toolkit.Collections;
using Prism.Navigation;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Hohoema.Presentation.ViewModels.Pages.Niconico.Mylist
{
    public class UserMylistPageViewModel : HohoemaListingPageViewModelBase<MylistPlaylist>, INavigatedAwareAsync, IPinablePage, ITitleUpdatablePage
	{
        HohoemaPin IPinablePage.GetPin()
        {
            return new HohoemaPin()
            {
                Label = UserName,
                PageType = HohoemaPageType.UserMylist,
                Parameter = $"id={UserId}"
            };
        }

        IObservable<string> ITitleUpdatablePage.GetTitleObservable()
        {
            return this.ObserveProperty(x => x.UserName);
        }

        public UserMylistPageViewModel(
            ApplicationLayoutManager applicationLayoutManager,
            PageManager pageManager,
            Services.DialogService dialogService,
            NiconicoSession niconicoSession,
            UserProvider userProvider,
            MylistRepository mylistRepository,
            LocalMylistManager localMylistManager,
            HohoemaPlaylist hohoemaPlaylist
            )
        {
            ApplicationLayoutManager = applicationLayoutManager;
            PageManager = pageManager;
            DialogService = dialogService;
            NiconicoSession = niconicoSession;
            UserProvider = userProvider;
            _mylistRepository = mylistRepository;
            _localMylistManager = localMylistManager;
            
            HohoemaPlaylist = hohoemaPlaylist;
        }

        public HohoemaPlaylist HohoemaPlaylist { get; }
        public ApplicationLayoutManager ApplicationLayoutManager { get; }
        public PageManager PageManager { get; }
        public Services.DialogService DialogService { get; }
        public NiconicoSession NiconicoSession { get; }
        public UserProvider UserProvider { get; }
        private readonly MylistRepository _mylistRepository;
        private readonly LocalMylistManager _localMylistManager;

        public string UserId { get; private set; }

        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { SetProperty(ref _UserName, value); }
        }

        public ReactiveCommand<IPlaylist> OpenMylistCommand { get; private set; }

        public override async Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            if (parameters.TryGetValue<string>("id", out string userId))
            {
                UserId = userId;
            }

            if ((UserId == null && NiconicoSession.IsLoggedIn) || NiconicoSession.IsLoginUserId(UserId))
            {
                // ログインユーザー用のマイリスト一覧ページにリダイレクト
                PageManager.ForgetLastPage();
                PageManager.OpenPage(HohoemaPageType.OwnerMylistManage);

                return;
            }
            else if (UserId != null)
            {
                try
                {
                    var userInfo = await UserProvider.GetUserInfoAsync(UserId);
                    UserName = userInfo.ScreenName;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            else
            {
                throw new Models.Infrastructure.HohoemaExpception("UserMylistPage が不明なパラメータと共に開かれました : " + parameters.ToString());
            }


            await base.OnNavigatedToAsync(parameters);
        }


        protected override (int, IIncrementalSource<MylistPlaylist>) GenerateIncrementalSource()
        {
            UserId ??= NiconicoSession.UserIdString;

            return (25 /* 全件取得するため指定不要 */, new OtherUserMylistIncrementalLoadingSource(UserId, _mylistRepository));
        }
    }

    public sealed class OtherUserMylistIncrementalLoadingSource : IIncrementalSource<MylistPlaylist>
    {
        List<MylistPlaylist> _userMylists { get; set; }

        public string UserId { get; }

        private readonly MylistRepository _mylistRepository;

        public OtherUserMylistIncrementalLoadingSource(string userId, MylistRepository mylistRepository)
        {
            UserId = userId;
            _mylistRepository = mylistRepository;
        }

        async Task<IEnumerable<MylistPlaylist>> IIncrementalSource<MylistPlaylist>.GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                _userMylists ??= await _mylistRepository.GetUserMylistsAsync(UserId);

                var head = pageIndex * pageSize;
                return _userMylists.Skip(head).Take(pageSize);
            }
            catch (Exception ex)
            {
                ErrorTrackingManager.TrackError(ex);
                return Enumerable.Empty<MylistPlaylist>();
            }

        }
    }
}