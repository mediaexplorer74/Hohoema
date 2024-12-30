// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Images.Users.Info.InfoResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Images.Users.Info
{
  public sealed class InfoResponse
  {
    internal InfoResponse(XElement userInfoXml)
    {
      this.UserId = userInfoXml.GetNamedChildNodeText("id").ToUInt();
      this.UserName = userInfoXml.GetNamedChildNodeText("nickname");
    }

    public uint UserId { get; private set; }

    public string UserName { get; private set; }
  }
}
