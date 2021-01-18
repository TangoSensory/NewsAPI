namespace HackerNewsAPI.Tests
{
    using HackerNewsAPI.Clients;
    using HackerNewsAPI.Common;
    using HackerNewsAPI.Model;
    using HackerNewsAPI.Repositories;
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
    public class StoryRepoTests : UnitTestBase
    {
        IStoryRepo sut;

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
