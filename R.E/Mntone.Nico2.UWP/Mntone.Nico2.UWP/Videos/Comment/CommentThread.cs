// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Comment.CommentThread
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Comment
{
  [XmlRoot(ElementName = "thread")]
  public class CommentThread
  {
    [XmlAttribute(AttributeName = "last_res")]
    public string CommentCount { get; set; }

    [XmlAttribute(AttributeName = "resultcode")]
    public string Resultcode { get; set; }

    [XmlAttribute(AttributeName = "revision")]
    public string Revision { get; set; }

    [XmlAttribute(AttributeName = "server_time")]
    public string Server_time { get; set; }

    [XmlAttribute(AttributeName = "thread")]
    public string _thread { get; set; }

    [XmlAttribute(AttributeName = "ticket")]
    public string Ticket { get; set; }
  }
}
