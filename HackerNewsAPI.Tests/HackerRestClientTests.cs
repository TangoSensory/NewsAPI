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
    using System.Threading;
    using System.Threading.Tasks;
    using Assert = NUnit.Framework.Assert;

    [TestClass]
    public class HackerRestClientTests : UnitTestBase
    {
        Mock<IRestClient> mockRestClient = new Mock<IRestClient>();
        Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
        IHackerRestClient sut;

        [TestInitialize]
        public void TestInit()
        {
            this.mockConfig.SetupGet(x => x[Constants.BaseUrlConfigKey]).Returns(base.config[Constants.BaseUrlConfigKey]);
            this.mockConfig.SetupGet(x => x[Constants.PathPostfixConfigKey]).Returns(base.config[Constants.PathPostfixConfigKey]);
        }

        [TestMethod]
        public async Task GetAll_T_Returns_Expected_IEnumerable_T()
        {
            // Arrange 
            var dummyList = new List<T>();
            var dummyResponse = new RestResponse<IEnumerable<T>> { Data = dummyList };
            this.mockRestClient.Setup(x => x.ExecuteAsync<IEnumerable<T>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(dummyResponse);
            this.sut = new HackerRestClient(this.mockRestClient.Object, this.mockConfig.Object);

            // Act
            var result = await this.sut.GetAll<T>(base.DummyUrl);

            // Assert
            Assert.IsTrue(result is List<T>);
            Assert.AreEqual(result, dummyList);
        }
    }
}
