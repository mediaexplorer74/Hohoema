// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Users.User.UserResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Users.User
{
  [XmlRoot(ElementName = "nicovideo_user_response")]
  public class UserResponse
  {
    [XmlElement(ElementName = "user")]
    public Mntone.Nico2.Users.User.User User { get; set; }

    [XmlElement(ElementName = "vita_option")]
    public Vita_option Vita_option { get; set; }

    [XmlElement(ElementName = "additionals")]
    public string Additionals { get; set; }

    [XmlAttribute(AttributeName = "status")]
    public string Status { get; set; }
  }
}
