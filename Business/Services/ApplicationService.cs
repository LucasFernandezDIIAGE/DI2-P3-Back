using Business.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationService(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<ApplicationDTO> CreateApplication(ApplicationDTO applicationDto)
        {
            Application application = new Application
            {
                Name = applicationDto.Name,
                Type = applicationDto.Type
            };

            var addedApplication = await _applicationRepository.CreateApplication(application);

            return new ApplicationDTO
            {
                Name = addedApplication.Name,
                Type = addedApplication.Type,
            };
        }

        public async Task<IEnumerable<ApplicationDTO>> GetAllApplications()
        {
            var applications = await _applicationRepository.GetAllApplications();

            return applications.Select(application => new ApplicationDTO
            {
                Id = application.Id,
                Name = application.Name,
                Type = application.Type,
            });
        }
    }
}
