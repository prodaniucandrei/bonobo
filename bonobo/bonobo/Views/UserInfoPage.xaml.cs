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
	public partial class UserInfoPage : ContentPage
	{
        public UserInfoPage()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            UserProfile user = new UserProfile(
                "Jon", 
                "Snow", 
                "http://acephalous.typepad.com/.a/6a00d8341c2df453ef017d3c2bd399970c-500wi", "https://manofmany.com/wp-content/uploads/2017/07/Jon-Snow-2.jpg");

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

            Lbl_FirstName.Text = user.FirstName;
            Lbl_LastName.Text = user.LastName;
            Lbl_TagLine.Text = "I conquer super villains and make the world a safer place.";
            Lbl_Email.Text = App.UserDatabase.GetUser().Email;

        }

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