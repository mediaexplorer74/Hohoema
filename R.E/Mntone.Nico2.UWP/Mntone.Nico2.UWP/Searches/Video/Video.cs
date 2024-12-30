// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Searches.Video.Video
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Searches.Video
{
  [DataContract]
  public class Video
  {
    private bool? _IsDeleted;
    private string _Title;
    private TimeSpan? _Length;
    private Uri _ThumbnailUrl;
    private uint? _ViewCount;
    private uint? _MylistCount;
    private bool? _IsCommunity;
    private uint? _Width;
    private uint? _Height;
    private bool? _IsVitaPlayable;

    [DataMember(Name = "id")]
    public string __id { get; private set; }

    public string Id => this.__id;

    [DataMember(Name = "user_id")]
    public string UserId { get; set; }

    [DataMember(Name = "deleted")]
    public string __deleted { get; private set; }

    public bool IsDeleted
    {
      get
      {
        return !this._IsDeleted.HasValue ? (this._IsDeleted = new bool?(int.Parse(this.__deleted) > 0)).Value : this._IsDeleted.Value;
      }
    }

    [DataMember(Name = "title")]
    public string __title { get; private set; }

    public string Title => this._Title ?? (this._Title = this.__title);

    [DataMember(Name = "description")]
    public string Description { get; set; }

    [DataMember(Name = "length_in_seconds")]
    public string __length_in_seconds { get; private set; }

    public TimeSpan Length
    {
      get
      {
        return !this._Length.HasValue ? (this._Length = new TimeSpan?(TimeSpan.FromSeconds((double) int.Parse(this.__length_in_seconds)))).Value : this._Length.Value;
      }
    }

    [DataMember(Name = "thumbnail_url")]
    public string __thumbnail_url { get; private set; }

    public Uri ThumbnailUrl
    {
      get
      {
        Uri thumbnailUrl = this._ThumbnailUrl;
        return (object) thumbnailUrl != null ? thumbnailUrl : (this._ThumbnailUrl = new Uri(this.__thumbnail_url));
      }
    }

    [DataMember(Name = "upload_time")]
    public DateTime __upload_time { get; private set; }

    public DateTime UploadTime => this.__upload_time;

    [DataMember(Name = "first_retrieve")]
    public DateTime __first_retrieve { get; private set; }

    public DateTime FirstRetrieve => this.__first_retrieve;

    [DataMember(Name = "default_thread")]
    public string DefaultThread { get; set; }

    [DataMember(Name = "view_counter")]
    public string __view_counter { get; private set; }

    public uint ViewCount
    {
      get
      {
        return !this._ViewCount.HasValue ? (this._ViewCount = new uint?(uint.Parse(this.__view_counter))).Value : this._ViewCount.Value;
      }
    }

    [DataMember(Name = "mylist_counter")]
    public string __mylist_counter { get; private set; }

    public uint MylistCount
    {
      get
      {
        return !this._MylistCount.HasValue ? (this._MylistCount = new uint?(uint.Parse(this.__mylist_counter))).Value : this._MylistCount.Value;
      }
    }

    [DataMember(Name = "option_flag_community")]
    public string __option_flag_community { get; private set; }

    public bool IsCommunity
    {
      get
      {
        return !this._IsCommunity.HasValue ? (this._IsCommunity = new bool?(this.__option_flag_community.ToBooleanFrom1())).Value : this._IsCommunity.Value;
      }
    }

    [DataMember(Name = "width")]
    public string __width { get; private set; }

    public uint Width
    {
      get
      {
        return !this._Width.HasValue ? (this._Width = new uint?(uint.Parse(this.__width))).Value : this._Width.Value;
      }
    }

    [DataMember(Name = "height")]
    public string __height { get; private set; }

    public uint Height
    {
      get
      {
        return !this._Height.HasValue ? (this._Height = new uint?(uint.Parse(this.__height))).Value : this._Height.Value;
      }
    }

    [DataMember(Name = "vita_playable")]
    public string __vita_playable { get; private set; }

    public bool IsVitaPlayable
    {
      get
      {
        return !this._IsVitaPlayable.HasValue ? (this._IsVitaPlayable = new bool?(this.__vita_playable.ToBooleanFrom1())).Value : this._IsVitaPlayable.Value;
      }
    }

    [DataMember(Name = "ppv_video")]
    public string __ppv_video { get; private set; }

    [DataMember(Name = "provider_type")]
    public string __provider_type { get; private set; }

    public string ProviderType => this.__provider_type;

    [DataMember(Name = "options")]
    public Options Options { get; private set; }

    [DataMember(Name = "community_id")]
    public string CommunityId { get; set; }
  }
}
