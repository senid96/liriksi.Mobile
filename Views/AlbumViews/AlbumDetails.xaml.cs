using Acr.UserDialogs;
using Flurl.Http;
using liriksi.Model;
using liriksi.Model.Requests;
using lirksi.Mobile.ViewModels;
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
    public partial class AlbumDetails : ContentPage
    {
        public AlbumDetailsViewModel model { get; set; }
        public AlbumDetails(int albumId)
        {
            InitializeComponent();
            BindingContext = model = new AlbumDetailsViewModel()
            {
                _albumId = albumId
            };
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                await model.Init();

                //if its already rated, disable rate feature
                if (model.UserRate != null)
                {
                    //int index = model.UserRate.Rate - 2;
                    //PickerRate.SelectedIndex = model.UserRate.Rate;
                    PickerRate.IsVisible = false;
                    Comment.IsEnabled = false;
                    Rate.IsVisible = false;
                }
                else
                {
                    //instance userRate VM, so it can be used with picker and rate feature
                    model.UserRate = new liriksi.Model.UsersAlbumRate();
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

        private async void SongList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var item = e.SelectedItem as SongGetRequest;
                await Navigation.PushAsync(new SongDetails(item.Id));
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

        private async void Rate_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (ValidationHelpers.ValidationHelper.IsAnyNullOrEmpty(model.UserRate))
                {
                    UserDialogs.Instance.Toast(ValidationHelpers.MessagesResource.rate_required);
                    return;
                }
                await model.RateAlbum();
                //call onAppearing to refresh rateFeature(to disable it)
                OnAppearing();
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

        private void PickerRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PickerRate.SelectedIndex == -1)
            {

            }
            else
            {
                model.UserRate.Rate = Convert.ToInt32(PickerRate.SelectedItem);
            }
        }
    }
}