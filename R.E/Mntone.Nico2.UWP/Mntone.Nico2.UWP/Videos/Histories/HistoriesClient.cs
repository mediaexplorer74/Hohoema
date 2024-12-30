// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Histories.HistoriesClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Videos.Histories
{
  internal sealed class HistoriesClient
  {
    public static Task<string> GetHistoriesDataAsync(NiconicoContext context)
    {
      return context.PostAsync(NiconicoUrls.VideoHistoryUrl);
    }

    public static HistoriesResponse ParseHistoriesData(string historiesData)
    {
      return JsonSerializerExtensions.Load<HistoriesResponse>(historiesData);
    }

    public static Task<HistoriesResponse> GetHistoriesAsync(NiconicoContext context)
    {
      return HistoriesClient.GetHistoriesDataAsync(context).ContinueWith<HistoriesResponse>((Func<Task<string>, HistoriesResponse>) (prevTask => HistoriesClient.ParseHistoriesData(prevTask.Result)));
    }
  }
}
