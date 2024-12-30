// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Comment.Chat_result
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Comment
{
  [XmlRoot(ElementName = "chat_result")]
  public class Chat_result
  {
    private ChatResult? _Status;
    private int? _No;

    [XmlAttribute(AttributeName = "thread")]
    public string Thread { get; set; }

    [XmlAttribute(AttributeName = "status")]
    public string __Status { get; set; }

    [XmlAttribute(AttributeName = "no")]
    public string __No { get; set; }

    public ChatResult Status
    {
      get
      {
        return (this._Status ?? (this._Status = new ChatResult?((ChatResult) Enum.Parse(typeof (ChatResult), this.__Status)))).Value;
      }
    }

    public int No => (this._No ?? (this._No = new int?(int.Parse(this.__No)))).Value;
  }
}
