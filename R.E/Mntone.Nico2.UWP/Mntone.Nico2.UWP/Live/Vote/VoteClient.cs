// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.Vote.VoteClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Live.Vote
{
  internal sealed class VoteClient
  {
    public static Task<string> VoteDataAsync(
      NiconicoContext context,
      string requestId,
      ushort choiceNumber)
    {
      if (!NiconicoRegex.IsLiveId(requestId))
        throw new ArgumentException();
      if (choiceNumber > (ushort) 8)
        throw new ArgumentException();
      return context.GetClient().GetStringAsync(string.Format("{0}?v={1}&id={2}", (object) NiconicoUrls.LiveVoteUrl, (object) requestId, (object) choiceNumber));
    }

    public static bool ParseVoteData(string voteData)
    {
      if (voteData.StartsWith("status="))
        return voteData.Substring(7) == "true";
      throw CustomExceptionFactory.Create(-1073479678);
    }

    public static Task<bool> VoteAsync(
      NiconicoContext context,
      string requestId,
      ushort choiceNumber)
    {
      return VoteClient.VoteDataAsync(context, requestId, choiceNumber).ContinueWith<bool>((Func<Task<string>, bool>) (prevTask => VoteClient.ParseVoteData(prevTask.Result)));
    }
  }
}
