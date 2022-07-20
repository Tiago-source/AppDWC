using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using AppDWC.Models;


namespace AppDWC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {

        private const string Url = "http://192.168.1.103:3000/quotes";
        private readonly HttpClient _client = new HttpClient();
        private ObservableCollection<Quotes> _quotes;
        public DashboardPage()
        {
            InitializeComponent();
        }

        async override protected void OnAppearing()
        {
            base.OnAppearing();
            string rescontent = await _client.GetStringAsync(Url);

            List<Quotes> quotes = JsonConvert.DeserializeObject<List<Quotes>>(rescontent);

            _quotes = new ObservableCollection<Quotes>(quotes);
            lvQuotes.ItemsSource = _quotes;

    

            lvQuotes.ItemSelected += QuoteItemSelected;
        

            this.BindingContext = new DashboardPage();

            base.OnAppearing();
        }
        private void QuoteItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Quotes objq;

            if (e.SelectedItem != null)
            {
                objq = (Quotes)e.SelectedItem;
                lvQuotes.SelectedItem = null;

                Navigation.PushAsync(new PageDetail(objq));
            }
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            await Navigation.PushAsync(new MainPage());
        }
    }
}
