using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utilities.Web.OAuth;

namespace FeedMeHash
{
    class TwitterOAuth : OAuth
    {
        public const string TWITTER_URL = "https://api.twitter.com/1.1/search/tweets.json";
        private const string CONSUMER_KEY = "EWWibRMCZu7wSEKy8RI0Tg";
        private const string CONSUMER_SECRET_KEY = "xs2dEfS5b9APQPewsGaxf2PabTrri2FXHlJHPQT4";
        private const string TOKEN = "1725422946-ZmnNGfO1re6oUJ6cuoQt448BnMXVidsITRWvETI";
        private const string TOKEN_SECRET = "9UYuBvV7FhtOvUjZOsAAva15Vkb4Tak3AENNczZQ";

        private const Signature SIGNATURE_TYPE = Signature.HMACSHA1;
        private const HTTPMethod HTTP_METHOD = HTTPMethod.GET;

        public TwitterOAuth()
            : base()
        {
            SignatureType = SIGNATURE_TYPE;
            Method = HTTP_METHOD;
            Url = new Uri(TWITTER_URL);
            ConsumerKey = CONSUMER_KEY;
            ConsumerKeySecret = CONSUMER_SECRET_KEY;
            Token = TOKEN;
            TokenSecret = TOKEN_SECRET;
        }

        protected string generateSearchRequest(string searchParamaters) {
            AddParameter("q", searchParamaters);
            return GenerateRequest();
        }

        public string httpSearchRequest(string searchParameters)
        {
            string text;
            using (WebClient client = new WebClient())
            {
                text = client.DownloadString(generateSearchRequest(searchParameters));
            }

            return text;
        }

    }
}
