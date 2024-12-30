// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Users.NG.NGCommentResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Collections.Generic;
using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Users.NG
{
  [XmlRoot(ElementName = "response_ngclient")]
  public class NGCommentResponse : NGCommentResponseCore
  {
    private uint? _Count;

    [XmlElement(ElementName = "count")]
    public string CountRaw { get; set; }

    [XmlElement(ElementName = "ngclient")]
    public List<NGCommentItem> NGCommentItems { get; set; }

    public uint Count
    {
      get => (this._Count ?? (this._Count = new uint?(uint.Parse(this.CountRaw)))).Value;
    }
  }
}
