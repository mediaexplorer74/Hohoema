﻿using Hohoema.Dialogs;
using Hohoema.Models.Domain;
using Hohoema.Models.Domain.Niconico.UserFeature.Mylist;
using Hohoema.Models.Domain.PageNavigation;
using Hohoema.Models.Domain.Playlist;
using Hohoema.Models.UseCase;
using Hohoema.Models.UseCase.NicoVideos;
using Hohoema.Presentation.Services;
using Hohoema.Presentation.Services.Page;
using Hohoema.Presentation.ViewModels.NicoVideos.Commands;
using I18NPortable;
using Microsoft.Toolkit.Uwp.UI;
using Prism.Commands;
using Prism.Navigation;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Uno.Extensions;
using Windows.UI.Popups;

namespace Hohoema.Presentation.ViewModels.Pages.MylistPages
{

    public class OwnerMylistManagePageViewModel : HohoemaViewModelBase
    {
        ObservableCollection<IPlaylist> _sourcePlaylistItems = new ObservableCollection<IPlaylist>();
        public AdvancedCollectionView ItemsView { get; }
        public ReactivePropertySlim<bool> NowLoading { get; } = new ReactivePropertySlim<bool>();

        private readonly NiconicoSession _niconicoSession;
        private readonly PageManager _pageManager;
        private readonly DialogService _dialogService;
        private readonly UserMylistManager _userMylistManager;

        public ApplicationLayoutManager ApplicationLayoutManager { get; }

        public PlaylistPlayAllCommand PlaylistPlayAllCommand { get; }
        public ReactiveCommand<LoginUserMylistPlaylist> OpenMylistCommand { get;  }
        public DelegateCommand AddMylistGroupCommand { get; }
        public DelegateCommand<LoginUserMylistPlaylist> RemoveMylistGroupCommand { get; }
        public DelegateCommand<LoginUserMylistPlaylist> EditMylistGroupCommand { get; }

        public OwnerMylistManagePageViewModel(
            NiconicoSession niconicoSession,
            PageManager pageManager,
            Services.DialogService dialogService,
            ApplicationLayoutManager applicationLayoutManager,
            UserMylistManager userMylistManager,
            PlaylistPlayAllCommand playlistPlayAllCommand
            )
        {
            _niconicoSession = niconicoSession;
            _pageManager = pageManager;
            _dialogService = dialogService;
            ApplicationLayoutManager = applicationLayoutManager;
            _userMylistManager = userMylistManager;
            PlaylistPlayAllCommand = playlistPlayAllCommand;

            ItemsView = new AdvancedCollectionView(_sourcePlaylistItems);

            OpenMylistCommand = new ReactiveCommand<LoginUserMylistPlaylist>()
                .AddTo(_CompositeDisposable);

            OpenMylistCommand.Subscribe(listItem =>
            {
                _pageManager.OpenPageWithId(HohoemaPageType.Mylist, listItem.Id);
            });

            AddMylistGroupCommand = new DelegateCommand(async () =>
            {
                MylistGroupEditData data = new MylistGroupEditData()
                {
                    Name = "",
                    Description = "",
                    IsPublic = false,
                    DefaultSortKey = Mntone.Nico2.Users.Mylist.MylistSortKey.AddedAt,
                    DefaultSortOrder = Mntone.Nico2.Users.Mylist.MylistSortOrder.Desc
                };

                // 成功するかキャンセルが押されるまで繰り返す
                while (true)
                {
                    if (true == await _dialogService.ShowCreateMylistGroupDialogAsync(data))
                    {
                        var result = await _userMylistManager.AddMylist(
                            data.Name,
                            data.Description,
                            data.IsPublic,
                            data.DefaultSortKey,
                            data.DefaultSortOrder
                        );

                        if (result != null)
                        {
                            await RefreshPlaylistItems();
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            , () => _userMylistManager.Mylists.Count < _userMylistManager.MaxMylistGroupCountCurrentUser
            );

            RemoveMylistGroupCommand = new DelegateCommand<LoginUserMylistPlaylist>(async (mylist) =>
            {
                if (mylist.IsDefaultMylist()) { return; }

                // 確認ダイアログ
                var contentMessage = "ConfirmDeleteX_ImpossibleReDo".Translate(mylist.Label);

                var dialog = new MessageDialog(contentMessage, "ConfirmDeleteX".Translate("Mylist".Translate()));
                dialog.Commands.Add(new UICommand("Delete".Translate(), async (i) =>
                {
                    if (await _userMylistManager.RemoveMylist(mylist.Id))
                    {
                        await RefreshPlaylistItems();
                    }
                }));

                dialog.Commands.Add(new UICommand("Cancel".Translate()));
                dialog.CancelCommandIndex = 1;
                dialog.DefaultCommandIndex = 1;

                await dialog.ShowAsync();
            });


            EditMylistGroupCommand = new DelegateCommand<LoginUserMylistPlaylist>(async mylist =>
            {
                if (mylist.IsDefaultMylist())
                {
                    return;
                }

                MylistGroupEditData data = new MylistGroupEditData()
                {
                    Name = mylist.Label,
                    Description = mylist.Description,
                    IsPublic = mylist.IsPublic,
                    DefaultSortKey = mylist.DefaultSortKey,
                    DefaultSortOrder = mylist.DefaultSortOrder,
                };

                // 成功するかキャンセルが押されるまで繰り返す
                while (true)
                {
                    if (true == await _dialogService.ShowCreateMylistGroupDialogAsync(data))
                    {
                        if (await mylist.UpdateMylistInfo(mylist.Id, data.Name, data.Description, data.IsPublic, data.DefaultSortKey, data.DefaultSortOrder))
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            });
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            new[]
            {
                _niconicoSession.ObserveProperty(x => x.IsLoggedIn).ToUnit(),
            }
            .Merge()
            .Subscribe(async _ =>
            {
                await RefreshPlaylistItems();
            })
            .AddTo(_NavigatingCompositeDisposable);

            /*
            new[]
            {
                SelectedDisplayMylistKind.ToUnit()
            }
            .Merge()
            .Subscribe(_ => ItemsView.RefreshFilter())
            .AddTo(_NavigatingCompositeDisposable);
            */
        }

        private async Task RefreshPlaylistItems()
        {
            NowLoading.Value = true;
            try
            {
                using (ItemsView.DeferRefresh())
                {
                    _sourcePlaylistItems.Clear();

                    // TODO: タイムアウト処理を追加する
                    using var _ = await _niconicoSession.SigninLock.LockAsync();
                    await _userMylistManager.WaitUpdate();

                    _sourcePlaylistItems.AddRange(_userMylistManager.Mylists.Where(x => x.IsDefaultMylist() is false));
                }
            }
            finally
            {
                NowLoading.Value = false;
            }

            AddMylistGroupCommand.RaiseCanExecuteChanged();
        }
    }
}