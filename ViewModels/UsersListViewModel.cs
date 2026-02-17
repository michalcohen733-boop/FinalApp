using FinalApp.Models;
using FinalApp.Service;
using FinalApp.Views;
using System.Collections.ObjectModel;

namespace FinalApp.ViewModels
{
    public class UsersListViewModel : ViewModelBase
    {
        #region Fields 
        private string? _searchText;
        private List<AppUser> _allUsers = new List<AppUser>(); // אתחול הרשימה למניעת Null
        public ObservableCollection<AppUser> AllUsers { get; set; } = new ObservableCollection<AppUser>();
        private bool _isRefreshing;
        #endregion

        #region Properties 
        public string? SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                    ClearFilterCommand?.ChangeCanExecute();
                    OnSearch(); // ביצוע חיפוש בזמן אמת כשנוח
                }
            }
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Commands 
        public Command SearchCommand { get; }
        public Command ClearFilterCommand { get; }
        public Command RefreshCommand { get; } // פקודה עבור ה-RefreshView
        public Command<AppUser> OpenUpdatePageCommand { get; }
        public Command<AppUser> DeleteUserCommand { get; }
        #endregion

        public UsersListViewModel()
        {
            SearchCommand = new Command(OnSearch);
            ClearFilterCommand = new Command(ClearFilter);
            RefreshCommand = new Command(RefreshList);
            OpenUpdatePageCommand = new Command<AppUser>(async (user) => await OpenUpdatePage(user));
            //DeleteUserCommand = new Command<AppUser>(async (user) => await OnDeleteUser(user));

            RefreshList();
        }

        public void RefreshList()
        {
            IsRefreshing = true;

            _allUsers = new DBMokup().GetUsers();

            AllUsers.Clear();
            foreach (var user in _allUsers)
            {
                AllUsers.Add(user);
            }

            IsRefreshing = false;
        }

        private async Task OpenUpdatePage(AppUser user)
        {
            if (user == null) return;

            var parameters = new Dictionary<string, object>
            {
                { "selectedUser", user }
            };

            // וודאי ש-UserDetailsPage רשום ב-AppShell.xaml.cs
            await Shell.Current.GoToAsync(nameof(UserDetailsPage), parameters);
        }

        //private async Task OnDeleteUser(AppUser user)
        //{
        //    if (user == null) return;

        //    bool confirm = await Shell.Current.DisplayAlert("מחיקה", $"האם למחוק את {user.FirstName}?", "כן", "לא");
        //    if (confirm)
        //    {
        //        new DBMokup().RemoveUser(user);
        //        RefreshList();
        //    }
        //}

        private void OnSearch()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                AllUsers.Clear();
                foreach (var user in _allUsers) AllUsers.Add(user);
                return;
            }

            var filtered = _allUsers.Where(u =>
                (u.FirstName?.ToLower().Contains(SearchText.ToLower()) ?? false) ||
                (u.LastName?.ToLower().Contains(SearchText.ToLower()) ?? false)).ToList();

            AllUsers.Clear();
            foreach (var user in filtered) AllUsers.Add(user);
        }

        private void ClearFilter()
        {
            SearchText = string.Empty;
            OnSearch();
        }
    }
}