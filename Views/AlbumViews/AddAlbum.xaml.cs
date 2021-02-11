using Acr.UserDialogs;
using Flurl.Http;
using lirksi.Mobile.ValidationHelpers;
using lirksi.Mobile.ViewModels.AlbumViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lirksi.Mobile.Views.AlbumViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddAlbum : ContentPage
	{
        public AddAlbumViewModel model { get; set; }
        public AddAlbum ()
		{
            InitializeComponent();
            BindingContext = model = new AddAlbumViewModel();
		}

        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                await model.GetPerformers();
                PerformerPicker.SelectedIndex = 0;
                await model.GetGenres();
                GenrePicker.SelectedIndex = 0;

                //populate yearlist picker
                for (int i = 1970; i < DateTime.Now.Year; i++)
                {
                    model.YearList.Add(i);
                }
                PickerYear.SelectedIndex = 0;
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

        private async void UploadImage_Clicked(object sender, EventArgs e)
        {
            byte[] bytes;
            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.MaxWidthHeight,
                CompressionQuality = 50
            });

            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.GetStream().CopyTo(memoryStream);
                    bytes = memoryStream.ToArray();
                }
                AlbumImage.Source = ImageSource.FromStream(() => file.GetStream());
                AlbumImage.WidthRequest = 200;
                AlbumImage.HeightRequest = 200;

                //set to viewmodel
                model.AlbumReq.Image = bytes;
            }
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (ValidationHelper.IsAnyNullOrEmpty(model.AlbumReq))
                {
                    UserDialogs.Instance.Toast(ValidationHelpers.MessagesResource.all_fields_required);
                    return;
                }

                await model.AddAlbum();
                await Navigation.PushAsync(new AlbumPage());
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