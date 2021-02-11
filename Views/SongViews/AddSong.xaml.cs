using Acr.UserDialogs;
using Flurl.Http;
using lirksi.Mobile.ViewModels.SongViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lirksi.Mobile.Views.SongViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddSong : ContentPage
	{
        public AddSongViewModel model { get; set; }
		public AddSong ()
		{
			InitializeComponent ();
            BindingContext = model = new AddSongViewModel();
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                await model.GetPerformers();
                PerformerPicker.SelectedIndex = 0;
                await model.GetAlbums();
                AlbumPicker.SelectedIndex = 0;
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
                if (PerformerPicker.SelectedIndex != -1)
                    await model.GetAlbums();

                AlbumPicker.SelectedIndex = 0;
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

        private async void SaveSongBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (ValidationHelpers.ValidationHelper.IsAnyNullOrEmpty(model.SongReq))
                {
                    UserDialogs.Instance.Toast(ValidationHelpers.MessagesResource.all_fields_required);
                    return;
                }
                await model.AddSong();
                await Navigation.PushAsync(new SongPage());
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