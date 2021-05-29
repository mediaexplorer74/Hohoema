﻿using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NiconicoToolkit.Rss.Video
{
    public class RssVideoResponse
    {
        public bool IsOK { get; set; }

        public Uri Link { get; set; }

        public string Language { get; set; }

        public List<RssVideoData> Items { get; set; }
    }

    public class RssVideoData
    {
        public string RawTitle { get; set; }

        public Uri WatchPageUrl { get; set; }

        public DateTimeOffset PubDate { get; set; }

        public string Description { get; set; }

    }

    public class RankingVideoMoreData
    {
        public string Title { get; set; }
        public TimeSpan Length { get; set; }
        public string ThumbnailUrl { get; set; }
        public int WatchCount { get; set; }
        public int CommentCount { get; set; }
        public int MylistCount { get; set; }
    }


    public static class RankingRssDataExtensions
    {
        public static string GetVideoId(this RssVideoData data)
        {
            return data.WatchPageUrl.Segments.Last();
        }

        public static string GetRankTrimmingTitle(this RssVideoData data)
        {
            var index = data.RawTitle.IndexOf('：');
            return data.RawTitle.Substring(index + 1);
        }

        public static RankingVideoMoreData GetMoreData(this RssVideoData data)
        {
            /* data.Description 
             
             <p class="nico-thumbnail"><img alt="異世界かるてっと 第12話" src="https://nicovideo.cdn.nimg.jp/thumbnails/35292329/35292329.7495360" width="94" height="70" border="0"/></p>
             <p class="nico-description">動画一覧はこちら第11話 watch/1560410886「Nアニメ」無料動画や最新情報・生放送・マ</p>
             <p class="nico-info"><small><strong class="nico-info-length">11:50</strong>｜<strong class="nico-info-date">2019年06月26日 00：45：00</strong> 投稿<br/><strong>合計</strong>&nbsp;&#x20;再生：<strong class="nico-info-total-view">81,344</strong>&nbsp;&#x20;コメント：<strong class="nico-info-total-res">3,555</strong>&nbsp;&#x20;マイリスト：<strong class="nico-info-total-mylist">600</strong></small></p>
             
             */

            var lines = data.Description.Split(separator: new char[] { '\n' }, options: StringSplitOptions.RemoveEmptyEntries);

            var parser = new HtmlParser();

            var thumbnailNode = parser.ParseFragment(lines[0].TrimStart(), null).First() as Node;
            var img = thumbnailNode.GetNodes<IHtmlImageElement>(deep: true).First();

            var infoNode = parser.ParseFragment(lines[2].TrimStart(), null).First() as Element;
            var infoContainer = infoNode.QuerySelectorAll("strong");

            RankingVideoMoreData moreData = new()
            {
                Title = img.GetAttribute("alt"),
                ThumbnailUrl = img.GetAttribute("src"),
            };

            // ex) 2019年06月26日 00：45：00
            //var dateNode = infoContainer.GetElementByClassName("nico-info-date");
            // ex) 81,344

            // ex) 11:50
            foreach (var node in infoContainer)
            {
                if (node.ClassName == "nico-info-length")
                {
                    moreData.Length = node.TextContent.ToTimeSpan();
                }
                else if (node.ClassName == "nico-info-total-view")
                {
                    moreData.WatchCount = int.Parse(new string(node.TextContent.Where(c => Char.IsDigit(c)).ToArray()));
                }
                else if (node.ClassName == "nico-info-total-res")
                {
                    moreData.CommentCount = int.Parse(new string(node.TextContent.Where(c => Char.IsDigit(c)).ToArray()));
                }
                else if (node.ClassName == "nico-info-total-mylist")
                {
                    moreData.MylistCount = int.Parse(new string(node.TextContent.Where(c => Char.IsDigit(c)).ToArray()));
                }
            }

            return moreData;
        }
    }
}
