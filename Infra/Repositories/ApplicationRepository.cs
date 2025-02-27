using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Application> CreateApplication(Application application)
        {
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            return application;
        }

        public async Task<IEnumerable<Application>> GetAllApplications()
        {
            return await _context.Applications.ToListAsync();
        }

        public async Task<Application> GetApplicationById(int id)
        {
            return await _context.Applications.FindAsync(id);
        }
    }
}
