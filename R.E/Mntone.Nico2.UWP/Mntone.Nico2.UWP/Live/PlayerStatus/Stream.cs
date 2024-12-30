// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.PlayerStatus.Stream
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Live.PlayerStatus
{
  public sealed class Stream
  {
    internal Stream(XElement streamXml, XElement rtmpXml, XElement ticketsXml, XElement playerXml)
    {
      this.IsFlashMediaServer = rtmpXml.GetNamedAttributeText("is_fms").ToBooleanFrom1();
      string namedAttributeText = rtmpXml.GetNamedAttributeText("rtmpt_port");
      this.RtmptPort = !string.IsNullOrEmpty(namedAttributeText) ? namedAttributeText.ToUShort() : (ushort) 0;
      this.RtmpUrl = rtmpXml.GetNamedChildNodeText("url").ToUri();
      this.Ticket = rtmpXml.GetNamedChildNodeText("ticket");
      if (ticketsXml != null)
        this.Tickets = (IReadOnlyDictionary<string, string>) ticketsXml.GetChildNodes().ToDictionary<XElement, string, string>((Func<XElement, string>) (ticketXml => ticketXml.GetNamedAttributeText("name")), (Func<XElement, string>) (ticketXml => ticketXml.GetText()));
      this.Contents = (IReadOnlyList<Content>) streamXml.GetNamedChildNode("contents_list").GetChildNodes().Select<XElement, Content>((Func<XElement, Content>) (contentsXml => new Content(contentsXml))).ToList<Content>();
      this.Position = !streamXml.GetNamedChildNodeText("split_top").ToBooleanFrom1() ? (!streamXml.GetNamedChildNodeText("split_bottom").ToBooleanFrom1() ? (streamXml.GetNamedChildNodeText("background_comment").ToBooleanFrom1() ? VideoPosition.Small : VideoPosition.Default) : VideoPosition.Bottom) : VideoPosition.Top;
      string namedChildNodeText = streamXml.GetNamedChildNodeText("aspect");
      this.Aspect = !string.IsNullOrEmpty(namedChildNodeText) ? namedChildNodeText.ToVideoAspect() : VideoAspect.Auto;
      this.BroadcastToken = streamXml.GetNamedChildNodeText("broadcast_token");
      this.IsQualityOfServiceAnalyticsEnabled = playerXml.GetNamedChildNodeText("qos_analytics").ToBooleanFrom1();
    }

    public bool IsFlashMediaServer { get; private set; }

    public ushort RtmptPort { get; private set; }

    public Uri RtmpUrl { get; private set; }

    public string Ticket { get; private set; }

    public IReadOnlyDictionary<string, string> Tickets { get; private set; }

    public IReadOnlyList<Content> Contents { get; private set; }

    public VideoPosition Position { get; private set; }

    public VideoAspect Aspect { get; private set; }

    public string BroadcastToken { get; private set; }

    public bool IsQualityOfServiceAnalyticsEnabled { get; private set; }
  }
}
