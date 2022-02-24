using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore
{
    [ServiceContract]
    public interface IRealTimeDriver
    {
        [OperationContract]
        void SendMessage(double number, string address, byte[] signature);
        [OperationContract]
        void changeAddress(string address);
        [OperationContract]
        void freeAddress(string addresss);
        [OperationContract]
        void makeDB();

    }
}
