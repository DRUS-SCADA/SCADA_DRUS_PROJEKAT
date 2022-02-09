using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Simulation;

namespace SCADACore
{
    public class TagProcessing : IAuthentication, IDatabaseManager, ITrending
    {
        public static SimulationDriver PLC = new SimulationDriver();
        private static Dictionary<string, User> autentificated_users = new Dictionary<string, User>();
        public static Dictionary<string, Thread> dictDi = new Dictionary<string, Thread>();
        public static Dictionary<string, Thread> dictAi = new Dictionary<string, Thread>();
        static ITrendingCallback proxy = null;
        delegate void ValueHandler(AnalogInput AI);
        delegate void ValueHandler1(DigitalInput DI);
        delegate void RemoveHandler(AnalogInput AI);
        delegate void RemoveHandler1(DigitalInput DI);
        delegate void ClearGrid();
        static event ValueHandler valueReceived = null;
        static event ValueHandler1 valueReceived1 = null;
        static event RemoveHandler valueRemoved = null;
        static event RemoveHandler1 valueRemoved1 = null;
        static event ClearGrid clearGrid = null;
        public static List<AnalogInput> analogInputs = new List<AnalogInput>();
        public static List<AnalogOutput> analogOutputs = new List<AnalogOutput>();
        public static List<DigitalInput> digitalInputs = new List<DigitalInput>();
        public static List<DigitalOutput> digitalOutputs = new List<DigitalOutput>();
        public static ObservableCollection<AnalogInput> analogInputsObservable = new ObservableCollection<AnalogInput>();
        public static ObservableCollection<AnalogOutput> analogOutputsObservable = new ObservableCollection<AnalogOutput>();
        public static ObservableCollection<DigitalInput> digitalInputsObservable = new ObservableCollection<DigitalInput>();
        public static ObservableCollection<DigitalOutput> digitalOutputsObservable = new ObservableCollection<DigitalOutput>();

        #region IAuthentication

        public string Registration(string name, string surname, string username, string password)
        {
            string encrypted_password = EncryptData(password);
            string encrypted_username = EncryptData(username);


            User user = new User(name, surname, encrypted_username, encrypted_password);

            using (var db = new UserContext())
            {
                try
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    return "Neuspesno registrovanje";
                }
            }

            return "Uspesno registrovanje";
        }

        public bool DeleteProfile(string username, string password)
        {
            string encryptedPassword = EncryptData(password);
            string encryptedUsername = EncryptData(username);

            bool help_variable = false;

            using (var db = new UserContext())
            {
                foreach (var user in db.Users)
                {
                    if (ValidateEncryptedData(username, user.EncryptedUsername) && ValidateEncryptedData(password, user.EncryptedPassword))
                    {
                        try
                        {
                            db.Users.Remove(user);


                            help_variable = true;
                        }
                        catch (Exception e)
                        {
                            help_variable = false;
                        }
                    }
                }

                db.SaveChanges();

            }

            if (help_variable == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string Login(string username, string password)
        {
            using (var db = new UserContext())
            {
                foreach (var user in db.Users)
                {
                    if (ValidateEncryptedData(username, user.EncryptedUsername) && ValidateEncryptedData(password, user.EncryptedPassword))
                    {
                        string token = GenerateToken(username);
                        autentificated_users.Add(token, user);
                        return token;
                    }
                }
            }

            return "Neuspesno logovanje";
        }

        public string ChangePassword(string username, string password, string update_password)
        {
            bool help_variable = false;
            using (var db = new UserContext())
            {
                foreach (var user in db.Users)
                {
                    if (ValidateEncryptedData(username, user.EncryptedUsername) && ValidateEncryptedData(password, user.EncryptedPassword))
                    {

                        user.EncryptedPassword = EncryptData(update_password);
                        help_variable = true;
                    }
                }
                db.SaveChanges();
            }

            if (help_variable == true)
            {
                return "Uspesna promena passworda";
            }
            else
            {
                return "Neuspesna promena passworda";
            }
        }

        public bool Logout(string token)
        {
            clearDictionaries();
            return autentificated_users.Remove(token);
        }
        #endregion

        #region Encrypt Methods
        private string EncryptData(string valueToEncrypt)
        {
            string GenerateSalt()
            {
                RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
                byte[] salt = new byte[32];
                crypto.GetBytes(salt);

                return Convert.ToBase64String(salt);
            }
            string EncryptValue(string strValue)
            {
                string saltValue = GenerateSalt();
                byte[] saltedPassword = Encoding.UTF8.GetBytes(saltValue + strValue);

                using (SHA256Managed sha = new SHA256Managed())
                {
                    byte[] hash = sha.ComputeHash(saltedPassword);
                    return $"{Convert.ToBase64String(hash)}:{saltValue}";
                }
            }
            return EncryptValue(valueToEncrypt);
        }

        private bool ValidateEncryptedData(string valuteToValidate, string valueFromDatabase)
        {
            string[] arrValues = valueFromDatabase.Split(':');
            string encryptedDbValue = arrValues[0];
            string salt = arrValues[1];

            byte[] saltedValue = Encoding.UTF8.GetBytes(salt + valuteToValidate);
            using (var sha = new SHA256Managed())
            {
                byte[] hash = sha.ComputeHash(saltedValue);

                string enteredValueToValidate = Convert.ToBase64String(hash);
                return encryptedDbValue.Equals(enteredValueToValidate);
            }
        }

        private string GenerateToken(string username)
        {
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            byte[] randVal = new byte[32];
            crypto.GetBytes(randVal);

            string randStr = Convert.ToBase64String(randVal);
            return username + randStr;
        }

        private bool IsUserAutenticated(string token)
        {
            return autentificated_users.ContainsKey(token);

        }
        #endregion

        #region Add Tags

        public void AddAO(AnalogOutput AO)
        {
            analogOutputsObservable.Add(AO);
            using (var db = new TagContext())
            {
                db.analogOutputs.Add(AO);
                db.SaveChanges();
            }
        }

        public void AddDO(DigitalOutput DO)
        {
            digitalOutputsObservable.Add(DO);
            using (var db = new TagContext())
            {
                db.digitalOutputs.Add(DO);
                db.SaveChanges();
            }
        }

        public void AddAI(AnalogInput AI)
        {
            analogInputsObservable.Add(AI);
            using (var db = new TagContext())
            {
                db.analogInputs.Add(AI);
                db.SaveChanges();
                Thread thread = new Thread(() => Simulation1(AI));
                dictAi.Add(AI.TagName, thread);
                thread.Start();
            }
        }
        public void AddDI(DigitalInput DI)
        {
            digitalInputsObservable.Add(DI);
            using (var db = new TagContext())
            {
                db.digitalInputs.Add(DI);
                db.SaveChanges();
                Thread thread = new Thread(() => Simulation(DI));
                dictDi.Add(DI.TagName, thread);
                thread.Start();
            }
        }
        #endregion

        #region HelpMethods
        
        public void clearDictionaries()
        {
            using (var db = new TagContext())
            {
                foreach (var i in db.digitalInputs)
                {
                    dictDi[i.TagName].Abort();
                    dictDi.Remove(i.TagName);
                }
                foreach (var j in db.analogInputs)
                {
                    dictAi[j.TagName].Abort();
                    dictAi.Remove(j.TagName);
                }
            }
        }
        public IEnumerable<DigitalOutput> LoadDataToGrid()
        {
            using (var db = new TagContext())
            {
                db.digitalOutputs.Load();
                return db.digitalOutputs.Local;
            }
            
        }
        public IEnumerable<AnalogOutput> LoadDataToGridAO()
        {
            using (var db = new TagContext())
            {
                db.analogOutputs.Load();
                return db.analogOutputs.Local;
            }
        }
        public IEnumerable<AnalogInput> LoadDataToGridAI()
        {
            using (var db = new TagContext())
            {
                db.analogInputs.Load();
                return db.analogInputs.Local;
            }
        }
        public IEnumerable<DigitalInput> LoadDataToGridDI()
        {
            using (var db = new TagContext())
            {
                db.digitalInputs.Load();
                return db.digitalInputs.Local;
            }
        }
        public void SaveChanges(AnalogOutput AO,double change)
        {
            foreach (var i in analogOutputsObservable.ToList())
            {
                if (i.TagName == AO.TagName)
                {
                    i.InitialValue = change;
                    break;
                }
            }
            /*AO.InitialValue = change;
            using (var db = new TagContext())
            {
                foreach (var i in db.analogOutputs)
                {
                    if(i.Id == AO.Id)
                    {
                        i.InitialValue = change;
                    }
                }
                db.SaveChanges();
            }*/
        }
        public void SaveChangesDO(DigitalOutput DO, double change)
        {
            foreach (var i in digitalOutputsObservable.ToList())
            {
                if (i.tag_name == DO.tag_name)
                {
                    i.initial_Value = change;
                    break;
                }
            }
            /*DO.initial_Value = change;
            using (var db = new TagContext())
            {
                foreach (var i in db.digitalOutputs)
                {
                    if (i.Id == DO.Id)
                    {
                        i.initial_Value = change;
                    }
                }
                db.SaveChanges();
            }*/
        }
        public void SaveChangesAI(AnalogInput AI, bool change)
        {
            foreach (var i in analogInputsObservable.ToList())
            {
                if (i.TagName == AI.TagName)
                {
                    i.ONOFF_scan = change;
                    break;
                }
            }
            /*AI.ONOFF_scan = change;
            using (var db = new TagContext())
            {
                foreach (var i in db.analogInputs)
                {
                    if (i.Id == AI.Id)
                    {
                        i.ONOFF_scan = change;
                    }
                }
                db.SaveChanges();
            }*/
        }

        public void SaveChangesDI(DigitalInput DI, bool change)
        {
            foreach (var i in digitalInputsObservable.ToList())
            {
                if (i.TagName == DI.TagName)
                {
                    i.ONOFF_scan = change;
                    break;
                }
            }
            /*DI.ONOFF_scan = change;
            using (var db = new TagContext())
            {
                foreach (var i in db.digitalInputs)
                {
                    if (i.Id == DI.Id)
                    {
                        i.ONOFF_scan = change;
                    }
                }
                db.SaveChanges();
            }*/
        }
        public void startPLC()
        {
            PLC.StartPLCSimulator();
        }
        public Dictionary<string, bool> loadAdressAI(Dictionary<string, bool>AI)
        {
            using (var db = new TagContext())
            {
                foreach (var i in db.analogInputs)
                {
                    AI[i.IOAdress] = true;
                }
            }
            return AI;
        }
        public Dictionary<string, bool> loadAdressAO(Dictionary<string, bool> AO)
        {
            using (var db = new TagContext())
            {
                foreach (var i in db.analogOutputs)
                {
                    AO[i.IOAdress] = true;
                }
            }
            return AO;
        }
        public Dictionary<string, bool> loadAdressDI(Dictionary<string, bool> DI)
        {
            using (var db = new TagContext())
            {
                foreach (var i in db.digitalInputs)
                {
                    DI[i.IOAdress] = true;
                }
            }
            return DI;
        }
        public Dictionary<string, bool> loadAdressDO(Dictionary<string, bool> DO)
        {
            using (var db = new TagContext())
            {
                foreach (var i in db.digitalOutputs)
                {
                    DO[i.IO_Adress] = true;
                }
            }
            return DO;
        }
        #endregion

        #region RemoveTags
        public void removeDO(DigitalOutput DO)
        {
            foreach (var i in digitalOutputsObservable.ToList())
            {
                if (i.tag_name == DO.tag_name)
                {
                    digitalOutputsObservable.Remove(i);
                    break;
                }
            }
            
            using (var db = new TagContext())
            {
                db.digitalOutputs.Attach(DO);
                db.digitalOutputs.Remove(DO);
                db.SaveChanges();
            }
            
        }
        
        public void removeAO(AnalogOutput AO)
        {
            foreach (var i in analogOutputsObservable.ToList())
            {
                if (i.TagName == AO.TagName)
                {
                    analogOutputsObservable.Remove(i);
                    break;
                }
            }
            using (var db = new TagContext())
            {
                db.analogOutputs.Attach(AO);
                db.analogOutputs.Remove(AO);
                db.SaveChanges();
            }
        }

        public void removeAI(AnalogInput AI)
        {
            foreach (var i in analogInputsObservable.ToList())
            {
                if (i.TagName == AI.TagName)
                {
                    analogInputsObservable.Remove(i);
                    break;
                }
            }
            using (var db = new TagContext())
            {
                dictAi[AI.TagName].Abort();
                dictAi.Remove(AI.TagName);
                db.analogInputs.Attach(AI);
                valueRemoved?.Invoke(AI);
                db.analogInputs.Remove(AI);
                db.SaveChanges();
            }
        }
        public void removeDI(DigitalInput DI)
        {
            foreach (var i in digitalInputsObservable.ToList())
            {
                if (i.TagName == DI.TagName)
                {
                    digitalInputsObservable.Remove(i);
                    break;
                }
            }
            using (var db = new TagContext())
            {
                dictDi[DI.TagName].Abort();
                dictDi.Remove(DI.TagName);
                db.digitalInputs.Attach(DI);
                valueRemoved1?.Invoke(DI);
                db.digitalInputs.Remove(DI);
                db.SaveChanges();
            }
        }
        #endregion

        #region PLC simulation methods
        public void Simulation(DigitalInput di)
        {
            while (true)
            {
                di.digitalValue = PLC.GetValue(di.IOAdress);
                Thread.Sleep(Convert.ToInt32(di.ScanTime) * 1000);
                if (di.ONOFF_scan == true)
                {
                    valueReceived1?.Invoke(di);
                }
            }
        }

        public void Simulation1(AnalogInput ai)
        {
            while (true)
            {
                ai.AnalogValue = PLC.GetValue(ai.IOAdress);
                Thread.Sleep(Convert.ToInt32(ai.ScanTime) * 1000);
                if (ai.ONOFF_scan == true)
                {
                    valueReceived?.Invoke(ai);
                }
            }
        }

        public void LoadThreadDi()
        {
            using (var db = new TagContext())
            {
                foreach (var d in db.digitalInputs)
                {
                    Thread thread1 = new Thread(() => Simulation(d));
                    dictDi.Add(d.TagName, thread1);
                    thread1.Start();
                }
            }
        }

        public void LoadThreadAi()
        {
            using (var db = new TagContext())
            {
                foreach (var a in db.analogInputs)
                {
                    Thread thread2 = new Thread(() => Simulation1(a));
                    dictAi.Add(a.TagName, thread2);
                    thread2.Start();
                }
            }
        }
        #endregion

        #region PubSub 
        public void SubscriberInitialization()
        {
            proxy = OperationContext.Current.GetCallbackChannel<ITrendingCallback>();
            valueReceived += proxy.OnValueReceived;
            valueReceived1 += proxy.OnValueReceived1;
            valueRemoved += proxy.OnRemoveAI;
            valueRemoved1 += proxy.OnRemoveDI;
            clearGrid += proxy.OnClearAI;
            clearGrid += proxy.OnClearDI;
        }
        public void clearData()
        {
            clearGrid?.Invoke();
        }

        #endregion

        #region XML
        public void WriteXML()
        {
            XDocument document = new XDocument(
                new XElement("RootName",
                new XElement("AnalogInputs",
                    (from analogInput in analogInputsObservable.ToList()
                     select new XElement("AnalogInput",
                          new XElement("TagName", analogInput.TagName),
                          new XElement("Description", analogInput.Description),
                          new XElement("IOAdress", analogInput.IOAdress),
                          new XElement("Units", analogInput.Units),
                          new XElement("HighLimit", analogInput.HighLimit),
                          new XElement("LowLimit", analogInput.LowLimit),
                          new XElement("ScanTime", analogInput.ScanTime),
                          new XElement("ONOFFscan", analogInput.ONOFF_scan)
                    )
                            )
                                         ),

                new XElement("AnalogOutputs",
                    (from analogOutput in analogOutputsObservable.ToList()
                     select new XElement("AnalogOutput",
                          new XElement("TagName", analogOutput.TagName),
                          new XElement("Description", analogOutput.Description),
                          new XElement("IOAdress", analogOutput.IOAdress),
                          new XElement("HighLimit", analogOutput.HighLimit),
                          new XElement("LowLimit", analogOutput.LowLimit),
                          new XElement("InitialValue", analogOutput.InitialValue)
                    )
                            )
                                         ),
                new XElement("DigitalInputs",
                    (from digitalInput in digitalInputsObservable.ToList()
                     select new XElement("DigitalInput",
                          new XElement("TagName", digitalInput.TagName),
                          new XElement("Description", digitalInput.Description),
                          new XElement("IOAdress", digitalInput.IOAdress),
                          new XElement("ScanTime", digitalInput.ScanTime),
                          new XElement("ONOFFscan", digitalInput.ONOFF_scan)
                    )
                            )
                                         ),
                new XElement("DigitalOutputs",
                    (from digitalOutput in digitalOutputsObservable.ToList()
                     select new XElement("DigitalOutput",
                          new XElement("TagName", digitalOutput.tag_name),
                          new XElement("Description", digitalOutput.description),
                          new XElement("IOAdress", digitalOutput.IO_Adress),
                          new XElement("InitialValue", digitalOutput.initial_Value)
                    )
                            )
                                         )
                                                ));
            
            document.Save(@"D:\scadaConfig.xml");

        }

        public void ReadXML()
        {
            XElement element = XElement.Load(@"D:\scadaConfig.xml");
            analogInputs = (from ai in element.Elements("AnalogInput")
                            select new AnalogInput
                            {
                                TagName = ai.Attribute("TagName").Value,
                                Description = ai.Attribute("Description").Value,
                                IOAdress = ai.Attribute("IOAdress").Value,
                                Units = ai.Attribute("Units").Value,
                                HighLimit = double.Parse(ai.Attribute("HighLimit").Value),
                                LowLimit = double.Parse(ai.Attribute("LowLimit").Value),
                                ScanTime = double.Parse(ai.Attribute("ScanTime").Value),
                                ONOFF_scan = bool.Parse(ai.Attribute("ONOFFscan").Value)
                            }).ToList();

            digitalInputs = (from di in element.Elements("DigitalInput")
                             select new DigitalInput
                             {
                                 TagName = di.Attribute("TagName").Value,
                                 Description = di.Attribute("Description").Value,
                                 IOAdress = di.Attribute("IOAdress").Value,
                                 ScanTime = double.Parse(di.Attribute("ScanTime").Value),
                                 ONOFF_scan = bool.Parse(di.Attribute("ONOFFscan").Value)
                             }).ToList();

            analogOutputs = (from ao in element.Elements("AnalogOutput")
                             select new AnalogOutput
                             {
                                 TagName = ao.Attribute("TagName").Value,
                                 Description = ao.Attribute("Description").Value,
                                 IOAdress = ao.Attribute("IOAdress").Value,
                                 HighLimit = double.Parse(ao.Attribute("HighLimit").Value),
                                 LowLimit = double.Parse(ao.Attribute("LowLimit").Value),
                                 InitialValue = double.Parse(ao.Attribute("InitialValue").Value)
                             }).ToList();

            digitalOutputs = (from doo in element.Elements("DigitalOutput")
                              select new DigitalOutput
                              {
                                  tag_name = doo.Attribute("TagName").Value,
                                  description = doo.Attribute("Description").Value,
                                  IO_Adress = doo.Attribute("IOAdress").Value,
                                  initial_Value = double.Parse(doo.Attribute("InitialValue").Value)
                              }).ToList();
        }
        #endregion
    }
}
