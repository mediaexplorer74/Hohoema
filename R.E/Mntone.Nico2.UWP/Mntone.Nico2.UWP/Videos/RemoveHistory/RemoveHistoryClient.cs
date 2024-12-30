// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.RemoveHistory.RemoveHistoryClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Videos.RemoveHistory
{
  internal sealed class RemoveHistoryClient
  {
    public static Task<string> RemoveHistoryDataAsync(
      NiconicoContext context,
      string token,
      string requestId)
    {
      if (!NiconicoRegex.IsVideoId(requestId))
        throw new ArgumentException();
      return context.GetClient().GetStringAsync(string.Format("{0}{1}&video_id={2}", (object) NiconicoUrls.VideoRemoveUrl, (object) token, (object) requestId));
    }

    public static Task<string> RemoveAllHistoriesDataAsync(NiconicoContext context, string token)
    {
      return context.GetClient().GetStringAsync(string.Format("{0}{1}&video_id=all", (object) NiconicoUrls.VideoRemoveUrl, (object) token));
    }

    public static RemoveHistoryResponse ParseRemoveHistoryData(string historiesData)
    {
      return JsonSerializerExtensions.Load<RemoveHistoryResponse>(historiesData);
    }

    public static Task<RemoveHistoryResponse> RemoveHistoryAsync(
      NiconicoContext context,
      string token,
      string requestId)
    {
      return RemoveHistoryClient.RemoveHistoryDataAsync(context, token, requestId).ContinueWith<RemoveHistoryResponse>((Func<Task<string>, RemoveHistoryResponse>) (prevTask => RemoveHistoryClient.ParseRemoveHistoryData(prevTask.Result)));
    }

    public static Task<RemoveHistoryResponse> RemoveAllHistoriesAsync(
      NiconicoContext context,
      string token)
    {
      return RemoveHistoryClient.RemoveAllHistoriesDataAsync(context, token).ContinueWith<RemoveHistoryResponse>((Func<Task<string>, RemoveHistoryResponse>) (prevTask => RemoveHistoryClient.ParseRemoveHistoryData(prevTask.Result)));
    }
  }
}
