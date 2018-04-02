using bonobo.Models;
using bonobo.ViewModels;
using bonobo.Helpers;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
	public partial class UserInfoPage : ContentPage
	{
        bool loggedUser = false;

        public UserInfoPage(UserProfileView user)
        {
            InitializeComponent();

            if (user == null)
            {
                //set user profile to currently logged user
                user = Helper.GetLoggedUserProfileView();
                loggedUser = true;
            }

            Init(user);
        }


        void Init(UserProfileView user)
        {
            Img_ProfilePicture.TranslationY = 100;
            Img_ProfilePicture.HeightRequest = 100;
            Img_ProfilePicture.WidthRequest = 100;
            Img_ProfilePicture.Aspect = Aspect.AspectFill;
            Img_ProfilePicture.BorderColor = Color.White;
            Img_ProfilePicture.BorderThickness = 3;
            Img_ProfilePicture.Source = user.ProfileImage;

            Img_ProfileHeader.Source = user.HeaderImage;
            Img_ProfileHeader.Aspect = Aspect.AspectFill;
            Img_ProfileHeader.HeightRequest = 250;

            Lbl_Name.Text = user.FirstName + " " + user.LastName;
            Lbl_Name.FontAttributes = FontAttributes.Bold;

            Lbl_TagLine.Text = user.Tagline;
            Lbl_TagLine.FontAttributes = FontAttributes.Italic;

            Lbl_Reviews.Text = user.Reviews.ToString();
            Lbl_Reviews.TextColor = Constants.MainTextColor;
            Lbl_Joined.Text = user.Joined.ToString();
            Lbl_Joined.TextColor = Constants.MainTextColor;
            Lbl_Hosted.Text = user.Hosted.ToString();
            Lbl_Hosted.TextColor = Constants.MainTextColor;

            Lbl_Desc.Text = user.ShortDesc;

            //compute user age
            DateTime zeroTime = new DateTime(1, 1, 1);
            TimeSpan span = DateTime.Now - user.Birthdate;
            int years = (zeroTime + span).Year - 1;
            Img_Birthdate.Source = "ageicon.png";
            Lbl_Birthdate.Text = years.ToString();
            Img_Gender.Source = "gendericon.png";
            Lbl_Gender.Text = user.Gender;

            if(loggedUser == false)
            {
                Lbl_SignOut.IsEnabled = false;
                Lbl_SignOut.IsVisible = false;
            }

        }


        //Change Profile picture
        async void OnProfilePictureTap(object sender, System.EventArgs e)
        {
            //var response = await DisplayActionSheet("Profile Photo Source", "Cancel", null, "Camera", "Photo Album", "Remove");

            //if (response == "Camera")
            {

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera available.", "Got It");
                    return;
                }

                var mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                {
                    AllowCropping = true,
                    Name = "item.jpg",
                    PhotoSize = PhotoSize.Medium,
                    CompressionQuality = 42,
                    SaveToAlbum = true
                });

                Img_ProfilePicture.Source = ImageSource.FromStream(() => mediaFile.GetStream());



            }
            //else if (response == "Photo Album")
            {
                //var pickerOptions = new PickMediaOptions();

                //var file = await CrossMedia.Current.PickPhotoAsync(pickerOptions);

                //Img_ProfilePicture.Source = ImageSource.FromStream(() => file.GetStream());

            }
            //else if (response == "Remove")
            {

            }
        }


        //Sign Out
        async void OnTapGestureForSigningOut(object sender, EventArgs args)
        {
            bool result = false;
            //LogOut API call
            try
            {
                result = await App.RestService.Logout();
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("NullReferenceException in Logout.");
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("TaskCanceledException in Logout.");
                await DisplayAlert("Logout", "Not able to reach server in time.", "Ok");
            }

            if (result)
            {
                //delete logged user from local storage
                App.UserDatabase.DeleteUser(0);
                App.TokenDatabase.DeleteToken(0);

                if (Device.RuntimePlatform == Device.Android)
                {
                    Application.Current.MainPage = new LoginPage();
                }
                else if (Device.RuntimePlatform == Device.iOS)
                {
                    await Navigation.PushModalAsync(new LoginPage());
                }
            }
        }
    }
}