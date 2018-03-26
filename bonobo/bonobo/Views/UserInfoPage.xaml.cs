﻿using bonobo.Models;
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
            UserProfileView user = new UserProfileView
            {
                FirstName = App.UserDatabase.GetUser().FirstName,
                LastName = App.UserDatabase.GetUser().LastName,
                Gender = App.UserDatabase.GetUser().Gender,
                Birthdate = App.UserDatabase.GetUser().BirthDate,
                HeaderImage = "http://acephalous.typepad.com/.a/6a00d8341c2df453ef017d3c2bd399970c-500wi",
                ProfileImage = "https://manofmany.com/wp-content/uploads/2017/07/Jon-Snow-2.jpg",
                Reviews = 16,
                Hosted = 11,
                Joined = 24
            };

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

            Lbl_TagLine.Text = "I conquer super villains and make the world a safer place.";
            Lbl_TagLine.FontAttributes = FontAttributes.Italic;

            Lbl_Reviews.Text = user.Reviews.ToString();
            Lbl_Reviews.TextColor = Constants.MainTextColor;
            Lbl_Joined.Text = user.Joined.ToString();
            Lbl_Joined.TextColor = Constants.MainTextColor;
            Lbl_Hosted.Text =user.Hosted.ToString();
            Lbl_Hosted.TextColor = Constants.MainTextColor;

            Lbl_Desc.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse malesuada ultricies arcu nec egestas. Nam porta fermentum aliquam. Nullam tincidunt odio purus.";

            //compute user age
            DateTime zeroTime = new DateTime(1, 1, 1);
            TimeSpan span = DateTime.Now - user.Birthdate;
            int years = (zeroTime + span).Year - 1;
            Lbl_Birthday.Text = "Age: " + years.ToString();
            Lbl_Gender.Text = "Gender: " + user.Gender;
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