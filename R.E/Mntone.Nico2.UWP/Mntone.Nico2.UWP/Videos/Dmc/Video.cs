// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Dmc.Video
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Dmc
{
  [DataContract]
  public class Video
  {
    [DataMember(Name = "id")]
    public string Id { get; set; }

    [DataMember(Name = "title")]
    public string Title { get; set; }

    [DataMember(Name = "originalTitle")]
    public string OriginalTitle { get; set; }

    [DataMember(Name = "description")]
    public string Description { get; set; }

    [DataMember(Name = "originalDescription")]
    public string OriginalDescription { get; set; }

    [DataMember(Name = "thumbnailURL")]
    public string ThumbnailURL { get; set; }

    [DataMember(Name = "postedDateTime")]
    public string PostedDateTime { get; set; }

    [DataMember(Name = "originalPostedDateTime")]
    public object OriginalPostedDateTime { get; set; }

    [DataMember(Name = "width")]
    public int? Width { get; set; }

    [DataMember(Name = "height")]
    public int? Height { get; set; }

    [DataMember(Name = "duration")]
    public int Duration { get; set; }

    [DataMember(Name = "viewCount")]
    public int ViewCount { get; set; }

    [DataMember(Name = "mylistCount")]
    public int MylistCount { get; set; }

    [DataMember(Name = "translation")]
    public bool Translation { get; set; }

    [DataMember(Name = "translator")]
    public object Translator { get; set; }

    [DataMember(Name = "movieType")]
    public string MovieType { get; set; }

    [DataMember(Name = "badges")]
    public object Badges { get; set; }

    [DataMember(Name = "introducedNicoliveDJInfo")]
    public object IntroducedNicoliveDJInfo { get; set; }

    [DataMember(Name = "dmcInfo")]
    public DmcInfo DmcInfo { get; set; }

    [DataMember(Name = "backCommentType")]
    public object BackCommentType { get; set; }

    [DataMember(Name = "isCommentExpired")]
    public bool IsCommentExpired { get; set; }

    [DataMember(Name = "isWide")]
    public string IsWide { get; set; }

    [DataMember(Name = "isOfficialAnime")]
    public object IsOfficialAnime { get; set; }

    [DataMember(Name = "isNoBanner")]
    public object IsNoBanner { get; set; }

    [DataMember(Name = "isDeleted")]
    public bool IsDeleted { get; set; }

    [DataMember(Name = "isTranslated")]
    public bool IsTranslated { get; set; }

    [DataMember(Name = "isR18")]
    public bool IsR18 { get; set; }

    [DataMember(Name = "isAdult")]
    public bool IsAdult { get; set; }

    [DataMember(Name = "isNicowari")]
    public object IsNicowari { get; set; }

    [DataMember(Name = "isPublic")]
    public bool IsPublic { get; set; }

    [DataMember(Name = "isPublishedNicoscript")]
    public object IsPublishedNicoscript { get; set; }

    [DataMember(Name = "isNoNGS")]
    public object IsNoNGS { get; set; }

    [DataMember(Name = "isCommunityMemberOnly")]
    public string IsCommunityMemberOnly { get; set; }

    [DataMember(Name = "isCommonsTreeExists")]
    public bool? IsCommonsTreeExists { get; set; }

    [DataMember(Name = "isNoIchiba")]
    public bool IsNoIchiba { get; set; }

    [DataMember(Name = "isOfficial")]
    public bool IsOfficial { get; set; }

    [DataMember(Name = "isMonetized")]
    public bool IsMonetized { get; set; }

    [DataMember(Name = "smileInfo")]
    public SmileInfo SmileInfo { get; set; }
  }
}
