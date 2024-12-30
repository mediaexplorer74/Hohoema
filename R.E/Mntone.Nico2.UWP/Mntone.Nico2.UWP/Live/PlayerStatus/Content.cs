// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.PlayerStatus.Content
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Live.PlayerStatus
{
  public sealed class Content
  {
    internal Content(XElement contentsXml)
    {
      this.Id = contentsXml.GetNamedAttributeText("id");
      this.IsAudioDisabled = contentsXml.GetNamedAttributeText("disableAudio").ToBooleanFrom1();
      this.IsVideoDisabled = contentsXml.GetNamedAttributeText("disableVideo").ToBooleanFrom1();
      this.StartedAt = contentsXml.GetNamedAttributeText("start_time").ToDateTimeOffsetFromUnixTime();
      this.Duration = contentsXml.GetNamedAttributeText("duration").ToTimeSpanFromSecondsString();
      this.Title = contentsXml.GetNamedAttributeText("title");
      this.Value = contentsXml.GetText();
    }

    public string Id { get; private set; }

    public bool IsAudioDisabled { get; private set; }

    public bool IsVideoDisabled { get; private set; }

    public DateTimeOffset StartedAt { get; private set; }

    public TimeSpan Duration { get; private set; }

    public string Title { get; private set; }

    public string Value { get; private set; }
  }
}
