using Microsoft.AspNetCore.Mvc;
using redis_explorations.Helpers;

namespace redis_explorations.Controllers
{
    [ApiController]
    [Route("greeting")]
    public class HelloWorldController
    {
        private readonly ICacheHelper cacheHelper;

        public HelloWorldController(ICacheHelper cacheHelper)
        {
            this.cacheHelper = cacheHelper;
        }

        [Route("{name}")]
        public string Get(string name)
        {
            if (cacheHelper.TryGetValue<string>(name, out var greeting))
            {
                Console.WriteLine($"{name} is already in cache... fetching from there.");
                return greeting!;
            }
            else
            {
                Console.WriteLine($"{name} is NOT in cache. Getting greeting from the database.");

                greeting = GetGreetingFromDatabase(name);

                cacheHelper.Set(name, greeting);
                return greeting;
            }
        }

        private string GetGreetingFromDatabase(string name)
        {
            // This method is a pretend database interaction
            // or any other resource intensive operation
            Thread.Sleep(5000);
            return $"Hello, {name}! I was created at {DateTime.Now.ToString("HH:mm:ss")}.";
        }
    }
}

