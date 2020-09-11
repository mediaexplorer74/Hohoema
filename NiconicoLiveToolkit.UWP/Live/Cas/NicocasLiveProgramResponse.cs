﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace NiconicoLiveToolkit.Live.Cas
{

    public partial class LiveProgramResponse
    {
        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }

        [JsonPropertyName("data")]
        public Data Data { get; set; }
    }

    public partial class Data
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("onAirTime")]
        public Time OnAirTime { get; set; }

        [JsonPropertyName("showTime")]
        public Time ShowTime { get; set; }

        [JsonPropertyName("viewers")]
        public long? Viewers { get; set; }

        [JsonPropertyName("comments")]
        public long? Comments { get; set; }

        [JsonPropertyName("timeshiftReservedCount")]
        public long TimeshiftReservedCount { get; set; }

        [JsonPropertyName("thumbnailUrl")]
        public Uri ThumbnailUrl { get; set; }

        [JsonPropertyName("largeThumbnailUrl")]
        public Uri LargeThumbnailUrl { get; set; }

        [JsonPropertyName("large1920x1080ThumbnailUrl")]
        public Uri Large1920X1080ThumbnailUrl { get; set; }

        [JsonPropertyName("large352x198ThumbnailUrl")]
        public Uri Large352X198ThumbnailUrl { get; set; }

        [JsonPropertyName("liveCycle")]
        public string LiveCycle { get; set; }

        public LiveStatus LiveStatus => LiveCycle switch
        {
            "on_air" => Live.LiveStatus.Onair,
            "before_open" => Live.LiveStatus.Reserved,
            "ended" => Live.LiveStatus.Past,
            _ => throw new NotSupportedException()
        };

        [JsonPropertyName("providerType")]
        public string ProviderType { get; set; }

        [JsonPropertyName("providerId")]
        public string ProviderId { get; set; }

        [JsonPropertyName("socialGroupId")]
        public string SocialGroupId { get; set; }

        [JsonPropertyName("isChannelRelatedOfficial")]
        public bool IsChannelRelatedOfficial { get; set; }

        [JsonPropertyName("isMemberOnly")]
        public bool IsMemberOnly { get; set; }

        [JsonPropertyName("tags")]
        public List<Tag> Tags { get; set; }

        [JsonPropertyName("isIchibaEditable")]
        public bool IsIchibaEditable { get; set; }

        [JsonPropertyName("isOnlyRegisteredCommentable")]
        public bool IsOnlyRegisteredCommentable { get; set; }

        [JsonPropertyName("isQuotable")]
        public bool IsQuotable { get; set; }

        [JsonPropertyName("isNicocasWebProgram")]
        public bool IsNicocasWebProgram { get; set; }

        [JsonPropertyName("isDmc")]
        public bool IsDmc { get; set; }

        [JsonPropertyName("isPayProgram")]
        public bool IsPayProgram { get; set; }

        [JsonPropertyName("isDomesticOnly")]
        public bool IsDomesticOnly { get; set; }

        [JsonPropertyName("isNicoAdEnabled")]
        public bool IsNicoAdEnabled { get; set; }

        [JsonPropertyName("isGiftEnabled")]
        public bool IsGiftEnabled { get; set; }

        [JsonPropertyName("isProductSerialEnabled")]
        public bool IsProductSerialEnabled { get; set; }

        [JsonPropertyName("broadcastStreamSettings")]
        public BroadcastStreamSettings BroadcastStreamSettings { get; set; }

        [JsonPropertyName("isSemiOfficial")]
        public bool IsSemiOfficial { get; set; }

        [JsonPropertyName("isTagOwnerLock")]
        public bool IsTagOwnerLock { get; set; }

        [JsonPropertyName("programType")]
        public string ProgramType { get; set; }

        [JsonPropertyName("functions")]
        public List<Function> Functions { get; set; }

        [JsonPropertyName("timeshift")]
        public Timeshift Timeshift { get; set; }

        [JsonPropertyName("deviceFilter")]
        public DeviceFilter DeviceFilter { get; set; }

        [JsonPropertyName("advertisementType")]
        public string AdvertisementType { get; set; }

        [JsonPropertyName("twitterHashTag")]
        public string TwitterHashTag { get; set; }

        [JsonPropertyName("isPremiumAppeal")]
        public bool IsPremiumAppeal { get; set; }

        [JsonPropertyName("isEmotionEnabled")]
        public bool IsEmotionEnabled { get; set; }

        [JsonPropertyName("purchaseUrl")]
        public Uri PurchaseUrl { get; set; }
    }

    public partial class BroadcastStreamSettings
    {
        [JsonPropertyName("maxQuality")]
        public string MaxQuality { get; set; }

        [JsonPropertyName("isPortrait")]
        public bool IsPortrait { get; set; }
    }

    public partial class DeviceFilter
    {
        [JsonPropertyName("isPlayable")]
        public bool IsPlayable { get; set; }

        [JsonPropertyName("isListing")]
        public bool IsListing { get; set; }

        [JsonPropertyName("isArchivePlayable")]
        public bool IsArchivePlayable { get; set; }

        [JsonPropertyName("isChasePlayable")]
        public bool IsChasePlayable { get; set; }
    }

    public partial class Function
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("permission")]
        public string Permission { get; set; }
    }

    public partial class Time
    {
        [JsonPropertyName("beginAt")]
        public DateTimeOffset BeginAt { get; set; }

        [JsonPropertyName("endAt")]
        public DateTimeOffset EndAt { get; set; }
    }

    public partial class Tag
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        // Note: CamelCase
        [JsonPropertyName("type")]
        [JsonConverter(typeof(JsonStringEnumMemberConverter))]
        public TagType Type { get; set; }

        [JsonPropertyName("isLocked")]
        public bool IsLocked { get; set; }

        [JsonPropertyName("isDeletable")]
        public bool IsDeletable { get; set; }

        [JsonPropertyName("isExistNicopedia")]
        public bool IsExistNicopedia { get; set; }
    }

    public partial class Timeshift
    {
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        // Note: SnakeCase 
        [JsonPropertyName("status")]
        public TimeshiftStatus Status { get; set; }
    }

    public partial class Meta
    {
        [JsonPropertyName("status")]
        public long Status { get; set; }
    }

    /// <summary>
    /// タグの種別
    /// </summary>
    public enum TagType { Category, Normal, MemberOnly };

    /// <summary>
    /// タイムシフトタイプ
    /// </summary>
    public enum TimeshiftStatus 
    {
        /// <summary>
        /// タイムシフト無し
        /// </summary>
        None,
        
        /// <summary>
        /// タイムシフトはまだリリースされていない
        /// </summary>
        BeforeRelease, 

        /// <summary>
        /// タイムシフト利用可能
        /// </summary>
        Released, 

        /// <summary>
        /// タイムシフト期限切れ
        /// </summary>
        Expired, 
    };



}