// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.PlayerStatus.Farre
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Live.PlayerStatus
{
  public sealed class Farre
  {
    internal Farre(XElement farreXml)
    {
      this.IsEnabled = farreXml.GetNamedChildNodeText("farremode").ToBooleanFrom1();
      this.IsAvatarmakerEnabled = farreXml.GetNamedChildNodeText("avatarmaker_enabled").ToBooleanFrom1();
      this.IsInvokeAvatarmakerEnabled = farreXml.GetNamedChildNodeText("is_invoke_avatarmaker").ToBooleanFrom1();
      this.IsMultiAngleEnabled = farreXml.GetNamedChildNodeText("multi_angle").ToBooleanFrom1();
      this.MultiAngleCount = farreXml.GetNamedChildNodeText("multi_angle_num").ToUShort();
    }

    public bool IsEnabled { get; private set; }

    public bool IsAvatarmakerEnabled { get; private set; }

    public bool IsInvokeAvatarmakerEnabled { get; private set; }

    public bool IsMultiAngleEnabled { get; private set; }

    public ushort MultiAngleCount { get; private set; }
  }
}
