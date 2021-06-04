﻿
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NiconicoToolkit.SearchWithPage.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiconicoToolkit.UWP.Test.Tests
{
    [TestClass]
    public sealed class SearchHtmlTest
    {
        [TestInitialize]
        public async Task Initialize()
        {
            var creationResult = await AccountTestHelper.CreateNiconicoContextAndLogInWithTestAccountAsync();
            _context = creationResult.niconicoContext;
            _searchClient = _context.SearchWithPage;
        }

        NiconicoContext _context;
        SearchWithPage.SearchWithPageClient _searchClient;


        [TestMethod]
        [DataRow("モンハン")]
        public async Task VideoKeywordSearchAsync(string keyword)
        {
            var res = await _searchClient.Video.CreateQueryBuilder()
                .KeywordSearchAsync(keyword);

            Assert.IsTrue(res.IsStatusOK);

            if (res.Count > 0)
            {
                foreach (var sampleItem in res.List.Take(5))
                {
                    Assert.IsNotNull(sampleItem.Title);

                    if (!sampleItem.IsAdItem)
                    {
                        Assert.AreNotEqual(sampleItem.Length, TimeSpan.Zero);
                        Assert.AreNotEqual(sampleItem.FirstRetrieve, default(DateTime));
                        Assert.IsNotNull(sampleItem.ThumbnailUrl);
                        Assert.AreNotEqual(sampleItem.ViewCount, 0);
                    }
                }

                foreach (var tag in res.RelatedTags.Take(3))
                {
                    Assert.IsTrue(!string.IsNullOrWhiteSpace(tag));
                }
            }
        }

        [TestMethod]
        [DataRow("アニメ")]
        public async Task VideoTagSearchAsync(string keyword)
        {
            var res = await _searchClient.Video.CreateQueryBuilder()
                .TagSearchAsync(keyword);

            Assert.IsTrue(res.IsStatusOK);

            if (res.Count > 0)
            {
                foreach (var sampleItem in res.List.Take(5))
                {
                    Assert.IsNotNull(sampleItem.Title);

                    if (!sampleItem.IsAdItem)
                    {
                        Assert.AreNotEqual(sampleItem.Length, TimeSpan.Zero);
                        Assert.AreNotEqual(sampleItem.FirstRetrieve, default(DateTime));
                        Assert.IsNotNull(sampleItem.ThumbnailUrl);
                        Assert.AreNotEqual(sampleItem.ViewCount, 0);
                    }
                }

                foreach (var tag in res.RelatedTags.Take(3))
                {
                    Assert.IsTrue(!string.IsNullOrWhiteSpace(tag));
                }
            }
        }
    }
}
