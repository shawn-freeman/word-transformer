using Microsoft.AspNetCore.Mvc;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace word_transformer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordTransformerController : Controller
    {
        public string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        [HttpGet]
        public IActionResult Get(string word1, string word2, long steps)
        {
            long maxRetry = 0;
            if (word1.Count() != word2.Count()) return BadRequest("Words are not of equal length.");

            //calc the max permutations
            maxRetry = GetMaxPermutations(word1.Length);

            var dictionary = new Dictionary<long, string>();
            dictionary.AddWord(word1);
            dictionary.AddWord(word2);

            Random random = new Random();
            var currentStep = 0;
            var retry = 0;
            while (currentStep < steps)
            {
                //determine word to add
                var index = Convert.ToInt64(Math.Floor(random.NextDouble() * dictionary.Keys.Count()) );
                var sbWord = new StringBuilder(dictionary.First(d => d.Key == index).Value);

                var charIndex = Convert.ToInt64(Math.Floor(random.NextDouble() * sbWord.Length));

                sbWord[charIndex] = characters[Convert.ToInt64(Math.Floor(random.NextDouble() * characters.Length))];
                var newWord = sbWord.ToString();

                if (!dictionary.ContainsValue(newWord))
                {
                    dictionary.AddWord(newWord);
                    currentStep++;
                    retry = 0;
                }
                else
                {
                    retry++;
                    if (retry == maxRetry) return BadRequest("Threshold Reached");
                    Console.WriteLine(retry.ToString());
                }
            }

            return Ok(dictionary.Values.Aggregate((agg, val) => agg += $", {val}"));
        }

        private long GetMaxPermutations(long length)
        {
            long val = 1;
            for (var i = 1; i <= length; i++ )
            {
                val = val * characters.Length;
            }

            val = val * val;
            return val;
        }
    }
}
