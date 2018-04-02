using bonobo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonobo.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Dashboard : ContentPage
	{
		public Dashboard (ContentPage page)
		{
            if(page == null)
                page = new HomePage();
            InitializeComponent();
            Init(page);
		}

        void Init(ContentPage page)
        {
            Row.Height = Constants.RowHeight;

            Img_Home.HeightRequest = Constants.RowHeight;
            Img_Add.HeightRequest = Constants.RowHeight;
            Img_Account.HeightRequest = Constants.RowHeight;
            Img_Explore.HeightRequest = Constants.RowHeight;
            Img_Message.HeightRequest = Constants.RowHeight;

            Tab_Home.BackgroundColor = Constants.DefaultTabColor;
            Tab_Add.BackgroundColor = Constants.DefaultTabColor;
            Tab_Account.BackgroundColor = Constants.DefaultTabColor;
            Tab_Explore.BackgroundColor = Constants.DefaultTabColor;
            Tab_Message.BackgroundColor = Constants.DefaultTabColor;

            var name = page.GetType().Name;
            switch (name)
            {
                case "HomePage":
                    Tab_Home.BackgroundColor = Constants.CurrentTabColor;
                    break;
                case "ExplorePage":
                    Tab_Explore.BackgroundColor = Constants.CurrentTabColor;
                    break;
                case "AddActivityPage":
                    Tab_Add.BackgroundColor = Constants.CurrentTabColor;
                    break;
                case "MessagePage":
                    Tab_Message.BackgroundColor = Constants.CurrentTabColor;
                    break;
            }

            InsertContent.Content = page.Content;
        }

        void HomeIcon_Tapped(object sender, EventArgs e)
        {
            Tab_Home.BackgroundColor = Constants.CurrentTabColor;
            Tab_Add.BackgroundColor = Constants.DefaultTabColor;
            Tab_Account.BackgroundColor = Constants.DefaultTabColor;
            Tab_Explore.BackgroundColor = Constants.DefaultTabColor;
            Tab_Message.BackgroundColor = Constants.DefaultTabColor;
            var page = new HomePage();
            InsertContent.Content = page.Content;
        }

        void ExploreIcon_Tapped(object sender, EventArgs e)
        {
            Tab_Explore.BackgroundColor = Constants.CurrentTabColor;
            Tab_Add.BackgroundColor = Constants.DefaultTabColor;
            Tab_Account.BackgroundColor = Constants.DefaultTabColor;
            Tab_Home.BackgroundColor = Constants.DefaultTabColor;
            Tab_Message.BackgroundColor = Constants.DefaultTabColor;
            var page = new ExplorePage();
            InsertContent.Content = page.Content;
        }

        void AddActivityIcon_Tapped(object sender, EventArgs e)
        {
            Tab_Add.BackgroundColor = Constants.CurrentTabColor;
            Tab_Home.BackgroundColor = Constants.DefaultTabColor;
            Tab_Account.BackgroundColor = Constants.DefaultTabColor;
            Tab_Explore.BackgroundColor = Constants.DefaultTabColor;
            Tab_Message.BackgroundColor = Constants.DefaultTabColor;
            var page = new AddActivityPage();
            InsertContent.Content = page.Content;
        }

        void MessageIcon_Tapped(object sender, EventArgs e)
        {
            Tab_Message.BackgroundColor = Constants.CurrentTabColor;
            Tab_Account.BackgroundColor = Constants.DefaultTabColor;
            Tab_Home.BackgroundColor = Constants.DefaultTabColor;
            Tab_Add.BackgroundColor = Constants.DefaultTabColor;
            Tab_Explore.BackgroundColor = Constants.DefaultTabColor;
            var page = new MessagePage();
            InsertContent.Content = page.Content;
        }

        void UserInfoIcon_Tapped(object sender, EventArgs e)
        {
            Tab_Account.BackgroundColor = Constants.CurrentTabColor;
            Tab_Home.BackgroundColor = Constants.DefaultTabColor;
            Tab_Add.BackgroundColor = Constants.DefaultTabColor;
            Tab_Message.BackgroundColor = Constants.DefaultTabColor;
            Tab_Explore.BackgroundColor = Constants.DefaultTabColor;

            var page = new UserInfoPage(null);
            InsertContent.Content = page.Content;
        }
    }
}