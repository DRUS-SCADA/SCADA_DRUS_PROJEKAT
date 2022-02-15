using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore
{
    public interface IAlarmDisplayCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnAlarmActivate(Alarm alarm, int count);
        [OperationContract(IsOneWay = true)]
        void OnAlarmStop(Alarm alarm);
    }
}
