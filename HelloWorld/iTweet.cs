using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedMeHash
{
    public interface iTweet
    {
        string FullName { get; }
        string TwitterName { get; }
        string Content { get; }
        string HashTags { get; }
        DateTime Date { get; }
        bool Contains(string s);
    }
}