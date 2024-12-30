// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.PostKey.PostKeyClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Live.PostKey
{
  internal sealed class PostKeyClient
  {
    public static Task<string> GetPostKeyDataAsync(
      NiconicoContext context,
      uint threadId,
      uint blockNo)
    {
      return context.GetClient().GetStringAsync(string.Format("{0}?thread={1}&block_no={2}", (object) NiconicoUrls.LivePostKeyUrl, (object) threadId, (object) blockNo));
    }

    public static string ParsePostKeyData(string postKeyData)
    {
      return postKeyData.StartsWith("postkey=") ? postKeyData.Substring(8) : throw CustomExceptionFactory.Create(-1073479678);
    }

    public static Task<string> GetPostKeyAsync(
      NiconicoContext context,
      uint threadId,
      uint blockNo)
    {
      return PostKeyClient.GetPostKeyDataAsync(context, threadId, blockNo).ContinueWith<string>((Func<Task<string>, string>) (prevTask => PostKeyClient.ParsePostKeyData(prevTask.Result)));
    }
  }
}
