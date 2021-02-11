using liriksi.Model;
using liriksi.Model.Requests;
using liriksi.Model.Requests.user;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace lirksi.Mobile.ViewModels.MyProfileViewModels
{
    public class EditProfileViewModel:BaseViewModel
    {
        private readonly APIService _userService = new APIService("user");

        /* View models */
        private UserUpdateRequest _user;
        public UserUpdateRequest User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        public EditProfileViewModel()
        {
            Title = "Edit profile";
        }

        /* methods */
        public void LoadUser()
        {
            User = new UserUpdateRequest()
            {
                Id = APIService._currentUser.Id,
                Name = APIService._currentUser.Name,
                Surname = APIService._currentUser.Surname,
                Username = APIService._currentUser.Username,
                Email = APIService._currentUser.Email,
                PhoneNumber = APIService._currentUser.PhoneNumber,
                Image = APIService._currentUser.Image
            };
        }

        public async Task SaveChanges()
        {
            await _userService.Update<UserGetRequest>(User.Id, User);
            APIService._currentUser = await _userService.Get<UserGetRequest>(null, "GetMyProfile");
        }

    }
}
