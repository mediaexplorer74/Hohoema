// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.NiconicoSession
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;

#nullable disable
namespace Mntone.Nico2
{
  public sealed class NiconicoSession
  {
    private DateTimeOffset _Expires = DateTimeOffset.MinValue;
    private NiconicoAccountAuthority _AccountAuthority;

    public NiconicoSession()
    {
    }

    public NiconicoSession(string key, DateTimeOffset expires)
    {
      this.Key = key;
      this.Expires = expires;
    }

    public string Key { get; set; }

    public DateTimeOffset Expires
    {
      get => this._Expires;
      set
      {
        this._Expires = !(value < DateTimeOffset.Now) ? value : throw new Exception("Expires is out of range.");
      }
    }

    public NiconicoAccountAuthority AccountAuthority
    {
      get => this._AccountAuthority;
      internal set => this._AccountAuthority = value;
    }

    public uint UserId { get; internal set; }
  }
}
