using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Dto;
using Sat.Recruitment.Api.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Utils;

namespace Sat.Recruitment.Api.Controllers
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {

        private IMapper _mapper;
        private IUserService _service;
        private readonly List<User> _users = new List<User>();
        public UsersController(IMapper mapper, IUserService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return new Result()
                {
                    IsSuccess = false,
                    Errors = string.Join(' ', ModelState.Values
                        .Select(_ => string.Join(' ',
                                    _.Errors.Select(error => error.ToString()))))
                };
            }

            //User newUser = _mapper.Map<User>(user);
            User newUser = new User();
            newUser.Address = user.Address;
            newUser.Email = user.Email;
            newUser.Money = Convert.ToDecimal(user.Money);
            newUser.Name = user.Name;
            newUser.Phone = user.Phone;
            newUser.UserType = Enum.Parse<Enums.UserType>(user.UserType);
            //_mapper.Map<User>(user);

            try
            {
                await _service.CreateUser(newUser);
            }
            catch (Exception exception)
            {
                return new Result()
                {
                    IsSuccess = false,
                    Errors = exception.Message
                };
            }

            return new Result()
            {
                IsSuccess = true,
                Errors = "User Created"
            };
        }


        //Validate errors
        private void ValidateErrors(string name, string email, string address, string phone, ref string errors)
        {
            if (name == null)
                //Validate if Name is null
                errors = "The name is required";
            if (email == null)
                //Validate if Email is null
                errors = errors + " The email is required";
            if (address == null)
                //Validate if Address is null
                errors = errors + " The address is required";
            if (phone == null)
                //Validate if Phone is null
                errors = errors + " The phone is required";
        }
    }
}
