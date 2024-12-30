// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Channels.Icon.IconClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Channels.Icon
{
  internal sealed class IconClient
  {
    public static Task<byte[]> GetIconAsync(NiconicoContext context, string requestId)
    {
      uint num = NiconicoRegex.IsChannelId(requestId) ? requestId.Substring(2).ToUInt() : throw new ArgumentException();
      return context.GetClient().GetByteArrayAsync(string.Format(NiconicoUrls.ChannelIconUrl, (object) num));
    }
  }
}
