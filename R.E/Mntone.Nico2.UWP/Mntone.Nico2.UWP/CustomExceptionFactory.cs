// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.CustomExceptionFactory
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;

#nullable disable
namespace Mntone.Nico2
{
  internal static class CustomExceptionFactory
  {
    public static Exception Create(int hResult)
    {
      return (Exception) new CustomExceptionFactory.CustomException(hResult);
    }

    public static Exception Create(string message, int hResult)
    {
      return (Exception) new CustomExceptionFactory.CustomException(message, hResult);
    }

    private class CustomException : Exception
    {
      public CustomException(int hResult) => this.HResult = hResult;

      public CustomException(string message, int hResult)
        : base(message)
      {
        this.HResult = hResult;
      }
    }
  }
}
