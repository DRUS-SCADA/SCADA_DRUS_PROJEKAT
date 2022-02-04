using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore
{
    public interface ITrendingCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnValueReceived(AnalogInput AI);
        [OperationContract(IsOneWay = true)]
        void OnValueReceived1(DigitalInput DI);
    }
}
