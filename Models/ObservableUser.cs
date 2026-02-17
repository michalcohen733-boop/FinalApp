//using Android.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FinalApp.Models
{
    public class ObservableUser : INotifyPropertyChanged
    {
        AppUser _user;
        public event PropertyChangedEventHandler? PropertyChanged;
        //public int Id;
        public string? FirstName;
        public string? LastName;
        public string? UEmail;
        public string? UPassword;
        public string? UMobile;
        public DateTime UBDate;
        public DateTime RegDate;
        public bool IsAdmin;
        public AppUser User
        {
            get => _user;
        }

        public int Id
        {
            get => _user.Id;
            set
            {
                if (_user.Id != value)
                {
                    _user.Id = value;
                    OnPropertyChanged();
                }
            }
        }
    
        public ObservableUser(AppUser user)
        {
            _user = user;
        }
            protected virtual void OnPropertyChanged([CallerMemberName] string?
            propertyName = null)
            {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }  
}