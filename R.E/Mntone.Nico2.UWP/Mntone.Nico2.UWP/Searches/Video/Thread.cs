// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Searches.Video.Thread
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Searches.Video
{
  [DataContract]
  public class Thread
  {
    [DataMember(Name = "id")]
    public string Id { get; private set; }

    [DataMember(Name = "num_res")]
    public string num_res { get; private set; }

    public uint GetCommentCount() => uint.Parse(this.num_res);

    [DataMember(Name = "summary")]
    public string summary { get; private set; }

    public string GetDecodedSummary() => this.summary.DecodeUTF8();

    [DataMember(Name = "community_id")]
    public string CommunityId { get; set; }

    [DataMember(Name = "group_type")]
    public string GroupType { get; set; }
  }
}
