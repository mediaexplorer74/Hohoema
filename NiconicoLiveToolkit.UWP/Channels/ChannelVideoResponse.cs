﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiconicoToolkit.Channels
{
    public sealed class ChannelVideoResponse : ResponseWithMeta
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public List<ChannelVideoItem> Videos { get; set; }
    }


    public sealed class ChannelVideoItem
    {
        public string ItemId { get; set; }

        public string Title { get; set; }
        public string ThumbnailUrl { get; set; }
        public TimeSpan Length { get; set; }

        public int ViewCount { get; set; }
        public int CommentCount { get; set; }
        public int MylistCount { get; set; }

        public DateTime PostedAt { get; set; }

        public string Description { get; set; }

        public string CommentSummary { get; set; }

        public bool IsRequirePayment { get; set; }
        public string PurchasePreviewUrl { get; set; }

        public bool IsMemberUnlimitedAccess { get; set; }
        public bool IsFreeForMember { get; set; }
    }
}
