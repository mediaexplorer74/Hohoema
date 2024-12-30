// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.NicoRepo.UserNicoRepo.UserNicoRepoClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.NicoRepo.UserNicoRepo
{
  public static class UserNicoRepoClient
  {
    public static Task<string> GetUserNicoRepoJsonAsync(
      NiconicoContext context,
      uint userId,
      string lastNicoRepoItemId)
    {
      return context.GetStringAsync(NicoRepoApi.MakeNicoRepoUrl_User(userId, lastNicoRepoItemId));
    }

    public static Task<NicoRepoResponse> GetUserNicoRepoAsync(
      NiconicoContext context,
      uint userId,
      string lastNicoRepoItemId)
    {
      return UserNicoRepoClient.GetUserNicoRepoJsonAsync(context, userId, lastNicoRepoItemId).ContinueWith<NicoRepoResponse>((Func<Task<string>, NicoRepoResponse>) (prevTask => NicoRepoResponse.ParseNicoRepoJson(prevTask.Result)));
    }
  }
}
