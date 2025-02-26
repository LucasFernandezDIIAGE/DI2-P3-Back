using Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IPasswordService
    {
        Task<IEnumerable<PasswordDTO>> GetAllPasswords();
        Task<PasswordDTO> CreatePassword(PasswordDTO passwordDto);
        Task<bool> DeletePassword(int id);
        string EncryptPassword(string password, int encryptionType);
    }
}
