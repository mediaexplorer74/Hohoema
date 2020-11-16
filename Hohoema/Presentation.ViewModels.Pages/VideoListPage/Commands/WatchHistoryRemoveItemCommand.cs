﻿
using Hohoema.Models.Domain.Niconico;
using Hohoema.Models.UseCase.NicoVideos;
using Prism.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hohoema.Presentation.ViewModels.NicoVideos.Commands
{
    public sealed class WatchHistoryRemoveItemCommand : DelegateCommandBase
    {
        private readonly WatchHistoryManager _watchHistoryManager;

        public WatchHistoryRemoveItemCommand(
            WatchHistoryManager watchHistoryManager
            )
        {
            _watchHistoryManager = watchHistoryManager;
        }

        protected override bool CanExecute(object parameter)
        {
            return true;
        }

        protected override async void Execute(object parameter)
        {
            var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
            Microsoft.AppCenter.Analytics.Analytics.TrackEvent($"{currentMethod.DeclaringType.Name}#{currentMethod.Name}");

            if (parameter is IWatchHistory watchHistory)
            {
                _ = _watchHistoryManager.RemoveHistoryAsync(watchHistory);
            }
            else if (parameter is IList histories)
            {
                foreach (var item in histories.Cast<IWatchHistory>().ToList())
                {
                    await _watchHistoryManager.RemoveHistoryAsync(item);
                }
            }
        }
    }
}