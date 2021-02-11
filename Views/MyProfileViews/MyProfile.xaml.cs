using Flurl.Http;
using liriksi.Model.Requests;
using lirksi.Mobile.ViewModels;
using lirksi.Mobile.Views.MyProfileViews;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lirksi.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyProfile : ContentPage
	{
        public MyProfileViewModel model { get; set; }
        public MyProfile ()
		{
			InitializeComponent ();
            BindingContext = model = new MyProfileViewModel();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                model.LoadUser();
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

        private void EditProfile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditProfile());
        }
    }
}