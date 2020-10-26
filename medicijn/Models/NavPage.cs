using System;
using Xamarin.Forms;

namespace medicijn.Models
{
    public class NavPage
    {
        public string PageTitle { get; set; }
        public ContentView Content { get; set; }

        public NavPage(string pageTitle, ContentView content)
        {
            PageTitle = pageTitle;
            Content = content;
        }
    }
}
