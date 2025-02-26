using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class ApplicationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ApplicationType Type { get; set; }
    }
}
