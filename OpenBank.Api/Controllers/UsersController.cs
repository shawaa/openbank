﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using OpenBank.Application;

namespace OpenBank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IAccountDetailsService _accountDetailsService;

        public UsersController(IUserService userService, IAccountDetailsService accountDetailsService)
        {
            _userService = userService;
            _accountDetailsService = accountDetailsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> Get(Guid id)
        {
            (UserDetailsDto dto, Error error) result = await _accountDetailsService.GetUserAccountDetails(id);

            if (result.error != null)
            {
                return BadRequest(result.error);
            }

            return Ok(result.dto);
        }

        [HttpGet("{id}/transactions")]
        public async Task<IActionResult> GetTransactions(Guid id)
        {
            (IEnumerable<TransactionDto> transactions, Error error) result = await _accountDetailsService.GetTransactions(id);

            if (result.error != null)
            {
                return BadRequest(result.error);
            }

            return Ok(result.transactions);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserDto user)
        {
            (User user, Error error) result = await _userService.AddUser(user);

            if (result.error != null)
            {
                return BadRequest(result.error);
            }

            return CreatedAtRoute(
                routeName: "GetUser",
                routeValues: new { id = result.user.Id },
                value: result.user);
        }
    }
}
