using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Utils;

namespace Sat.Recruitment.Api.Services
{
    public class UserService : IUserService
    {
        IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateUser(User newUser)
        {
            switch (newUser.UserType)
            {
                case Enums.UserType.Normal:
                    if (newUser.Money > 100)
                    {
                        var percentage = Convert.ToDecimal(0.12);
                        //If new user is normal and has more than USD100
                        var gif = newUser.Money * percentage;
                        newUser.Money = newUser.Money + gif;
                    }
                    else if (newUser.Money < 100)
                    {
                        if (newUser.Money > 10)
                        {
                            var percentage = Convert.ToDecimal(0.8);
                            var gif = newUser.Money * percentage;
                            newUser.Money = newUser.Money + gif;
                        }
                    }
                    break;
                case Enums.UserType.SuperUser:
                    if (newUser.Money > 100)
                    {
                        var percentage = Convert.ToDecimal(0.20);
                        var gif = newUser.Money * percentage;
                        newUser.Money = newUser.Money + gif;
                    }
                    break;
                case Enums.UserType.Premium:
                    if (newUser.Money > 100)
                    {
                        var gif = newUser.Money * 2;
                        newUser.Money = newUser.Money + gif;
                    }
                    break;
                default:
                    break;
            }

            //Normalize email
            var aux = newUser.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            newUser.Email = string.Join("@", new string[] { aux[0], aux[1] });


            var isDuplicated = false;
            var users = await _repository.GetAll();

            foreach (var user in users)
            {
                if (user.Email == newUser.Email
                    || user.Phone == newUser.Phone)
                {
                    isDuplicated = true;
                    break;
                }
                else if (user.Name == newUser.Name
                            && user.Address == newUser.Address)
                {
                    isDuplicated = true;
                    break;
                }
            }

            if (!isDuplicated)
            {
                Debug.WriteLine("User Created");
            }
            else
            {
                Debug.WriteLine("The user is duplicated");

                throw new Exception("The user is duplicated");
            }
        }
    }
}
