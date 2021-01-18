namespace HackerNewsAPI.Repositories
{
    using FluentAssertions;
    using HackerNewsAPI.Common;
    using HackerNewsAPI.Model;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    public class StoryRepo : IStoryRepo
    {
        #region Constructors ---------------------------------------------------------------------------------------------------------------------
        public StoryRepo(IConfiguration configuration, IConcurrentFifoDictionary<int, Story> cache)
        {
            this.configuration = configuration;
            this.cache = cache;
            Task.Run(() => Init());
        }
        #endregion

        #region Fields ---------------------------------------------------------------------------------------------------------------------
        IConcurrentFifoDictionary<int, Story> cache;
        IConfiguration configuration;
        int defaultCacheCount;
        IMemoryCache memoryCache;
        #endregion

        #region Public Methods ---------------------------------------------------------------------------------------------------------------------
        public bool AddStory(Story story)
        {
            try
            {
                story.Should().NotBeNull();
                this.cache.GetOrAdd(story.Id, () => story);
            }
            catch (Exception ex)
            {
                // Handle exception
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks the repo for the supplied Id
        /// </summary>
        /// <param name="id">The integer Story Id to check for</param>
        /// <returns>bool. True if the Id is in the cache</returns>
        public bool CheckRepoContainsId(int id)
        {
            return this.cache.CheckKeyExists(id);
        }

        public bool Init()
        {
            string fromConf = null;

            try
            {
                fromConf = this.configuration[Constants.DefaultRetrieveCountConfigKey];
            }
            catch (Exception ex)
            {
                // Handle exception
                return false;
            }

            bool configParseResult;
            try
            {
                configParseResult = int.TryParse(fromConf, out this.defaultCacheCount);
                configParseResult.Should().BeTrue();

                return true;
            }
            catch (Exception ex)
            {
                // Handle exception
                return false;
            }
        }

        public IEnumerable<Story> RetrieveStories(int count = 0)
        {
            List<Story> result = null;

            try
            {
                result = this.cache.GetValues(count);
            }
            catch (Exception ex)
            {
                // Handle exception
            }

            return result;
        }
        #endregion
    }
}
