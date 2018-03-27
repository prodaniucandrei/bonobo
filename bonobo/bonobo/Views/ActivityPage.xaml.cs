using bonobo.ViewModels;
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

            //set to some default activity
            if (activity == null)
            {
                activity = new ActivityView
                {
                    ActivityTitle = "What would it be like to live on Mars?",
                    Category = "Astronomy",
                    ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse malesuada ultricies arcu nec egestas. Nam porta fermentum aliquam. Nullam tincidunt odio purus.",
                    NoPlaces = 3,
                    Where = "Botanical garden",
                    When = new DateTime(2018, 3, 30),
                    Image = "http://cdns.yournewswire.com/wp-content/uploads/2016/06/NASA-life-on-Mars-678x381.jpg",
                    JoinedUsersList = new List<UserProfileView> {
                        new UserProfileView
                            {
                                FirstName = "Daenerys",
                                LastName = "Stormborn",
                                Gender = "Female",
                                Birthdate = new DateTime(1993, 3, 21),
                                HeaderImage = "http://hdqwalls.com/wallpapers/game-of-thrones-season-7-drogon-and-khaleesi-im.jpg",
                                ProfileImage = "https://cdn.techjuice.pk/wp-content/uploads/2017/08/daenerys-1024x576.jpg",
                                Tagline = "I was born to rule the Seven Kingdoms, and I will.",
                                ShortDesc = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse malesuada ultricies arcu nec egestas. Nam porta fermentum aliquam. Nullam tincidunt odio purus.",
                                Reviews = 31,
                                Hosted = 9,
                                Joined = 15
                            } },
                    HostUser = new UserProfileView
                    {
                        FirstName = App.UserDatabase.GetUser().FirstName,
                        LastName = App.UserDatabase.GetUser().LastName,
                        Gender = App.UserDatabase.GetUser().Gender,
                        Birthdate = App.UserDatabase.GetUser().BirthDate,
                        HeaderImage = "http://acephalous.typepad.com/.a/6a00d8341c2df453ef017d3c2bd399970c-500wi",
                        ProfileImage = "https://manofmany.com/wp-content/uploads/2017/07/Jon-Snow-2.jpg",
                        Tagline = "I conquer super villains and make the world a safer place.",
                        ShortDesc = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse malesuada ultricies arcu nec egestas. Nam porta fermentum aliquam. Nullam tincidunt odio purus.",
                        Reviews = 16,
                        Hosted = 11,
                        Joined = 24
                    }
                };

            }

            Init(activity);
        }

        void Init(ActivityView activity)
        {
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
            Lbl_Guests.Text =  activity.JoinedUsersList.Count.ToString() + "/" + activity.NoPlaces.ToString();
            Img_Guests.Source = "multipleusers.png";
            Lbl_Location.Text = activity.Where;
            Img_Location.Source = "locationicon.png";
            Lbl_Date.Text = activity.When.ToString("ddd dd MM",
                  CultureInfo.CreateSpecificCulture("en-US"));
            Img_Date.Source = "dateicon.png";

            //host user profile image and name
            Img_ProfilePicture.Aspect = Aspect.AspectFill;
            Img_ProfilePicture.Source = activity.HostUser.ProfileImage;
            Img_ProfilePicture.HeightRequest = 60;
            Img_ProfilePicture.WidthRequest = 60;
            Lbl_HostName.Text = activity.HostUser.FirstName + " " + activity.HostUser.LastName;

            //activity description
            Lbl_Desc.Text = activity.ShortDescription;
        }

        void JoinUnjoinActivity(object sender, EventArgs args)
        {
            //TODO: implement join/unjoin
        }
    }
}