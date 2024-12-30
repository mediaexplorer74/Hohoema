// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.HttpClientExtensions
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Web.Http;

#nullable disable
namespace Mntone.Nico2
{
  internal static class HttpClientExtensions
  {
    public static Task<HttpResponseMessage> GetAsync(this HttpClient client, string uri)
    {
      return WindowsRuntimeSystemExtensions.AsTask<HttpResponseMessage, HttpProgress>(client.GetAsync(new Uri(uri)));
    }

    public static Task<string> GetStringAsync(this HttpClient client, string uri)
    {
      return WindowsRuntimeSystemExtensions.AsTask<string, HttpProgress>(client.GetStringAsync(new Uri(uri)));
    }

    public static Task<string> GetConvertedStringAsync(this HttpClient client, string uri)
    {
      return client.GetConvertedStringAsync(uri, Encoding.UTF8);
    }

    public static Task<string> GetConvertedStringAsync(
      this HttpClient client,
      string uri,
      Encoding encoding)
    {
      return WindowsRuntimeSystemExtensions.AsTask<IBuffer, HttpProgress>(client.GetBufferAsync(new Uri(uri))).ContinueWith<string>((Func<Task<IBuffer>, string>) (stream =>
      {
        byte[] array = WindowsRuntimeBufferExtensions.ToArray(stream.Result);
        return Encoding.UTF8.GetString(array, 0, array.Length);
      }));
    }

    public static Task<byte[]> GetByteArrayAsync(this HttpClient client, string uri)
    {
      return WindowsRuntimeSystemExtensions.AsTask<IBuffer, HttpProgress>(client.GetBufferAsync(new Uri(uri))).ContinueWith<byte[]>((Func<Task<IBuffer>, byte[]>) (stream => WindowsRuntimeBufferExtensions.ToArray(stream.Result)));
    }
  }
}
