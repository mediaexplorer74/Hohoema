﻿using Hohoema.Models.Domain.Niconico.Video;
using Hohoema.Presentation.ViewModels.Niconico.Video.Commands;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using NiconicoToolkit.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace Hohoema.Models.Domain.Playlist
{

    public sealed class LocalPlaylistItemRemovedEventArgs
    {
        public string PlaylistId { get; internal set; }
        public IReadOnlyCollection<VideoId> RemovedItems { get; internal set; }
    }

    public sealed class LocalPlaylistItemAddedEventArgs
    {
        public string PlaylistId { get; internal set; }
        public IReadOnlyCollection<VideoId> AddedItems { get; internal set; }
    }

    public sealed class LocalPlaylistItemAddedMessage : ValueChangedMessage<LocalPlaylistItemAddedEventArgs>
    {
        public LocalPlaylistItemAddedMessage(LocalPlaylistItemAddedEventArgs value) : base(value)
        {
        }
    }

    public sealed class LocalPlaylistItemRemovedMessage : ValueChangedMessage<LocalPlaylistItemRemovedEventArgs>
    {
        public LocalPlaylistItemRemovedMessage(LocalPlaylistItemRemovedEventArgs value) : base(value)
        {
        }
    }



    public sealed class LocalPlaylist : IPlaylist
    {
        private readonly PlaylistRepository _playlistRepository;
        private readonly NicoVideoProvider _nicoVideoProvider;
        private readonly IMessenger _messenger;

        internal LocalPlaylist(string id, string label, PlaylistRepository playlistRepository, NicoVideoProvider nicoVideoProvider, IMessenger messenger)
        {
            Id = id;
            Name = label;
            _playlistRepository = playlistRepository;
            _nicoVideoProvider = nicoVideoProvider;
            _messenger = messenger;

            Count = _playlistRepository.GetCount(Id);
        }

        public string Id { get; }

        public string Name { get; private set; }

        public void UpdateLabel(string label)
        {
            if (string.IsNullOrWhiteSpace(label)) { throw new InvalidOperationException(); }

            if (string.Compare(Name, label) != 0)
            {
                Name = label;
                UpdatePlaylistInfo();
            }
        }

        public int Count { get; set; }

        public int SortIndex { get; set; }

        Uri[] _thumbnailImages = new Uri[1];
        public Uri[] ThumbnailImages => _thumbnailImages;
        public Uri ThumbnailImage
        {
            get => _thumbnailImages[0];
            set => _thumbnailImages[0] = value;
        }


        public void AddPlaylistItem(IVideoContent item)
        {
            _playlistRepository.AddItem(Id, item.VideoId);

            var message = new LocalPlaylistItemAddedMessage(new()
            {
                PlaylistId = Id,
                AddedItems = new[] { item.VideoId }
            });
            _messenger.Send(message);
            _messenger.Send(message, Id);

            Count = _playlistRepository.GetCount(Id);
            if (Count == 1)
            {
                UpdateThumbnailImage(new Uri(item.ThumbnailUrl));
            }
        }

        public void UpdateThumbnailImage(Uri thumbnailImage)
        {
            this.ThumbnailImage = thumbnailImage;
            UpdatePlaylistInfo();
        }


        public void AddPlaylistItem(IEnumerable<IVideoContent> items)
        {
            var ids = items.Select(x => x.VideoId).ToList();
            _playlistRepository.AddItems(Id, ids);

            var message = new LocalPlaylistItemAddedMessage(new()
            {
                PlaylistId = Id,
                AddedItems = ids
            });
            _messenger.Send(message);
            _messenger.Send(message, Id);
        }



        public List<PlaylistItemEntity> GetPlaylistItems(int start, int count)
        {
            return _playlistRepository.GetItems(Id, start, count);
        }

        public bool RemovePlaylistItem(IVideoContent item)
        {
            var result = _playlistRepository.DeleteItem(Id, item.VideoId);

            if (result)
            {
                var message = new LocalPlaylistItemRemovedMessage(new()
                {
                    PlaylistId = Id,
                    RemovedItems = new[] { item.VideoId }
                });
                _messenger.Send(message);
                _messenger.Send(message, Id);
            }

            Count = _playlistRepository.GetCount(Id);

            return result;
        }

        public int RemovePlaylistItems(IEnumerable<IVideoContent> items)
        {
            var ids = items.Select(x => x.VideoId).ToList();
            var result = _playlistRepository.DeleteItems(Id, ids);

            if (result > 0)
            {
                var message = new LocalPlaylistItemRemovedMessage(new()
                {
                    PlaylistId = Id,
                    RemovedItems = ids
                });
                _messenger.Send(message);
                _messenger.Send(message, Id);
            }

            Count = _playlistRepository.GetCount(Id);

            return result;
        }



        private void UpdatePlaylistInfo()
        {
            _playlistRepository.UpsertPlaylist(new PlaylistEntity()
            {
                Id = this.Id,
                Label = this.Name,
                Count = 1,
                PlaylistOrigin = PlaylistOrigin.Local,
                ThumbnailImage = this.ThumbnailImage,
            });
        }
    }
    
    
}
