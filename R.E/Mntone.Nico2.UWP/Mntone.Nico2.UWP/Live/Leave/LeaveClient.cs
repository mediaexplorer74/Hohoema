// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.Leave.LeaveClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Threading.Tasks;
using Windows.Web.Http;

#nullable disable
namespace Mntone.Nico2.Live.Leave
{
  internal sealed class LeaveClient
  {
    public static Task<string> LeaveDataAsync(NiconicoContext context, string requestId)
    {
      if (!NiconicoRegex.IsLiveId(requestId))
        throw new ArgumentException();
      return WindowsRuntimeSystemExtensions.AsTask<string, HttpProgress>(context.GetClient().GetStringAsync(new Uri(string.Format("{0}?v={1}", (object) NiconicoUrls.LiveLeaveUrl, (object) requestId))));
    }

    public static bool ParseLeaveData(string leaveData) => leaveData.ToBooleanFromString();

    public static Task<bool> LeaveAsync(NiconicoContext context, string requestId)
    {
      return LeaveClient.LeaveDataAsync(context, requestId).ContinueWith<bool>((Func<Task<string>, bool>) (prevTask => LeaveClient.ParseLeaveData(prevTask.Result)));
    }
  }
}
