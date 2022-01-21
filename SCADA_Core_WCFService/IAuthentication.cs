using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core_WCFService
{
   public interface IAuthentication
    {
            string Registration(string name, string surname, string username, string password);
            bool DeleteProfile(string username, string password);
            string Login(string username, string password);
            string ChangePassword(string username, string password, string update_password);
            bool Logout(string token);    
    }
}
