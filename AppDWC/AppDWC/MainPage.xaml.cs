using AppDWC.API;
using AppDWC.Models;
using AppDWC;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace AppDWC
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            getData();
        }


        private async void getData()
        {
            var oauthToken = await SecureStorage.GetAsync("oauth_token");
            if (!string.IsNullOrEmpty(oauthToken))
            {
                txtEmail.IsVisible = false;
                txtPassword.IsVisible = false;
                txtPin.IsVisible = true;
                btnRegister.IsVisible = false;
                btnSignIn.IsVisible = false;
                btnPin.IsVisible = true;
            }
        }

        private async void btnRegister_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        private async void btnSignIn_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                txtSignInResult.Text = "Email can not be empty or null!";
            }
            else if (string.IsNullOrEmpty(txtPassword.Text))
            {
                txtSignInResult.Text = "Password can not be empty or null!";
            }
            else
            {
                var authAPI = RestService.For<IAuthAPI>("http://10.0.2.2:3000");
                User user = new User
                {
                    Email = txtEmail.Text.ToString(),
                    Password = txtPassword.Text.ToString()
                };

                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("email", user.Email);
                data.Add("password", user.Password);
                var result = await authAPI.SignIn(data);

                    txtSignInResult.Text = result.ToString();
                    if (result.Contains("Login Successful"))
                    {
                        await Navigation.PushAsync(new DashboardPage());
                    }
                    else if (result.Contains("Configure PIN"))
                    {
                        await Navigation.PushAsync(new PinPage(user));
                    } 
            }
        }

        private async void btnPin_Clicked(object sender, EventArgs e)
        {
            var authAPI = RestService.For<IAuthAPI>("http://10.0.2.2:3000");
            var t = await SecureStorage.GetAsync("oauth_token");
            var p = txtPin.Text.ToString();
            User user = new User
            {
                Token = Int32.Parse(t),
                Pin = Int32.Parse(p)
            };

            Dictionary<string, int> data = new Dictionary<string, int>();
            data.Add("token", user.Token);
            data.Add("pin", user.Pin);


            var result = await authAPI.LoginPin(data);

            txtSignInResult.Text = result.ToString();
            if (result.Contains("Login Successful"))
            {
                App.IsUserLoggedIn = true;
                await Navigation.PushAsync(new DashboardPage());
            }
        }
    }
}