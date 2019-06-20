using System;
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

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userService.GetAllUsers());
        }

        // GET api/values/5
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

        // POST api/values
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

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
