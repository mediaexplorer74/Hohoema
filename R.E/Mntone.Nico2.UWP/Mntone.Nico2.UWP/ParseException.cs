// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.ParseException
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;

#nullable disable
namespace Mntone.Nico2
{
  public sealed class ParseException : Exception
  {
    private const string MESSAGE = "Text has invalid context.";

    public ParseException()
      : base("Text has invalid context.")
    {
      this.HResult = -1073479678;
    }

    public ParseException(string message)
      : base(message)
    {
      this.HResult = -1073479678;
    }

    public ParseException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.HResult = -1073479678;
    }
  }
}
