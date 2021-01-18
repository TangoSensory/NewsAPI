namespace HackerNewsAPI.Tests
{
    using HackerNewsAPI.Clients;
    using HackerNewsAPI.Common;
    using HackerNewsAPI.Model;
    using Microsoft.Extensions.Configuration;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Assert = NUnit.Framework.Assert;
     
    [TestClass]
    public class HackerRestClientIntegrationTests : UnitTestBase
    {
        Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
        IHackerRestClient sut;

        [TestInitialize]
        public void TestInit()
        {
            this.mockConfig.SetupGet(x => x[Constants.BaseUrlConfigKey]).Returns(base.config[Constants.BaseUrlConfigKey]);
            this.mockConfig.SetupGet(x => x[Constants.PathPostfixConfigKey]).Returns(base.config[Constants.PathPostfixConfigKey]);
            this.sut = new HackerRestClient(new RestClient(), this.mockConfig.Object);
        }

        [TestMethod]
        public async Task GetAll_int_Returns_IList_int_Non_Empty()
        {
            // Arrange
            var path = base.config[Constants.StoriesPathConfigKey] + base.config[Constants.PathPostfixConfigKey];

            // Act 
            var result = await this.sut.GetAll<int>(path);

            // Assert
            Assert.IsTrue(result is IList<int>);
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public async Task Get_Story_Returns_Correct_Story()
        {
            // Arrange
            var dummyItemId = 25771022;
            var path = $"{base.config[Constants.StoryDetailPathPrefixConfigKey]}{dummyItemId}{base.config[Constants.PathPostfixConfigKey]}";

            // Act
            var result = await this.sut.Get<Story>(path);

            // Assert
            Assert.IsTrue(result is Story);
            Assert.IsTrue(result.Id == dummyItemId);
        }
    }
}
