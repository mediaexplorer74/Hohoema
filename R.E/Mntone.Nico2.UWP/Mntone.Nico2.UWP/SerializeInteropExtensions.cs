// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.SerializeInteropExtensions
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;

#nullable disable
namespace Mntone.Nico2
{
  internal static class SerializeInteropExtensions
  {
    public static bool ToBooleanFrom1(this string value)
    {
      return value != null && value.Length == 1 && value[0] == '1';
    }

    public static bool ToBooleanFromString(this string value) => value == "true";

    public static short ToShort(this string value) => short.Parse(value);

    public static ushort ToUShort(this string value) => ushort.Parse(value);

    public static int ToInt(this string value) => int.Parse(value);

    public static uint ToUInt(this string value) => uint.Parse(value);

    public static long ToLong(this string value) => long.Parse(value);

    public static ulong ToULong(this string value) => ulong.Parse(value);

    public static float ToSingle(this string value) => float.Parse(value);

    public static double ToDouble(this string value) => double.Parse(value);

    public static long ToLongFromDateTimeOffset(this DateTimeOffset value)
    {
      return value.Ticks / 10000000L - 116444736000000000L;
    }

    public static DateTimeOffset ToDateTimeOffsetFromUnixTime(this string value)
    {
      return long.Parse(value).ToDateTimeOffsetFromUnixTime();
    }

    public static DateTimeOffset ToDateTimeOffsetFromUnixTime(this long value)
    {
      return DateTimeOffset.FromFileTime(10000000L * value + 116444736000000000L);
    }

    public static DateTimeOffset ToDateTimeOffsetFromIso8601(this string value)
    {
      return string.IsNullOrEmpty(value) ? DateTimeOffset.MinValue : DateTimeOffset.Parse(value);
    }

    public static TimeSpan ToTimeSpan(this string value)
    {
      string[] strArray = value.Split(':');
      if (strArray.Length == 3)
        return new TimeSpan(int.Parse(strArray[0]), int.Parse(strArray[1]), int.Parse(strArray[2]));
      if (strArray.Length == 2)
        return new TimeSpan(0, int.Parse(strArray[0]), int.Parse(strArray[1]));
      return strArray.Length == 1 ? new TimeSpan(0, 0, int.Parse(strArray[1])) : throw new ArgumentException();
    }

    public static TimeSpan ToTimeSpanFromSecondsString(this string value)
    {
      return string.IsNullOrEmpty(value) ? TimeSpan.MinValue : new TimeSpan(0, 0, int.Parse(value));
    }

    public static Uri ToUri(this string value)
    {
      return string.IsNullOrEmpty(value) ? (Uri) null : new Uri(value);
    }

    public static string ToString1Or0(this bool value) => !value ? "0" : "1";
  }
}
