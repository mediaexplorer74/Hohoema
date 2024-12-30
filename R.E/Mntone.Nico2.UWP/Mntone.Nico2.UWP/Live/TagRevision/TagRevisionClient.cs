// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.TagRevision.TagRevisionClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Live.TagRevision
{
  internal sealed class TagRevisionClient
  {
    public static Task<string> GetTagRevisionDataAsync(NiconicoContext context, string requestId)
    {
      if (!NiconicoRegex.IsLiveId(requestId))
        throw new ArgumentException();
      return context.GetClient().GetConvertedStringAsync(NiconicoUrls.LiveTagRevisionUrl + requestId);
    }

    public static ushort ParseTagRevisionData(string tagRevisionData)
    {
      return tagRevisionData.Substring(7, tagRevisionData.Length - 8).ToUShort();
    }

    public static Task<ushort> GetTagRevisionAsync(NiconicoContext context, string requestId)
    {
      return TagRevisionClient.GetTagRevisionDataAsync(context, requestId).ContinueWith<ushort>((Func<Task<string>, ushort>) (prevTask => TagRevisionClient.ParseTagRevisionData(prevTask.Result)));
    }
  }
}
