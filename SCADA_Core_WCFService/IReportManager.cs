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
        List<Alarm> Report1(double time);
        [OperationContract]
        List<Alarm> Report2(string priority);
        [OperationContract]
        Tag Report3(string tag, double time);
        [OperationContract]
        List<AITag> Report4();
        [OperationContract]
        List<DITag> Report5();
        [OperationContract]
        Tag Report6(string tagName,string tag);
    }
}
