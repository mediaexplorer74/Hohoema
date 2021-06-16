﻿using Hohoema.Presentation.Services;
using NiconicoToolkit.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hohoema.Models.Domain.Niconico.Video
{
    public interface IVideoContent : INiconicoContent, IEquatable<IVideoContent>
    {
        VideoId VideoId { get; }
        TimeSpan Length { get; }
        string ThumbnailUrl { get; }
        DateTime PostedAt { get; }
    }

    public interface IVideoContentProvider
    {
        string ProviderId { get; }
        OwnerType ProviderType { get; }
    }

    public interface IVideoDetail : IVideoContent, IVideoContentProvider
    {
        int ViewCount { get; }
        int MylistCount { get; }
        int CommentCount { get; }

        string Description { get; }
        bool IsDeleted { get; }
        VideoPermission Permission { get; }

        string ProviderName { get; }
        string ProviderIconUrl { get; }
    }
}
