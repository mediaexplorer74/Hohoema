// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.PlayerStatus.Marquee
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Live.PlayerStatus
{
  public sealed class Marquee
  {
    internal Marquee(XElement marqueeXml)
    {
      this.Category = marqueeXml.GetNamedChildNodeText("category");
      this.GameKey = marqueeXml.GetNamedChildNodeText("game_key");
      this.GameTime = marqueeXml.GetNamedChildNodeText("game_time").ToDateTimeOffsetFromUnixTime();
      this.IsNotInterruptionForced = marqueeXml.GetNamedChildNodeText("force_nicowari_off").ToBooleanFrom1();
    }

    public string Category { get; private set; }

    public string GameKey { get; private set; }

    public DateTimeOffset GameTime { get; private set; }

    public bool IsNotInterruptionForced { get; private set; }
  }
}
