// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Comment.CommentPostData
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.IO;
using System.Text;
using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Comment
{
  [XmlRoot(ElementName = "packet")]
  public class CommentPostData
  {
    public CommentPostData()
    {
    }

    public CommentPostData(
      string threadId,
      string threadKey,
      string userId,
      uint videoLength,
      string force184,
      bool requestOwnerComment = false,
      bool requestScore = true)
    {
      this.FirstThread = new CommentPostThread()
      {
        _thread = threadId,
        ThreadKey = threadKey,
        Version = "20061206",
        Scores = "1",
        Nicoru = "0"
      };
      this.ThreadLeaves = new Thread_leaves()
      {
        Thread = threadId,
        Scores = "1",
        Nicoru = "0",
        Text = Thread_leaves.MakeText(videoLength)
      };
      this.SecondThread = new CommentPostThread()
      {
        _thread = threadId,
        ThreadKey = threadKey,
        Version = "20061206",
        Res_from = "-1000",
        Fork = "1"
      };
    }

    [XmlElement(ElementName = "thread")]
    public CommentPostThread FirstThread { get; set; }

    [XmlElement(ElementName = "thread_leaves")]
    public Thread_leaves ThreadLeaves { get; set; }

    [XmlElement(ElementName = "thread")]
    public CommentPostThread SecondThread { get; set; }

    public string ToXml()
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (StreamWriter streamWriter = new StreamWriter((Stream) memoryStream, Encoding.UTF8))
        {
          XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
          namespaces.Add(string.Empty, string.Empty);
          new XmlSerializer(typeof (CommentPostData)).Serialize((TextWriter) streamWriter, (object) this, namespaces);
          streamWriter.Flush();
          return Encoding.UTF8.GetString(memoryStream.ToArray());
        }
      }
    }
  }
}
