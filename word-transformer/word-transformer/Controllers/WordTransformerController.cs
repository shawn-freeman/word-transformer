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
            var startTime = DateTime.Now;
            long maxRetry = 0;
            if (word1.Count() != word2.Count()) return BadRequest("Words are not of equal length.");

            word1 = word1.ToUpper();
            word2 = word2.ToUpper();
            //calc the max permutations
            maxRetry = GetMaxPermutations(word1.Length);

            var dictionary = new Dictionary<string, long>();
            dictionary.AddWord(word1);
            dictionary.AddWord(word2);

            Random random = new Random();
            var currentStep = 0;
            
            while (currentStep < steps)
            {
                //determine word to add
                var index = Convert.ToInt64(Math.Floor(random.NextDouble() * dictionary.Keys.Count()) );
                var word = dictionary.First(d => d.Value == index).Key;

                var retry = 0;
                var done = false;
                while (retry < maxRetry && !done)
                {
                    string newWord = word;
                    try
                    {
                        var sbWord = new StringBuilder(newWord);
                        var charIndex = Convert.ToInt32(Math.Floor(random.NextDouble() * sbWord.Length));

                        sbWord[charIndex] = characters[Convert.ToInt32(Math.Floor(random.NextDouble() * characters.Length))];
                        newWord = sbWord.ToString();

                        var exists = dictionary[newWord];
                        retry++;
                    }
                    catch (Exception ex)
                    {
                        dictionary.AddWord(newWord);
                        done = true;
                        retry = 0;
                    }
                }
                Console.WriteLine($"DictionaryCount:{dictionary.Count()}");
                currentStep++;
            }

            //output total processing time to console
            var totalTime = DateTime.Now - startTime;
            Console.WriteLine();
            Console.WriteLine($"TotalTime:{totalTime}");

            return Ok(dictionary.Keys.Aggregate((agg, val) => agg += $", {val}"));
        }

        private long GetMaxPermutations(long length)
        {
            long val = 1;
            for (var i = 1; i <= length; i++ )
            {
                val = val * characters.Length;
            }

            return val;
        }
    }
}
