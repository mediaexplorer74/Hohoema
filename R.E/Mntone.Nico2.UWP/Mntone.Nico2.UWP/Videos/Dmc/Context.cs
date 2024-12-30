// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Dmc.Context
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Dmc
{
  [DataContract]
  public class Context
  {
    [DataMember(Name = "playFrom")]
    public object PlayFrom { get; set; }

    [DataMember(Name = "initialPlaybackPosition")]
    public object InitialPlaybackPosition { get; set; }

    [DataMember(Name = "initialPlaybackType")]
    public object InitialPlaybackType { get; set; }

    [DataMember(Name = "playLength")]
    public object PlayLength { get; set; }

    [DataMember(Name = "returnId")]
    public object ReturnId { get; set; }

    [DataMember(Name = "returnTo")]
    public object ReturnTo { get; set; }

    [DataMember(Name = "returnMsg")]
    public object ReturnMsg { get; set; }

    [DataMember(Name = "watchId")]
    public string WatchId { get; set; }

    [DataMember(Name = "isNoMovie")]
    public object IsNoMovie { get; set; }

    [DataMember(Name = "isNoRelatedVideo")]
    public object IsNoRelatedVideo { get; set; }

    [DataMember(Name = "isDownloadCompleteWait")]
    public object IsDownloadCompleteWait { get; set; }

    [DataMember(Name = "isNoNicotic")]
    public object IsNoNicotic { get; set; }

    [DataMember(Name = "isNeedPayment")]
    public bool IsNeedPayment { get; set; }

    [DataMember(Name = "isAdultRatingNG")]
    public bool IsAdultRatingNG { get; set; }

    [DataMember(Name = "isPlayable")]
    public object IsPlayable { get; set; }

    [DataMember(Name = "isTranslatable")]
    public bool IsTranslatable { get; set; }

    [DataMember(Name = "isTagUneditable")]
    public bool IsTagUneditable { get; set; }

    [DataMember(Name = "isVideoOwner")]
    public bool IsVideoOwner { get; set; }

    [DataMember(Name = "isThreadOwner")]
    public bool IsThreadOwner { get; set; }

    [DataMember(Name = "isOwnerThreadEditable")]
    public object IsOwnerThreadEditable { get; set; }

    [DataMember(Name = "useChecklistCache")]
    public object UseChecklistCache { get; set; }

    [DataMember(Name = "isDisabledMarquee")]
    public object IsDisabledMarquee { get; set; }

    [DataMember(Name = "isDictionaryDisplayable")]
    public bool IsDictionaryDisplayable { get; set; }

    [DataMember(Name = "isDefaultCommentInvisible")]
    public bool IsDefaultCommentInvisible { get; set; }

    [DataMember(Name = "accessFrom")]
    public object AccessFrom { get; set; }

    [DataMember(Name = "csrfToken")]
    public string CsrfToken { get; set; }

    [DataMember(Name = "translationVersionJsonUpdateTime")]
    public int TranslationVersionJsonUpdateTime { get; set; }

    [DataMember(Name = "userkey")]
    public string Userkey { get; set; }

    [DataMember(Name = "watchAuthKey")]
    public string WatchAuthKey { get; set; }

    [DataMember(Name = "watchTrackId")]
    public string WatchTrackId { get; set; }

    [DataMember(Name = "watchPageServerTime")]
    public long WatchPageServerTime { get; set; }

    [DataMember(Name = "isAuthenticationRequired")]
    public bool IsAuthenticationRequired { get; set; }

    [DataMember(Name = "isPeakTime")]
    public object IsPeakTime { get; set; }

    [DataMember(Name = "ngRevision")]
    public int? NgRevision { get; set; }

    [DataMember(Name = "categoryName")]
    public string CategoryName { get; set; }

    [DataMember(Name = "categoryKey")]
    public string CategoryKey { get; set; }

    [DataMember(Name = "categoryGroupName")]
    public string CategoryGroupName { get; set; }

    [DataMember(Name = "categoryGroupKey")]
    public string CategoryGroupKey { get; set; }

    [DataMember(Name = "yesterdayRank")]
    public int? YesterdayRank { get; set; }

    [DataMember(Name = "highestRank")]
    public int? HighestRank { get; set; }

    [DataMember(Name = "isMyMemory")]
    public bool IsMyMemory { get; set; }

    [DataMember(Name = "ownerNGList")]
    public IList<Mntone.Nico2.Videos.Dmc.OwnerNGList> OwnerNGList { get; set; }
  }
}
