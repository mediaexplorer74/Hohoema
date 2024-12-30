// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Comment.Thread
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Comment
{
  [DataContract]
  public class Thread
  {
    [DataMember(Name = "thread")]
    public string ThreadId { get; set; }

    [DataMember(Name = "version")]
    public string Version { get; set; } = "20090904";

    [DataMember(Name = "language")]
    public int Language { get; set; }

    [DataMember(Name = "user_id")]
    public int UserId { get; set; }

    [DataMember(Name = "with_global")]
    public int WithGlobal { get; set; } = 1;

    [DataMember(Name = "scores")]
    public int Scores { get; set; } = 1;

    [DataMember(Name = "nicoru")]
    public int Nicoru { get; set; }

    [DataMember(Name = "userkey")]
    public string Userkey { get; set; }

    [DataMember(Name = "force_184")]
    public string Force184 { get; set; }

    [DataMember(Name = "threadkey")]
    public string Threadkey { get; set; }
  }
}
