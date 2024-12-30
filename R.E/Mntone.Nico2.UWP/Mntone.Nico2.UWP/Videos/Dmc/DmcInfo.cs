// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Dmc.DmcInfo
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Dmc
{
  [DataContract]
  public class DmcInfo
  {
    [DataMember(Name = "time")]
    public int Time { get; set; }

    [DataMember(Name = "time_ms")]
    public long TimeMs { get; set; }

    [DataMember(Name = "video")]
    public DmcVideo Video { get; set; }

    [DataMember(Name = "thread")]
    public DmcThread Thread { get; set; }

    [DataMember(Name = "user")]
    public User User { get; set; }

    [DataMember(Name = "hiroba")]
    public Hiroba Hiroba { get; set; }

    [DataMember(Name = "error")]
    public object Error { get; set; }

    [DataMember(Name = "session_api")]
    public SessionApi SessionApi { get; set; }

    [DataMember(Name = "storyboard_session_api")]
    public object StoryboardSessionApi { get; set; }

    [DataMember(Name = "quality")]
    public Quality Quality { get; set; }
  }
}
