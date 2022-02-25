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

        public static Dictionary<string, bool> RTU_adress = new Dictionary<string, bool>()
        {
            {"ADDR013", false},
            {"ADDR014", false},
            {"ADDR015", false},
            {"ADDR016", false},

        };
       
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

            RTU_adress[address] = true;


        }
        public void freeAddress(string address)
        {


            if (RTU_adress[address] == true)
            {
                RTU_adress[address] = false;
            }


        }

        public List<string> GetAddress()
        {
            List<string> updatedList = new List<string>();
            foreach (var i in RTU_adress.Keys)
            {
                if (RTU_adress[i] == false)
                {
                    updatedList.Add(i);
                }
            }

            return updatedList;
        }

    }
}
