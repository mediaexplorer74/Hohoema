// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.OnAirStreams.OnAirStreamsClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

#nullable disable
namespace Mntone.Nico2.Live.OnAirStreams
{
  internal sealed class OnAirStreamsClient
  {
    public static Task<string> GetOnAirStreamsIndexDataAsync(
      NiconicoContext context,
      ushort pageIndex)
    {
      StringBuilder stringBuilder = new StringBuilder(NiconicoUrls.LiveZappingListIndexUrl);
      if (pageIndex != (ushort) 1)
      {
        stringBuilder.Append("&zpage=");
        stringBuilder.Append(pageIndex);
      }
      return WindowsRuntimeSystemExtensions.AsTask<string, HttpProgress>(context.GetClient().GetStringAsync(new Uri(stringBuilder.ToString())));
    }

    public static Task<string> GetOnAirStreamsRecentDataAsync(
      NiconicoContext context,
      ushort pageIndex,
      Category category,
      Order direction,
      SortType type)
    {
      StringBuilder stringBuilder = new StringBuilder(NiconicoUrls.LiveZappingListRecentUrl);
      if (pageIndex != (ushort) 1)
      {
        stringBuilder.Append("&zpage=");
        stringBuilder.Append(pageIndex);
      }
      stringBuilder.Append("&tab=");
      stringBuilder.Append(category.ToCategoryString());
      if (direction == Order.Descending)
        stringBuilder.Append("&order=desc");
      stringBuilder.Append("&sort=");
      stringBuilder.Append(type.ToSortTypeString());
      return context.GetClient().GetStringAsync(stringBuilder.ToString());
    }

    public static OnAirStreamsResponse ParseOnAirStreamsData(string onAirStreamsData)
    {
      return JsonSerializerExtensions.Load<OnAirStreamsResponse>(onAirStreamsData);
    }

    public static Task<OnAirStreamsResponse> GetOnAirStreamsIndexAsync(
      NiconicoContext context,
      ushort pageIndex = 1)
    {
      return OnAirStreamsClient.GetOnAirStreamsIndexDataAsync(context, pageIndex).ContinueWith<OnAirStreamsResponse>((Func<Task<string>, OnAirStreamsResponse>) (prevTask => OnAirStreamsClient.ParseOnAirStreamsData(prevTask.Result)));
    }

    public static Task<OnAirStreamsResponse> GetOnAirStreamsRecentAsync(
      NiconicoContext context,
      ushort pageIndex,
      Category category,
      Order direction = Order.Ascending,
      SortType type = SortType.StartTime)
    {
      return OnAirStreamsClient.GetOnAirStreamsRecentDataAsync(context, pageIndex, category, direction, type).ContinueWith<OnAirStreamsResponse>((Func<Task<string>, OnAirStreamsResponse>) (prevTask => OnAirStreamsClient.ParseOnAirStreamsData(prevTask.Result)));
    }
  }
}
