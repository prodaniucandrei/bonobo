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

            //after the username is entered, the focus should be on the password entry field 
            Entry_Username.Completed += (s, e) => Entry_Password.Focus();
            //after the username and the password have been entered, the login function should be triggered
            Entry_Password.Completed += (s, e) => SignInProcedure(s, e);
        }

        void SignInProcedure(object sender, EventArgs e)
        {
            User user = new User(Entry_Username.Text, Entry_Password.Text);
            if (user.CheckInformation())
            {
                //TODO: delete previously saved user; should happen on sign out procedure
                App.UserDatabase.DeleteUser(0);
                //save current logged user in local DB
                App.UserDatabase.SaveUser(user);
                //await makes sure that the code below won't be executed before the user presses 'OK'
                DisplayAlert("Login", "Login success. Hi " + App.UserDatabase.GetUser().Username, "OK");
            }
            else
            {
                DisplayAlert("Login", "Login not correct: empty username or password.", "Ok");
            }
        }
	}
}