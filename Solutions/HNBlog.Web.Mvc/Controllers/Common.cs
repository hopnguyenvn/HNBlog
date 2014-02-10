
using System.Text.RegularExpressions;
namespace HNBlog.Web.Mvc.Controllers
{
    public class Common
    {
        // this paging function is for future use
        private static int defaultLength = 200;
        // define pattern to cut the post content at the end of the sentence, pararaph, new line,...
        private static string pattern = @"(\n|\.\s)";
        // this keep the content make sence.
        /// <summary>
        /// Use this to make to get short content. 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ShortenString(string input)
        {
            if (input.Length <= defaultLength)
            {
                return input;
            }
            else
            {
                Regex reg = new Regex(pattern);
                Match match = reg.Match(input, defaultLength);
                if (match.Success)
                {
                    return input.Substring(0, match.Index);
                }
                else
                {
                    // return the full content
                    return input;
                }
            }
        }
    }
}