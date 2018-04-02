using bonobo.Dtos;
using bonobo.Helpers;
using bonobo.Models;
using bonobo.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private List<ActivityView> Activity_List;
        private List<ActivityDto> ActivityDto_List;

        public HomePage()
        {
            InitializeComponent();
            ActivityDto_List = new List<ActivityDto>();
            Activity_List = new List<ActivityView>();
            InitAsync();
        }

        private async void InitAsync()
        {
            //check for internet connection 
            App.StartCheckIfInternet(Lbl_NoInternet, this);
            if (!Lbl_NoInternet.IsVisible)
            {
                Activities_List.ItemsSource = await GetActivityListFromServer();
            }
            else
            {
                //TODO: load from local db
                Activities_List.ItemsSource = Helper.SeedActivityViewList();
            }

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

        //On Item Tapped => open activity
        private async void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var activity = e.Item as ActivityView;
            var page = new ActivityPage(activity);

            if (Device.RuntimePlatform == Device.Android)
            {
                Application.Current.MainPage = new Dashboard(page);
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                await Navigation.PushModalAsync(new Dashboard(page));
            }
        }

        //TODO: figure out what to do On Item Selected
        private async Task ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                Activities_List.SelectedItem = null;
            }

            var activity = e.SelectedItem as ActivityView;
            await DisplayAlert("Item Selected", activity.ActivityTitle, "Ok");
        }


        //On Refreshing => get new data from the server
        private async void ListView_OnRefreshing(object sender, EventArgs e)
        {
            //if there is internet connection
            if (!Lbl_NoInternet.IsVisible)
            {
                Activities_List.ItemsSource = await GetActivityListFromServer();
            }
            else
            {
                //TODO: load from local db
                Activities_List.ItemsSource = Helper.SeedActivityViewList();
            }
            Activities_List.EndRefresh();
        }


        //On Text Changed => search through all activities by name
        private async void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //if there is internet connection
            if (!Lbl_NoInternet.IsVisible)
            {
                Activities_List.ItemsSource = await GetActivityListFromServer(e.NewTextValue);
            }
            else
            {
                //TODO: load from local db
                Activities_List.ItemsSource = Helper.SeedActivityViewList();
            }
        }


        //TODO: filter through activities
        void FilterIcon_Tapped(object sender, EventArgs e)
        {
 
        }


        //Profile Image Tapped => go to activity host user profile page
        async Task ProfileImageTapped(object sender, MyListItemEventArgs e)
        {
            UserProfileView user = await Helper.GetUserProfileViewObj(e.MyItem.HostUserId);

            var page = new UserInfoPage(user);

            if (Device.RuntimePlatform == Device.Android)
            {
                Application.Current.MainPage = new Dashboard(page);
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                await Navigation.PushModalAsync(new Dashboard(page));
            }
        }


        //Get Activity List from server
        private async Task<IEnumerable<ActivityView>> GetActivityListFromServer(string searchText = null)
        {
            //make sure the list is not populated from previous refreshing
            Activity_List.Clear();

            //if there is internet connection
            if (!Lbl_NoInternet.IsVisible)
            {
                try
                {
                    ActivityDto_List = await App.RestService.GetAllActivities();
                }
                catch (NullReferenceException)
                {
                    Activity_List = Helper.SeedActivityViewList();
                    ActivityDto_List = null;
                }
                catch (TaskCanceledException)
                {
                    Activity_List = Helper.SeedActivityViewList();
                    ActivityDto_List = null;
                }

                if (ActivityDto_List != null)
                {
                    foreach (ActivityDto a in ActivityDto_List)
                    {
                        if (a != null)
                        {
                            Activity_List.Add(new ActivityView
                            {
                                ActivityTitle = a.ActivityName,
                                Category = a.Category,
                                ShortDescription = a.ShortDescription,
                                NoPlaces = a.NoPlaces,
                                Where = a.Where,
                                When = a.When,
                                JoinedUsersListIds = a.JoinedUsersIds,
                                HostUserId = a.ActivityHostId,
                                //TODO: search for host profile image
                                Image = "https://placeimg.com/640/480/people/8",
                            });
                        }

                    }
                }
                else
                {
                    Activity_List = Helper.SeedActivityViewList();
                }
            }
            else
            {
                //TODO: load from local db
                Activity_List = Helper.SeedActivityViewList();
            }

            return string.IsNullOrEmpty(searchText) ? Activity_List : Activity_List
                .Where(c => c.ActivityTitle.ToLower()
                .StartsWith(searchText.ToLower()));
        }
    }
}