using Flurl.Http;
using liriksi.Model;
using lirksi.Mobile.ViewModels;
using lirksi.Mobile.Views.AlbumViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lirksi.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlbumPage : ContentPage
    {
        public AlbumViewModel model { get; set; }

        public AlbumPage()
        {
            InitializeComponent();
            BindingContext = model = new AlbumViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                await model.GetPerformers();
                PerformerPicker.SelectedIndex = 0;
                await model.GetAlbums();
            }
            catch (FlurlHttpException ex)
            {
                if(ex.Call.HttpStatus == System.Net.HttpStatusCode.Unauthorized)
                {
                    SecureStorage.Remove("username");
                    SecureStorage.Remove("password");
                    Application.Current.MainPage = new NavigationPage(new Views.LoginPage());
                }else
                    await Navigation.PushAsync(new ErrorPage());
            }
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Album;
            try
            {
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

        private async void PerformerPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (PerformerPicker.SelectedIndex == -1)
                {
                }
                else
                {
                    await model.GetAlbums();
                }
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

        private async void AddAlbumBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new AddAlbum());
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