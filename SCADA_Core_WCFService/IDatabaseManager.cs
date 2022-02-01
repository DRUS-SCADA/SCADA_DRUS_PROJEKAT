using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore
{
    [ServiceContract]
    public interface IDatabaseManager
    {
        //void AddAI(AnalogInput AI);
        [OperationContract]
        void AddAO(AnalogOutput AO);
        //void AddDI(DigitalInput DI);
        [OperationContract]
        void AddDO(DigitalOutput DO);
        [OperationContract]
        IEnumerable<DigitalOutput> LoadDataToGrid();
        [OperationContract]
        void removeDO(DigitalOutput DO);
        [OperationContract]
        void removeAO(AnalogOutput AO);
        [OperationContract]
        IEnumerable<AnalogOutput> LoadDataToGridAO();
        /*void removeAI(AnalogInput AI);
        void removeDI(DigitalInput DI);
        void turnONscanDigital(DigitalInput DI);
        void turnONscanAnalog(AnalogInput AI);
        void turnOFFscanDigital(DigitalInput DI);
        void turnOFFscanAnalog(AnalogInput AI);*/

    }
}
