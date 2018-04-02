using bonobo.Helpers;
using bonobo.Models;
using bonobo.ViewModels;
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
    public partial class AddActivityPage : ContentPage
    {

        public AddActivityPage()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            //check for internet connection 
            App.StartCheckIfInternet(Lbl_NoInternet, this);

            Lbl_PageTitle.TextColor = Constants.MainTextColor;
            Lbl_PageTitle.FontSize = 25;
            Lbl_PageTitle.FontAttributes = FontAttributes.Bold;
            Lbl_PageTitle.Text = "What do you want to talk about?";
            Lbl_PageTitle.HorizontalTextAlignment = TextAlignment.Center;

            Img_Guests.Source = "multipleusers.png";
            Img_Location.Source = "locationicon.png";
            Img_Date.Source = "dateicon.png";
        }


        //Publish an Activity
        async void PublishActivity(object sender, EventArgs e)
        {
            //check for InternetConnection before calling the webserver
            if (!Lbl_NoInternet.IsVisible)
            {
                CreateActivityViewModel activity = new CreateActivityViewModel
                {
                    ActivityName = Entry_ActivityTitle.Text,
                    Category = Picker_Category.SelectedItem.ToString(),
                    ShortDescription = Edit_Description.Text,
                    NoPlaces = int.Parse(Entry_Guests.Text),
                    Where = Entry_Location.Text,
                    When = Picker_Date.Date
                };

                if (Helper.CheckNullInformation(activity))
                {
                    string result = null; ;
                    try
                    {
                        //Create Activity API call
                        result = await App.RestService.CreateActivityAsync(activity);
                    }
                    catch (NullReferenceException)
                    {
                        result = null;
                    }
                    catch (TaskCanceledException)
                    {
                        result = null;
                        await DisplayAlert("Add activity", "Not able to reach server in time.", "Ok");
                    }

                    //if the activity was created
                    if (result != null)
                    {
                        //TODO: get the actual activity from local db
                        ActivityView a = await Helper.GetActivityById(result);
                        //go to activity page
                        var page = new ActivityPage(a);

                        if (Device.RuntimePlatform == Device.Android)
                        {
                            Application.Current.MainPage = new Dashboard(page);
                        }
                        else if (Device.RuntimePlatform == Device.iOS)
                        {
                            await Navigation.PushModalAsync(new Dashboard(page));
                        }
                    }
                }
                else
                {
                    await DisplayAlert("Add Activity", "Please fill in all the fields.", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Add Activity", "No internet. Cannot reach the server. Please connect to a network.", "Ok");
            }
        }
    }
}