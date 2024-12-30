// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.StringExtention
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Linq;
using System.Text;

#nullable disable
namespace Mntone.Nico2
{
  public static class StringExtention
  {
    public static string DecodeUTF8(this string encoded)
    {
      return encoded != null ? Encoding.UTF8.GetString(encoded.Select<char, byte>((Func<char, byte>) (x => Convert.ToByte(x))).ToArray<byte>()) : "";
    }
  }
}
