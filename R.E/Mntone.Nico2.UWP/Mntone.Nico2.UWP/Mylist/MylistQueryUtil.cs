// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Mylist.MylistQueryUtil
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Mntone.Nico2.Mylist
{
  public static class MylistQueryUtil
  {
    public static Tuple<string, string> MylistDataToQueryString(MylistData mylistData)
    {
      return new Tuple<string, string>(NiconicoQueryHelper.Make_idlist_QueryKeyString(mylistData.ItemType), NiconicoQueryHelper.RemoveIdPrefix(mylistData.ItemId));
    }

    public static string MylistDataToQueryString(IEnumerable<MylistData> list)
    {
      return string.Join("&", list.Select<MylistData, Tuple<string, string>>(new Func<MylistData, Tuple<string, string>>(MylistQueryUtil.MylistDataToQueryString)).Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (x => x.Item1 + "=" + x.Item2)));
    }
  }
}
