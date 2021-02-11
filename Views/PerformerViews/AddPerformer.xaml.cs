using Acr.UserDialogs;
using Flurl.Http;
using lirksi.Mobile.ViewModels.PerformerViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lirksi.Mobile.Views.PerformerViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPerformer : ContentPage
    {
        public AddPerformerViewModel model {get;set;}
		public AddPerformer ()
		{
			InitializeComponent ();
            BindingContext = model = new AddPerformerViewModel();
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
                PerfImg.Source = ImageSource.FromStream(() => file.GetStream());
                PerfImg.WidthRequest = 200;
                PerfImg.HeightRequest = 200;

                //set to viewmodel
                model.PerformerReq.Image = bytes;
            }
        }

        private async void SavePerformerBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (ValidationHelpers.ValidationHelper.IsAnyNullOrEmpty(model.PerformerReq))
                {

                    UserDialogs.Instance.Toast(ValidationHelpers.MessagesResource.all_fields_required);
                    return;
                }
                await model.SaveNewPerformer();
                await Navigation.PushAsync(new PerformerPage());
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