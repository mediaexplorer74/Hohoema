// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.ReservationsInDetail.Program
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Live.ReservationsInDetail
{
  public sealed class Program
  {
    internal Program(XElement reservedItemXml)
    {
      this.Id = "lv" + reservedItemXml.GetNamedChildNodeText("vid");
      this.Title = reservedItemXml.GetNamedChildNodeText("title");
      this.Status = reservedItemXml.GetNamedChildNodeText("status");
      this.IsUnwatched = reservedItemXml.GetNamedChildNodeText("unwatch").ToBooleanFrom1();
      string namedChildNodeText = reservedItemXml.GetNamedChildNodeText("expire");
      this.ExpiredAt = namedChildNodeText != "0" ? namedChildNodeText.ToDateTimeOffsetFromUnixTime() : DateTimeOffset.MaxValue;
    }

    public string Id { get; private set; }

    public string Title { get; private set; }

    public string Status { get; private set; }

    public bool IsUnwatched { get; private set; }

    public DateTimeOffset ExpiredAt { get; private set; }
  }
}
