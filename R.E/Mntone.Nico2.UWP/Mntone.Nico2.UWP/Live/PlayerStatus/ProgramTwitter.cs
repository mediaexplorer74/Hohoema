// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.PlayerStatus.ProgramTwitter
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Live.PlayerStatus
{
  public sealed class ProgramTwitter
  {
    internal ProgramTwitter(XElement streamXml, XElement twitterXml)
    {
      this.IsEnabled = twitterXml.GetNamedChildNodeText("live_enabled").ToBooleanFrom1();
      this.Hashtag = streamXml.GetNamedChildNodeText("twitter_tag");
      this.VipModeCount = twitterXml.GetNamedChildNodeText("vip_mode_count").ToUInt();
    }

    public bool IsEnabled { get; private set; }

    public string Hashtag { get; private set; }

    public uint VipModeCount { get; private set; }
  }
}
