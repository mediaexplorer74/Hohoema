﻿using Hohoema.Models.UseCase.NicoVideos;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hohoema.Presentation.ViewModels.NicoVideos.Commands
{
    public sealed class WatchHistoryRemoveAllCommand : DelegateCommandBase
    {
        private readonly WatchHistoryManager _watchHistoryManager;

        public WatchHistoryRemoveAllCommand(
            WatchHistoryManager watchHistoryManager
            )
        {
            _watchHistoryManager = watchHistoryManager;
        }

        protected override bool CanExecute(object parameter)
        {
            return true;
        }

        protected override void Execute(object parameter)
        {
            var currentMethod = System.Reflection.MethodBase.GetCurrentMethod();
            Microsoft.AppCenter.Analytics.Analytics.TrackEvent($"{currentMethod.DeclaringType.Name}#{currentMethod.Name}");

            _ = _watchHistoryManager.RemoveAllHistoriesAsync();
        }
    }
}