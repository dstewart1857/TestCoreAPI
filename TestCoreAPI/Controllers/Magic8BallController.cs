using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;

namespace TestCoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Magic8BallController : Controller
    {
        private readonly static ImmutableArray<string> responses = ImmutableArray.Create(new[] {
                "It is certain!",
                "Without a doubt!",
                "Yes, definitely",
                "Concentrate and think, ask again",
                "Ask again later",
                "Don't count on it!",
                "My sources say no!",
                "Outlook not so good!",
                "Signs point to yes",
                "Better not tell you now",
                "You may rely on it",
                "Cannot predict now"
        });

        [HttpGet(Name = "magic8Ball")]
        public String magic8Ball()
        {
            String result;
            Random random = new Random();
            int selection = random.Next(responses.Length);
            try
            {
                result = responses[selection];
            }
            catch (System.IndexOutOfRangeException)
            {
                result = "My Magic 8 Ball is Broken. :( -- Sadness...";
            }

            return result;
        }
    }
}
