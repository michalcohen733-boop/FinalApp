//using Android.App.Job;
//using Android.Health.Connect.DataTypes.Units;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using FinalApp.Models;
using FinalApp.Service;
using FinalApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinalApp.ViewModels
{
    public class SignUpPageViewModel : ViewModelBase
    {
        DBMokup _db;

        private string? _firstName;
        private string? _lastName;
        private string? _email;
        private string? _password;
        private string? _mobile;
        private bool _entryAsPassword;
        private string? _passwordIconCode;
        public ICommand? ShowPasswordCommand { get; }
        public ICommand? SignUpCommand { get; }
        public ICommand GoToSignInCommand { get; }
        public INavigation Navigation { get; set; }

        public SignUpPageViewModel()
        {
            _db = new DBMokup();

            EntryAsPassword = true;
            PasswordIconCode = "\ue8f4";
            ShowPasswordCommand = new Command(TogglePasswordButton);
            SignUpCommand = new Command(SignUp, Validate);
            GoToSignInCommand = new Command(async () => await GoToSignInPage());


        }

        private async Task GoToSignInPage()
        {
            Application.Current!.Windows[0].Page = new NavigationPage(new SignInPage(new ViewModels.SignInPageViewModel()));
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged();
                    (SignUpCommand as Command)?.ChangeCanExecute();

                }
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged();
                    (SignUpCommand as Command)?.ChangeCanExecute();

                }
            }
        }
        public string UserEmail
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                    (SignUpCommand as Command)?.ChangeCanExecute();

                }
            }
        }
        public string UserPassword
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                    (SignUpCommand as Command)?.ChangeCanExecute();

                }
            }
        }
        public string Mobile
        {
            get => _mobile;
            set
            {
                if (_mobile != value)
                {
                    _mobile = value;
                    OnPropertyChanged();
                    (SignUpCommand as Command)?.ChangeCanExecute();

                }
            }
        }
        public bool EntryAsPassword
        {
            get { return _entryAsPassword; }
            set
            {
                if (_entryAsPassword != value)
                {
                    _entryAsPassword = value;
                    OnPropertyChanged();
                    //OnPropertyChanged(nameof(IsSignInButtonEnabled));

                }

            }
        }
        public string PasswordIconCode
        {
            get => _passwordIconCode;
            set
            {
                if (_passwordIconCode != value)
                {
                    _passwordIconCode = value;
                    OnPropertyChanged();
                    (SignUpCommand as Command)?.ChangeCanExecute();

                }
            }
        }
        private async void SignUp()
        {
            //Register user into DB
            //Save User to Current User
            //Go to Main Page
            string first = (FirstName ?? "").Trim();
            string last = (LastName ?? "").Trim();
            string email = (UserEmail ?? "").Trim();
            string pass = (UserPassword ?? "").Trim();
            string mobile = (Mobile ?? "").Trim();
            AppUser newUser = new AppUser
            {
                FirstName = first,
                LastName = last,
                UserEmail = email,
                UserPassword = pass,
                UMobile = mobile,
                IsAdmin = false,
                RegDate = DateTime.Now
            };

            _db.AddUser(newUser);
            if (App.Current is App currentApp)
            {
                currentApp.CurrentUser = newUser;
            }
            Application.Current!.Windows[0].Page = new AppShell();

           // await Toast.Make($"SignUp new user", ToastDuration.Short, 14).Show();
        }
        private bool Validate()
        {
            var fnameOK = !string.IsNullOrEmpty(FirstName);
            var lnameOK = !string.IsNullOrEmpty(LastName);
            var emailOK = !string.IsNullOrEmpty(UserEmail);
            var passOK = string.IsNullOrEmpty(UserPassword) ? false :
            UserPassword.Length > 5;
            var mobileOK = string.IsNullOrEmpty(Mobile) ? false :
            Mobile.Length == 10;
            return fnameOK && lnameOK && emailOK && passOK && mobileOK;
        }
        private void TogglePasswordButton()
        {
            EntryAsPassword = !EntryAsPassword;
            if (EntryAsPassword)
                PasswordIconCode = "\ue8f4";
            else
                PasswordIconCode = "\ue8f5";
        }
       

    }
}
