// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.ContentZoningException
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;

#nullable disable
namespace Mntone.Nico2
{
  public class ContentZoningException : Exception
  {
    public ContentZoningException()
    {
    }

    public ContentZoningException(string message)
      : base(message)
    {
    }

    public ContentZoningException(string message, Exception exception)
      : base(message, exception)
    {
    }
  }
}
