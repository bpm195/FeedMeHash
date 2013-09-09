using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedMeHash
{
    public class Tweet :iTweet
    {
        public string FullName { get; private set; }
        public string TwitterName { get; private set; }
        public string Content { get; private set; }
        public string HashTags { get; private set; }
        public DateTime Date { get; private set; }

        public Tweet(string _fullName, string _twitterName, string _content, string _hashTags, string _date)
        {
            FullName = _fullName;
            TwitterName = _twitterName;
            Content = _content;
            HashTags = _hashTags;
            Date = DateTime.Parse(_date);
        }

        public bool Contains(string s)
        {
            s = s.ToLower();
            return(Content.ToLower().Contains(s)||HashTags.ToLower().Contains(s));
        }


    }
}