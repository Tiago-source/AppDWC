using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;
using AppDWC.Models;
using AppDWC.API;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppDWC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageDetail : ContentPage
    {
        public PageDetail(Quotes objc)
        {
            InitializeComponent();
            lblNome.Text = "Name: " + objc.nome;
            int val = objc.id;
            lblId.Text = val.ToString();
            lblValue.Text = "Value:" + objc.value;
            lblFrom.Text = "From:" + objc.from;
            lblRequest_id.Text = "Request id: " + objc.request_id;
            lblSpecialty.Text = "Speciality :" + objc.specialty;
            lblStartDate.Text = "Start Date :" + objc.start_date;
            lblTo.Text = "To: " + objc.to;
            lblTurnaround.Text = "Turnaround: " + objc.turnaround;
            lblType.Text = "Type: " + objc.type;
            lblWords.Text = "Words: " + objc.words;
            lblState.Text = "State: " + objc.state;

        }

        private async void ApproveQuote(object sender, EventArgs e)
        {
           
            var authAPI = RestService.For<IAuthAPI>("http://10.0.2.2:3000");
            string str = lblId.Text.ToString();
            int var = Convert.ToInt32(str);
            
            Quotes quote = new Quotes
                {
                    id = var,
                    state = "Approved"
                };

                Dictionary<string,string> data = new Dictionary<string,string>();
                int val = quote.id;
                data.Add("id", val.ToString());
                data.Add("state", quote.state);
                var result = await authAPI.Update(data);

                txtUpdateResult.Text = result.ToString();
                if (result.Contains("Success updated!"))
                {
                    await Navigation.PushAsync(new DashboardPage());
                }
            }

        private async void RejectQuote(object sender, EventArgs e)
        {

            var authAPI = RestService.For<IAuthAPI>("http://10.0.2.2:3000");
            string str = lblId.Text.ToString();
            int var = Convert.ToInt32(str);

            Quotes quote = new Quotes
            {
                id = var,
                state = "Rejected"
            };

            Dictionary<string, string> data = new Dictionary<string, string>();
            int val = quote.id;
            data.Add("id", val.ToString());
            data.Add("state", quote.state);
            var result = await authAPI.Update(data);

            txtUpdateResult.Text = result.ToString();
            if (result.Contains("Success updated!"))
            {
                await Navigation.PushAsync(new DashboardPage());
            }
        }
    }
}