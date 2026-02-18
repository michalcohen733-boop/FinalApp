using FinalApp.Models;
using FinalApp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinalApp.ViewModels
{
    [QueryProperty(nameof(ReceivedUser), "selectedUser")]

    public partial class UserDetailsPageViewModel : ViewModelBase
    {
        private AppUser _receivedUser;
        public AppUser ReceivedUser
        {
            get => _receivedUser;
            set
            {
                if (_receivedUser != value)
                {
                    _receivedUser = value;
                    OnPropertyChanged();
                    if (value != null)
                    {
                        MapUserToProperties();
                        OnPropertyChanged(nameof(CanDeleteUser));

                    }

 
                }
            }
        }
        #region Edit Properties
        public ObservableCollection<AppUser> AllUsers { get; set; }
        private string _firstName;
        public string FirstName { get => _firstName; set { _firstName = value; OnPropertyChanged(); } }

        private string _lastName;
        public string LastName { get => _lastName; set { _lastName = value; OnPropertyChanged(); } }

        private string _email;
        public string Email { get => _email; set { _email = value; OnPropertyChanged(); } }

        private string _mobile;
        public string Mobile { get => _mobile; set { _mobile = value; OnPropertyChanged(); } }
        #endregion
        public ICommand UpdateUserCommand { get; }
        public Command? DeleteUserCommand { get; }


        public UserDetailsPageViewModel()
        {
            UpdateUserCommand = new Command(async () => await UpdateButton_Clicked());
            AllUsers = new();
            DeleteUserCommand = new Command(async () => await DeleteUser());
        }
        private void MapUserToProperties()
        {
            if (ReceivedUser == null) return;

            FirstName = ReceivedUser.FirstName ?? "";
            LastName = ReceivedUser.LastName ?? "";
            Email = ReceivedUser.UserEmail ?? "";
            Mobile = ReceivedUser.UMobile ?? "";
        }
        public bool CanDeleteUser
        {
            get
            {
                var loggedInUser = (App.Current as App)?.CurrentUser;

                // הגנה: אם אחד מהם ריק, אל תבצע בדיקה ותחזיר false
                if (loggedInUser == null || ReceivedUser == null)
                    return false;

                bool isSelf = ReceivedUser.UserEmail == loggedInUser.UserEmail;

                if (!isSelf) return true;

                return !loggedInUser.IsAdmin;
            }
        }
        private async Task DeleteUser()
        {
            if (ReceivedUser == null) return;

            bool confirm = await Shell.Current.DisplayAlert("מחיקה", $"האם למחוק את {ReceivedUser.FirstName}?", "כן", "ביטול");

            if (confirm)
            {
                new DBMokup().RemoveUser(ReceivedUser);

                var app = (App.Current as App);
                var loggedInUser = app?.CurrentUser;

                if (loggedInUser != null && ReceivedUser.UserEmail == loggedInUser.UserEmail)
                {
                    app.CurrentUser = null;

                    await Shell.Current.GoToAsync("///SignInPage");
                }
                else
                {
                    await Shell.Current.GoToAsync("..");
                }
            }
        }
        private async Task UpdateButton_Clicked()
        {
            ReceivedUser.FirstName = FirstName;
            ReceivedUser.LastName = LastName;
            ReceivedUser.UserEmail = Email;
            ReceivedUser.UMobile = Mobile;

            DBMokup db = new DBMokup();
            db.UpdateUser(ReceivedUser);

            await Shell.Current.GoToAsync("///MainPage");
        }
        
    }
}
    
  