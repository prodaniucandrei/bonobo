using bonobo.Dtos;
using bonobo.ViewModels;
using bonobo.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bonobo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivityPage : ContentPage
    {
        public ActivityPage(ActivityView activity)
        {
            InitializeComponent();

            if (activity == null)
            {
                //set to some default activity
                activity = Helper.GetDefaultActivity();
            }

            InitAsync(activity);
        }

        async void InitAsync(ActivityView activity)
        {
            //get user profile who hosts this activity
            UserProfileView user = await Helper.GetUserProfileViewObj(activity.HostUserId);

            //activity picture
            Img_ActivityPicture.Source = activity.Image;
            Img_ActivityPicture.Aspect = Aspect.AspectFill;
            Img_ActivityPicture.HeightRequest = 250;

            //activity title and category
            Lbl_ActivityTitle.Text = activity.ActivityTitle;
            Lbl_ActivityTitle.FontAttributes = FontAttributes.Bold;
            Lbl_Category.Text = activity.Category;
            Lbl_Category.FontAttributes = FontAttributes.Italic;

            //activity metrics
            Lbl_Guests.Text =  activity.JoinedUsersListIds.Count.ToString() + "/" + activity.NoPlaces.ToString();
            Img_Guests.Source = "multipleusers.png";
            Lbl_Location.Text = activity.Where;
            Img_Location.Source = "locationicon.png";
            Lbl_Date.Text = activity.When.ToString("ddd dd MM",
                  CultureInfo.CreateSpecificCulture("en-US"));
            Img_Date.Source = "dateicon.png";

            //host user profile image and name
            Img_ProfilePicture.Aspect = Aspect.AspectFill;
            Img_ProfilePicture.Source = user.ProfileImage;
            Img_ProfilePicture.HeightRequest = 60;
            Img_ProfilePicture.WidthRequest = 60;
            Lbl_HostName.Text = user.FirstName + " " + user.LastName;

            //activity description
            Lbl_Desc.Text = activity.ShortDescription;
        }

        void JoinUnjoinActivity(object sender, EventArgs args)
        {
            //TODO: implement join/unjoin
        }
    }
}