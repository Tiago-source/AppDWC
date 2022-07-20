using System;
using System.Collections.Generic;
using AppDWC.API;
using AppDWC.Models;
using Refit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace AppDWC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PinPage : ContentPage
    {
        public PinPage(User user)
        {
            InitializeComponent();
            lblEmail.Text = user.Email;
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }


        private async void btnRegistPin_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirstDigit.Text))
            {
                txtRegisterResult.Text = "First Digit can not be empty or null!";
            }
            else if (string.IsNullOrEmpty(txtSecDigit.Text))
            {
                txtRegisterResult.Text = "Second Digit can not be empty or null!";
            }
            else if (string.IsNullOrEmpty(txtTrdDigit.Text))
            {
                txtRegisterResult.Text = "Third Digit not be empty or null!";
            }
            else if (string.IsNullOrEmpty(txtFrtDigit.Text))
            {
                txtRegisterResult.Text = "Fouth Digit can not be empty or null!";
            }
            else
            {
                var authAPI = RestService.For<IAuthAPI>("http://10.0.2.2:3000");
                var pdigit = txtFirstDigit.Text.ToString();
                var sdigit = txtSecDigit.Text.ToString();
                var tdigit = txtTrdDigit.Text.ToString();
                var qdigit = txtFrtDigit.Text.ToString();
                int newNumber = int.Parse(pdigit + sdigit + tdigit + qdigit);
                string str = lblEmail.Text.ToString();

                User user = new User
                {
                    Pin = newNumber,
                    Email = str
            };
                Dictionary<string, string> data = new Dictionary<string, string>();
                var pin = user.Pin;
                data.Add("pin", pin.ToString());
                data.Add("email", user.Email);
                var result = await authAPI.RegisterPin(data);

                var res = await authAPI.GetToken(data);
                await SecureStorage.SetAsync("oauth_token", res);


                txtRegisterResult.Text = result.ToString();
                if (result.Contains("Pin Registered"))
                {
                    await Navigation.PushAsync(new MainPage());
                }
            }     
        }
    }
}