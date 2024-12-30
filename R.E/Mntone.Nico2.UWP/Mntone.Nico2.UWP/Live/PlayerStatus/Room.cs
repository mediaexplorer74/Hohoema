// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.PlayerStatus.Room
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Live.PlayerStatus
{
  public sealed class Room
  {
    internal Room(XElement streamXml, XElement userXml)
    {
      this.Name = userXml.GetNamedChildNodeText("room_label");
      this.SeatId = userXml.GetNamedChildNodeText("room_seetno").ToUInt();
      this.SeatToken = userXml.GetNamedChildNodeText("seat_token");
    }

    public string Name { get; private set; }

    public uint SeatId { get; private set; }

    public string SeatToken { get; private set; }
  }
}
