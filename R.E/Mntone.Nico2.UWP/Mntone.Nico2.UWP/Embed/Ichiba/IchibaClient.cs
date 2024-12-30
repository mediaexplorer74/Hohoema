// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Embed.Ichiba.IchibaClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Embed.Ichiba
{
  public sealed class IchibaClient
  {
    public static Task<string> GetIchibaJsonAsync(NiconicoContext context, string requestId)
    {
      string url = string.Format("http://ichiba.nicovideo.jp/embed/zero/show_ichiba?v={0}&country=ja-jp&ch=&rev=20120220", (object) requestId);
      return context.GetStringAsync(url);
    }

    private static IchibaResponse ParseIchibaJson(string json)
    {
      return JsonConvert.DeserializeObject<IchibaResponse>(json);
    }

    public static Task<IchibaResponse> GetIchibaAsync(NiconicoContext context, string requestId)
    {
      return IchibaClient.GetIchibaJsonAsync(context, requestId).ContinueWith<IchibaResponse>((Func<Task<string>, IchibaResponse>) (prevTask => IchibaClient.ParseIchibaJson(prevTask.Result)));
    }
  }
}
