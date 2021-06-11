﻿using Mntone.Nico2.Communities.Detail;
using Hohoema.Models.Domain.Niconico.Video;
using System;
using System.Linq;
using System.Reactive.Linq;
using NiconicoToolkit.Video;

namespace Hohoema.Presentation.ViewModels.Community
{
    public class CommunityVideoInfoViewModel : HohoemaListingPageItemBase, IVideoContent
    {
        public string Title { get; }

        public string ProviderId => null;

        public string ProviderName => null;

        public OwnerType ProviderType => OwnerType.User;

        public string Id { get; }

        public string VideoId => throw new NotImplementedException();

        public TimeSpan Length => TimeSpan.Zero;

        public DateTime PostedAt => DateTime.MinValue;

        public int ViewCount => 0;

        public int MylistCount => 0;

        public int CommentCount => 0;

        public string ThumbnailUrl { get; }

        public bool IsDeleted { get; set; }

        public VideoPermission Permission => VideoPermission.Unknown;

        public CommunityVideoInfoViewModel(NiconicoToolkit.Community.CommunityVideoListItemsResponse.CommunityVideoListItem video)
		{
			Title = video.Title;
            Id = video.Id;
            

            Label = video.Title;
            if (video.ThumbnailUrl != null)
            {
                AddImageUrl(video.ThumbnailUrl.OriginalString);
            }
            ThumbnailUrl = video.ThumbnailUrl.OriginalString;
        }

        public CommunityVideoInfoViewModel(Mntone.Nico2.RssVideoData rssVideoData)
        {
            Title = rssVideoData.RawTitle;
            Id = rssVideoData.WatchPageUrl.OriginalString.Split('/').Last();
            Label = Title;
        }


        public bool Equals(IVideoContent other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
