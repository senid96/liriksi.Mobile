using Flurl.Http;
using liriksi.Model.Requests.rates;
using lirksi.Mobile.ViewModels.AlbumRatingsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lirksi.Mobile.Views.AlbumRatingsViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AlbumRatings : ContentPage
	{
        public AlbumRatingsViewModel model { get; set; }
		public AlbumRatings ()
		{
			InitializeComponent ();
            BindingContext = model = new AlbumRatingsViewModel();
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                await model.GetTop10Albums();
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

        private async void AlbumRateList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var item = e.SelectedItem as RateDetails;
                await Navigation.PushAsync(new AlbumDetails(item.Id));
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
    }
}