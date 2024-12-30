// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Comment.CommentResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Comment
{
  [XmlRoot(ElementName = "packet")]
  public class CommentResponse
  {
    public static CommentResponse CreateFromXml(string xmlText)
    {
      return (CommentResponse) new XmlSerializer(typeof (CommentResponse)).Deserialize((TextReader) new StringReader(xmlText));
    }

    [XmlElement(ElementName = "thread")]
    public CommentThread Thread { get; set; }

    [XmlElement(ElementName = "view_counter")]
    public ViewCounter View_counter { get; set; }

    [XmlElement(ElementName = "chat")]
    public List<Mntone.Nico2.Videos.Comment.Chat> Chat { get; set; }

    public uint GetCommentCount()
    {
      return this.Thread.CommentCount == null ? 0U : uint.Parse(this.Thread.CommentCount);
    }

    public uint GetMylistCount() => uint.Parse(this.View_counter.Mylist);

    public uint GetViewCount() => uint.Parse(this.View_counter.Video);
  }
}
