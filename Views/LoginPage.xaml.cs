using Acr.UserDialogs;
using lirksi.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace lirksi.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel model { get; set; }
        public LoginPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            BindingContext = model = new LoginViewModel();
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(model.Password)|| string.IsNullOrEmpty(model.Username))
            {
                UserDialogs.Instance.Toast(ValidationHelpers.MessagesResource.login_required);
                return;
            }
            await model.Login();

        }
    }
}