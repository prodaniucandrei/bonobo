using bonobo.Dtos;
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
                    Debug.WriteLine("TaskCanceledException in Login.");
                    await DisplayAlert("Login", "Not able to reach server in time.", "Ok");
                }
                if(result != null)
                {
                    //make sure previous token is deleted
                    if(App.TokenDatabase.GetToken() != null)
                        App.TokenDatabase.DeleteToken(0);
                    //save token for current user
                    App.TokenDatabase.SaveToken(result);
                    Debug.WriteLine("Login: saved token = " + App.TokenDatabase.GetToken().AccessToken);
                    //retrieve info about the user who logges in
                    UserDto userdto;
                    try
                    {
                        Debug.WriteLine("Login: user.Email = " + user.Email);
                        userdto = await App.RestService.FindUserByEmail(new FindUserByEmailViewModel { Email = user.Email });
                    }
                    catch (NullReferenceException)
                    {
                        userdto = null;
                    }
                    catch (TaskCanceledException)
                    {
                        userdto = null;
                        Debug.WriteLine("TaskCanceledException in Login.");
                        await DisplayAlert("Login", "Not able to reach server in time.", "Ok");
                    }
                    
                    User usr;
                    Debug.WriteLine("Login: userDTO = " + userdto.FirstName + " " + userdto.BirthDate);
                    if (userdto == null)
                    {
                        Debug.WriteLine("Login: Could not retrieve user data from server.");
                        usr = new User(
                            user.Email,
                            user.Email,
                            user.Email,
                            user.Email,
                            user.Password,
                            DateTime.Now,
                            user.Email);
                    }
                    else
                    {
                        usr = new User(
                            userdto.RemoteId,
                            userdto.FirstName, 
                            userdto.LastName, 
                            user.Email, 
                            user.Password, 
                            userdto.BirthDate,
                            userdto.Gender);
                    }

                    //make sure previous user is deleted
                    if(App.UserDatabase.GetUser() != null)
                        App.UserDatabase.DeleteUser(0);
                    //save current logged user in local DB
                    App.UserDatabase.SaveUser(usr);

                    //navigate to dashboard
                    if (Device.RuntimePlatform == Device.Android) {
                        Application.Current.MainPage = new Dashboard(new HomePage());
                    }
                    else if (Device.RuntimePlatform == Device.iOS)
                    {
                        await Navigation.PushModalAsync(new Dashboard(new HomePage()));
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