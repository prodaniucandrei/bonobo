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
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
            InitializeComponent();
            Init();
            Activities_List.ItemsSource = SeedList();
        }

        void Init()
        {
            SearchRow.Height = new GridLength(1, GridUnitType.Star);
            ListRow.Height = new GridLength(8, GridUnitType.Star);

            Box_View.BackgroundColor = Constants.DefaultTabColor;

            Search_Bar.CancelButtonColor = Color.White;
            Search_Bar.PlaceholderColor = Color.White;
            Search_Bar.Placeholder = "search";

            Activities_List.Margin = 10;
            Activities_List.IsPullToRefreshEnabled = true;
            Activities_List.HasUnevenRows = true;
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Activity activity) DisplayAlert("Following", activity.ActivityName, "Ok", "Cancel");
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Activities_List.SelectedItem = null;
        }

        private void ListView_OnRefreshing(object sender, EventArgs e)
        {
            Activities_List.ItemsSource = SeedList();
            Activities_List.EndRefresh();
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Activities_List.ItemsSource = SeedList(e.NewTextValue);
        }

        void FilterIcon_Tapped(object sender, EventArgs e)
        {
            //TODO: go to new message page
        }

        private static IEnumerable<Activity> SeedList(string searchText = null)
        {
            List<Activity> activities = new List<Activity>
            {
                 new Activity{
                    ActivityName = "Do you think all the hype about privacy is warranted?",
                    Category = "Privacy",
                    ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    NoPlaces = 8,
                    Where = "Central Park",
                    Image = "https://placeimg.com/640/480/people/8"},
                new Activity{
                    ActivityName = "What’s the last book you read?",
                    Category = "Books",
                    ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    NoPlaces = 2,
                    Where = "Botanical garden",
                    Image = "https://placeimg.com/640/480/people/1"},
                new Activity{
                    ActivityName = "What place do I really need to see?",
                    Category = "Travel",
                    ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    NoPlaces = 4,
                    Where = "Bega River",
                    Image = "https://placeimg.com/640/480/people/2"},
                 new Activity{
                    ActivityName = "Would you buy a self-driving car if it was affordable?",
                    Category = "Automotive",
                    ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    NoPlaces = 4,
                    Where = "Bega River",
                    Image = "https://placeimg.com/640/480/people/7"},
                new Activity{
                    ActivityName = "What common misconceptions do people have about your hobby?",
                    Category = "Hobbies",
                    ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    NoPlaces = 3,
                    Where = "Roses Park",
                    Image = "https://placeimg.com/640/480/people/3"},
                new Activity{
                    ActivityName = "What would it be like to live on Mars?",
                    Category = "Astronomy",
                    ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    NoPlaces = 6,
                    Where = "Astroclubul Antares",
                    Image = "https://placeimg.com/640/480/people/4"},
                new Activity{
                    ActivityName = "What kinds of things are you interested in learning more about?",
                    Category = "Personal Development",
                    ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    NoPlaces = 1,
                    Where = "Bega River",
                    Image = "https://placeimg.com/640/480/people/5"},
                new Activity{
                    ActivityName = "What do direction do you think the internet is headed in?",
                    Category = "Internet",
                    ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    NoPlaces = 3,
                    Where = "Central Park",
                    Image = "https://placeimg.com/640/480/people/6"},
                new Activity{
                    ActivityName = "Do you think that VR will become mainstream in the near future?",
                    Category = "Technology",
                    ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    NoPlaces = 4,
                    Where = "Bega River",
                    Image = "https://placeimg.com/640/480/people/7"},
                new Activity{
                    ActivityName = "Do you think we’ll find microscopic alien life in our own solar system?",
                    Category = "Astronomy",
                    ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    NoPlaces = 4,
                    Where = "Botanical garden",
                    Image = "https://placeimg.com/640/480/people/7"},
            };

            return string.IsNullOrEmpty(searchText) ? activities : activities
                .Where(c => c.ActivityName.ToLower()
                .StartsWith(searchText.ToLower()));
        }
    }
}