
using AppDWC.API;
using AppDWC.Models;
using Refit;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppDWC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void btnRegisteration_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirstName.Text))
            {
                txtRegisterResult.Text = "First Name can not be empty or null!";
            }
            else if (string.IsNullOrEmpty(txtLastName.Text))
            {
                txtRegisterResult.Text = "Last Name can not be empty or null!";
            }
            else if (string.IsNullOrEmpty(txtMobile.Text))
            {
                txtRegisterResult.Text = "Mobile can not be empty or null!";
            }
            else if (string.IsNullOrEmpty(txtEmail.Text))
            {
                txtRegisterResult.Text = "Email can not be empty or null!";
            }
            else if (string.IsNullOrEmpty(txtPassword.Text))
            {
                txtRegisterResult.Text = "Password can not be empty or null!";
            }
            else
            {
                var authAPI = RestService.For<IAuthAPI>("http://10.0.2.2:3000");
                User user = new User
                {
                    Email = txtEmail.Text.ToString(),
                    FirstName = txtFirstName.Text.ToString(),
                    LastName = txtLastName.Text.ToString(),
                    Mobile = txtMobile.Text.ToString(),
                    Password = txtPassword.Text.ToString()
                };
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("firstname", user.FirstName);
                data.Add("lastname", user.LastName);
                data.Add("email", user.Email);
                data.Add("mobile", user.Mobile);
                data.Add("password", user.Password);
                var result = await authAPI.Register(data);




                txtRegisterResult.Text = result.ToString();
                if (result.Contains("Registration Successful"))
                {
                    await Navigation.PushAsync(new MainPage());
                }
            }
        }
    }
}
