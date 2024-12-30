// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.OtherStreams.OtherStream
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Live.OtherStreams
{
  [DataContract]
  public sealed class OtherStream
  {
    private string _Id = string.Empty;
    private StatusType _Status;
    private ushort _TimeshiftEnabled;
    private DateTimeOffset _StartedAt = DateTimeOffset.MinValue;
    private DateTimeOffset _EndedAt = DateTimeOffset.MinValue;
    private CommunityType _CommunityType;

    internal OtherStream()
    {
    }

    public string Id => this._Id;

    [DataMember(Name = "id")]
    private string IdImpl
    {
      get => this._Id == null || this._Id.Length <= 2 ? (string) null : this._Id.Substring(2);
      set => this._Id = "lv" + value;
    }

    public StatusType Status => this._Status;

    [DataMember(Name = "currentstatus")]
    private string StatusImpl
    {
      get => this._Status.ToStatusTypeString();
      set => this._Status = value.ToStatusType();
    }

    [DataMember(Name = "title")]
    public string Title { get; private set; }

    [DataMember(Name = "description")]
    public string Description { get; private set; }

    [DataMember(Name = "is_exclude_non_display")]
    public bool IsHidden { get; private set; }

    [DataMember(Name = "is_exclude_private")]
    public bool IsPrivate { get; private set; }

    [DataMember(Name = "is_product")]
    public bool IsProduct { get; private set; }

    public ushort TimeshiftEnabled => this._TimeshiftEnabled;

    [DataMember(Name = "timeshift_enabled")]
    private object TimeshiftEnabledImpl
    {
      get => (object) this._TimeshiftEnabled;
      set
      {
        switch (value)
        {
          case bool flag:
            this._TimeshiftEnabled = flag ? (ushort) 1 : (ushort) 0;
            break;
          case int num:
            this._TimeshiftEnabled = (ushort) num;
            break;
        }
      }
    }

    [DataMember(Name = "is_timeshift_already_closed")]
    public bool IsTimeshiftClosed { get; private set; }

    [DataMember(Name = "is_timeshift_preparing")]
    public bool IsTimeshiftPreparing { get; private set; }

    [DataMember(Name = "picture_url")]
    public Uri ThumbnailUrl { get; private set; }

    [DataMember(Name = "ticket_url")]
    public Uri TicketPageUrl { get; private set; }

    [DataMember(Name = "twitter_disabled")]
    public bool IsTwitterDisabled { get; private set; }

    [DataMember(Name = "twitter_tag")]
    public string TwitterHashtag { get; set; }

    [DataMember(Name = "view_counter")]
    public uint ViewCount { get; private set; }

    [DataMember(Name = "comment_count")]
    public uint CommentCount { get; private set; }

    [DataMember(Name = "timeshift_reserved_count")]
    public uint TimeshiftReservedCount { get; private set; }

    public DateTimeOffset StartedAt => this._StartedAt;

    [DataMember(Name = "start_date_timestamp_sec")]
    private long StartedAtImpl
    {
      get => this._StartedAt.ToLongFromDateTimeOffset();
      set => this._StartedAt = value.ToDateTimeOffsetFromUnixTime();
    }

    public DateTimeOffset EndedAt => this._EndedAt;

    [DataMember(Name = "end_date_timestamp_sec")]
    private long EndedAtImpl
    {
      get => this._EndedAt.ToLongFromDateTimeOffset();
      set => this._EndedAt = value.ToDateTimeOffsetFromUnixTime();
    }

    public bool IsOfficial => this.CommunityType == CommunityType.Official;

    public bool IsChannel => this.CommunityType == CommunityType.Channel;

    public bool IsCommunity => this.CommunityType == CommunityType.Community;

    public CommunityType CommunityType => this._CommunityType;

    [DataMember(Name = "provider_type")]
    private string CommunityTypeImpl
    {
      get => string.Empty;
      set => this._CommunityType = value.ToCommunityType();
    }

    [DataMember(Name = "view_channel_icon")]
    public bool IsChannelIconEnabled { get; private set; }
  }
}
