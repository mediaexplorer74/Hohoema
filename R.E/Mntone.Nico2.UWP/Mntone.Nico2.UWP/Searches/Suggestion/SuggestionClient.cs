// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Searches.Suggestion.SuggestionClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Searches.Suggestion
{
  internal sealed class SuggestionClient
  {
    public static Task<string> GetSuggestionDataAsync(NiconicoContext context, string targetWord)
    {
      return context.GetClient().GetConvertedStringAsync(NiconicoUrls.SearchSuggestionUrl + Uri.EscapeUriString(targetWord));
    }

    public static SuggestionResponse ParseSuggestionData(string suggestionData)
    {
      return JsonSerializerExtensions.Load<SuggestionResponse>(suggestionData);
    }

    public static Task<SuggestionResponse> GetSuggestionAsync(
      NiconicoContext context,
      string targetWord)
    {
      return SuggestionClient.GetSuggestionDataAsync(context, targetWord).ContinueWith<SuggestionResponse>((Func<Task<string>, SuggestionResponse>) (prevTask => SuggestionClient.ParseSuggestionData(prevTask.Result)));
    }
  }
}
