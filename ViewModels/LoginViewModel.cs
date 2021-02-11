using liriksi.Model.Requests;
using lirksi.Mobile.ValidationHelpers;
using lirksi.Mobile.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace lirksi.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        /* service */
        private readonly APIService _userService = new APIService("User");

        /* ViewModels */
        string _username = string.Empty;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        string _password = string.Empty;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        /* Methods */
        public LoginViewModel()
        {
        }

        public async Task Login()
        {

            APIService._username = Username;     
            APIService._password = Password;

            //za brzi razvoj samo TODO obrisati kasnije
            //APIService._username = "testiranje"; 
            //APIService._password = "testiranje";

            if(Connectivity.NetworkAccess < NetworkAccess.Local)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You need to be connected to the WLAN", "OK");
                return;
            }

            try
            {
                APIService._currentUser = await _userService.Get<UserGetRequest>(null, "GetMyProfile");
                if (APIService._currentUser != null && APIService._currentUser.UserType.Type.Equals("User"))
                {
                    Application.Current.MainPage = new MainPage();
                    await SecureStorage.SetAsync("username", Username);
                    await SecureStorage.SetAsync("password", Password);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Authentication", "Login data incorrect or users has been blocked", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Exception authentication", "Username or password incorect", "Ok");
            }

        }

      
    }
}
