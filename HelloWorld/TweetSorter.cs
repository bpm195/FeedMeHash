using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedMeHash
{
    public class SortTweetsByFullName : IComparer<iTweet>
    {
        public int Compare(iTweet x, iTweet y)
        {
            return x.FullName.CompareTo(y.FullName);
        }
    }

    public class SortTweetsByTwitterName :IComparer<iTweet>
    {
        public int Compare(iTweet x, iTweet y)
        {
            return x.TwitterName.CompareTo(y.TwitterName);
        }
    }
    public class SortTweetsByDateAscending : IComparer<iTweet>
    {
        public int Compare(iTweet x, iTweet y)
        {
            return x.Date.CompareTo(y.Date);
        }
    }

    public class SortTweetsByDateDescending : IComparer<iTweet>
    {
        public int Compare(iTweet x, iTweet y)
        {
            return y.Date.CompareTo(x.Date);
        }
    }

    public class Unsorted : IComparer<iTweet>
    {
        public int Compare(iTweet x, iTweet y)
        {
            return 0;
        }
    }
}