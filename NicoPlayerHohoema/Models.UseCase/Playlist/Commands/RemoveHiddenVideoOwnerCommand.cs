﻿using Hohoema.Models.Domain;
using Hohoema.Models.Domain.Niconico.Video;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hohoema.Models.UseCase.Playlist.Commands
{
    public sealed class RemoveHiddenVideoOwnerCommand : DelegateCommandBase
    {
        private readonly VideoFilteringSettings _ngSettings;

        public RemoveHiddenVideoOwnerCommand(VideoFilteringSettings ngSettings)
        {
            _ngSettings = ngSettings;
        }

        protected override bool CanExecute(object parameter)
        {
            if (parameter is IVideoContent video)
            {
                if (video.ProviderId != null)
                {
                    return _ngSettings.IsHiddenVideoOwnerId(video.ProviderId);
                }
            }

            return false;
        }

        protected override void Execute(object parameter)
        {
            if (parameter is IVideoContent video)
            {
                if (video.ProviderId != null)
                {
                    _ngSettings.RemoveHiddenVideoOwnerId(video.ProviderId);
                }
            }
        }
    }
}