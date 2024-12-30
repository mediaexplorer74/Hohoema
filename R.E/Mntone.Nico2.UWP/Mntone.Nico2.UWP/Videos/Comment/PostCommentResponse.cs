// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Comment.PostCommentResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Comment
{
  [XmlRoot(ElementName = "packet")]
  public class PostCommentResponse
  {
    [XmlElement(ElementName = "chat_result")]
    public Chat_result Chat_result { get; set; }
  }
}
