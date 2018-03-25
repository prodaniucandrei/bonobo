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
            Lbl_FirstName.TextColor = Constants.MainTextColor;
            Lbl_LastName.TextColor = Constants.MainTextColor;
            Lbl_Birthdate.TextColor = Constants.MainTextColor;
            Lbl_Gender.TextColor = Constants.MainTextColor;
            Lbl_Email.TextColor = Constants.MainTextColor;
            Lbl_Password.TextColor = Constants.MainTextColor;
            Lbl_RepeatPassword.TextColor = Constants.MainTextColor;
            ActivitySpinner.IsVisible = false;
            LoginIcon.HeightRequest = Constants.LoginIconHeight;
            //check for internet connection 
            App.StartCheckIfInternet(Lbl_NoInternet, this);

            //after the firstname is entered, the focus should be on the lastname field 
            Entry_FirstName.Completed += (s, e) => Entry_LastName.Focus();
            //after the lastname is entered, the focus should be on the birthday field 
            Entry_LastName.Completed += (s, e) => DatePicker_Birthday.Focus();
            //after the birthday is entered, the focus should be on the gender field 
            DatePicker_Birthday.DateSelected += (s, e) => Picker_Gender.Focus();
            //after the gender is entered, the focus should be on the email entry field 
            Picker_Gender.SelectedIndexChanged += (s, e) => Entry_Email.Focus();
            //after the email is entered, the focus should be on the password entry field 
            Entry_Email.Completed += (s, e) => Entry_Password.Focus();
            //after the password is entered, the focus should be on the repeat password entry field 
            Entry_Password.Completed += (s, e) => Entry_RepeatPassword.Focus();
            //after the username and the password have been entered, the login function should be triggered
            Entry_RepeatPassword.Completed += (s, e) => RegisterProcedure(s, e);
        }

        async void RegisterProcedure(object sender, EventArgs e)
        {
            //TODO: other password and email checks
            if (Entry_Password.Text != Entry_RepeatPassword.Text)
                await DisplayAlert("Register", "The two passwords you typed do not match. Try again.", "Ok");
            else
            {
                Debug.WriteLine("Register: New User object.");
                RegisterView user = new RegisterView(
                    Entry_FirstName.Text,
                    Entry_LastName.Text,
                    Entry_Email.Text,
                    Entry_Password.Text,
                    Entry_RepeatPassword.Text,
                    DatePicker_Birthday.Date,
                    Picker_Gender.SelectedItem.ToString()
                    );

                Debug.WriteLine("Register: Check if all fields are completed.");
                if (user.CheckNullInformation())
                {
                    var result = false;
                    //TODO: check for InternetConnection before calling the webserver
                    try
                    {
                        //Register API call
                        result = await App.RestService.Register(user);
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine("NullReferenceException in Register.");
                        result = false;
                    }
                    catch (TaskCanceledException)
                    {
                        result = false;
                        Console.WriteLine("TaskCanceledException in Register.");
                        await DisplayAlert("Register", "Not able to reach server in time.", "Ok");
                    }

                    Debug.WriteLine("Register: Check result from API.");
                    if (result)
                    {
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
            }
        }

        async void OnTapGestureForLogin(object sender, EventArgs args)
        {
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