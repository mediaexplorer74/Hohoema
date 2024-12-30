// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Comment.CommentClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Users.Info;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Windows.Web.Http;

#nullable disable
namespace Mntone.Nico2.Videos.Comment
{
  internal sealed class CommentClient
  {
    private static readonly int PostKeyCharCount = "postkey=".Count<char>();

    public static async Task<string> GetThreadKeyDataAsync(NiconicoContext context, long threadId)
    {
      return await context.GetClient().GetStringAsync(NiconicoUrls.VideoThreadKeyApiUrl + threadId.ToString());
    }

    public static ThreadKeyResponse ParseThreadKey(string threadKeyData)
    {
      if (string.IsNullOrEmpty(threadKeyData))
        return (ThreadKeyResponse) null;
      IDictionary<string, string> dictionary = HttpQueryExtention.QueryToDictionary(threadKeyData);
      return new ThreadKeyResponse(dictionary["threadkey"], dictionary["force_184"]);
    }

    public static async Task<string> GetCommentDataAsync(
      NiconicoContext context,
      int userId,
      string commentServerUrl,
      int threadId,
      bool isKeyRequired)
    {
      Dictionary<string, string> paramDict = new Dictionary<string, string>();
      paramDict.Add("user_id", userId.ToString());
      paramDict.Add("version", "20090904");
      paramDict.Add("thread", threadId.ToString());
      paramDict.Add("res_from", "-1000");
      if (isKeyRequired)
      {
        ThreadKeyResponse threadKeyResponse = await CommentClient.GetThreadKeyDataAsync(context, (long) threadId).ContinueWith<ThreadKeyResponse>((Func<Task<string>, ThreadKeyResponse>) (prevTask => CommentClient.ParseThreadKey(prevTask.Result)));
        if (threadKeyResponse != null)
        {
          paramDict.Add("threadkey", threadKeyResponse.ThreadKey);
          paramDict.Add("force_184", threadKeyResponse.Force184);
        }
      }
      string uri = string.Format("{0}thread?{1}", (object) commentServerUrl, (object) Uri.EscapeUriString(HttpQueryExtention.DictionaryToQuery((IDictionary<string, string>) paramDict)));
      return await context.GetClient().GetStringAsync(uri);
    }

    public static CommentResponse ParseComment(string commentData)
    {
      return CommentResponse.CreateFromXml(commentData);
    }

    public static Task<CommentResponse> GetCommentAsync(
      NiconicoContext context,
      int userId,
      string commentServerUrl,
      int threadId,
      bool isKeyRequired)
    {
      return CommentClient.GetCommentDataAsync(context, userId, commentServerUrl, threadId, isKeyRequired).ContinueWith<CommentResponse>((Func<Task<string>, CommentResponse>) (prevTask => CommentClient.ParseComment(prevTask.Result)));
    }

    public static Task<string> GetNMSGCommentDataAsync(
      NiconicoContext context,
      long threadId,
      int userId,
      string userKey,
      TimeSpan videoLength)
    {
      HttpStringContent content = new HttpStringContent(NMSG_RequestParamaterBuilder.MakeVideoCommmentRequest(threadId.ToString(), userId, userKey, videoLength));
      return context.PostAsync("http://nmsg.nicovideo.jp/api.json/", (IHttpContent) content);
    }

    public static Task<NMSG_Response> GetNMSGCommentAsync(
      NiconicoContext context,
      long threadId,
      int userId,
      string userKey,
      TimeSpan videoLength)
    {
      return CommentClient.GetNMSGCommentDataAsync(context, threadId, userId, userKey, videoLength).ContinueWith<NMSG_Response>((Func<Task<string>, NMSG_Response>) (prevTask => CommentClient.ParseNMSGResponseJson(prevTask.Result)));
    }

    public static async Task<string> GetOfficialVideoNMSGCommentDataAsync(
      NiconicoContext context,
      long threadId,
      long sub_threadId,
      int userId,
      string userKey,
      TimeSpan videoLength)
    {
      ThreadKeyResponse threadKeyResponse = await CommentClient.GetThreadKeyDataAsync(context, threadId).ContinueWith<ThreadKeyResponse>((Func<Task<string>, ThreadKeyResponse>) (prevTask => CommentClient.ParseThreadKey(prevTask.Result)));
      string threadKey = threadKeyResponse != null ? threadKeyResponse.ThreadKey : throw new Exception("can not get ThreadKey, threadId is " + (object) threadId);
      bool booleanFrom1 = threadKeyResponse.Force184.ToBooleanFrom1();
      return await context.PostAsync("http://nmsg.nicovideo.jp/api.json/", (IHttpContent) new HttpStringContent(NMSG_RequestParamaterBuilder.MakeOfficialVideoCommmentRequest(threadId.ToString(), sub_threadId.ToString(), threadKey, userId, userKey, videoLength, booleanFrom1)));
    }

    public static Task<NMSG_Response> GetOfficialVideoNMSGCommentAsync(
      NiconicoContext context,
      long threadId,
      long sub_threadId,
      int userId,
      string userKey,
      TimeSpan videoLength)
    {
      return CommentClient.GetOfficialVideoNMSGCommentDataAsync(context, threadId, sub_threadId, userId, userKey, videoLength).ContinueWith<NMSG_Response>((Func<Task<string>, NMSG_Response>) (prevTask => CommentClient.ParseNMSGResponseJson(prevTask.Result)));
    }

    private static NMSG_Response ParseNMSGResponseJson(string json)
    {
      List<object> source = JsonConvert.DeserializeObject<List<object>>(json);
      NMSG_Response nmsgResponseJson = new NMSG_Response();
      foreach (JObject jobject in source.Cast<JObject>())
      {
        JToken jtoken1;
        if (jobject.TryGetValue("thread", ref jtoken1))
        {
          if (nmsgResponseJson.Thread == null)
            nmsgResponseJson.Thread = jtoken1.ToObject<NGMS_Thread_Response>();
        }
        else
        {
          JToken jtoken2;
          if (jobject.TryGetValue("global_num_res", ref jtoken2))
          {
            if (nmsgResponseJson.GlobalNumRes == null)
              nmsgResponseJson.GlobalNumRes = jtoken2.ToObject<NGMS_GlobalNumRes>();
          }
          else
          {
            JToken jtoken3;
            if (jobject.TryGetValue("chat", ref jtoken3))
              nmsgResponseJson._CommentsSource.Add(jtoken3);
          }
        }
      }
      return nmsgResponseJson;
    }

    public static async Task<string> GetPostKeyAsync(
      NiconicoContext context,
      string threadId,
      int commentCount)
    {
      return await context.GetStringAsync("http://flapi.nicovideo.jp/api/getpostkey", new Dictionary<string, string>()
      {
        {
          "version",
          "1"
        },
        {
          "version_sub",
          "2"
        },
        {
          "thread",
          threadId
        },
        {
          "block_no",
          (commentCount / 100).ToString()
        },
        {
          "device",
          "1"
        },
        {
          "yugi",
          ""
        }
      });
    }

    public static string ParsePostKey(string getPostKeyResult)
    {
      return new string(getPostKeyResult.Skip<char>(CommentClient.PostKeyCharCount).ToArray<char>());
    }

    public static async Task<string> PostCommentDataAsync(
      NiconicoContext context,
      string commentServerUrl,
      string threadId,
      string ticket,
      int commentCount,
      string comment,
      TimeSpan position,
      string command)
    {
      string postKey = await CommentClient.GetPostKeyAsync(context, threadId, commentCount).ContinueWith<string>((Func<Task<string>, string>) (prevResult => CommentClient.ParsePostKey(prevResult.Result)));
      InfoResponse infoAsync = await context.User.GetInfoAsync();
      uint id = infoAsync.Id;
      bool isPremium = infoAsync.IsPremium;
      PostComment o = new PostComment()
      {
        user_id = id.ToString(),
        mail = command,
        thread = threadId,
        vpos = ((uint) position.TotalMilliseconds / 10U).ToString(),
        ticket = ticket,
        premium = isPremium.ToString1Or0(),
        postkey = postKey,
        comment = comment
      };
      string str = "";
      XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces(new XmlQualifiedName[1]
      {
        XmlQualifiedName.Empty
      });
      XmlSerializer xmlSerializer = new XmlSerializer(typeof (PostComment));
      XmlWriterSettings settings = new XmlWriterSettings()
      {
        OmitXmlDeclaration = true
      };
      using (MemoryStream output = new MemoryStream())
      {
        xmlSerializer.Serialize(XmlWriter.Create((Stream) output, settings), (object) o, namespaces);
        output.Flush();
        output.Seek(0L, SeekOrigin.Begin);
        using (StreamReader streamReader = new StreamReader((Stream) output))
          str = streamReader.ReadToEnd();
      }
      return await context.PostAsync(commentServerUrl, (IHttpContent) new HttpStringContent(str));
    }

    public static PostCommentResponse ParsePostCommentResult(string postCommentResult)
    {
      using (StringReader stringReader = new StringReader(postCommentResult))
        return (PostCommentResponse) new XmlSerializer(typeof (PostCommentResponse)).Deserialize((TextReader) stringReader);
    }

    public static Task<PostCommentResponse> PostCommentAsync(
      NiconicoContext context,
      string commentServerUrl,
      string threadId,
      string ticket,
      int commentCount,
      string comment,
      TimeSpan position,
      string commands)
    {
      return CommentClient.PostCommentDataAsync(context, commentServerUrl, threadId, ticket, commentCount, comment, position, commands).ContinueWith<PostCommentResponse>((Func<Task<string>, PostCommentResponse>) (prevResult => CommentClient.ParsePostCommentResult(prevResult.Result)));
    }
  }
}
