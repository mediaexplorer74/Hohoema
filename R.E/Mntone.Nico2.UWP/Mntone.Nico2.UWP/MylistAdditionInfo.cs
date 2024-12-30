// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.MylistAdditionInfo
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Collections.Generic;

#nullable disable
namespace Mntone.Nico2
{
  public class MylistAdditionInfo
  {
    public string GroupId { get; set; }

    public Dictionary<string, string> Values { get; } = new Dictionary<string, string>();

    public string ItemType => this.Values["item_type"];

    public string ItemId => this.Values["item_id"];

    public string ItemAmc => this.Values["item_amc"];

    public string Token { get; set; }
  }
}
