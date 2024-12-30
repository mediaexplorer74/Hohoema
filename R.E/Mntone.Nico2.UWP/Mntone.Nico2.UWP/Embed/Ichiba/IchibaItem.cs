// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Embed.Ichiba.IchibaItem
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;

#nullable disable
namespace Mntone.Nico2.Embed.Ichiba
{
  public class IchibaItem
  {
    public string Id { get; set; }

    public string Title { get; set; }

    public Uri ThumbnailUrl { get; set; }

    public Uri AmazonItemLink { get; set; }

    public string Maker { get; set; }

    public string Price { get; set; }

    public string DiscountText { get; set; }

    public Uri IchibaUrl { get; set; }

    public IchibaItemReservation Reservation { get; set; }

    public IchibaItemSell Sell { get; set; }
  }
}
