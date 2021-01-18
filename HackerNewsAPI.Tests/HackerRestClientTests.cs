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

    /// <summary>
    /// Sample Tests - not full coverage
    /// </summary>
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
        public async Task GetAll_T_ExecuteAsync_Is_Called_Once()
        {
            // Arrange 
            var dummyList = new List<T>();
            var dummyResponse = new RestResponse<IList<T>> { Data = dummyList };
            this.mockRestClient.Setup(x => x.ExecuteAsync<IList<T>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(dummyResponse);
            this.sut = new HackerRestClient(this.mockRestClient.Object, this.mockConfig.Object);

            // Act
            var result = await this.sut.GetAll<T>(base.DummyUrl);

            // Assert
            this.mockRestClient.Verify(x => x.ExecuteAsync<IList<T>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task GetAll_T_Null_path_Generates_AssertionException()
        {
            // Arrange 
            var dummyList = new List<T>();
            this.sut = new HackerRestClient(this.mockRestClient.Object, this.mockConfig.Object);

            // Act / Assert
            Assert.ThrowsAsync<NUnit.Framework.AssertionException>(async () => await this.sut.GetAll<T>(null));
        }

        [TestMethod]
        public async Task GetAll_T_Returns_Expected_List_T()
        {
            // Arrange 
            var dummyList = new List<T>();
            var dummyResponse = new RestResponse<IList<T>> { Data = dummyList };
            this.mockRestClient.Setup(x => x.ExecuteAsync<IList<T>>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(dummyResponse);
            this.sut = new HackerRestClient(this.mockRestClient.Object, this.mockConfig.Object);

            // Act
            var result = await this.sut.GetAll<T>(base.DummyUrl);

            // Assert
            Assert.IsTrue(result is List<T>);
            Assert.AreEqual(result, dummyList);
        }

        [TestMethod]
        public async Task SomeOtherTestsHere()
        {
            // Arrange 

            // Act

            // Assert
        }
    }
}
