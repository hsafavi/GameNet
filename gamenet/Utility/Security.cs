using System;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace gamenet.Utility
{
    class Security
    {
        static readonly string PasswordHash = "hs14@Progm";
        static readonly string SaltKey = "Gm@proj400";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";
        private static string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }


        public static string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }
        internal static string GetId()
        {
            string cpuInfo = string.Empty;
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                if (cpuInfo == "")
                {
                    //Get only the first CPU's ID
                    cpuInfo = mo.Properties["processorID"].Value.ToString();
                    break;
                }
            }
            ManagementObject dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""c:""");
            dsk.Get();
            string volumeSerial = dsk["VolumeSerialNumber"].ToString();
            string s = volumeSerial + cpuInfo+"Rf,gi";
            s=Encrypt(s);
           
            return s;
        }
        internal static bool CheckSerial(string serial)
        {
            //string cpuInfo = string.Empty;
            //ManagementClass mc = new ManagementClass("win32_processor");
            //ManagementObjectCollection moc = mc.GetInstances();

            //foreach (ManagementObject mo in moc)
            //{
            //    if (cpuInfo == "")
            //    {
            //        //Get only the first CPU's ID
            //        cpuInfo = mo.Properties["processorID"].Value.ToString();
            //        break;
            //    }
            //}
            //ManagementObject dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""c:""");
            //dsk.Get();
            //string volumeSerial = dsk["VolumeSerialNumber"].ToString();

            //string r = "";
            //int cp = 1, v = 1;
            //for (int i = 0; i < cpuInfo.Length; i++)
            //{
            //    cp = (int)cpuInfo[i];

            //    if (i < volumeSerial.Length)
            //    {
            //        v = (int)volumeSerial[i];
            //        r += (Math.Pow((cp + v) / 3, 3) % 100).ToString();
            //    }
            //}
            //return r.GetHashCode();
            var e = Encrypt(GetId());
            return serial == e;
        }
    }
}
