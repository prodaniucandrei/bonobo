using System;
using bonobo.Models;
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
            Init();
            Categories_List.ItemsSource = SeedList();
        }

        void Init()
        {
            SearchRow.Height = new GridLength(1, GridUnitType.Star);
            ListRow.Height = new GridLength(8, GridUnitType.Star);

            Box_View.BackgroundColor = Constants.DefaultTabColor;

            Search_Bar.CancelButtonColor = Color.White;
            Search_Bar.PlaceholderColor = Color.White;
            Search_Bar.Placeholder = "search";

            Categories_List.Margin = 20;
            Categories_List.IsPullToRefreshEnabled = true;
            Categories_List.HasUnevenRows = true;
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Category category) DisplayAlert("Following", category.Name, "Ok", "Cancel");
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Categories_List.SelectedItem = null;
        }

        private void ListView_OnRefreshing(object sender, EventArgs e)
        {
            Categories_List.ItemsSource = SeedList();
            Categories_List.EndRefresh();
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Categories_List.ItemsSource = SeedList(e.NewTextValue);
        }

        private static IEnumerable<Category> SeedList(string searchText = null)
        {
            List<Category> categories = new List<Category>
            {
                 new Category{
                    Name = "Astronomy",
                    ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Image = "https://placeimg.com/640/480/tech"},
                 new Category{
                     Name = "Books",
                     ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                     Image = "https://placeimg.com/640/480/arch"
                 },
                 new Category{
                    Name = "Personal Development",
                    ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Image = "http://www.myrkothum.com/wp-content/uploads/2012/04/personal-development.jpg"},

                 new Category{
                    Name = "Technology",
                    ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Image = "https://placeimg.com/640/480/tech"},
                 new Category{
                    Name = "Travel",
                    ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Image = "https://placeimg.com/640/480/nature"},
            };

            return string.IsNullOrEmpty(searchText) ? categories : categories
                .Where(c => c.Name.ToLower()
                .StartsWith(searchText.ToLower()));
        }
    }
}