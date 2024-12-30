// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.NicoRepo.NicoRepoMeta
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.NicoRepo
{
  [DataContract]
  public class NicoRepoMeta
  {
    [DataMember(Name = "status")]
    public int Status { get; set; }

    [DataMember(Name = "maxId")]
    public string MaxId { get; set; }

    [DataMember(Name = "minId")]
    public string MinId { get; set; }

    [DataMember(Name = "impressionId")]
    public string ImpressionId { get; set; }

    [DataMember(Name = "clientAppGroup")]
    public string ClientAppGroup { get; set; }

    [DataMember(Name = "_limit")]
    public int Limit { get; set; }
  }
}
