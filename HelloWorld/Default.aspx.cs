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



namespace FeedMeHash
{

    public partial class WebForm1 : System.Web.UI.Page
    {
        private int _index = 0;
        protected int Index
        {
            get { return ++_index; }
        }
        protected IComparer<iTweet> _sortType = new Unsorted();

        protected void Page_Load(object sender, EventArgs e){}

        //I haven't been able to properly interface with Twitter so this is just a place holder.
        protected List<iTweet> FindTweets(String hashTags)
        {
            var tweets = new List<iTweet>();
            tweets.Add(new FakeTweet("Twitter Guy", "Twitter Guy", "Something important about kittens", "#kittens, #awesomesauce", DateTime.Now));
            tweets.Add(new FakeTweet("Twitter Gurl", "Twitter Gurl", "Some content", "#kittens", DateTime.Now));
            tweets.Add(new FakeTweet("Twitter Dude", "TD1337", "stuff","#awesomesauce", DateTime.Now));

            return tweets;
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
            switch (_type){
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
            control.ID = "Tweet" +Index;
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

        /* Broken please ignore
public static string url = @"https://api.twitter.com/1.1/search/tweets.json";
public static string urlParameters = @"?q=kittens&src=typd";
protected string CreateRequest()
{
    return @"https://api.twitter.com/1.1/search/tweets.json?q=%23freebandnames&since_id=24012619984051000&max_id=250126199840518145&result_type=mixed&count=4";
}
protected HttpResponse MakeRequest(string request)
{
    HttpClient client = new HttpClient();
    client.BaseAddress = new Uri(url);

    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    HttpResponseMessage response = client.GetAsync(urlParameters).Result;

    //var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;
    //foreach (var d in dataObjects)
    //{
    //    Console.WriteLine("{0}", d.Name);
    //}
    int stall = 0;

    return null;
}
*/
    }

}