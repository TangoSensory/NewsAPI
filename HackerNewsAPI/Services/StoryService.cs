namespace HackerNewsAPI.Services
{
    using FluentAssertions;
    using HackerNewsAPI.Clients;
    using HackerNewsAPI.Common;
    using HackerNewsAPI.Model;
    using HackerNewsAPI.Repositories;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    public class StoryService : IStoryService
    {
        #region Constructors ---------------------------------------------------------------------------------------------------------------------
        public StoryService(IHackerRestClient hackerClient, IConfiguration configuration, IStoryRepo storyRepo)
        {
            this.storyRepo = storyRepo;
            this.hackerClient = hackerClient;
            this.configuration = configuration;
            this.postfix = this.configuration["HackerApi:PathPostfix"];
            this.storyDetailPathPrefix = this.configuration["HackerApi:StoryDetailPathPrefix"];
            this.storiesPath = this.BuildStoriesPath();
            Task.Run(() => this.StartAutoRetrieveStories());
        }
        #endregion

        #region Fields ---------------------------------------------------------------------------------------------------------------------
        IConfiguration configuration;
        int defaultRetrieveCount;
        IHackerRestClient hackerClient;
        string postfix;
        int refreshRateInSeconds;
        string storiesPath;
        string storyDetailPathPrefix;
        IStoryRepo storyRepo;
        #endregion

        #region Public Methods ---------------------------------------------------------------------------------------------------------------------
        public void CheckForNewNewStories()
        {
            try
            {
                this.hackerClient.GetAllAsObservable<int>(this.storiesPath)
                    // TODO - will return unexpected reults when the ID wraps round to zero
                    .Select(x => x.OrderByDescending(x => x).Take(this.defaultRetrieveCount).Where(y => !this.storyRepo.CheckRepoContainsId(y)).ToList())
                    .Do(RetrieveNewStories)
                    .Subscribe();
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }

        public IEnumerable<Story> RetrieveBestStories()
        {
            return this.storyRepo.RetrieveStories(this.defaultRetrieveCount);
        }

        public async Task<Story> RetrieveStoryForId(int id)
        {
            string itemPath = null;
            Story result = null;
            try
            {
                itemPath = this.BuildStoryDetailPath(id);
                result = await this.hackerClient.Get<Story>(itemPath);
            }
            catch (Exception ex)
            {
                // Handle exception
            }

            return result;
        }
        #endregion

        #region Private Methods ---------------------------------------------------------------------------------------------------------------------
        private string BuildStoriesPath()
        {
            string storiesPath = null;

            try
            {
                storiesPath = this.configuration[Constants.StoriesPathConfigKey];
                storiesPath.Should().NotBeNullOrEmpty();
            }
            catch (Exception ex)
            {
                // Handle exception
            }

            return $"{storiesPath}{postfix}";
        }

        private string BuildStoryDetailPath(int id)
        {
            return $"{this.storyDetailPathPrefix}{id}{postfix}";
        }

        private async void RetrieveNewStories(IList<int> storyIds)
        {
            storyIds.Should().NotBeNull();

            foreach (int storyId in storyIds)
            {
                Story story = null;
                try
                {
                    story = await this.RetrieveStoryForId(storyId);

                    story.Should().NotBeNull();

                    var addStoryResult = this.storyRepo.AddStory(story);

                    addStoryResult.Should().BeTrue();
                }
                catch (Exception ex)
                {
                    // Handle exception
                }
            }
        }

        private void StartAutoRetrieveStories()
        {
            try
            {
                int.TryParse(this.configuration[Constants.RefreshRateInSecondsConfigKey], out this.refreshRateInSeconds);
                this.refreshRateInSeconds.Should().NotBe(default(int));

                int.TryParse(this.configuration[Constants.DefaultRetrieveCountConfigKey], out this.defaultRetrieveCount);
                this.defaultRetrieveCount.Should().NotBe(default(int));

                Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(this.refreshRateInSeconds))
                   .Do(x => this.CheckForNewNewStories())
                   .Subscribe();
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }
        #endregion
    }
}
