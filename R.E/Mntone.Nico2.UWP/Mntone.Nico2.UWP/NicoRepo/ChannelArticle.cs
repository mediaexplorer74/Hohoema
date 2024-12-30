// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.NicoRepo.ChannelArticle
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.NicoRepo
{
  [DataContract]
  public class ChannelArticle
  {
    [DataMember(Name = "id")]
    public int Id { get; set; }

    [DataMember(Name = "thumbnailUrl")]
    public string ThumbnailUrl { get; set; }

    [DataMember(Name = "title")]
    public string Title { get; set; }

    [DataMember(Name = "watchUrls")]
    public WatchUrls WatchUrls { get; set; }
  }
}
