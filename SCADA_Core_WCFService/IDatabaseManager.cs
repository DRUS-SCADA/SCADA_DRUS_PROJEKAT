using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore
{
    public interface IDatabaseManager
    {
        void AddAI(AnalogInput AI);
        void AddAO(AnalogOutput AO);
        void AddDI(DigitalInput DI);
        void AddDO(DigitalOutput DO);
        void removeAI(AnalogInput AI);
        void removeAO(AnalogOutput AO);
        void removeDI(DigitalInput DI);
        void removeDO(DigitalOutput DO);
        void turnONscanDigital(DigitalInput DI);
        void turnONscanAnalog(AnalogInput AI);
        void turnOFFscanDigital(DigitalInput DI);
        void turnOFFscanAnalog(AnalogInput AI);

    }
}
