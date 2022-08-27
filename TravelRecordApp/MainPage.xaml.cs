using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TravelRecordApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // Logo image
            // Need this for passing into image source
            var assembly = typeof(MainPage);

            imageLogo.Source = ImageSource.FromResource("TravelRecordApp.Assets.Images.app-logo.png", assembly);
        }

        void loginButton_Clicked(System.Object sender, System.EventArgs e)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(emailEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);

            if (isEmailEmpty || isPasswordEmpty)
            {

            }
            else
            {
                // Authentication and tokens should be done here
                // If successful proceed,
                // Else show a dialog for failed authentication
                Navigation.PushAsync(new HomePage());
            }
        }
    }
}
