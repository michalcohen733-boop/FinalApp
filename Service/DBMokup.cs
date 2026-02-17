using FinalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalApp.Service
{
    public class DBMokup
    {
        private static List<AppUser> _users = new List<AppUser>();
        public List<AppUser> GetUsers() { return _users; }
        public DBMokup()
        {
            if (_users.Count == 0)
            {
                _users.Add(new AppUser { FirstName = "Michal", LastName = "Cohen", UserEmail = "a", UserPassword = "a", IsAdmin = true,  UMobile = "0585550954" });
                _users.Add(new AppUser { FirstName = "Yali", LastName = "Matok", UserEmail = "b", UserPassword = "b", UMobile="0503131965" });
                _users.Add(new AppUser { FirstName = "Alon", LastName = "Zisman", UserEmail = "user2@mail.com", UserPassword = "pass2" });
            }
        }
        public bool isExist(string uEmail, string uPass)
        {
            return _users.Any(u => u.UserEmail == uEmail && u.UserPassword == uPass);
        }
        public AppUser? GetUser(string uEmail, string uPass)
        {
            return _users.FirstOrDefault(u => u.UserEmail == uEmail && u.UserPassword == uPass);
        }
        public AppUser GetUserByEmail(string uEmail)
        {
            return _users.FirstOrDefault(u => u.UserEmail == uEmail);
        }
        public void AddUser(AppUser user)
        {
            if (user != null)
            {
                _users.Add(user);
            }
        }
        public void RemoveUser(AppUser user)
        {
            if (user != null && _users.Contains(user))
            {
                _users.Remove(user);
            }
        }
        public void UpdateUser(AppUser user)
        {
            if (user != null && _users.Contains(user))
            {
                var index = _users.IndexOf(user);
                if (index >= 0)
                {
                    _users[index] = user;
                }
            }
        }
    }
}
    
