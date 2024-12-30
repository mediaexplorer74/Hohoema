// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.WatchAPI.VideoDetail
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.WatchAPI
{
  public class VideoDetail
  {
    [DataMember(Name = "v")]
    public string v { get; set; }

    [DataMember(Name = "id")]
    public string id { get; set; }

    [DataMember(Name = "title")]
    public string title { get; set; }

    [DataMember(Name = "description")]
    public string description { get; set; }

    [DataMember(Name = "is_translated")]
    public bool is_translated { get; set; }

    [DataMember(Name = "title_original")]
    public string title_original { get; set; }

    [DataMember(Name = "description_original")]
    public string description_original { get; set; }

    [DataMember(Name = "thumbnail")]
    public string thumbnail { get; set; }

    [DataMember(Name = "postedAt")]
    public string postedAt { get; set; }

    [DataMember(Name = "length")]
    public int? length { get; set; }

    [DataMember(Name = "viewCount")]
    public int? viewCount { get; set; }

    [DataMember(Name = "mylistCount")]
    public int? mylistCount { get; set; }

    [DataMember(Name = "commentCount")]
    public int? commentCount { get; set; }

    [DataMember(Name = "mainCommunityId")]
    public int? mainCommunityId { get; set; }

    [DataMember(Name = "communityId")]
    public int? communityId { get; set; }

    [DataMember(Name = "channelId")]
    public int? channelId { get; set; }

    [DataMember(Name = "isDeleted")]
    public bool isDeleted { get; set; }

    [DataMember(Name = "isMymemory")]
    public bool isMymemory { get; set; }

    [DataMember(Name = "isMonetized")]
    public bool isMonetized { get; set; }

    [DataMember(Name = "isR18")]
    public bool isR18 { get; set; }

    [DataMember(Name = "is_adult")]
    public object is_adult { get; set; }

    [DataMember(Name = "language")]
    public string language { get; set; }

    [DataMember(Name = "area")]
    public string area { get; set; }

    [DataMember(Name = "can_translate")]
    public bool can_translate { get; set; }

    [DataMember(Name = "video_translation_info")]
    public bool video_translation_info { get; set; }

    [DataMember(Name = "category")]
    public string category { get; set; }

    [DataMember(Name = "thread_id")]
    public string thread_id { get; set; }

    [DataMember(Name = "main_genre")]
    public string main_genre { get; set; }

    [DataMember(Name = "has_owner_thread")]
    public object has_owner_thread { get; set; }

    [DataMember(Name = "is_video_owner")]
    public object is_video_owner { get; set; }

    [DataMember(Name = "is_uneditable_tag")]
    public bool is_uneditable_tag { get; set; }

    [DataMember(Name = "commons_tree_exists")]
    public object commons_tree_exists { get; set; }

    [DataMember(Name = "yesterday_rank")]
    public string yesterday_rank { get; set; }

    [DataMember(Name = "highest_rank")]
    public string highest_rank { get; set; }

    [DataMember(Name = "for_bgm")]
    public bool for_bgm { get; set; }

    [DataMember(Name = "is_nicowari")]
    public object is_nicowari { get; set; }

    [DataMember(Name = "is_public")]
    public bool is_public { get; set; }

    [DataMember(Name = "is_official")]
    public bool is_official { get; set; }

    [DataMember(Name = "no_ichiba")]
    public bool no_ichiba { get; set; }

    [DataMember(Name = "community_name")]
    public string community_name { get; set; }

    [DataMember(Name = "dicArticleURL")]
    public string dicArticleURL { get; set; }

    [DataMember(Name = "is_playable")]
    public bool is_playable { get; set; }

    [DataMember(Name = "tagList")]
    public List<TagList> tagList { get; set; }

    [DataMember(Name = "is_thread_owner")]
    public bool is_thread_owner { get; set; }

    [DataMember(Name = "width")]
    public int width { get; set; }

    [DataMember(Name = "height")]
    public int height { get; set; }

    [DataMember(Name = "ownerChannelInfo")]
    public OwnerChannelInfo ownerChannelInfo { get; set; }
  }
}
