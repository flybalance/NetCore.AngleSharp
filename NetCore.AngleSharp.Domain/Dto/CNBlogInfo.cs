namespace NetCore.AngleSharpProgram
{
    /// <summary>
    /// 博客信息实体
    /// </summary>
    public class CNBlog
    {
        /// <summary>
        /// 博客分类
        /// </summary>
        public string BlogType { get; set; }

        /// <summary>
        /// 博客标题
        /// </summary>
        public string BlogTitle { get; set; }

        /// <summary>
        /// 博客摘要
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 博客地址
        /// </summary>
        public string BlogUrl { get; set; }

        /// <summary>
        /// 发布人
        /// </summary>
        public string Publisher { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public long PublishTime { get; set; }

        /// <summary>
        /// 推荐数
        /// </summary>
        public int DiggNum { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        public int CommentNum { get; set; }

        /// <summary>
        /// 阅读数
        /// </summary>
        public int ReadNum { get; set; }
    }
}
