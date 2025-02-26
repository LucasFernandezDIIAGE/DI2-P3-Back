using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Encryption
{
    public class RSAEncryptionStrategy : IEncryptionStrategy
    {
        // Méthode pour chiffrer un mot de passe avec la clé publique
        public string Encrypt(string password, string publicKey)
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(publicKey), out _);
                byte[] messageBytes = Encoding.UTF8.GetBytes(password);
                byte[] encryptedBytes = rsa.Encrypt(messageBytes, RSAEncryptionPadding.OaepSHA256);
                return Convert.ToBase64String(encryptedBytes);
            }
        }

        // Déchiffre un message en utilisant la clé privée
        public static string Decrypt(string encryptedMessage, string privateKey)
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.ImportPkcs8PrivateKey(Convert.FromBase64String(privateKey), out _);
                byte[] encryptedBytes = Convert.FromBase64String(encryptedMessage);
                byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, RSAEncryptionPadding.OaepSHA256);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }

        // Méthode pour générer une paire de clés RSA (publique et privée)
        public (string publicKey, string privateKey) GenerateKeys()
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.KeySize = 2048; // Taille de la clé en bits
                string publicKey = Convert.ToBase64String(rsa.ExportSubjectPublicKeyInfo());
                string privateKey = Convert.ToBase64String(rsa.ExportPkcs8PrivateKey());

                return (publicKey, privateKey);
            }
        }
    }
}
