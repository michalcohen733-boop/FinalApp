using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalApp.ViewModels
{
    public class AdminPageViewModel : ViewModelBase
    {
        public Command GoToUsersListCommand { get; }

        public AdminPageViewModel()
        {
            GoToUsersListCommand = new Command(async () =>
            {

                await Shell.Current.GoToAsync("UsersListPage");
            });
        }

    }
}
