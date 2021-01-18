namespace HackerNewsAPI
{
    using HackerNewsAPI.Clients;
    using HackerNewsAPI.Model;
    using HackerNewsAPI.Repositories;
    using HackerNewsAPI.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Startup
    {
        #region Constructors ---------------------------------------------------------------------------------------------------------------------
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        #region Public Properties ---------------------------------------------------------------------------------------------------------------------
        public IConfiguration Configuration { get; }
        #endregion

        #region Public Methods ---------------------------------------------------------------------------------------------------------------------
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IStoryService storyService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IRestClient, RestClient>();
            services.AddTransient<IHackerRestClient, HackerRestClient>();
            services.AddSingleton<IStoryService, StoryService>();
            services.AddSingleton<IStoryRepo, StoryRepo>();
            services.AddSingleton<IConcurrentFifoDictionary<int, Story>, ConcurrentFifoDictionary<int, Story>>();
        }
        #endregion
    }
}
