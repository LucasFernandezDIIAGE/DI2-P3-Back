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
    public class PasswordRepository : IPasswordRepository
    {
        private readonly ApplicationDbContext _context;

        public PasswordRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Password> CreatePassword(Password password)
        {
            _context.Passwords.Add(password);
            await _context.SaveChangesAsync();

            return password;
        }

        public async Task DeletePassword(int id)
        {
            var password = await _context.Passwords.FindAsync(id);

            if (password == null)
            {
                throw new KeyNotFoundException("Password not found.");
            }

            _context.Passwords.Remove(password);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Password>> GetAllPasswords()
        {
            return await _context.Passwords.ToListAsync();
        }
    }
}
