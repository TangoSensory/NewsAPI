# NewsAPI
An API wrapper for the Hacker News API

## Instructions
- Open HackerNewsAPI_sln in Visual Studio 2019 (or later). 
- Launch using F5. A browser will launch with the correct URL and the data will be displayed in JSON format. 
- *** NB Please be patient. The BestStories data may take up to a minute to populate fully, following application launch ***
- The data can be consumed via the browser (F5 to refresh) or via a suitable web client using the URL shown initially in the browser

## Assumptions
- Site encryption (SSL) not required
- User auth not required
- Logging/auditing not required
- Analytics not required
- Multithreading support not in scope
- Publishing to a web-server not in scope
- Persistent data store not required
- It is assumed that the user has access to and knowledge of Visual Studio 2019 (or later)
- It is assumed that the user is familiar with JSON
- The supplied news items may be up to 30 seconds out-of-date compared to Hacker News
- Using a background service to constantly check for and retrieve new stories, may not be optimal if the API is accessed infrequently
- Later Stories have higher Id's. At some point the Story Id's will cycle back to 0 but handling this scenario has not been included in the code
- A Repository was used in place of a MemoryCache, as this approach offers more flexibility in terms of data store options
- The ConcurrentFifiDictionary "cache" used by the StoryRepo is 3rd party (tweaked) and not fully tested

## Time/context related limitations
- The Solution's Project structure is simplified. For production it would likely be split into API, Data, Shared/Services
- Test coverage is limited to provide unit test examples only and omits integration testing
- Test project structure is simplified
- All dev will be completed on the main branch
- XML comments are limited to provide examples only
- The HackerRestClient uses a static Observable method that is not testable as is. A wrapper would be required
