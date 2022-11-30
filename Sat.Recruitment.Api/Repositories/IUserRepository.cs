using Sat.Recruitment.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
    }
}
