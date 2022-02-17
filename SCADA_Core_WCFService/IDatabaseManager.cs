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
        IEnumerable<Alarm> LoadDataToGridAlarm(AnalogInput AI);
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
        [OperationContract]
        Dictionary<string, bool> loadAdressAI(Dictionary<string,bool> AI);
        [OperationContract]
        Dictionary<string, bool> loadAdressDI(Dictionary<string, bool> DI);
        [OperationContract]
        Dictionary<string, bool> loadAdressAO(Dictionary<string, bool> AO);
        [OperationContract]
        Dictionary<string, bool> loadAdressDO(Dictionary<string, bool> DO);
        [OperationContract]
        Dictionary<string, bool> loadAdressAIfree(Dictionary<string, bool> AI);
        [OperationContract]
        Dictionary<string, bool> loadAdressDIfree(Dictionary<string, bool> DI);
        [OperationContract]
        Dictionary<string, bool> loadAdressAOfree(Dictionary<string, bool> AO);
        [OperationContract]
        Dictionary<string, bool> loadAdressDOfree(Dictionary<string, bool> DO);
        [OperationContract]
        void clearData();
        [OperationContract]
        void WriteXML();
        [OperationContract]
        void ReadXML();
        [OperationContract]
        void RemoveAlarm(Alarm alarm, AnalogInput AI);
        [OperationContract]
        void AddAlarmToAI(Alarm alarm, AnalogInput AI);
        [OperationContract]
        void ClearCollections();
        [OperationContract]
        void ClearDictionaries();
        [OperationContract]
        void clearDataAlarmDisplay();
        [OperationContract]
        void ShutdownAlarmDisplay();
        [OperationContract]
        void ShutdownTrendingApp();
    }
}
