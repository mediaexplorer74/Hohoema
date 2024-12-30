// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Comment.NMSG_RequestParamaterBuilder
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Newtonsoft.Json;
using System;

#nullable disable
namespace Mntone.Nico2.Videos.Comment
{
  public static class NMSG_RequestParamaterBuilder
  {
    public static string MakeVideoCommmentRequest(
      string threadId,
      int userId,
      string userKey,
      TimeSpan video_length)
    {
      return JsonConvert.SerializeObject((object) new object[8]
      {
        (object) new PingItem("rs:0"),
        (object) new PingItem("ps:0"),
        (object) new ThreadItem()
        {
          Thread = new Thread()
          {
            ThreadId = threadId,
            UserId = userId,
            Userkey = userKey
          }
        },
        (object) new PingItem("pf:0"),
        (object) new PingItem("ps:1"),
        (object) new ThreadLeavesItem()
        {
          ThreadLeaves = new ThreadLeaves()
          {
            ThreadId = threadId,
            UserId = userId,
            Content = ThreadLeaves.MakeContentString(video_length),
            Userkey = userKey,
            Scores = 1,
            Nicoru = 0
          }
        },
        (object) new PingItem("pf:1"),
        (object) new PingItem("rf:0")
      }, new JsonSerializerSettings()
      {
        NullValueHandling = (NullValueHandling) 1
      });
    }

    public static string MakeOfficialVideoCommmentRequest(
      string threadId,
      string sub_threadId,
      string threadKey,
      int userId,
      string userKey,
      TimeSpan videoLength,
      bool force184)
    {
      string str = ThreadLeaves.MakeContentString(videoLength);
      return JsonConvert.SerializeObject((object) new object[14]
      {
        (object) new PingItem("rs:0"),
        (object) new PingItem("ps:0"),
        (object) new ThreadItem()
        {
          Thread = new Thread()
          {
            ThreadId = sub_threadId,
            UserId = userId,
            Userkey = userKey
          }
        },
        (object) new PingItem("pf:0"),
        (object) new PingItem("ps:1"),
        (object) new ThreadLeavesItem()
        {
          ThreadLeaves = new ThreadLeaves()
          {
            ThreadId = sub_threadId,
            UserId = userId,
            Content = str,
            Userkey = userKey,
            Scores = 1,
            Nicoru = 0
          }
        },
        (object) new PingItem("pf:1"),
        (object) new PingItem("ps:2"),
        (object) new ThreadItem()
        {
          Thread = new Thread()
          {
            ThreadId = threadId,
            UserId = userId,
            Threadkey = threadKey,
            Force184 = (force184 ? "1" : (string) null)
          }
        },
        (object) new PingItem("pf:2"),
        (object) new PingItem("ps:3"),
        (object) new ThreadLeavesItem()
        {
          ThreadLeaves = new ThreadLeaves()
          {
            ThreadId = threadId,
            UserId = userId,
            Content = str,
            Threadkey = threadKey,
            Force184 = (force184 ? "1" : (string) null),
            Scores = 1,
            Nicoru = 0
          }
        },
        (object) new PingItem("pf:3"),
        (object) new PingItem("rf:0")
      }, new JsonSerializerSettings()
      {
        NullValueHandling = (NullValueHandling) 1
      });
    }
  }
}
