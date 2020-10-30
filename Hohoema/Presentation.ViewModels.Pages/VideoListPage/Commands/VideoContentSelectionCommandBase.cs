﻿
using Hohoema.Models.Domain.Niconico.Video;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hohoema.Presentation.ViewModels.NicoVideos.Commands
{
    public abstract class VideoContentSelectionCommandBase : DelegateCommandBase
    {
        public VideoContentSelectionCommandBase()
        {
            IsActive = true;
        }

        protected override bool CanExecute(object parameter)
        {
            return parameter is IVideoContent
                || parameter is IEnumerable<IVideoContent>
                ;
        }

        protected override void Execute(object parameter)
        {
            if (parameter is IVideoContent content)
            {
                Execute(content);
            }
            else if (parameter is IEnumerable<IVideoContent> items)
            {
                Execute(items);
            }
        }

        protected virtual void Execute(IVideoContent content)
        {

        }

        protected virtual void Execute(IEnumerable<IVideoContent> items)
        {
            foreach (var item in items)
            {
                Execute(item);
            }
        }
    }
}
