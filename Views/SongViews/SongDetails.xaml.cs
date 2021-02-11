using Acr.UserDialogs;
using Flurl.Http;
using liriksi.Model.Requests;
using lirksi.Mobile.Services.OfflineModeServices;
using lirksi.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lirksi.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SongDetails : ContentPage
    {

        private SongDetailsViewModel model { get; set; }
        public SongDetails(int songId)
        {
            InitializeComponent();
            BindingContext = model = new SongDetailsViewModel()
            {
                _songId = songId
            };
        }

        protected override async void OnAppearing()
        {
            try
            {
                if (Connectivity.NetworkAccess < NetworkAccess.Internet)
                {
                    base.OnAppearing();
                    await model.GetSongDetailsBySongIdOffline();
                    Rate.IsVisible = false;
                    Comment.IsVisible = false;
                    PickerRate.IsVisible = false;
                    DownloadSongBtn.IsVisible = false;
                    recommendedLabel.IsVisible = false;
                }
                else
                {
                    base.OnAppearing();
                    await model.Init();
                    await model.GetRecommendedSongs();

                    //if its already rated, disable rate feature
                    if (model.UserRate != null)
                    {
                        PickerRate.SelectedItem = model.UserRate.Rate;
                        PickerRate.IsVisible = false;
                        Comment.IsEnabled = false;
                        Rate.IsVisible = false;
                    }
                    else
                    {
                        //instance userRate VM, so it can be used with picker and rate feature
                        model.UserRate = new liriksi.Model.UsersSongRate();
                    }
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

        private async void Rate_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (ValidationHelpers.ValidationHelper.IsAnyNullOrEmpty(model.UserRate))
                {
                    UserDialogs.Instance.Toast(ValidationHelpers.MessagesResource.rate_required);
                    return;
                }
                await model.RateSong();
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

        private async void RecommendedList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as SongGetRequest;
            await Navigation.PushAsync(new SongDetails(item.Id));
        }

        private async void DownloadSongBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                SongGetRequest obj = model.GetSongByIdOffline(model._songId);
                if (obj != null)
                {
                    UserDialogs.Instance.Toast(ValidationHelpers.MessagesResource.already_downloaded);
                    return;
                }
                else
                {
                    await model.DownloadSongOffline();
                    UserDialogs.Instance.Toast(ValidationHelpers.MessagesResource.download_success);

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
    }
}