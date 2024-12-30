// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.WatchAPI.TagList
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.WatchAPI
{
  public class TagList
  {
    [DataMember(Name = "id")]
    public string id { get; set; }

    [DataMember(Name = "tag")]
    public string tag { get; set; }

    [DataMember(Name = "cat")]
    public bool? cat { get; set; }

    [DataMember(Name = "dic")]
    public bool? dic { get; set; }

    [DataMember(Name = "lck")]
    public string lck { get; set; }
  }
}
