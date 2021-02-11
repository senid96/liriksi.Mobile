using Flurl.Http;
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
	public partial class PerformerDetails : ContentPage
	{
        public PerformerDetailsViewModel model { get; set; }
        public PerformerDetails(int performerId)
        {
            BindingContext = model = new PerformerDetailsViewModel()
            {
                _performerId = performerId
            };
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                await model.GetPerformer();
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