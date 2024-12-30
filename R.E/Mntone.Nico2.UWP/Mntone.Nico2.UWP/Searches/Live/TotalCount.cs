// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Searches.Live.TotalCount
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Searches.Live
{
  [DataContract]
  public class TotalCount
  {
    private int? _OnairCount;
    private int? _ClosedCount;
    private int? _ReservedCount;
    private int? _FilteredCount;

    [DataMember(Name = "onair")]
    public string __OnairCount { get; set; }

    public int OnairCount
    {
      get
      {
        return !this._OnairCount.HasValue ? (this._OnairCount = new int?(int.Parse(this.__OnairCount))).Value : this._OnairCount.Value;
      }
    }

    [DataMember(Name = "closed")]
    public string __ClosedCount { get; set; }

    public int ClosedCount
    {
      get
      {
        return !this._ClosedCount.HasValue ? (this._ClosedCount = new int?(int.Parse(this.__ClosedCount))).Value : this._ClosedCount.Value;
      }
    }

    [DataMember(Name = "reserved")]
    public string __ReservedCount { get; set; }

    public int ReservedCount
    {
      get
      {
        return !this._ReservedCount.HasValue ? (this._ReservedCount = new int?(int.Parse(this.__ReservedCount))).Value : this._ReservedCount.Value;
      }
    }

    [DataMember(Name = "filtered")]
    public string __FilteredCount { get; set; }

    public int FilteredCount
    {
      get
      {
        return !this._FilteredCount.HasValue ? (this._FilteredCount = new int?(int.Parse(this.__FilteredCount))).Value : this._FilteredCount.Value;
      }
    }
  }
}
