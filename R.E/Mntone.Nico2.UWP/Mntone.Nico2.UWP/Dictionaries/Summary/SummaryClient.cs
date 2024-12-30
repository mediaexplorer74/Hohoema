// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Dictionaries.Summary.SummaryClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Dictionaries.Summary
{
  internal sealed class SummaryClient
  {
    public static Task<string> GetSummaryDataAsync(NiconicoContext context, string targetWord)
    {
      return context.GetClient().GetStringAsync(NiconicoUrls.DictionarySummarytUrl + Uri.EscapeUriString(targetWord));
    }

    public static SummaryResponse ParseSummaryData(string summaryData)
    {
      return JsonSerializerExtensions.Load<SummaryResponse>(summaryData);
    }

    public static Task<SummaryResponse> GetSummaryAsync(NiconicoContext context, string targetWord)
    {
      return SummaryClient.GetSummaryDataAsync(context, targetWord).ContinueWith<SummaryResponse>((Func<Task<string>, SummaryResponse>) (prevTask => SummaryClient.ParseSummaryData(prevTask.Result)));
    }
  }
}
