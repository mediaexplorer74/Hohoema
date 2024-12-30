// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Dmc.RequestSession
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Dmc
{
  [DataContract]
  public class RequestSession
  {
    [DataMember(Name = "recipe_id")]
    public string RecipeId { get; set; }

    [DataMember(Name = "content_id")]
    public string ContentId { get; set; }

    [DataMember(Name = "content_src_id_sets")]
    public IList<ContentSrcIdSet> ContentSrcIdSets { get; set; }

    [DataMember(Name = "content_type")]
    public string ContentType { get; set; }

    [DataMember(Name = "timing_constraint")]
    public string TimingConstraint { get; set; }

    [DataMember(Name = "keep_method")]
    public KeepMethod KeepMethod { get; set; }

    [DataMember(Name = "protocol")]
    public Protocol Protocol { get; set; }

    [DataMember(Name = "content_uri")]
    public string ContentUri { get; set; }

    [DataMember(Name = "session_operation_auth")]
    public SessionOperationAuth_Request SessionOperationAuth { get; set; }

    [DataMember(Name = "content_auth")]
    public ContentAuth_Request ContentAuth { get; set; }

    [DataMember(Name = "client_info")]
    public ClientInfo ClientInfo { get; set; }

    [DataMember(Name = "priority")]
    public double Priority { get; set; }
  }
}
