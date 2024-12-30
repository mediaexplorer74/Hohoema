// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.OnAirStreams.ReservedStream
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Live.OnAirStreams
{
  [DataContract]
  public sealed class ReservedStream
  {
    private string _Id = string.Empty;
    private DateTimeOffset _OpenedAt = DateTimeOffset.MinValue;

    internal ReservedStream()
    {
    }

    [DataMember(Name = "gauge_level")]
    public ushort GaugeLevel { get; private set; }

    [DataMember(Name = "hide_zapping")]
    public bool IsHidden { get; private set; }

    public string Id => this._Id;

    [DataMember(Name = "id")]
    private string IdImpl
    {
      get => this._Id == null || this._Id.Length <= 2 ? (string) null : this._Id.Substring(2);
      set => this._Id = "lv" + value;
    }

    [DataMember(Name = "is_nsen")]
    public bool IsNsen { get; private set; }

    [DataMember(Name = "is_product")]
    public bool IsProduct { get; private set; }

    [DataMember(Name = "is_zapping_mode_enabled")]
    public bool IsZappingModeEnabled { get; private set; }

    public DateTimeOffset OpenedAt => this._OpenedAt;

    [DataMember(Name = "open_time")]
    private long OpenedAtImpl
    {
      get => this._OpenedAt.ToLongFromDateTimeOffset();
      set => this._OpenedAt = value.ToDateTimeOffsetFromUnixTime();
    }

    [DataMember(Name = "thumbnail_small_url")]
    public Uri SmallThumbnailUrl { get; private set; }

    [DataMember(Name = "title")]
    public string Title { get; private set; }
  }
}
