using Business.DTOs;
using Business.Services.Encryption;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordRepository _passwordRepository;

        public PasswordService(IPasswordRepository passwordRepository)
        {
            _passwordRepository = passwordRepository;
        }

        public async Task<IEnumerable<PasswordDTO>> GetAllPasswords()
        {
            var passwords = await _passwordRepository.GetAllPasswords();

            return passwords.Select(password => new PasswordDTO
            {
                Id = password.Id,
                EncryptedValue = password.EncryptedValue,
            });
        }

        public async Task<PasswordDTO> CreatePassword(PasswordDTO passwordDto)
        {
            Password password = new Password
            {
                EncryptedValue = passwordDto.EncryptedValue
            };

            var addedPassword = await _passwordRepository.CreatePassword(password);

            return new PasswordDTO
            {
                Id = addedPassword.Id,
                EncryptedValue = addedPassword.EncryptedValue,
            };
        }

        public async Task<bool> DeletePassword(int id)
        {
            await _passwordRepository.DeletePassword(id);
            return true;
        }

        public string EncryptPassword(string password, int encryptionType)
        {
            ApplicationType appType = (ApplicationType)encryptionType;

            if (appType == ApplicationType.GeneralPublic)
            {
                AESEncryptionStrategy encryptionStrategy = new AESEncryptionStrategy();
                string key = encryptionStrategy.GenerateKey();

                return encryptionStrategy.Encrypt(password, key);
            }
            else
            {
                RSAEncryptionStrategy encryptionStrategy = new RSAEncryptionStrategy();
                var (publicKey, privateKey) = encryptionStrategy.GenerateKeys();

                return encryptionStrategy.Encrypt(password, publicKey);
            }
        }
    }
}
