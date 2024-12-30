// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.Heartbeat.HeartbeatResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Live.Heartbeat
{
  public sealed class HeartbeatResponse
  {
    internal HeartbeatResponse(XElement heartbeatXml)
    {
      this.LoadedAt = heartbeatXml.GetNamedAttributeText("time").ToDateTimeOffsetFromUnixTime();
      this.WatchCount = heartbeatXml.GetNamedChildNodeText("watchCount").ToUInt();
      this.CommentCount = heartbeatXml.GetNamedChildNodeText("commentCount").ToUInt();
      this.IsRestrict = heartbeatXml.GetNamedChildNodeText("is_restrict").ToBooleanFrom1();
      this.Ticket = heartbeatXml.GetNamedChildNodeText("ticket");
      this.WaitDuration = heartbeatXml.GetNamedChildNodeText("waitTime").ToTimeSpanFromSecondsString();
    }

    public DateTimeOffset LoadedAt { get; private set; }

    public uint WatchCount { get; private set; }

    public uint CommentCount { get; private set; }

    public bool IsRestrict { get; private set; }

    public string Ticket { get; private set; }

    public TimeSpan WaitDuration { get; private set; }
  }
}
