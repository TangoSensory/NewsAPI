namespace HackerNewsAPI.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HackerNewsAPI.Model;

    public interface IStoryRepo
    {
        bool AddStory(Story story);

        /// <summary>
        /// Checks the cache for the supplied Id
        /// </summary>
        /// <param name="id">The integer Story Id to check for</param>
        /// <returns>bool. True if the Id is in the cache</returns>
        bool CheckRepoContainsId(int id);
        bool Init();
        IEnumerable<Story> RetrieveStories(int count = 0);
    }
}
