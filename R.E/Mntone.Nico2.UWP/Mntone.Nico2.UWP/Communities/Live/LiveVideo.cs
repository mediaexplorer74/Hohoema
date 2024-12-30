// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Communities.Live.LiveVideo
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Live;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Communities.Live
{
  [DataContract]
  public class LiveVideo
  {
    private DateTime? _OpenTime;
    private DateTime? _StartTime;
    private DateTime? _EndTime;
    private CommunityType? _ProviderType;
    private bool? _HidescoreOnline;
    private bool? _HidescoreComment;
    private bool? _CommunityOnly;
    private bool? _ChannelOnly;
    private bool? _TimeshiftEnabled;
    private bool? _IsHq;

    [DataMember(Name = "id")]
    public string Id { get; set; }

    [DataMember(Name = "title")]
    public string Title { get; set; }

    [DataMember(Name = "open_time")]
    public string __OpenTime { get; set; }

    public DateTime OpenTime
    {
      get
      {
        return !this._OpenTime.HasValue ? (this._OpenTime = new DateTime?(DateTime.Parse(this.__OpenTime))).Value : this._OpenTime.Value;
      }
    }

    [DataMember(Name = "start_time")]
    public string __StartTime { get; set; }

    public DateTime StartTime
    {
      get
      {
        return !this._StartTime.HasValue ? (this._StartTime = new DateTime?(DateTime.Parse(this.__StartTime))).Value : this._StartTime.Value;
      }
    }

    [DataMember(Name = "schedule_end_time")]
    public string __ScheduleEndTime { get; set; }

    public DateTime? GetShcduleEndTime()
    {
      return !string.IsNullOrEmpty(this.__ScheduleEndTime) ? new DateTime?(DateTime.Parse(this.__ScheduleEndTime)) : new DateTime?();
    }

    [DataMember(Name = "end_time")]
    public string __EndTime { get; set; }

    public DateTime EndTime
    {
      get
      {
        return !this._EndTime.HasValue ? (this._EndTime = new DateTime?(DateTime.Parse(this.__EndTime))).Value : this._EndTime.Value;
      }
    }

    [DataMember(Name = "provider_type")]
    public string __ProviderType { get; set; }

    public CommunityType ProviderType
    {
      get
      {
        return !this._ProviderType.HasValue ? (this._ProviderType = new CommunityType?(this.__ProviderType.ToCommunityType())).Value : this._ProviderType.Value;
      }
    }

    [DataMember(Name = "related_channel_id")]
    public string RelatedChannelId { get; set; }

    public bool HasRelatedChannelId => !string.IsNullOrEmpty(this.RelatedChannelId);

    [DataMember(Name = "hidescore_online")]
    public string __HidescoreOnline { get; set; }

    public bool HidescoreOnline
    {
      get
      {
        return !this._HidescoreOnline.HasValue ? (this._HidescoreOnline = new bool?(this.__HidescoreOnline.ToBooleanFrom1())).Value : this._HidescoreOnline.Value;
      }
    }

    [DataMember(Name = "hidescore_comment")]
    public string __HidescoreComment { get; set; }

    public bool HidescoreComment
    {
      get
      {
        return !this._HidescoreComment.HasValue ? (this._HidescoreComment = new bool?(this.__HidescoreComment.ToBooleanFrom1())).Value : this._HidescoreComment.Value;
      }
    }

    [DataMember(Name = "community_only")]
    public string __CommunityOnly { get; set; }

    public bool CommunityOnly
    {
      get
      {
        return !this._CommunityOnly.HasValue ? (this._CommunityOnly = new bool?(this.__CommunityOnly.ToBooleanFrom1())).Value : this._CommunityOnly.Value;
      }
    }

    [DataMember(Name = "channel_only")]
    public string __ChannelOnly { get; set; }

    public bool ChannelOnly
    {
      get
      {
        return !this._ChannelOnly.HasValue ? (this._ChannelOnly = new bool?(this.__ChannelOnly.ToBooleanFrom1())).Value : this._ChannelOnly.Value;
      }
    }

    [DataMember(Name = "view_counter")]
    public string ViewCounter { get; set; }

    [DataMember(Name = "comment_count")]
    public string CommentCount { get; set; }

    [DataMember(Name = "_ts_reserved_count")]
    public string TsReservedCount { get; set; }

    [DataMember(Name = "timeshift_enabled")]
    public string __TimeshiftEnabled { get; set; }

    public bool TimeshiftEnabled
    {
      get
      {
        return !this._TimeshiftEnabled.HasValue ? (this._TimeshiftEnabled = new bool?(this.__TimeshiftEnabled.ToBooleanFrom1())).Value : this._TimeshiftEnabled.Value;
      }
    }

    [DataMember(Name = "is_hq")]
    public string __IsHq { get; set; }

    public bool IsHq
    {
      get
      {
        return !this._IsHq.HasValue ? (this._IsHq = new bool?(this.__IsHq.ToBooleanFrom1())).Value : this._IsHq.Value;
      }
    }

    [DataMember(Name = "_thumbnail_url")]
    public string ThumbnailUrl { get; set; }

    [DataMember(Name = "_picture_url")]
    public string PictureUrl { get; set; }
  }
}
