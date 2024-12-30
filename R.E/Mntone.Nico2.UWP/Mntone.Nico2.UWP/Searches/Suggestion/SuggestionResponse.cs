// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Searches.Suggestion.SuggestionResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Searches.Suggestion
{
  [DataContract]
  public sealed class SuggestionResponse
  {
    private List<string> _Candidates;

    internal SuggestionResponse()
    {
    }

    public IReadOnlyList<string> Candidates => (IReadOnlyList<string>) this._Candidates;

    [DataMember(Name = "candidates")]
    private List<string> CandidatesImpl
    {
      get => this._Candidates ?? (this._Candidates = new List<string>());
      set => this._Candidates = value;
    }
  }
}
