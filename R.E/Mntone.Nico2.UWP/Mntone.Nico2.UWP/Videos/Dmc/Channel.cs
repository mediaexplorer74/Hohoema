// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Dmc.Channel
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Dmc
{
  [DataContract]
  public class Channel
  {
    [DataMember(Name = "id")]
    public string Id { get; set; }

    [DataMember(Name = "name")]
    public string Name { get; set; }

    [DataMember(Name = "iconURL")]
    public string IconURL { get; set; }

    [DataMember(Name = "favoriteToken")]
    public string FavoriteToken { get; set; }

    [DataMember(Name = "favoriteTokenTime")]
    public int? FavoriteTokenTime { get; set; }

    [DataMember(Name = "isFavorited")]
    public bool IsFavorited { get; set; }

    [DataMember(Name = "ngList")]
    public IList<Mntone.Nico2.Videos.Dmc.NgList> NgList { get; set; }

    [DataMember(Name = "threadType")]
    public string ThreadType { get; set; }

    [DataMember(Name = "globalId")]
    public string GlobalId { get; set; }
  }
}
