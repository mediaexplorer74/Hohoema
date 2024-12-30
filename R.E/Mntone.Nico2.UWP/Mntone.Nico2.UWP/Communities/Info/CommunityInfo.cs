// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Communities.Info.CommunityInfo
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Communities.Info
{
  [DataContract]
  public class CommunityInfo
  {
    private bool? _IsPublic;
    private bool? _IsOfficial;
    private bool? _IsHidden;
    private DateTime? _CreateTime;
    private uint? _UserMax;
    private uint? _UserCount;
    private uint? _Level;

    [DataMember(Name = "id")]
    public string Id { get; private set; }

    [DataMember(Name = "name")]
    public string Name { get; private set; }

    [DataMember(Name = "description")]
    public string Description { get; private set; }

    [DataMember(Name = "channel_id")]
    public string ChannelId { get; private set; }

    [DataMember(Name = "public")]
    public string __IsPublic { get; private set; }

    public bool IsPublic
    {
      get
      {
        return !this._IsPublic.HasValue ? (this._IsPublic = new bool?(this.__IsPublic.ToBooleanFrom1())).Value : this._IsPublic.Value;
      }
    }

    [DataMember(Name = "type")]
    public string Type { get; private set; }

    [DataMember(Name = "official")]
    public string __IsOfficial { get; private set; }

    public bool IsOfficial
    {
      get
      {
        return !this._IsOfficial.HasValue ? (this._IsOfficial = new bool?(this.__IsOfficial.ToBooleanFrom1())).Value : this._IsOfficial.Value;
      }
    }

    [DataMember(Name = "option_flag")]
    public string OptionFlag { get; private set; }

    [DataMember(Name = "hidden")]
    public string __IsHidden { get; private set; }

    public bool IsHidden
    {
      get
      {
        return !this._IsHidden.HasValue ? (this._IsHidden = new bool?(this.__IsHidden.ToBooleanFrom1())).Value : this._IsHidden.Value;
      }
    }

    [DataMember(Name = "user_id")]
    public string UserId { get; private set; }

    [DataMember(Name = "create_time")]
    public string __CreateTime { get; private set; }

    public DateTime CreateTime
    {
      get
      {
        return !this._CreateTime.HasValue ? (this._CreateTime = new DateTime?(DateTime.Parse(this.__CreateTime))).Value : this._CreateTime.Value;
      }
    }

    [DataMember(Name = "global_id")]
    public string GlobalId { get; private set; }

    [DataMember(Name = "user_max")]
    public string __UserMax { get; private set; }

    public uint UserMax
    {
      get
      {
        return !this._UserMax.HasValue ? (this._UserMax = new uint?(uint.Parse(this.__UserMax))).Value : this._UserMax.Value;
      }
    }

    [DataMember(Name = "user_count")]
    public string __UserCount { get; private set; }

    public uint UserCount
    {
      get
      {
        return !this._UserCount.HasValue ? (this._UserCount = new uint?(uint.Parse(this.__UserCount))).Value : this._UserCount.Value;
      }
    }

    [DataMember(Name = "level")]
    public string __Level { get; private set; }

    public uint Level
    {
      get
      {
        return !this._Level.HasValue ? (this._Level = new uint?(uint.Parse(this.__Level))).Value : this._Level.Value;
      }
    }

    [DataMember(Name = "option")]
    public string Option { get; private set; }

    [DataMember(Name = "thumbnail")]
    public string Thumbnail { get; private set; }

    [DataMember(Name = "thumbnail_small")]
    public string ThumbnailSmall { get; private set; }

    [DataMember(Name = "option_flag_details")]
    public OptionFlagDetails option_flag_details { get; private set; }

    [DataMember(Name = "top_url")]
    public string TopUrl { get; private set; }

    [DataMember(Name = "@key")]
    public string key { get; private set; }
  }
}
