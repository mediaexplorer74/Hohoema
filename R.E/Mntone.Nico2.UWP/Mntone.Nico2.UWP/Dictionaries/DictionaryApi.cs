// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Dictionaries.DictionaryApi
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Dictionaries.Exist;
using Mntone.Nico2.Dictionaries.Recent;
using Mntone.Nico2.Dictionaries.Summary;
using Mntone.Nico2.Dictionaries.WordExist;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Dictionaries
{
  public sealed class DictionaryApi
  {
    private NiconicoContext _context;

    internal DictionaryApi(NiconicoContext context) => this._context = context;

    public Task<bool> WordExistAsync(string targetWord)
    {
      return WordExistClient.WordExistAsync(this._context, targetWord);
    }

    public Task<SummaryResponse> GetSummaryAsync(string targetWord)
    {
      return SummaryClient.GetSummaryAsync(this._context, targetWord);
    }

    public Task<bool> ExistAsync(Category targetCategory, string targetWord)
    {
      return ExistClient.ExistAsync(this._context, targetCategory, targetWord);
    }

    public Task<RecentResponse> GetRecentAsync() => RecentClient.GetRecentAsync(this._context);
  }
}
