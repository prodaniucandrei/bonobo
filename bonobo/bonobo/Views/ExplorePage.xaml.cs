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
	public partial class ExplorePage : ContentPage
	{
        public ExplorePage()
        {
            InitializeComponent();
      
            //dynamically generates the grid with user photos 
            //UserList should be generated based on user interests and preferences
            GeneratePhotoGridAsync(SeedUserList());
        }

        private async Task GeneratePhotoGridAsync(List<UserProfile> userList)
        {
            PhotoGrid.ColumnSpacing = 2;
            PhotoGrid.RowSpacing = 2;
            PhotoGrid.Margin = 2;
            PhotoGrid.ColumnDefinitions.Add(new ColumnDefinition());
            PhotoGrid.ColumnDefinitions.Add(new ColumnDefinition());
            PhotoGrid.ColumnDefinitions.Add(new ColumnDefinition());

            int userIndex = 0;
            int rowIndex = -1;
            int columnIndex = 0;

            while (userIndex < userList.Count)
            {
                UserProfile user = userList[userIndex];
                userIndex += 1;

                //user picture
                var userImage = new Image
                {
                    Aspect = Aspect.AspectFill,
                    Source = user.ProfileImage,
                    HeightRequest = 100,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                
                //username
                var userName = new Label
                {
                    Text = user.FirstName + " " + user.LastName,
                    VerticalOptions = LayoutOptions.End,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    TextColor = Color.White,
                    BackgroundColor = Color.FromHsla(0, 0, 0, 0.4)
                };

                Random random = new Random();
                //interests match percentage
                var progressBar = new ProgressBar
                {
                    Progress = random.NextDouble(),
                    VerticalOptions = LayoutOptions.Start,
                    TranslationY = -6
                };

                if (columnIndex == 3)
                    columnIndex = 0;

                if (columnIndex == 0)
                {
                    PhotoGrid.RowDefinitions.Add(new RowDefinition());
                    rowIndex += 1;
                }

                PhotoGrid.Children.Add(userImage, columnIndex, rowIndex);
                PhotoGrid.Children.Add(userName, columnIndex, rowIndex);
                PhotoGrid.Children.Add(progressBar, columnIndex, rowIndex);
                columnIndex += 1;
            }
        }

        private List<UserProfile> SeedUserList()
        {
            List<UserProfile> userList = new List<UserProfile>();

            for(int i = 0; i < 20; i++)
            {
                userList.Add(new UserProfile(
                    "Lorem",
                    "Ipsum",
                    "https://placeimg.com/640/480/arch/" + i.ToString(),
                    "https://placeimg.com/640/480/people/" + i.ToString()));
            }
           
            return userList;
        }
    }
}