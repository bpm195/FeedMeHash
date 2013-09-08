<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FeedMeHash.WebForm1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Feed Me Hash</title>
    <script runat="server">
        protected List<FeedMeHash.iTweet> tweets = new List<FeedMeHash.iTweet>();
        public void InitialFindTweetsButtonClicked(Object s, EventArgs e)
        {
            InitialForm.Visible = false;
            RepeatingForm.Visible = true;
            SearchBox.Text = initialsearchBox.Text;
            FindTweetsButtonClicked(s, e);
        }
        public void FindTweetsButtonClicked(Object s, EventArgs e)
        {
           tweets = FindTweets(SearchBox.Text);
            
            AttachTweets(tweets, TweetHolder);
        }
        public void SortListIndexChanged(Object s, EventArgs e)
        {
            setSort(SortList.SelectedItem.Text);
            AttachTweets(tweets, TweetHolder);
        }
    </script>
</head>
<body>
    <form id="InitialForm" runat="server">
        <div style="text-align: center">
            <h1 style="text-align: center">Feed Me Hash</h1>
            <asp:TextBox ID="initialsearchBox" runat="server" Width="800px" />
            <br />
            <asp:Button ID="initialfindTweetsButton" runat="server" Text="Find Tweets" Width="200px" OnClick="InitialFindTweetsButtonClicked" />
            <br />
        </div>
    </form>
    <form id="RepeatingForm" runat="server" visible="false">
        <div style="text-align: Left; width: 80%; margin: 0 auto">
            <h2 style="text-align: Left">Feed Me Hash</h2>
            <asp:TextBox ID="SearchBox" runat="server" Width="60%" />
            <asp:Button ID="SearchButton" runat="server" Text="Find Tweets" OnClick="FindTweetsButtonClicked" Width="20%" />
            <br />
            <asp:TextBox ID="FilterBox" runat="server" Width="60%" />
            <asp:DropDownList ID="SortList" runat="server" OnSelectedIndexChanged="SortListIndexChanged" Width="20%" AutoPostBack="true">
                <asp:ListItem>Date Ascending</asp:ListItem>
                <asp:ListItem>Date Descending</asp:ListItem>
                <asp:ListItem>Full Name</asp:ListItem>
                <asp:ListItem>Twitter Name</asp:ListItem>
            </asp:DropDownList>
        </div>
    </form>
    <asp:PlaceHolder ID="TweetHolder" runat="server" />
</body>
</html>
