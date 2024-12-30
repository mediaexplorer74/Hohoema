// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.HttpQueryExtention
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Mntone.Nico2
{
  public static class HttpQueryExtention
  {
    public static string DictionaryToQuery(IDictionary<string, string> dict)
    {
      return string.Join("&", dict.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (pair => string.Format("{0}={1}", (object) pair.Key, (object) pair.Value))));
    }

    public static IDictionary<string, string> QueryToDictionary(string query)
    {
      return (IDictionary<string, string>) ((IEnumerable<string>) query.Split('&')).ToDictionary<string, string, string>((Func<string, string>) (source => source.Substring(0, source.IndexOf('='))), (Func<string, string>) (source => Uri.UnescapeDataString(source.Substring(source.IndexOf('=') + 1))));
    }
  }
}
