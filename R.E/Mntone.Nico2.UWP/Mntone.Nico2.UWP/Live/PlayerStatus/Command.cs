// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.PlayerStatus.Command
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Live.PlayerStatus
{
  public sealed class Command
  {
    internal Command(XElement queXml)
    {
      this.Position = TimeSpan.FromTicks(queXml.GetNamedAttributeText("vpos").ToLong() * 10000L);
      this.Mail = queXml.GetNamedAttributeText("mail");
      this.Name = queXml.GetNamedAttributeText("name");
      this.Value = queXml.GetText();
    }

    public TimeSpan Position { get; private set; }

    public string Mail { get; private set; }

    public string Name { get; private set; }

    public string Value { get; private set; }
  }
}
