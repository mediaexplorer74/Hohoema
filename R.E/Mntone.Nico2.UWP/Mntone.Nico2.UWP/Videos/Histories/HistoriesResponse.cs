// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Histories.HistoriesResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Histories
{
  [DataContract]
  public sealed class HistoriesResponse
  {
    private List<History> _Histories;

    internal HistoriesResponse()
    {
    }

    [DataMember(Name = "status")]
    private string StatusImpl
    {
      get => string.Empty;
      set
      {
        if (value != "ok")
          throw new Exception("Parse Error.");
      }
    }

    [DataMember(Name = "token")]
    public string Token { get; private set; }

    public IReadOnlyList<History> Histories => (IReadOnlyList<History>) this._Histories;

    [DataMember(Name = "history")]
    private List<History> HistoriesImpl
    {
      get => this._Histories ?? (this._Histories = new List<History>());
      set => this._Histories = value;
    }
  }
}
