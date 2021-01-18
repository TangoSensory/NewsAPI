namespace HackerNewsAPI.Tests
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Reactive.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    public class UnitTestBase : ReactiveTest
    {
        #region Fields ---------------------------------------------------------------------------------------------------------------------
        protected IConfiguration config;
        protected int DummyId = 1;

        protected T DummyT = new T();
        protected string DummyUrl = "dummyUrl";
        protected TestScheduler Scheduler = new TestScheduler();
        #endregion

        #region Public Methods ---------------------------------------------------------------------------------------------------------------------
        [TestInitialize]
        public void UnitTestBaseSetUp()
        {
            this.config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }
        #endregion
    }

    public class T { }
}
