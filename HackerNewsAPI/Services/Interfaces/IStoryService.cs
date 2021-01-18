namespace HackerNewsAPI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HackerNewsAPI.Model;

    public interface IStoryService
    {
        void CheckForNewNewStories();
        IEnumerable<Story> RetrieveBestStories();
        Task<Story> RetrieveStoryForId(int id);
    }
}
