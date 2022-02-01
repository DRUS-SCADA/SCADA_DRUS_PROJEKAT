using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore
{
    public class TagProcessing : IAuthentication, IDatabaseManager
    {
        #region IAuthentication
        private static Dictionary<string, User> autentificated_users = new Dictionary<string, User>();

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
        #region IDatabaseManager
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
        #endregion
    }
}
