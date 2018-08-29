using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Parser.Html;
using NetCore.AngleSharpProgram.Common.Util;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetCore.AngleSharpProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo();
        }
        private static void Demo()
        {
            // 设置配置以支持文档加载
            var config = Configuration.Default.WithDefaultLoader();
            string mainSite = "https://www.cnblogs.com/";

            // 请求博客园首页
            var mainSiteDocument = BrowsingContext.New(config).OpenAsync(mainSite);

            var lastPageSelector = mainSiteDocument.Result.QuerySelector("#paging_block > .pager > a.last");
            var totlaPage = 0;
            if (null != lastPageSelector)
            {
                Int32.TryParse(lastPageSelector.TextContent, out totlaPage);
            }

            if (totlaPage == 0)
            {
                return;
            }

            for (var i = 1; i <= totlaPage; i++)
            {
                var eachPageUrl = $"https://www.cnblogs.com/#p{i}";
                var eachPageDocument = BrowsingContext.New(config).OpenAsync(eachPageUrl);

                var itemSelector = eachPageDocument.Result.QuerySelectorAll("#post_list > .post_item");
                foreach (var item in itemSelector)
                {
                    CNBlog cNBlog = GetCNBlog(item);
                    if (null == cNBlog)
                    {
                        continue;
                    }

                    string message = Newtonsoft.Json.JsonConvert.SerializeObject(cNBlog);
                    Console.WriteLine(message);
                    RabbitUtil.Send(message);
                }
            }
        }

        private static CNBlog GetCNBlog(IElement item)
        {
            CNBlog cNBlog = null;

            try
            {
                cNBlog = new CNBlog();

                cNBlog.BlogType = "首页";

                // 推荐数
                var diggNumStr = item.QuerySelector(".diggit > .diggnum").TextContent;
                int diggNum = 0;
                int.TryParse(diggNumStr, out diggNum);
                cNBlog.DiggNum = diggNum;

                var titleLnk = item.QuerySelector(".post_item_body .titlelnk");

                // 博客标题
                cNBlog.BlogTitle = titleLnk.TextContent;

                // 博客地址
                cNBlog.BlogUrl = titleLnk.GetAttribute("href");

                // 博客摘要
                cNBlog.Summary = item.QuerySelector(".post_item_body .post_item_summary").TextContent;

                // 发布人
                cNBlog.Publisher = item.QuerySelector(".post_item_body .post_item_foot > a.lightblue").TextContent;

                var itemFoot = item.QuerySelector(".post_item_body .post_item_foot");

                // 发布时间
                DateTime publishTime = DateTime.Now;
                var publishTimeStr = ((AngleSharp.Dom.Html.IHtmlElement)itemFoot).InnerText;
                if (!string.IsNullOrEmpty(publishTimeStr))
                {
                    int startIndex = publishTimeStr.IndexOf("于");
                    int endIndex = publishTimeStr.IndexOf("评");

                    publishTimeStr = publishTimeStr.Substring(startIndex + 1, endIndex - startIndex - 1);

                    DateTime.TryParse(publishTimeStr.Trim(), out publishTime);
                }

                cNBlog.PublishTime = publishTime.DataTimeToLong();

                // 评论数
                var commentsNumStr = itemFoot.QuerySelector(".article_comment > a.gray").TextContent;
                if (!string.IsNullOrWhiteSpace(commentsNumStr))
                {
                    commentsNumStr = commentsNumStr.Replace(")", "").Trim();
                    commentsNumStr = commentsNumStr.Substring(3);
                }
                int commentsNum = 0;
                int.TryParse(commentsNumStr, out commentsNum);
                cNBlog.CommentNum = commentsNum;

                // 阅读数
                var readNumStr = itemFoot.QuerySelector(".article_view > a.gray").TextContent;
                if (!string.IsNullOrEmpty(readNumStr))
                {
                    readNumStr = readNumStr.Replace(")", "").Trim();
                    readNumStr = readNumStr.Substring(3);
                }
                int readNum = 0;
                int.TryParse(readNumStr, out readNum);
                cNBlog.ReadNum = readNum;
            }
            catch (Exception e)
            {

                throw e;
            }

            return cNBlog;
        }

    }
}
