namespace HackerNewsAPI.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public static class Constants
    {
        public static readonly string HackerConfigRoot = "HackerApi";
        public static readonly string BaseUrlConfigKey = $"{HackerConfigRoot}:BaseUrl";
        public static readonly string StoriesPathConfigKey = $"{HackerConfigRoot}:StoriesPath";
        public static readonly string StoryDetailPathPrefixConfigKey = $"{HackerConfigRoot}:StoryDetailPathPrefix";
        public static readonly string PathPostfixConfigKey = $"{HackerConfigRoot}:PathPostfix";
        public static readonly string RefreshRateInSecondsConfigKey = $"{HackerConfigRoot}:RefreshRateInSeconds";
        public static readonly string StoriesInCacheConfigKey = $"{HackerConfigRoot}:StoriesInCache";
        public static readonly string DefaultRetrieveCountConfigKey = $"{HackerConfigRoot}:DefaultRetrieveCount";
    }
}
