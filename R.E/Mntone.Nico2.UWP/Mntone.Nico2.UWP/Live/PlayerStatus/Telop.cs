// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.PlayerStatus.Telop
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Live.PlayerStatus
{
  public sealed class Telop
  {
    internal Telop(XElement telopNode)
    {
      this.IsEnabled = telopNode.GetNamedChildNodeText("enable").ToBooleanFrom1();
      string namedChildNodeText = telopNode.GetNamedChildNodeText("mail");
      if (!string.IsNullOrEmpty(namedChildNodeText))
      {
        this.Mail = namedChildNodeText;
        this.Value = telopNode.GetNamedChildNodeText("caption");
      }
      else
      {
        this.Mail = string.Empty;
        this.Value = string.Empty;
      }
    }

    public bool IsEnabled { get; private set; }

    public string Mail { get; private set; }

    public string Value { get; private set; }
  }
}
