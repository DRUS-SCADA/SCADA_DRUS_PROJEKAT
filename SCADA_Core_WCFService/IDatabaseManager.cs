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
        [OperationContract]
        void AddAI(AnalogInput AI);
        [OperationContract]
        void AddAO(AnalogOutput AO);
        [OperationContract]
        void AddDI(DigitalInput DI);
        [OperationContract]
        void AddDO(DigitalOutput DO);
        [OperationContract]
        void removeDO(DigitalOutput DO);
        [OperationContract]
        void removeAO(AnalogOutput AO);
        [OperationContract]
        void removeAI(AnalogInput AI);
        [OperationContract]
        void removeDI(DigitalInput DI);
        [OperationContract]
        IEnumerable<AnalogOutput> LoadDataToGridAO();
        [OperationContract]
        IEnumerable<DigitalOutput> LoadDataToGrid();
        [OperationContract]
        IEnumerable<AnalogInput> LoadDataToGridAI();
        [OperationContract]
        IEnumerable<DigitalInput> LoadDataToGridDI();
        [OperationContract]
        void SaveChanges(AnalogOutput AO, double change);
        [OperationContract]
        void SaveChangesDO(DigitalOutput DO, double change);
        [OperationContract]
        void SaveChangesAI(AnalogInput AI, bool change);
        [OperationContract]
        void SaveChangesDI(DigitalInput DI, bool change);
        [OperationContract]
        void Simulation(DigitalInput DI);
        [OperationContract]
        void Simulation1(AnalogInput AI);
        [OperationContract]
        void LoadThreadDi();
        [OperationContract]
        void LoadThreadAi();
        [OperationContract]
        void startPLC();
    }
}
