// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.OnAirStreams.OnAirStreamsResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Live.OnAirStreams
{
  [DataContract]
  public sealed class OnAirStreamsResponse
  {
    private List<OnAirStream> _OnAirStreams;
    private List<ReservedStream> _ReservedStreams;

    internal OnAirStreamsResponse()
    {
    }

    public IReadOnlyList<OnAirStream> OnAirStreams
    {
      get => (IReadOnlyList<OnAirStream>) this._OnAirStreams;
    }

    [DataMember(Name = "onair_stream_list")]
    private List<OnAirStream> OnAirStreamsImpl
    {
      get => this._OnAirStreams ?? (this._OnAirStreams = new List<OnAirStream>());
      set => this._OnAirStreams = value;
    }

    public IReadOnlyList<ReservedStream> ReservedStreams
    {
      get => (IReadOnlyList<ReservedStream>) this._ReservedStreams;
    }

    [DataMember(Name = "reserved_stream_list")]
    private List<ReservedStream> ReservedStreamsImpl
    {
      get => this._ReservedStreams ?? (this._ReservedStreams = new List<ReservedStream>());
      set => this._ReservedStreams = value;
    }
  }
}
