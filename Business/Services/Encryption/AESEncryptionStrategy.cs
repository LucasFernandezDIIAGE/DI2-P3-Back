using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Encryption
{
    public class AESEncryptionStrategy : IEncryptionStrategy
    {
        // Méthode pour chiffrer un mot de passe avec une clé AES
        public string Encrypt(string password, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Convert.FromBase64String(key); // Clé AES en Base64
                aesAlg.GenerateIV(); // Génération d'un vecteur d'initialisation (IV) aléatoire

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(password);
                        }
                    }

                    byte[] iv = aesAlg.IV;
                    byte[] encrypted = msEncrypt.ToArray();

                    byte[] combined = new byte[iv.Length + encrypted.Length];
                    Array.Copy(iv, 0, combined, 0, iv.Length);
                    Array.Copy(encrypted, 0, combined, iv.Length, encrypted.Length);

                    return Convert.ToBase64String(combined); // Retourne le texte chiffré avec l'IV
                }
            }
        }

        // Méthode pour déchiffrer un mot de passe avec une clé AES
        public string Decrypt(string encryptedPassword, string key)
        {
            byte[] combined = Convert.FromBase64String(encryptedPassword);
            byte[] iv = new byte[16];
            byte[] cipherText = new byte[combined.Length - iv.Length];

            Array.Copy(combined, 0, iv, 0, iv.Length);
            Array.Copy(combined, iv.Length, cipherText, 0, cipherText.Length);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Convert.FromBase64String(key); // Clé AES en Base64
                aesAlg.IV = iv; // Utilisation du même IV qu'au chiffrement

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        // Méthode pour générer une clé AES aléatoire (en Base64)
        public string GenerateKey()
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.KeySize = 256; // Taille de la clé (en bits)
                aesAlg.GenerateKey(); // Génération d'une clé aléatoire
                return Convert.ToBase64String(aesAlg.Key); // Retourne la clé en Base64
            }
        }
    }
}
