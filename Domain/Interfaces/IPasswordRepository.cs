﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPasswordRepository
    {
        Task<IEnumerable<Password>> GetAllPasswords();
        Task<Password> CreatePassword(Password password);
        Task DeletePassword(int id);
        Task<IEnumerable<Password>> GetPasswordsByApplicationId(int applicationId);

    }
}
