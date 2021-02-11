using Flurl.Http;
using liriksi.Model;
using lirksi.Mobile.ViewModels.PerformerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lirksi.Mobile.Views.PerformerViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PerformerPage : ContentPage
	{
        public PerformerViewModel model;
		public PerformerPage ()
		{
			InitializeComponent ();
            BindingContext = model = new PerformerViewModel();
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                await model.GetPerformers();
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

        private async void PerformerList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var item = e.SelectedItem as Performer;
                await Navigation.PushAsync(new PerformerDetails(item.Id));
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

        private async void PerformerName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                await model.GetPerformersBySearchObj();
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

        private async void PerformerSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                await model.GetPerformersBySearchObj();
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

        private async void PerformerArtisticName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                await model.GetPerformersBySearchObj();
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

        private void AddPerformerBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddPerformer());
        }
    }
}