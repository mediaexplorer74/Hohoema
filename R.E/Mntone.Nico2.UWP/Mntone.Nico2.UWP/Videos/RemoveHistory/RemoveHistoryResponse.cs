// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.RemoveHistory.RemoveHistoryResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.RemoveHistory
{
  [DataContract]
  public sealed class RemoveHistoryResponse
  {
    internal RemoveHistoryResponse()
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

    [DataMember(Name = "count")]
    public ushort HistoryCount { get; private set; }

    [DataMember(Name = "removed")]
    public string RemovedId { get; private set; }
  }
}
