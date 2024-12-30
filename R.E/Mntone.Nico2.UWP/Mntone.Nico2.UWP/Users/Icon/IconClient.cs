// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Users.Icon.IconClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Users.Icon
{
  internal sealed class IconClient
  {
    public static Task<byte[]> GetIconAsync(NiconicoContext context, uint userId)
    {
      return context.GetClient().GetByteArrayAsync(string.Format(NiconicoUrls.UserIconUrl, (object) (userId / 10000U), (object) userId)).ContinueWith<byte[]>((Func<Task<byte[]>, byte[]>) (prevTask =>
      {
        try
        {
          return prevTask.Result;
        }
        catch (AggregateException ex)
        {
          if (ex.HResult != -2146233088)
            throw;
        }
        return context.GetClient().GetByteArrayAsync(NiconicoUrls.UserBlankIconUrl).Result;
      }));
    }
  }
}
