// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.OtherStreams.OtherStreamsResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Live.OtherStreams
{
  [DataContract]
  public sealed class OtherStreamsResponse
  {
    private List<OtherStream> _Streams;

    internal OtherStreamsResponse()
    {
    }

    public IReadOnlyList<OtherStream> Streams
    {
      get
      {
        return (IReadOnlyList<OtherStream>) this._Streams ?? (IReadOnlyList<OtherStream>) (this._Streams = new List<OtherStream>());
      }
    }

    [DataMember(Name = "reserved_stream_list")]
    private List<OtherStream> StreamsImpl
    {
      get => this._Streams ?? (this._Streams = new List<OtherStream>());
      set => this._Streams = value;
    }
  }
}
