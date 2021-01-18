namespace HackerNewsAPI.Tests
{
    using HackerNewsAPI.Clients;
    using HackerNewsAPI.Common;
    using HackerNewsAPI.Model;
    using HackerNewsAPI.Services;
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
    /// Tests omitted for brevity - see HackerRestClientTests/IntegrationTests for test examples
    /// </summary>
    [TestClass]
    public class StoryServiceTests : UnitTestBase
    {
        IStoryService sut;

        [TestInitialize]
        public void TestInit()
        {
        }

        [TestMethod]
        public async Task SomeTestsHere()
        {
            // Arrange 

            // Act

            // Assert
        }
    }
}
