// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.CKey.CKeyClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Live.CKey
{
  internal sealed class CKeyClient
  {
    public static Task<string> GetCKeyDataAsync(
      NiconicoContext context,
      string refererId,
      string requestId)
    {
      if (!NiconicoRegex.IsLiveId(refererId))
        throw new ArgumentException();
      if (!NiconicoRegex.IsVideoId(requestId))
        throw new ArgumentException();
      return context.GetClient().GetStringAsync(string.Format("{0}?referer_id={1}&id={2}", (object) NiconicoUrls.LiveCKeyUrl, (object) refererId, (object) requestId));
    }

    public static string ParseCKeyData(string cKeyData)
    {
      return cKeyData.Length > 5 && cKeyData.StartsWith("ckey=") ? cKeyData.Substring(5) : throw new Exception("Parse Error");
    }

    public static Task<string> GetCKeyAsync(
      NiconicoContext context,
      string refererId,
      string requestId)
    {
      return CKeyClient.GetCKeyDataAsync(context, refererId, requestId).ContinueWith<string>((Func<Task<string>, string>) (prevTask => CKeyClient.ParseCKeyData(prevTask.Result)));
    }
  }
}
