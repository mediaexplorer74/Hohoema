// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Histories.History
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Histories
{
  [DataContract]
  public sealed class History
  {
    private PrivateReasonType _DeleteStatus;
    private TimeSpan _Length = TimeSpan.Zero;
    private DateTimeOffset _WatchedAt = DateTimeOffset.MinValue;

    internal History()
    {
    }

    public PrivateReasonType DeleteStatus => this._DeleteStatus;

    [DataMember(Name = "deleted")]
    private uint IsDeletedImpl
    {
      get => (uint) this._DeleteStatus;
      set => this._DeleteStatus = (PrivateReasonType) value;
    }

    [DataMember(Name = "device")]
    public ushort Device { get; private set; }

    [DataMember(Name = "item_id")]
    public string ItemId { get; private set; }

    public TimeSpan Length => this._Length;

    [DataMember(Name = "length")]
    private string LengthImpl
    {
      get
      {
        TimeSpan length = this._Length;
        return (60 * this._Length.Hours + this._Length.Minutes).ToString() + ":" + (object) this._Length.Seconds;
      }
      set => this._Length = value.ToTimeSpan();
    }

    [DataMember(Name = "thumbnail_url")]
    public Uri ThumbnailUrl { get; private set; }

    [DataMember(Name = "title")]
    public string Title { get; private set; }

    [DataMember(Name = "video_id")]
    public string Id { get; private set; }

    [DataMember(Name = "watch_count")]
    public uint WatchCount { get; private set; }

    public DateTimeOffset WatchedAt => this._WatchedAt;

    [DataMember(Name = "watch_date")]
    private long WatchedAtImpl
    {
      get => this._WatchedAt.ToLongFromDateTimeOffset();
      set => this._WatchedAt = value.ToDateTimeOffsetFromUnixTime();
    }
  }
}
