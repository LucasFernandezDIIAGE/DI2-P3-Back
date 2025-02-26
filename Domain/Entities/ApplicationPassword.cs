using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ApplicationPassword
    {
        public int ApplicationId { get; set; }
        public Application Application { get; set; } = null!;

        public int PasswordId { get; set; }
        public Password Password { get; set; } = null!;
    }
}
