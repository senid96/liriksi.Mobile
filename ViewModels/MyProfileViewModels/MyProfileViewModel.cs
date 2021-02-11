using liriksi.Model;
using liriksi.Model.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace lirksi.Mobile.ViewModels
{
    public class MyProfileViewModel: BaseViewModel
    {
        private UserGetRequest _user;
        public UserGetRequest User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        public MyProfileViewModel()
        {
            Title = "My profile";
        }

        public void LoadUser()
        {
            User =  APIService._currentUser;
        }
    }
}
