using HackerNewsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackerNewsAPI.Model;
using System.Reflection;

namespace HackerNewsAPI.API
{
    [ApiController]
    public class BestStoriesController : ControllerBase
    {
        #region Constructors ---------------------------------------------------------------------------------------------------------------------
        public BestStoriesController(IStoryService storyService, ILogger<BestStoriesController> logger)
        {
            this.storyService = storyService;
            this.logger = logger;
        }
        #endregion

        #region Fields ---------------------------------------------------------------------------------------------------------------------
        ILogger<BestStoriesController> logger;
        IStoryService storyService;
        #endregion

        #region Public Methods ---------------------------------------------------------------------------------------------------------------------
        [HttpGet]
        [Route("api/beststories")]
        public IActionResult Get()
        {
            IEnumerable<Story> stories = null;
            try
            {
                stories = this.storyService.RetrieveBestStories();
            }
            catch (Exception ex)
            {
                this.logger.LogCritical($"{MethodBase.GetCurrentMethod().Name} - Esception - {ex.Message}");
                // Handle exception
                return BadRequest("Retrieve failed");
            }

            return Ok(stories);
        }
        #endregion
    }
}
