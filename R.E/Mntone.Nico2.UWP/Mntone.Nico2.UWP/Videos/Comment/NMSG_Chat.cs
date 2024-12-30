// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Comment.NMSG_Chat
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Comment
{
  [DataContract]
  public class NMSG_Chat
  {
    [DataMember(Name = "thread")]
    public string Thread { get; set; }

    [DataMember(Name = "no")]
    public int No { get; set; }

    [DataMember(Name = "vpos")]
    public int Vpos { get; set; }

    [DataMember(Name = "leaf")]
    public int Leaf { get; set; }

    [DataMember(Name = "date")]
    public int Date { get; set; }

    [DataMember(Name = "date_usec")]
    public int DateUsec { get; set; }

    [DataMember(Name = "premium")]
    public int Premium { get; set; }

    [DataMember(Name = "anonymity")]
    public int Anonymity { get; set; }

    [DataMember(Name = "user_id")]
    public string UserId { get; set; }

    [DataMember(Name = "mail")]
    public string Mail { get; set; }

    [DataMember(Name = "content")]
    public string Content { get; set; }

    [DataMember(Name = "score")]
    public int? Score { get; set; }

    [DataMember(Name = "deleted")]
    public int? Deleted { get; set; }

    public IEnumerable<CommandType> ParseCommandTypes()
    {
      return CommandTypesHelper.ParseCommentCommandTypes(this.Mail);
    }
  }
}
