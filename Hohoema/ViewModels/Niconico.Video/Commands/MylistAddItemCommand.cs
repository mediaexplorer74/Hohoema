﻿using I18NPortable;
using Hohoema.Models;
using Hohoema.Models.Niconico.Mylist.LoginUser;
using Hohoema.Models.Niconico.Video;
using Hohoema.Models.Playlist;
using Hohoema.Services;
using CommunityToolkit.Mvvm.Input;
using System.Linq;
using System.Collections.Generic;
using Hohoema.Dialogs;
using System;
using System.Threading.Tasks;
using Hohoema.Services.Playlist;

namespace Hohoema.ViewModels.Niconico.Video.Commands
{
    public sealed class MylistAddItemCommand : VideoContentSelectionCommandBase
    {
        private readonly DialogService _dialogService;
        private readonly LoginUserOwnedMylistManager _userMylistManager;

        public MylistAddItemCommand(
            NotificationService notificationService,
            DialogService dialogService,
            LoginUserOwnedMylistManager userMylistManager
            )
        {
            NotificationService = notificationService;
            _dialogService = dialogService;
            _userMylistManager = userMylistManager;
        }

        public NotificationService NotificationService { get; }
        public DialogService DialogService { get; }

        protected override void Execute(IVideoContent content)
        {
            Execute(new[] { content });
        }

        protected override async void Execute(IEnumerable<IVideoContent> items)
        {
            var targetMylist = _userMylistManager.Mylists.Any() ?
                    await _dialogService.ShowSingleSelectDialogAsync(
                    _userMylistManager.Mylists.ToList(),
                    nameof(LoginUserMylistPlaylist.Name),
                    (mylist, s) => mylist.Name.Contains(s),
                    "SelectMylist".Translate(),
                    "Select".Translate(),
                    "CreateNew".Translate(),
                    () => CreateMylistAsync()
                    )
                    : await CreateMylistAsync()
                    ;

            if (targetMylist != null)
            {
                var addedResult = await targetMylist.AddItem(items);
                if (addedResult.SuccessedItems.Any() && addedResult.FailedItems.Any() is false)
                {
//                    NotificationService.ShowLiteInAppNotification("InAppNotification_MylistAddedItems_Success".Translate(targetMylist.Label, addedResult.SuccessedItems.Count));
                }
                else
                {
//                    NotificationService.ShowLiteInAppNotification("InAppNotification_MylistAddedItems_Fail".Translate(targetMylist.Label));
                }
            }
        }

        async Task<LoginUserMylistPlaylist> CreateMylistAsync()
        {
            // 新規作成
            var data = new MylistGroupEditData();
            if (await _dialogService.ShowEditMylistGroupDialogAsync(data))
            {
                return await _userMylistManager.AddMylist(
                 data.Name,
                 data.Description,
                 data.IsPublic,
                 data.DefaultSortKey,
                 data.DefaultSortOrder
                 );
            }
            else
            {
                return default;
            }
        }
    }
}
