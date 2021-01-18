namespace HackerNewsAPI.Clients
{
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using FluentAssertions;
    using HackerNewsAPI.Common;
    using System.Threading;

    public class HackerRestClient : IHackerRestClient
    {
        #region Constructors ---------------------------------------------------------------------------------------------------------------------
        public HackerRestClient(IRestClient client, IConfiguration configuration)
        {
            this.client = client;
            this.baseUrl = configuration[Constants.BaseUrlConfigKey];
            this.postfix = configuration[Constants.PathPostfixConfigKey];
            this.client.BaseUrl = new Uri(this.baseUrl);
        }
        #endregion

        #region Fields ---------------------------------------------------------------------------------------------------------------------
        string baseUrl;
        IRestClient client;
        string postfix;
        #endregion

        #region Public Methods ---------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Send a request via GET to retrieve a response of type T
        /// </summary>
        /// <param name="url">API Url string</param>
        /// <returns>A Task with result object of type T</returns>
        public async Task<T> Get<T>(string path)
        {
            path.Should().NotBeNullOrEmpty();

            IRestResponse<T> result = null;
            IRestRequest request = null;

            try
            {
                request = new RestRequest(path, Method.GET);
                result = await this.client.ExecuteAsync<T>(request);

                result.Should().NotBeNull();
            }
            catch (Exception ex)
            {
                // Handle exception
            }

            return result.Data;
        }

        /// <summary>
        /// Send a request via GET to retrieve a collection of all objects
        /// </summary>
        /// <param name="url">API Url string</param>
        /// <returns>A Task with result object of type IEnumerable<T></returns>
        public async Task<IList<T>> GetAll<T>(string path)
        {
            path.Should().NotBeNullOrEmpty();

            IRestResponse<IList<T>> result = null;
            IRestRequest request = null;

            try
            {
                request = new RestRequest(path, Method.GET);
                result = await this.client.ExecuteAsync<IList<T>>(request, cancellationToken: default(CancellationToken));

                result.Should().NotBeNull();
            }
            catch (Exception ex)
            {
                // Handle exception
            }

            return result.Data;
        }

        /// <summary>
        /// Send a request via GET and return an Observable
        /// </summary>
        /// <param name="path">API Url path string</param>
        /// <returns>An observable result object of type IEnumerable<T></returns>
        public IObservable<IList<T>> GetAllAsObservable<T>(string path)
        {
            path.Should().NotBeNullOrEmpty();

            try
            {
                //TODO - Untestable as is. Would need a wrapper
                return Observable.FromAsync(async () => await GetAll<T>(path));
            }
            catch (Exception ex)
            {
                // Handle exception
                throw ex;
            }
        }
        #endregion
    }
}
