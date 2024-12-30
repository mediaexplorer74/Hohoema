// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.Watch.LeoPlayerProps
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Live.Watch
{
  [DataContract]
  public class LeoPlayerProps
  {
    [DataMember(Name = "apiBaseUrl")]
    public string ApiBaseUrl { get; set; }

    [DataMember(Name = "staticResourceBaseUrl")]
    public string StaticResourceBaseUrl { get; set; }

    [DataMember(Name = "programReportApiBaseUrl")]
    public string ProgramReportApiBaseUrl { get; set; }

    [DataMember(Name = "webSocketBaseUrl")]
    public string WebSocketBaseUrl { get; set; }

    [DataMember(Name = "nicolivePublicApiBaseUrl")]
    public string NicolivePublicApiBaseUrl { get; set; }

    [DataMember(Name = "nicolivePublicRootUrl")]
    public string NicolivePublicRootUrl { get; set; }

    [DataMember(Name = "broadcastId")]
    public string BroadcastId { get; set; }

    [DataMember(Name = "providerType")]
    public string ProviderType { get; set; }

    [DataMember(Name = "relatedNicoliveProgramId")]
    public string RelatedNicoliveProgramId { get; set; }

    [DataMember(Name = "title")]
    public string Title { get; set; }

    [DataMember(Name = "openTime")]
    public int OpenTime { get; set; }

    [DataMember(Name = "beginTime")]
    public int BeginTime { get; set; }

    [DataMember(Name = "endTime")]
    public int? EndTime { get; set; }

    [DataMember(Name = "serverTime")]
    public long ServerTime { get; set; }

    [DataMember(Name = "audienceToken")]
    public string AudienceToken { get; set; }

    [DataMember(Name = "premiumMemberRegistration")]
    public PremiumMemberRegistration PremiumMemberRegistration { get; set; }

    [DataMember(Name = "userStatus")]
    public UserStatus UserStatus { get; set; }

    [DataMember(Name = "coeResourcesBaseUrl")]
    public string CoeResourcesBaseUrl { get; set; }

    [DataMember(Name = "isSocialGroupMember")]
    public bool IsSocialGroupMember { get; set; }

    [DataMember(Name = "socialGroup")]
    public SocialGroup SocialGroup { get; set; }

    [DataMember(Name = "bspComment")]
    public BspComment BspComment { get; set; }

    [DataMember(Name = "commentFiltersApiUrl")]
    public string CommentFiltersApiUrl { get; set; }

    [DataMember(Name = "csrfToken")]
    public string CsrfToken { get; set; }

    [DataMember(Name = "isCommentBanned")]
    public bool IsCommentBanned { get; set; }

    [DataMember(Name = "programState")]
    public string ProgramState { get; set; }
  }
}
