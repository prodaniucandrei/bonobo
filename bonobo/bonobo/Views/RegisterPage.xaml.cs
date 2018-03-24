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
	public partial class RegisterPage : ContentPage
	{
        public RegisterPage()
        {
            InitializeComponent(); //initializes all the components in the .xaml file
            Init();
        }

        void Init()
        {
            BackgroundColor = Constants.BackgroundColor;
            Lbl_Email.TextColor = Constants.MainTextColor;
            Lbl_Password.TextColor = Constants.MainTextColor;
            Lbl_RepeatPassword.TextColor = Constants.MainTextColor;
            ActivitySpinner.IsVisible = false;
            LoginIcon.HeightRequest = Constants.LoginIconHeight;
            //check for internet connection 
            App.StartCheckIfInternet(Lbl_NoInternet, this);

            //after the username is entered, the focus should be on the password entry field 
            Entry_Email.Completed += (s, e) => Entry_Password.Focus();
            //after the password is entered, the focus should be on the repeat password entry field 
            Entry_Password.Completed += (s, e) => Entry_RepeatPassword.Focus();
            //after the username and the password have been entered, the login function should be triggered
            Entry_RepeatPassword.Completed += (s, e) => RegisterProcedure(s, e);
        }

        async void RegisterProcedure(object sender, EventArgs e)
        {
            //TODO: other password and email checks
            if(Entry_Password.Text != Entry_RepeatPassword.Text)
                await DisplayAlert("Register", "The two passwords you typed do not match. Try again.", "Ok");
            else
            {
                //TODO: register user un the DB
                await DisplayAlert("Register", "You successfully registered. You can now login.", "Ok");
                //go to LoginPage
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

        async void OnTapGestureForLogin(object sender, EventArgs args)
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
            }
        }
    }
}