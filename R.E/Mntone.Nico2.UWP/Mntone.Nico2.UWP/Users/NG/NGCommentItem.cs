// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Users.NG.NGCommentItem
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Users.NG
{
  [XmlRoot(ElementName = "ngclient")]
  public class NGCommentItem
  {
    private NGCommentType? _NGType;
    private DateTime? _RegisterTime;

    [XmlElement(ElementName = "type")]
    public string TypeRaw { get; set; }

    [XmlElement(ElementName = "source")]
    public string Source { get; set; }

    [XmlElement(ElementName = "register_time")]
    public string Register_time { get; set; }

    public NGCommentType NGType
    {
      get
      {
        return (this._NGType ?? (this._NGType = new NGCommentType?((NGCommentType) Enum.Parse(typeof (NGCommentType), this.TypeRaw)))).Value;
      }
    }

    public DateTime RegisterTime
    {
      get
      {
        return (this._RegisterTime ?? (this._RegisterTime = new DateTime?(new DateTime((long) uint.Parse(this.Register_time))))).Value;
      }
    }
  }
}
