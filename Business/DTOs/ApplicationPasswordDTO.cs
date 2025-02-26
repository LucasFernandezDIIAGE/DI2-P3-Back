using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class ApplicationPasswordDTO
    {
        public int ApplicationId { get; set; }
        public int PasswordId { get; set; }
        public ApplicationDTO Application { get; set; } = new ApplicationDTO();
        public PasswordDTO Password { get; set; } = new PasswordDTO();
    }
}
