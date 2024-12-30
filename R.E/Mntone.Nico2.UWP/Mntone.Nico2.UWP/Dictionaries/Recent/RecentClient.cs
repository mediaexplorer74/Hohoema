// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Dictionaries.Recent.RecentClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Dictionaries.Recent
{
  internal sealed class RecentClient
  {
    public static Task<string> GetRecentDataAsync(NiconicoContext context)
    {
      return context.GetClient().GetStringAsync(NiconicoUrls.DictionaryRecentUrl);
    }

    public static RecentResponse ParseRecentData(string recentData)
    {
      return JsonSerializerExtensions.Load<RecentResponse>(recentData);
    }

    public static Task<RecentResponse> GetRecentAsync(NiconicoContext context)
    {
      return RecentClient.GetRecentDataAsync(context).ContinueWith<RecentResponse>((Func<Task<string>, RecentResponse>) (prevTask => RecentClient.ParseRecentData(prevTask.Result)));
    }
  }
}
