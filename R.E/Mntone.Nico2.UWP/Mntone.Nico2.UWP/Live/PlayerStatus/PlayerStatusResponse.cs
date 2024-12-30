// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.PlayerStatus.PlayerStatusResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Live.PlayerStatus
{
  public sealed class PlayerStatusResponse
  {
    internal PlayerStatusResponse(XElement playerStatusXml)
    {
      XElement namedChildNode1 = playerStatusXml.GetNamedChildNode("stream");
      XElement namedChildNode2 = playerStatusXml.GetNamedChildNode("user");
      XElement namedChildNode3 = playerStatusXml.GetNamedChildNode("player");
      XElement namedChildNode4 = playerStatusXml.GetNamedChildNode("farre");
      this.LoadedAt = playerStatusXml.GetNamedAttributeText("time").ToDateTimeOffsetFromUnixTime();
      this.Program = new Program(namedChildNode1, namedChildNode3, playerStatusXml.GetNamedChildNode("nsen"), new ProgramTwitter(namedChildNode1, playerStatusXml.GetNamedChildNode("twitter")));
      this.Room = new Room(namedChildNode1, namedChildNode2);
      this.Stream = new Stream(namedChildNode1, playerStatusXml.GetNamedChildNode("rtmp"), playerStatusXml.GetNamedChildNode("tickets"), namedChildNode3);
      this.Comment = new Comment(namedChildNode1, new CommentServer(playerStatusXml.GetNamedChildNode("ms"), playerStatusXml.GetNamedChildNode("tid_list")));
      this.Telop = new Telop(namedChildNode1.GetNamedChildNode("telop"));
      this.NetDuetto = new NetDuetto(namedChildNode1);
      this.Farre = namedChildNode4 != null ? new Farre(namedChildNode4) : (Farre) null;
      this.Marquee = new Marquee(playerStatusXml.GetNamedChildNode("marquee"));
      this.User = new User(namedChildNode1, namedChildNode2);
    }

    public DateTimeOffset LoadedAt { get; private set; }

    public Program Program { get; private set; }

    public Room Room { get; private set; }

    public Stream Stream { get; private set; }

    public Comment Comment { get; private set; }

    public Telop Telop { get; private set; }

    public NetDuetto NetDuetto { get; private set; }

    public Farre Farre { get; private set; }

    public Marquee Marquee { get; private set; }

    public User User { get; private set; }
  }
}
