// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Dmc.DmcThread
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Dmc
{
  [DataContract]
  public class DmcThread
  {
    [DataMember(Name = "server_url")]
    public string ServerUrl { get; set; }

    [DataMember(Name = "sub_server_url")]
    public string SubServerUrl { get; set; }

    [DataMember(Name = "thread_id")]
    public int ThreadId { get; set; }

    [DataMember(Name = "nicos_thread_id")]
    public object NicosThreadId { get; set; }

    [DataMember(Name = "optional_thread_id")]
    public object OptionalThreadId { get; set; }

    [DataMember(Name = "thread_key_required")]
    public bool ThreadKeyRequired { get; set; }

    [DataMember(Name = "channel_ng_words")]
    public IList<object> ChannelNgWords { get; set; }

    [DataMember(Name = "owner_ng_words")]
    public IList<object> OwnerNgWords { get; set; }

    [DataMember(Name = "maintenances_ng")]
    public bool MaintenancesNg { get; set; }

    [DataMember(Name = "postkey_available")]
    public bool PostkeyAvailable { get; set; }

    [DataMember(Name = "ng_revision")]
    public int? NgRevision { get; set; }
  }
}
