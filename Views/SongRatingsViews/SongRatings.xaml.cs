using Flurl.Http;
using liriksi.Model.Requests.rates;
using lirksi.Mobile.ViewModels.SongRatingsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lirksi.Mobile.Views.SongRatingsViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SongRatings : ContentPage
	{
        public SongRatingsViewModel model { get; set; }
        public SongRatings ()
		{
			InitializeComponent ();
            BindingContext = model = new SongRatingsViewModel();
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                await model.GetTop10Songs();
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Unauthorized)
                {
                    SecureStorage.Remove("username");
                    SecureStorage.Remove("password");
                    Application.Current.MainPage = new NavigationPage(new Views.LoginPage());
                }
                else
                    await Navigation.PushAsync(new ErrorPage());
            }
        }

        private async void SongRateList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as RateDetails;
            await Navigation.PushAsync(new SongDetails(item.Id));
        }
    }
}