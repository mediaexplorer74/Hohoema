﻿using Hohoema.Models.Niconico.Mylist.LoginUser;
using Hohoema.Models.Niconico.Video;
using Hohoema.Models.Playlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hohoema.ViewModels.Niconico.Video.Commands
{
    public sealed class MylistRemoveItemCommand : VideoContentSelectionCommandBase
    {
        private readonly LoginUserMylistPlaylist _playlist;

        public MylistRemoveItemCommand(LoginUserMylistPlaylist playlist)
        {
            _playlist = playlist;
        }

        protected override void Execute(IVideoContent content)
        {
            if (content is IPlaylistItemPlayable playlistItemPlayable && playlistItemPlayable.PlaylistItemToken != null)
            {
                _playlist.RemoveItem(playlistItemPlayable.PlaylistItemToken);
            }
        }
    }
}
