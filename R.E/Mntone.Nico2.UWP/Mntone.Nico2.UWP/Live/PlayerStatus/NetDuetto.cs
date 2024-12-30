// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.PlayerStatus.NetDuetto
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Live.PlayerStatus
{
  public sealed class NetDuetto
  {
    internal NetDuetto(XElement streamXml)
    {
      this.IsEnabled = streamXml.GetNamedChildNodeText("allow_netduetto").ToBooleanFrom1();
      this.Token = streamXml.GetNamedChildNodeText("nd_token");
    }

    public bool IsEnabled { get; private set; }

    public string Token { get; private set; }
  }
}
