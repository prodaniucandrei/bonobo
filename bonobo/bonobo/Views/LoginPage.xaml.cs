﻿using bonobo.Dtos;
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
			InitializeComponent (); 
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
            LoginView user = new LoginView {
                Email = Entry_Username.Text,
                Password = Entry_Password.Text };

            //check for InternetConnection before calling the webserver
            if (!Lbl_NoInternet.IsVisible)
            {
                if (user.CheckNullInformation())
                {
                    Token result;
                    try
                    {
                        //Login API call
                        result = await App.RestService.Login(user);
                    }
                    catch (NullReferenceException)
                    {
                        result = null;
                    }
                    catch (TaskCanceledException)
                    {
                        result = null;
                        await DisplayAlert("Login", "Not able to reach server in time.", "Ok");
                    }

                    //if the login was successful
                    if (result != null)
                    {
                        //make sure previous token is deleted
                        if (App.TokenDatabase.GetToken() != null)
                            App.TokenDatabase.DeleteToken(0);
                        //save token for current user
                        App.TokenDatabase.SaveToken(result);

                        //retrieve info about the user who logges in
                        UserDto userdto;
                        try
                        {
                            userdto = await App.RestService.FindUserByEmail(new FindUserByEmailViewModel { Email = user.Email });
                        }
                        catch (NullReferenceException)
                        {
                            userdto = null;
                        }
                        catch (TaskCanceledException)
                        {
                            userdto = null;
                            await DisplayAlert("Login", "Not able to reach server in time.", "Ok");
                        }

                        //prepare user obj for saving in local db
                        User usr;
                        if (userdto == null)
                        {
                            await DisplayAlert("Login", "Not able to get user data from server.", "Ok");
                            //TODO: try to reach server later
                            usr = new User
                            {
                                RemoteId = user.Email,
                                FirstName = user.Email,
                                LastName = user.Email,
                                Email = user.Email,
                                Password = user.Password,
                                BirthDate = DateTime.Now,
                                Gender = user.Email
                            };
                        }
                        else
                        {
                            usr = new User
                            {
                                RemoteId = userdto.RemoteId,
                                FirstName = userdto.FirstName,
                                LastName = userdto.LastName,
                                Email = user.Email,
                                Password = user.Password,
                                BirthDate = userdto.BirthDate,
                                Gender = userdto.Gender
                            };
                        }

                        //make sure previous user is deleted
                        if (App.UserDatabase.GetUser() != null)
                            App.UserDatabase.DeleteUser(0);
                        //save current logged user in local DB
                        App.UserDatabase.SaveUser(usr);

                        //navigate to dashboard
                        if (Device.RuntimePlatform == Device.Android)
                        {
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
            else
            {
                await DisplayAlert("Login", "No internet. Cannot reach the server. Please connect in order to login.", "Ok");
            }
        }


        //Go to Registration Page
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


        //Go to Forgot Passoword Page
        void OnTapGestureForForgotPassword(object sender, EventArgs args)
        {
            //TODO: implement password recovery procedure
        }
    }
}