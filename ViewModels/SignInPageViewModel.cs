using FinalApp.Models;
using FinalApp.Service;
using FinalApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using FinalApp.Service;

namespace FinalApp.ViewModels
{
    public class SignInPageViewModel : ViewModelBase
    {
        DBMokup _db;
        private string _userName;
        private string _password;
        private string _togglePasswordButtonText;
        private bool _entryAsPassword;
        private string _loginMessage;
        private bool _signInMessageVisible;
        private Color _signInColor;
        private bool _isRememberMeChecked;
        public INavigation Navigation { get; set; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand SignInCommand { get; }
        public ICommand GoToSignUpCommand { get; }

        public SignInPageViewModel()
        {
            _db = new DBMokup();
            EntryAsPassword = true;
            togglePasswordButtonText = "\ue8f4";
            SignInMessageVisible = false;
            loginMessage = string.Empty;

            ShowPasswordCommand = new Command(TogglePasswordButton);
            SignInCommand = new Command(SignInButton, CanSignIn);
            GoToSignUpCommand = new Command(async () => await Navigation!.PushAsync(new SignUpPage()));
        }
        public bool RememberMeChecked
        {
            get { return _isRememberMeChecked; }
            set
            {
                if (_isRememberMeChecked != value)
                {
                    _isRememberMeChecked = value;
                    OnPropertyChanged();
                }

            }
        }
        public Color SignInColor
        {
            get { return _signInColor; }
            set
            {
                if (_signInColor != value)
                {
                    _signInColor = value;
                    OnPropertyChanged();
                }

            }
        }
        public string togglePasswordButtonText
        {
            get { return _togglePasswordButtonText; }
            set
            {
                if (_togglePasswordButtonText != value)
                {
                    _togglePasswordButtonText = value;
                    OnPropertyChanged();
                }

            }
        }
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged();
                    //OnPropertyChanged(nameof(IsSignInButtonEnabled));
                    (SignInCommand as Command).ChangeCanExecute();
                }
            }
        }
        public string UserPassword
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                    //OnPropertyChanged(nameof(IsSignInButtonEnabled));
                    (SignInCommand as Command).ChangeCanExecute();
                }

            }

        }
        public string loginMessage
        {
            get { return _loginMessage; }
            set
            {
                if (_loginMessage != value)
                {
                    _loginMessage = value;
                    OnPropertyChanged();
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
        public bool SignInMessageVisible
        {
            get { return _signInMessageVisible; }
            set
            {
                if (_signInMessageVisible != value)
                {
                    _signInMessageVisible = value;
                    OnPropertyChanged();
                }
            }
        }
        private void TogglePasswordButton()
        {
            EntryAsPassword = !EntryAsPassword;
            if (EntryAsPassword)
                togglePasswordButtonText = "\ue8f4";
            else
                togglePasswordButtonText = "\ue8f5";
        }

        private void SignInButton()
        {
            SignInMessageVisible = true;
            if (_db.isExist(UserName, UserPassword))
            {
            //    if (RememberMeChecked)
            //    {
            //        // save user to secure storage
            //        SecureStorage.Default.SetAsync("current_user_object", UserName);
            //    }
                (App.Current as App)!.CurrentUser = _db.GetUserByEmail(UserName);
                Application.Current!.Windows[0].Page = new AppShell();
            }
            else
            {
                loginMessage = "user does not exist!";
                SignInColor = Colors.Red;
            }
        }

        private bool CanSignIn()
        {
            return !(string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserPassword));
        }

        //internal async void OnAppearing()
        //{
        //    // chack if user exist in storage
        //    string? token = await SecureStorage.Default.GetAsync("current_user_object");
        //    if (!string.IsNullOrEmpty(token))
        //    {
        //        AppUser user =  _db.GetUserByEmail(token);
        //        if (user != null)
        //        {
        //            // set current user 
        //            (App.Current as App)!.CurrentUser = user;
        //            //navigte to main page of shell
        //            var mainPage = IPlatformApplication.Current!.Services.GetService<AppShell>();
        //            Application.Current!.Windows[0].Page = mainPage;
        //        }
        //    }

        //}
    }
}
