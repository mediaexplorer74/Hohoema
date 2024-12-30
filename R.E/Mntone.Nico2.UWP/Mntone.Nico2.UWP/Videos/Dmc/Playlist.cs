// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Dmc.Playlist
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Dmc
{
  [DataContract]
  public class Playlist
  {
    [DataMember(Name = "items")]
    public IList<Item> Items { get; set; }

    [DataMember(Name = "type")]
    public string Type { get; set; }

    [DataMember(Name = "ref")]
    public string Ref { get; set; }

    [DataMember(Name = "option")]
    public IList<object> Option { get; set; }
  }
}
