using Sat.Recruitment.Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Utils;

namespace Sat.Recruitment.Api.Repositories
{
    public class UserRepository : IUserRepository
    {

        public async Task<List<User>> GetAll()
        {
            var users = new List<User>();

            using (var reader = ReadUsersFromFile())
            {
                while (reader.Peek() >= 0)
                {
                    var row = await reader.ReadLineAsync();
                    var fields = row.Split(',');
                    var user = new User
                    {
                        Name = fields[0].ToString(),
                        Email = fields[1].ToString(),
                        Phone = fields[2].ToString(),
                        Address = fields[3].ToString(),
                        UserType = Enum.Parse<Enums.UserType>(fields[4].ToString()),
                        Money = decimal.Parse(fields[5].ToString()),
                    };
                    users.Add(user);
                }
                reader.Close();
            }

            return users;
        }

        private StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
    }
}
