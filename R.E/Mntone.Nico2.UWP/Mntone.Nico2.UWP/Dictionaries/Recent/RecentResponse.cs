// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Dictionaries.Recent.RecentResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Dictionaries.Recent
{
  [DataContract]
  public sealed class RecentResponse
  {
    private List<Word> _Words;

    internal RecentResponse()
    {
    }

    public IReadOnlyList<Word> Words => (IReadOnlyList<Word>) this._Words;

    [DataMember(Name = "pages")]
    private List<Word> WordsImpl
    {
      get => this._Words ?? (this._Words = new List<Word>());
      set => this._Words = value;
    }
  }
}
