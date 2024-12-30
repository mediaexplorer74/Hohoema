// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Comment.Chat
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Comment
{
  [XmlRoot(ElementName = "chat")]
  public class Chat
  {
    [XmlAttribute(AttributeName = "anonymity")]
    public string Anonymity { get; set; }

    [XmlAttribute(AttributeName = "date")]
    public string Date { get; set; }

    [XmlAttribute(AttributeName = "mail")]
    public string Mail { get; set; }

    [XmlAttribute(AttributeName = "no")]
    public string No { get; set; }

    [XmlAttribute(AttributeName = "thread")]
    public string Thread { get; set; }

    [XmlAttribute(AttributeName = "user_id")]
    public string User_id { get; set; }

    [XmlAttribute(AttributeName = "vpos")]
    public string Vpos { get; set; }

    [XmlText]
    public string Text { get; set; }

    public string GetDecodedText()
    {
      return Encoding.UTF8.GetString(this.Text.Select<char, byte>((Func<char, byte>) (x => Convert.ToByte(x))).ToArray<byte>());
    }

    public bool GetAnonymity() => this.Anonymity == "1";

    public uint GetCommentNo() => uint.Parse(this.No);

    public uint GetVpos() => uint.Parse(this.Vpos);

    public IEnumerable<CommandType> ParseCommandTypes()
    {
      return CommandTypesHelper.ParseCommentCommandTypes(this.Mail);
    }
  }
}
