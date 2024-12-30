// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.OtherStreams.OtherStreamsClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Live.OtherStreams
{
  internal sealed class OtherStreamsClient
  {
    public static Task<string> GetOtherStreamsDataAsync(
      NiconicoContext context,
      StatusType status,
      ushort pageIndex)
    {
      return context.GetClient().GetStringAsync(pageIndex > (ushort) 1 ? string.Format("{0}{1}&zpage={2}", (object) NiconicoUrls.LiveIndexZeroStreamListUrl, (object) status.ToStatusTypeString(), (object) pageIndex) : string.Format("{0}{1}", (object) NiconicoUrls.LiveIndexZeroStreamListUrl, (object) status.ToStatusTypeString()));
    }

    public static OtherStreamsResponse ParseOtherStreamsData(string otherStreamsData)
    {
      return JsonSerializerExtensions.Load<OtherStreamsResponse>(otherStreamsData);
    }

    public static Task<OtherStreamsResponse> GetOtherStreamsAsync(
      NiconicoContext context,
      StatusType status,
      ushort pageIndex)
    {
      return OtherStreamsClient.GetOtherStreamsDataAsync(context, status, pageIndex).ContinueWith<OtherStreamsResponse>((Func<Task<string>, OtherStreamsResponse>) (prevTask => OtherStreamsClient.ParseOtherStreamsData(prevTask.Result)));
    }
  }
}
