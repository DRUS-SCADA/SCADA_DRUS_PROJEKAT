using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore
{
    public class RealTimeDriver:IRealTimeDriver
    {
        public static RealTimeUnit RTU = new RealTimeUnit();
        public static string ContainerName { get; private set; }
       
        public void SendMessage(double number, string address, byte[] signature)
        {
            byte[] messageHash = ComputeMessageHash(number.ToString());
            if (VerifySignedMessage(messageHash, signature))
            {
                Console.WriteLine($"Message: {number.ToString()} is valid!");
                Console.WriteLine($"Vrednost {number} na adresi {address} \n");
                RTU.SetValue(address, number);
            }
            else
            {
                Console.WriteLine($"Message: {number.ToString()} is not valid! DO NOT TRUST THIS MESSAGE!!!");
            }
        }
        private static bool VerifySignedMessage(byte[] hash, byte[] signature)
        {
            ContainerName = "KeyContainer";
            CspParameters csp = new CspParameters();
            csp.KeyContainerName = ContainerName;

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp))
            {
                var deformatter = new RSAPKCS1SignatureDeformatter(rsa);
                deformatter.SetHashAlgorithm("SHA256");
                return deformatter.VerifySignature(hash, signature);
            }
        }
        private static byte[] ComputeMessageHash(string value)
        {
            using (var sha = SHA256Managed.Create())
            {
                return sha.ComputeHash(Encoding.UTF8.GetBytes(value));
            }
        }
        public void changeAddress(string address)
        {
            using (var db = new AddressContext())
            {
                foreach (var i in db.RTUAddresses)
                {
                    if (i.address == address)
                    {
                        i.isFree = false;
                    }
                }
                db.SaveChanges();
            }
        }
        public void freeAddress(string address)
        {
            using(var db = new AddressContext())
            {
                foreach (var i in db.RTUAddresses)
                {
                    if(i.address == address)
                    {
                        i.isFree = true;
                    }
                }
                db.SaveChanges();
            }
        }
        public void makeDB()
        {
            using (var db = new AddressContext())
            {
                RTUAddress rtu = new RTUAddress() { address = "ADDR013", isFree = false };
                RTUAddress rtu2 = new RTUAddress() { address = "ADDR014", isFree = false };
                RTUAddress rtu3 = new RTUAddress() { address = "ADDR015", isFree = false };
                RTUAddress rtu4 = new RTUAddress() { address = "ADDR016", isFree = false };
                db.RTUAddresses.Add(rtu);
                db.RTUAddresses.Add(rtu2);
                db.RTUAddresses.Add(rtu3);
                db.RTUAddresses.Add(rtu4);
                db.SaveChanges();
            }
        }
    }
}
