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
	public partial class AddActivityPage : ContentPage
	{

        public AddActivityPage ()
		{
			InitializeComponent ();
            Init();
        }

        void Init()
        {
            Lbl_PageTitle.TextColor = Constants.MainTextColor;
            Lbl_PageTitle.FontSize = 25;
            Lbl_PageTitle.FontAttributes = FontAttributes.Bold;
            Lbl_PageTitle.Text = "What do you want to talk about?";
            Lbl_PageTitle.HorizontalTextAlignment = TextAlignment.Center;

            Img_Guests.Source = "multipleusers.png";
            Img_Location.Source = "locationicon.png";
            Img_Date.Source = "dateicon.png";
        }
    }
}