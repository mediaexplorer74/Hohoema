// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.NiconicoSessionExtetion
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Globalization;

#nullable disable
namespace Mntone.Nico2
{
  public static class NiconicoSessionExtetion
  {
    public static string ToCookieText(this NiconicoSession session, string userSessionName)
    {
      string str1 = userSessionName;
      string key = session.Key;
      DateTimeOffset dateTimeOffset = session.Expires;
      dateTimeOffset = dateTimeOffset.ToUniversalTime();
      string str2 = dateTimeOffset.ToString("ddd, dd-MMM-yyyy HH:mm:ss' GMT'", (IFormatProvider) CultureInfo.InvariantCulture);
      return string.Format("{0}={1}; expires={2}", (object) str1, (object) key, (object) str2);
    }
  }
}
