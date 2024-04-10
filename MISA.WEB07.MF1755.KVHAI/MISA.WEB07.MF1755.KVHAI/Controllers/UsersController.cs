﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB07.MF1755.KVHAI;
using MySqlConnector;
using Dapper;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MISA.WEB07.MF1755.KVHAI.Application;
using MISA.WEB07.MF1755.KVHAI.Domain;

namespace MISA.WEB07.MF1755.KVHAI.Controllers
{
    public class UsersController : BaseController<UserDto, UserCreateDto, UserUpdateDto, UserSearchPaging, UserFilterResultDto>
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) : base(userService)
        {
            _userService = userService;
        }
    }
}
