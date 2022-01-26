using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core_WCFService
{
   [ServiceContract]
   public interface IAuthentication
    {
        [OperationContract]
        string Registration(string name, string surname, string username, string password);
        [OperationContract]
        bool DeleteProfile(string username, string password);
        [OperationContract]
        string Login(string username, string password);
        [OperationContract]
        string ChangePassword(string username, string password, string update_password);
        [OperationContract]
        bool Logout(string token);    
    }
}
