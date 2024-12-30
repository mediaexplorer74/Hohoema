// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.NicoRepo.Video
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.NicoRepo
{
  [DataContract]
  public class Video
  {
    [DataMember(Name = "id")]
    public string Id { get; set; }

    [DataMember(Name = "resolution")]
    public Resolution Resolution { get; set; }

    [DataMember(Name = "status")]
    public string Status { get; set; }

    [DataMember(Name = "thumbnailUrl")]
    public ThumbnailUrl ThumbnailUrl { get; set; }

    [DataMember(Name = "title")]
    public string Title { get; set; }

    [DataMember(Name = "videoWatchPageId")]
    public string VideoWatchPageId { get; set; }
  }
}
