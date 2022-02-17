using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore
{
    [ServiceContract]
    public interface IReportManager
    {
        [OperationContract]
        void Report1(double time);
        [OperationContract]
        void Report2(string priority);
        [OperationContract]
        void Report3(string tag, double time);
        [OperationContract]
        void Report4();
        [OperationContract]
        void Report5();
        [OperationContract]
        void Report6(string tagName,string tag);
    }
}
