using Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IApplicationService
    {
        Task<IEnumerable<ApplicationDTO>> GetAllApplications();
        Task<ApplicationDTO> CreateApplication(ApplicationDTO applicationDto);
    }
}
