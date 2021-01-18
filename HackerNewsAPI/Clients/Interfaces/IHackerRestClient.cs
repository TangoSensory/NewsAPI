namespace HackerNewsAPI.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public interface IHackerRestClient
    {
        /// <summary>
        /// Send a request via GET to retrieve a response of type T
        /// </summary>
        /// <param name="url">API Url string</param>
        /// <returns>A Task with result object of type T</returns>
        Task<T> Get<T>(string path);

        /// <summary>
        /// Send a request via GET to retrieve a collection of all objects
        /// </summary>
        /// <param name="url">API Url string</param>
        /// <returns>A Task with result object of type IEnumerable<T></returns>
        Task<IList<T>> GetAll<T>(string path);

        /// <summary>
        /// Send a request via GET and return an Observable
        /// </summary>
        /// <param name="path">API Url path string</param>
        /// <returns>An observable result object of type IEnumerable<T></returns>
        IObservable<IList<T>> GetAllAsObservable<T>(string path);
    }
}
