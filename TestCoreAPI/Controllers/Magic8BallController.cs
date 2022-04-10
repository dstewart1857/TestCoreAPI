using Microsoft.AspNetCore.Mvc;

namespace TestCoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Magic8BallController : Controller
    {
        [HttpGet(Name = "magic8Ball")]
        public String magic8Ball()
        {
            String result = "My Magic 8 Ball is Broken. :( -- Sadness...";

            Random random = new Random();
            int eightBall = random.Next(1, 8);

            if (eightBall == 1) {
                result = "It is certain!";
            }
            else if (eightBall == 2)
            {
                result = "Without a doubt!";
            }
            else if (eightBall == 3)
            {
                result = "Yes, definitely";
            }
            else if (eightBall == 4)
            {
                result = "Concentrate and think ask again";
            }
            else if (eightBall == 5)
            {
                result = "Ask again later";
            }
            else if (eightBall == 6)
            {
                result = "Don't count on it!";
            }
            else if (eightBall == 7)
            {
                result = "My sources say no!";
            }
            else if (eightBall == 8)
            {
                result = "Outlook not so good!";
            }

            return result;
        }
    }
}
