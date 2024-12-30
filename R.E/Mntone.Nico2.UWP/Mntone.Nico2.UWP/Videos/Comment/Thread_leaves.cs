// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Comment.Thread_leaves
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Comment
{
  [XmlRoot(ElementName = "thread_leaves")]
  public class Thread_leaves
  {
    public Thread_leaves()
    {
    }

    public Thread_leaves(
      string threadId,
      string threadKey,
      string userId,
      uint videoLengthMinute,
      string force184,
      bool requestScore = true)
    {
      this.Thread = threadId;
      this.ThreadKey = threadKey;
      this.Text = Thread_leaves.MakeText(videoLengthMinute);
    }

    [XmlAttribute(AttributeName = "thread")]
    public string Thread { get; set; }

    [XmlAttribute(AttributeName = "threadkey")]
    public string ThreadKey { get; set; }

    [XmlAttribute(AttributeName = "scores")]
    public string Scores { get; set; }

    [XmlAttribute(AttributeName = "nicoru")]
    public string Nicoru { get; set; }

    [XmlAttribute(AttributeName = "language")]
    public string Language { get; set; }

    [XmlText]
    public string Text { get; set; }

    public static string MakeText(uint videoLengthMinute)
    {
      uint num1 = videoLengthMinute + 1U;
      uint num2 = num1 * 100U;
      return string.Format("0-{0};100,{1}", (object) num1, (object) num2);
    }
  }
}
