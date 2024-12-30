// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.ReservationsInDetail.ReservationsInDetailResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Live.ReservationsInDetail
{
  public sealed class ReservationsInDetailResponse
  {
    internal ReservationsInDetailResponse(XElement reservedItemsXml)
    {
      if (reservedItemsXml != null)
        this.ReservedProgram = (IReadOnlyList<Program>) reservedItemsXml.GetChildNodes().Select<XElement, Program>((Func<XElement, Program>) (reservedItemXml => new Program(reservedItemXml))).ToList<Program>();
      else
        this.ReservedProgram = (IReadOnlyList<Program>) new List<Program>();
    }

    public IReadOnlyList<Program> ReservedProgram { get; private set; }
  }
}
