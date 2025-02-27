using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class PasswordDTO
    {
        public int Id { get; set; }
        public string EncryptedValue { get; set; }
        public string AccountName { get; set; }
        public int ApplicationId { get; set; }
    }
}
