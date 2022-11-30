using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Repositories;
using Sat.Recruitment.Api.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Test
{
    public class TestUserRepository : IUserRepository
    {
        public async Task<List<User>> GetAll()
        {
            return new List<User>()
            {
                new User
                {
                    Name = "Joaquín Puertolas",
                    Email = "jpuertolas@gmail.com",
                    Phone = "351 5754349",
                    Address = "Maldonado 1185",
                    UserType = Enums.UserType.Normal,
                    Money = 112234
                },
                new User
                {
                    Name = "Benjamín Puertolas",
                    Email = "bpuertolas@gmail.com",
                    Phone = "351 5754345",
                    Address = "Maldonado 1185",
                    UserType = Enums.UserType.Normal,
                    Money = 112234
                },
                new User
                {
                    Name = "Ana Laura Font",
                    Email = "analauracba@hotmail.com",
                    Phone = "+5493516843614",
                    Address = "Maldonado 1185",
                    UserType = Enums.UserType.Premium,
                    Money = 112234
                },
                new User
                {
                    Name = "Agustina",
                    Email = "Agustina@gmail.com",
                    Address = "Av. Juan G",
                    Phone = "+349 1122354210",
                    UserType = Enums.UserType.Premium,
                    Money = 124
                },
                new User
                {
                    Name = "Daniel Puertolas",
                    Email = "puertolas189@hotmail.com",
                    Phone = "+5493513056632",
                    Address = "Maldonado 1185",
                    UserType = Enums.UserType.SuperUser,
                    Money = 2500
                },
            };
        }
    }
}
