﻿using Hohoema.Models.Domain.Niconico.Video;
using Hohoema.Models.Domain.Playlist;
using Hohoema.Models.UseCase;
using Hohoema.Models.UseCase.NicoVideos;
using Hohoema.Presentation.ViewModels.Niconico.Video.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hohoema.Presentation.ViewModels.Pages.Hohoema.Video
{
    public sealed class VideoQueuePageViewModel : HohoemaViewModelBase, INavigationAware
    {
        private readonly HohoemaPlaylist _hohoemaPlaylist;
        private readonly PlaylistObservableCollection _watchAfterPlaylist;

        public IReadOnlyCollection<IVideoContent> PlaylistItems { get; }

        public IPlaylist Playlist => _watchAfterPlaylist;
        public ICommand PlayCommand => _hohoemaPlaylist.PlayCommand;

        public ApplicationLayoutManager ApplicationLayoutManager { get; }
        public RemoveWatchedItemsInAfterWatchPlaylistCommand RemoveWatchedItemsInAfterWatchPlaylistCommand { get; }
        public PlaylistPlayAllCommand PlaylistPlayAllCommand { get; }
        public SelectionModeToggleCommand SelectionModeToggleCommand { get; }

        public VideoQueuePageViewModel(
            HohoemaPlaylist hohoemaPlaylist,
            ApplicationLayoutManager applicationLayoutManager,
            RemoveWatchedItemsInAfterWatchPlaylistCommand removeWatchedItemsInAfterWatchPlaylistCommand,
            PlaylistPlayAllCommand playlistPlayAllCommand,
            SelectionModeToggleCommand selectionModeToggleCommand
            )
        {
            _hohoemaPlaylist = hohoemaPlaylist;
            ApplicationLayoutManager = applicationLayoutManager;
            RemoveWatchedItemsInAfterWatchPlaylistCommand = removeWatchedItemsInAfterWatchPlaylistCommand;
            PlaylistPlayAllCommand = playlistPlayAllCommand;
            SelectionModeToggleCommand = selectionModeToggleCommand;
            _watchAfterPlaylist = _hohoemaPlaylist.QueuePlaylist;
            PlaylistItems = _watchAfterPlaylist;
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

            base.OnNavigatedFrom(parameters);
        }
    }
}