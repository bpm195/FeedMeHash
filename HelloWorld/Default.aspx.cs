﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Collections;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;



namespace FeedMeHash
{

    public partial class WebForm1 : System.Web.UI.Page
    {
        protected readonly char[] _searchSeparators = {' ',','};
        protected readonly string acceptableHashTagCharactersRegEx = @"[A-Za-z0-9_]";

        private int _index = 0;
        protected int Index
        {
            get { return ++_index; }
        }
        protected IComparer<iTweet> _sortType = new Unsorted();

        protected void Page_Load(object sender, EventArgs e) { }

        protected List<iTweet> FakeFindTweets(String hashTags)
        {
            var tweets = new List<iTweet>();
            tweets.Add(new FakeTweet("Twitter Guy", "Twitter Guy", "Something important about kittens", "#kittens, #awesomesauce", DateTime.Now));
            tweets.Add(new FakeTweet("Twitter Gurl", "Twitter Gurl", "Some content", "#kittens", DateTime.Now));
            tweets.Add(new FakeTweet("Twitter Dude", "TD1337", "stuff", "#awesomesauce", DateTime.Now));

            return tweets;
        }
        protected List<iTweet> FindTweets(String dirtySearch)
        {
            string cleanSearch = createValidEncodedSearchString(dirtySearch);
            if (cleanSearch ==string.Empty){
                return new List<iTweet>();
            }

            TwitterOAuth twitterHelper = new TwitterOAuth();
            string json = twitterHelper.httpSearchRequest(cleanSearch);

            return jsonToTweets(json);
        }
        protected string createValidEncodedSearchString(string dirtyString)
        {
            string[] hashTags = dirtyString.Split(_searchSeparators);
            string cleanString = string.Empty;
            string regex = String.Format("^{0}+$", acceptableHashTagCharactersRegEx);

            foreach (string hashTag in hashTags)
            {
                string mutableHashTag = hashTag;
                if (String.IsNullOrEmpty(hashTag))
                {
                    continue;
                }
                if (mutableHashTag.StartsWith("#"))
                {
                    if (mutableHashTag.Length == 1)
                    {
                        continue;
                    }
                    mutableHashTag = mutableHashTag.Substring(1, mutableHashTag.Length - 1);
                }
                if (!Regex.IsMatch(mutableHashTag, regex))
                {
                    continue;
                }
                cleanString += String.Format("#{0} ", mutableHashTag);
            }
            return HttpUtility.UrlEncode(cleanString, System.Text.Encoding.UTF8);
        }

        protected List<iTweet> jsonToTweets(string json)
        {
            List<iTweet> tweets = new List<iTweet>();
            JObject root = JObject.Parse(json);
            foreach (var status in root["statuses"])
            {
                string creationTime = DateTime.Now.ToString();
                string fullName = status["user"]["name"].Value<string>();
                string twitterName = status["user"]["screen_name"].Value<string>();
                string tweetContent = status["text"].Value<string>();
                string hashTags = jsonHashTagsToHumanIntelligible(status["entities"]["hashtags"]);

                tweets.Add(new Tweet(fullName, twitterName, tweetContent, hashTags, creationTime));
            }
            return tweets;
        }

        protected string jsonHashTagsToHumanIntelligible(JToken json)
        {
            string hashTags = string.Empty;
            foreach (var hashTag in json){
                hashTags += string.Format("#{0} ",hashTag["text"]);
            }

            hashTags = hashTags.Trim();
            if (hashTags == string.Empty)
            {
                return " ";
            }
            return hashTags;
        }


        protected void AttachTweets(List<iTweet> tweets, PlaceHolder tweetHolder)
        {
            tweets.Sort(_sortType);
            tweetHolder.Controls.Clear();
            foreach (iTweet tweet in tweets)
            {
                tweetHolder.Controls.Add(FormatTweet(tweet));
            }
        }

        protected void setSort(String _type)
        {
            _type = _type.ToLower();
            switch (_type)
            {
                case "twitter name": _sortType = new SortTweetsByTwitterName(); break;
                case "full name": _sortType = new SortTweetsByFullName(); break;
                case "data ascending": _sortType = new SortTweetsByDateAscending(); break;
                case "data descending": _sortType = new SortTweetsByDateDescending(); break;
                default: _sortType = new Unsorted(); break;
            }

        }

        protected HtmlControl FormatTweet(iTweet tweet)
        {
            var control = new HtmlGenericControl("div");
            control.ID = "Tweet" + Index;
            var innerpanel = new Panel();
            innerpanel.Attributes.Add("style", "background-color: #DDDDDD ;border:black 3px; border-style:solid; margin:0 auto; width:80%");

            var nameLabel = new Label();
            nameLabel.Attributes.Add("style", "font-size:24px");
            nameLabel.Text = String.Format("{0} (@{1})", tweet.FullName, tweet.TwitterName);

            var contentLabel = new Label();
            contentLabel.Text = tweet.Content;

            var tagsLabel = new Label();
            tagsLabel.Text = tweet.HashTags;


            innerpanel.Controls.Add(nameLabel);
            innerpanel.Controls.Add(new LiteralControl("<br/>"));
            innerpanel.Controls.Add(contentLabel);
            innerpanel.Controls.Add(new LiteralControl("<br/>"));
            innerpanel.Controls.Add(tagsLabel);

            control.Controls.Add(innerpanel);
            return control;
        }
    }

}