// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Dmc.Hiroba
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Dmc
{
  [DataContract]
  public class Hiroba
  {
    [DataMember(Name = "fms_token")]
    public object FmsToken { get; set; }

    [DataMember(Name = "server_url")]
    public string ServerUrl { get; set; }

    [DataMember(Name = "server_port")]
    public int? ServerPort { get; set; }

    [DataMember(Name = "thread_id")]
    public int? ThreadId { get; set; }

    [DataMember(Name = "thread_key")]
    public string ThreadKey { get; set; }
  }
}
