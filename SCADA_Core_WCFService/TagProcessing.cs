using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        static event ValueHandler valueReceived = null;
        static event ValueHandler1 valueReceived1 = null;
        static event RemoveHandler valueRemoved = null;
        static event RemoveHandler1 valueRemoved1 = null;

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
            using (var db = new TagContext())
            {
                db.analogOutputs.Add(AO);
                db.SaveChanges();
            }
        }

        public void AddDO(DigitalOutput DO)
        {
            using (var db = new TagContext())
            {
                db.digitalOutputs.Add(DO);
                db.SaveChanges();
            }
        }

        public void AddAI(AnalogInput AI)
        {
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
            }
        }
        public void SaveChangesDO(DigitalOutput DO, double change)
        {
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
            }
        }
        public void SaveChangesAI(AnalogInput AI, bool change)
        {
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
            }
        }

        public void SaveChangesDI(DigitalInput DI, bool change)
        {
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
            }
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
            using (var db = new TagContext())
            {
                db.digitalOutputs.Attach(DO);
                db.digitalOutputs.Remove(DO);
                db.SaveChanges();
            }
            
        }
        
        public void removeAO(AnalogOutput AO)
        {
            using (var db = new TagContext())
            {
                db.analogOutputs.Attach(AO);
                db.analogOutputs.Remove(AO);
                db.SaveChanges();
            }
        }

        public void removeAI(AnalogInput AI)
        {
            
            using (var db = new TagContext())
            {
                dictAi[AI.TagName].Abort();
                db.analogInputs.Attach(AI);
                valueRemoved?.Invoke(AI);
                db.analogInputs.Remove(AI);
                db.SaveChanges();
                
            }
            
        }
        public void removeDI(DigitalInput DI)
        {
            
            using (var db = new TagContext())
            {
                dictDi[DI.TagName].Abort();
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
                using (var db = new TagContext())
                {
                    foreach (var i in db.digitalInputs)
                    {
                        if (i.Id == di.Id)
                        {
                            di.ONOFF_scan = i.ONOFF_scan;
                        }
                    }
                }
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
                using (var db = new TagContext())
                {
                    foreach (var i in db.analogInputs)
                    {
                        if (i.Id == ai.Id)
                        {
                            ai.ONOFF_scan = i.ONOFF_scan;
                        }
                    }
                }
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
        }
        #endregion
    }
}
