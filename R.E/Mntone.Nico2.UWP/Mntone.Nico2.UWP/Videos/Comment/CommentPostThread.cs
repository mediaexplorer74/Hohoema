// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Comment.CommentPostThread
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Comment
{
  [XmlRoot(ElementName = "thread")]
  public class CommentPostThread
  {
    public CommentPostThread()
    {
    }

    public CommentPostThread(
      string threadId,
      string threadKey,
      string userId,
      string force184,
      bool requestOwnerComment = false)
    {
      this._thread = threadId;
      this.ThreadKey = threadKey;
    }

    [XmlAttribute(AttributeName = "thread")]
    public string _thread { get; set; }

    [XmlAttribute(AttributeName = "threadkey")]
    public string ThreadKey { get; set; }

    [XmlAttribute(AttributeName = "version")]
    public string Version { get; set; }

    [XmlAttribute(AttributeName = "scores")]
    public string Scores { get; set; }

    [XmlAttribute(AttributeName = "nicoru")]
    public string Nicoru { get; set; }

    [XmlAttribute(AttributeName = "language")]
    public string Language { get; set; }

    [XmlAttribute(AttributeName = "with_global")]
    public string With_global { get; set; }

    [XmlAttribute(AttributeName = "res_from")]
    public string Res_from { get; set; }

    [XmlAttribute(AttributeName = "fork")]
    public string Fork { get; set; }
  }
}
