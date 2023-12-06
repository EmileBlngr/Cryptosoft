using System;

namespace Cryptosoft
{
    public class Encrypt
    {
        public string EncryptingKey { get; set; }
        public string SourceEncryptPath { get; set; }
        public string TargetEncryptPath { get; set; }

        public string TargetDecryptPath { get; set; }

        public Encrypt() 
        {
            GenerateEncryptingkey();
        }
        public void GenerateEncryptingkey()
        {
            Random random = new Random();
            EncryptingKey = "";
            for (int i = 0; i < 128; i++)
            {
                EncryptingKey += random.Next(2);
            }
        }
        public void EncryptData()
        {
            if (File.Exists(SourceEncryptPath))
            {
                EncryptFile(SourceEncryptPath);
                Console.WriteLine("Encryption completed successfully.");
            }
            else if (Directory.Exists(SourceEncryptPath))
            {
                EncryptDirectory();
            }
        }
        public void EncryptDirectory()
        {
            string[] files = Directory.GetFiles(SourceEncryptPath);

            foreach (string filePath in files)
            {
                EncryptFile(filePath);
            }

            Console.WriteLine("Encryption completed successfully for all files in the directory.");
        }
        public void EncryptFile(string filePath)
        {
            byte[] fileContent = File.ReadAllBytes(filePath);

            byte[] encryptedContent = XOREncrypt(fileContent);

            string fileName = Path.GetFileName(filePath);
            string encryptedFilePath = Path.Combine(TargetEncryptPath, fileName);
            File.WriteAllBytes(encryptedFilePath, encryptedContent);
        }

        public byte[] XOREncrypt(byte[] data)
        {
            byte[] encryptedData = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                encryptedData[i] = (byte)(data[i] ^ EncryptingKey[i % EncryptingKey.Length]);
                // ^ = XOR
                //i % EncryptingKey.Length to ensure not to reach the end of the key
            }
            return encryptedData;
        }
        public void DecryptFiles()
        {
            string[] encryptedFiles = Directory.GetFiles(TargetDecryptPath);

            foreach (string encryptedFilePath in encryptedFiles)
            {
                byte[] encryptedContent = File.ReadAllBytes(encryptedFilePath);

                byte[] decryptedContent = Decrypt(encryptedContent);

                string decryptedFileName = Path.GetFileName(encryptedFilePath);
                string decryptedFilePath = Path.Combine(TargetDecryptPath, decryptedFileName);

                File.WriteAllBytes(decryptedFilePath, decryptedContent);

                Console.WriteLine($"Decryption completed. File saved at: {decryptedFilePath}");
            }
        }

        public byte[] Decrypt(byte[] data)
        {
            byte[] decryptedData = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                decryptedData[i] = (byte)(data[i] ^ EncryptingKey[i % EncryptingKey.Length]);
            }
            return decryptedData;
        }
        public string ConvertToLocalUNC(string localPath)
        {
            try
            {
                string uncPath = Path.GetFullPath(localPath);
                return uncPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error converting path to UNC: {ex.Message}");
                return null;
            }
        }
    }
}
