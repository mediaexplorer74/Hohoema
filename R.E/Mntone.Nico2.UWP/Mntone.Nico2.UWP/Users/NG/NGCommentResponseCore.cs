// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Users.NG.NGCommentResponseCore
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Users.NG
{
  [XmlRoot(ElementName = "response_ngclient")]
  public class NGCommentResponseCore
  {
    private bool? _IsStatusOK;
    private uint? _Revision;

    [XmlAttribute(AttributeName = "status")]
    public string StatusRaw { get; set; }

    [XmlElement(ElementName = "revision")]
    public string RevisionRaw { get; set; }

    public bool IsStatusOK
    {
      get => (this._IsStatusOK ?? (this._IsStatusOK = new bool?(this.StatusRaw == "ok"))).Value;
    }

    public uint Revision
    {
      get => (this._Revision ?? (this._Revision = new uint?(uint.Parse(this.RevisionRaw)))).Value;
    }
  }
}
