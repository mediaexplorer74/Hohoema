// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.NiconicoQueryHelper
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Mntone.Nico2
{
  public static class NiconicoQueryHelper
  {
    public static string RemoveIdPrefix(string withPrefixId)
    {
      char ch = withPrefixId.ElementAt<char>(0);
      int num = (int) withPrefixId.ElementAt<char>(1);
      return ch >= '0' && ch <= '9' ? withPrefixId : withPrefixId.Substring(2);
    }

    public static IEnumerable<string> RemoveIdPrefix(IEnumerable<string> items)
    {
      return items.Select<string, string>((Func<string, string>) (x => NiconicoQueryHelper.RemoveIdPrefix(x)));
    }

    public static string Make_idlist_QueryKeyString(NiconicoItemType itemType)
    {
      return string.Format("id_list[{0}][]", (object) (uint) itemType);
    }
  }
}
