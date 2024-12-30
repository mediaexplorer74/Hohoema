// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Users.NG.NGClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Users.NG
{
  internal sealed class NGClient
  {
    public static Task<string> GetNGCommentDataAsync(NiconicoContext context)
    {
      new Dictionary<string, string>() { { "mode", "get" } };
      return context.GetClient().GetStringAsync("http://flapi.nicovideo.jp/api/configurengclient?mode=get");
    }

    public static Task<string> AddNGCommentDataAsync(
      NiconicoContext context,
      NGCommentType type,
      string source)
    {
      return context.PostAsync("http://flapi.nicovideo.jp/api/configurengclient", new Dictionary<string, string>()
      {
        {
          "mode",
          "add"
        },
        {
          "language",
          "0"
        },
        {
          nameof (type),
          type.ToString()
        },
        {
          nameof (source),
          source
        }
      });
    }

    public static Task<string> DeleteNGCommentDataAsync(
      NiconicoContext context,
      NGCommentType type,
      string source)
    {
      return context.PostAsync("http://flapi.nicovideo.jp/api/configurengclient", new Dictionary<string, string>()
      {
        {
          "mode",
          "delete"
        },
        {
          "language",
          "0"
        },
        {
          nameof (type),
          type.ToString()
        },
        {
          nameof (source),
          source
        }
      });
    }

    private static T ParseNGCommentListXml<T>(string xml) where T : NGCommentResponseCore
    {
      using (StringReader stringReader = new StringReader(xml))
        return (T) new XmlSerializer(typeof (T)).Deserialize((TextReader) stringReader);
    }

    public static Task<NGCommentResponse> GetNGCommentAsync(NiconicoContext context)
    {
      return NGClient.GetNGCommentDataAsync(context).ContinueWith<NGCommentResponse>((Func<Task<string>, NGCommentResponse>) (prevTask => NGClient.ParseNGCommentListXml<NGCommentResponse>(prevTask.Result)));
    }

    public static Task<NGCommentResponseCore> AddNGCommentAsync(
      NiconicoContext context,
      NGCommentType type,
      string source)
    {
      return NGClient.AddNGCommentDataAsync(context, type, source).ContinueWith<NGCommentResponseCore>((Func<Task<string>, NGCommentResponseCore>) (prevTask => NGClient.ParseNGCommentListXml<NGCommentResponseCore>(prevTask.Result)));
    }

    public static Task<NGCommentResponseCore> DeleteNGCommentAsync(
      NiconicoContext context,
      NGCommentType type,
      string source)
    {
      return NGClient.DeleteNGCommentDataAsync(context, type, source).ContinueWith<NGCommentResponseCore>((Func<Task<string>, NGCommentResponseCore>) (prevTask => NGClient.ParseNGCommentListXml<NGCommentResponseCore>(prevTask.Result)));
    }
  }
}
