using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Joking
{
    class Entity
    {
        public string Error { get; set; }
    }
    class DadJoke : Entity
    {
        public string Id { get; set; }
        public string Joke { get; set; }
        public string Status { get; set; }

        static public DadJoke construct(string responseString)
        {
            DadJoke joke;

            try
            {
                var jss = new JavaScriptSerializer();
                joke = jss.Deserialize<DadJoke>(responseString);
            }
            catch (Exception ex)
            {
                joke = new DadJoke();
                joke.Error = ex.Message;
            }

            return joke;
        }
    }

    class MotivationalQuote : Entity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Link { get; set; }

        static public MotivationalQuote construct(string responseString)
        {
            MotivationalQuote quote;

            try
            {
                var jss = new JavaScriptSerializer();
                List<MotivationalQuote> quotes = jss.Deserialize<List<MotivationalQuote>>(responseString);

                quote = quotes.First<MotivationalQuote>();
            }
            catch (Exception ex)
            {
                quote = new MotivationalQuote();
                quote.Error = ex.Message;
            }

            return quote;
        }
    }
}
