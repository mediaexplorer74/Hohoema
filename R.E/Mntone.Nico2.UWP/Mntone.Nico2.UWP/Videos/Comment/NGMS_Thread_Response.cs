// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Comment.NGMS_Thread_Response
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Comment
{
  [DataContract]
  public class NGMS_Thread_Response
  {
    [DataMember(Name = "resultcode")]
    public int Resultcode { get; set; }

    [DataMember(Name = "thread")]
    public string Thread { get; set; }

    [DataMember(Name = "server_time")]
    public int ServerTime { get; set; }

    [DataMember(Name = "last_res")]
    public int LastRes { get; set; }

    [DataMember(Name = "ticket")]
    public string Ticket { get; set; }

    [DataMember(Name = "revision")]
    public int Revision { get; set; }
  }
}
