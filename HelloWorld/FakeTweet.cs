using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedMeHash
{
    public class FakeTweet : iTweet
    {
        public string FullName { get; private set; }
        public string TwitterName { get; private set; }
        public string Content { get; private set; }
        public string HashTags { get; private set; }
        public DateTime Date { get; private set; }

        public FakeTweet() { }
        public FakeTweet(string fullName, string twitterName, string content, string hashTags, DateTime date)
        {
            FullName = fullName;
            TwitterName = twitterName;
            Content = content;
            HashTags = hashTags;
            Date = date;
        }
    }
}