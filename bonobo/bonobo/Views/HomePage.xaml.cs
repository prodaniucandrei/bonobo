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
            Activities_List.ItemsSource = GetList();
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

        private static IEnumerable<Activity> GetList(string searchText = null)
        {
            List<Activity> activities = new List<Activity>
            {
                new Activity{
                    ActivityName = "Dancing",
                    Category = "Fun",
                    ShortDescription = "bla bla bla bla bla bla bla bla bla bla bla bla bla bla",
                    NoPlaces = 2,
                    Where = "home",
                    Image = "https://placeimg.com/640/480/people/1"},
                new Activity{
                    ActivityName = "Walking",
                    Category = "Fun",
                    ShortDescription = "bla bla bla bla bla bla bla bla bla bla bla bla bla bla",
                    NoPlaces = 4,
                    Where = "home",
                    Image = "https://placeimg.com/640/480/people/2"},
                new Activity{
                    ActivityName = "Climbing",
                    Category = "Sport",
                    ShortDescription = "bla bla bla bla bla bla bla bla bla bla bla bla bla bla",
                    NoPlaces = 3,
                    Where = "home",
                    Image = "https://placeimg.com/640/480/people/3"},
                new Activity{
                    ActivityName = "Singing",
                    Category = "Fun",
                    ShortDescription = "bla bla bla bla bla bla bla bla bla bla bla bla bla bla",
                    NoPlaces = 6,
                    Where = "home",
                    Image = "https://placeimg.com/640/480/people/4"},
                new Activity{
                    ActivityName = "Resting",
                    Category = "Relax",
                    ShortDescription = "bla bla bla bla bla bla bla bla bla bla bla bla bla bla",
                    NoPlaces = 1,
                    Where = "home",
                    Image = "https://placeimg.com/640/480/people/5"},
                new Activity{
                    ActivityName = "Yoga",
                    Category = "Relax",
                    ShortDescription = "bla bla bla bla bla bla bla bla bla bla bla bla bla bla",
                    NoPlaces = 3,
                    Where = "home",
                    Image = "https://placeimg.com/640/480/people/6"},
                new Activity{
                    ActivityName = "Reading",
                    Category = "Relax",
                    ShortDescription = "bla bla bla bla bla bla bla bla bla bla bla bla bla bla",
                    NoPlaces = 4,
                    Where = "home",
                    Image = "https://placeimg.com/640/480/people/7"},
                new Activity{
                    ActivityName = "Boardgames",
                    Category = "Fun",
                    ShortDescription = "bla bla bla bla bla bla bla bla bla bla bla bla bla bla",
                    NoPlaces = 8,
                    Where = "home",
                    Image = "https://placeimg.com/640/480/people/8"},
            };

            return string.IsNullOrEmpty(searchText) ? activities : activities
                .Where(c => c.ActivityName.ToLower()
                .StartsWith(searchText.ToLower()));
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
            Activities_List.ItemsSource = GetList();
            Activities_List.EndRefresh();
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Activities_List.ItemsSource = GetList(e.NewTextValue);
        }
    }
}