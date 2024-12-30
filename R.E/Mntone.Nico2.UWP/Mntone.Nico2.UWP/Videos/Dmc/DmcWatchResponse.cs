// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Dmc.DmcWatchResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Dmc
{
  [DataContract]
  public class DmcWatchResponse
  {
    [DataMember(Name = "video")]
    public Video Video { get; set; }

    [DataMember(Name = "thread")]
    public Thread Thread { get; set; }

    [DataMember(Name = "tags")]
    public IList<Tag> Tags { get; set; }

    [DataMember(Name = "playlist")]
    public Playlist Playlist { get; set; }

    [DataMember(Name = "owner")]
    public Owner Owner { get; set; }

    [DataMember(Name = "viewer")]
    public Viewer Viewer { get; set; }

    [DataMember(Name = "community")]
    public object Community { get; set; }

    [DataMember(Name = "channel")]
    public Channel Channel { get; set; }

    [DataMember(Name = "ad")]
    public Ad Ad { get; set; }

    [DataMember(Name = "lead")]
    public Lead Lead { get; set; }

    [DataMember(Name = "maintenance")]
    public object Maintenance { get; set; }

    [DataMember(Name = "context")]
    public Context Context { get; set; }

    [DataMember(Name = "liveTopics")]
    public LiveTopics LiveTopics { get; set; }
  }
}
