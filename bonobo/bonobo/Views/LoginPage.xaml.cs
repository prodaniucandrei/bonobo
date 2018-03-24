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
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent (); //initializes all the components in the .xaml file
            Init();
		}

        void Init()
        {
            BackgroundColor = Constants.BackgroundColor;
            Lbl_Username.TextColor = Constants.MainTextColor;
            Lbl_Password.TextColor = Constants.MainTextColor;
            ActivitySpinner.IsVisible = false;
            LoginIcon.HeightRequest = Constants.LoginIconHeight;
            //check for internet connection 
            App.StartCheckIfInternet(Lbl_NoInternet, this);

            //after the username is entered, the focus should be on the password entry field 
            Entry_Username.Completed += (s, e) => Entry_Password.Focus();
            //after the username and the password have been entered, the login function should be triggered
            Entry_Password.Completed += (s, e) => SignInProcedure(s, e);
        }

        async void SignInProcedure(object sender, EventArgs e)
        {
            LoginView user = new LoginView(Entry_Username.Text, Entry_Password.Text);
            if (user.CheckNullInformation())
            {
                Token result;
                //TODO: check for InternetConnection before calling the webserver
                //Login call
                try {
                    result = await App.RestService.Login(user);
                } catch(NullReferenceException) {
                    result = null;
                } catch (TaskCanceledException) {
                    result = null;
                    Console.WriteLine("TaskCanceledException in Login.");
                    await DisplayAlert("Login", "Not able to reach server in time.", "Ok");
                }
                if(result != null)
                {
                    //save current logged user in local DB
                    App.UserDatabase.SaveUser(user);
                    //save token for current user
                    App.TokenDatabase.SaveToken(result);
                    //await makes sure that the code below won't be executed before the user presses 'OK'
                    await DisplayAlert("Login", "Login success. Hi " + App.UserDatabase.GetUser().Email, "OK");
                    /* With the `PushModalAsync` method we navigate the user 
                     * the the orders page and do not give them an option to
                     * navigate back to the LoginPage by clicking the back button */
                    if (Device.RuntimePlatform == Device.Android) {
                        Application.Current.MainPage = new Dashboard();
                    }
                    else if (Device.RuntimePlatform == Device.iOS)
                    {
                        await Navigation.PushModalAsync(new Dashboard());
                    }
                }
                else
                {
                    await DisplayAlert("Login", "Login not correct: wrong username or password.", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Login", "Login not correct: empty username or password.", "Ok");
            }
        }

        async void OnTapGestureForRegistration(object sender, EventArgs args)
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                Application.Current.MainPage = new RegisterPage();
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                await Navigation.PushModalAsync(new RegisterPage());
            }
        }

        void OnTapGestureForForgotPassword(object sender, EventArgs args)
        {
            //TODO: implement password recovery procedure
        }
    }
}