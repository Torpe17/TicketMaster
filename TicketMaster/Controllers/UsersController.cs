using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TicketMaster.DataContext.Context;
using TicketMaster.DataContext.Models;
using TicketMaster.DataContext.UnitsOfWork;
using TicketMaster.Services;
using TicketMaster.Services.DTOs.AddressDTOs;
using TicketMaster.Services.DTOs.UserDTOs;

namespace TicketMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;
        private readonly IAddressService _addressService;

        public UsersController(AppDbContext context, IUserService userService, IAddressService addressService)
        {
            _context = context;
            _userService = userService;
            _addressService = addressService;
        }

        // GET: api/Users
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            return await _userService.GetUsersAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                return Ok(user);
            }
            catch (KeyNotFoundException e) { return NotFound(e.Message); }
            catch (Exception e) { return BadRequest(e.Message); }
        }


        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO userDto)
        {
            try
            {
                var result = await _userService.RegisterAsync(userDto);
                return Ok(result);
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userDto)
        {
            try
            {
                var token = await _userService.LoginAsync(userDto);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException e) { return Unauthorized(e.Message); }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserUpdateDTO userDto)
        {
            try
            {
                var userId = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
                var result = await _userService.UpdateProfileAsync(userId, userDto);
                return Ok(result);
            }
            catch (KeyNotFoundException e) { return NotFound(e.Message); }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        [HttpPut("{userId}/role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetRole(int userId, [FromBody] RoleUpdateDTO roleDto)
        {
            try
            {
                await _userService.AddRolesAsync(userId, roleDto);
                return Ok();
            }
            catch (UnauthorizedAccessException e) { return Unauthorized(e.Message); }
            catch (KeyNotFoundException e) { return NotFound(e.Message); }
            catch (Exception e) { return BadRequest(e.Message); }
        }


        [HttpGet("address")]
        public async Task<ActionResult<AddressGetDTO>> GetAddress()
        {
            try
            {
                var userId = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
                var result = await _addressService.GetAddressByUserIdAsync(userId);
                return Ok(result);
            }
            catch (KeyNotFoundException e) { return NotFound(e.Message); }
            catch (SqlException e) { return BadRequest(e.InnerException); }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        [HttpPost("address")]
        public async Task<IActionResult> CreateAddress([FromBody] AddressPostDTO addressDto)
        {
            try
            {
                var userId = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
                var result = await _addressService.CreateAddressAsync(userId, addressDto);
                return Ok(result);
            }
            catch (KeyNotFoundException e) { return NotFound(e.Message); }
            catch (SqlException e) { return BadRequest(e.InnerException.Message); }
            catch (Exception e) { return BadRequest(e.Message); }
        }


        [HttpPut("address")]
        public async Task<IActionResult> UpdateAddress([FromBody] AddressPutDTO addressDto)
        {
            try
            {
                var userId = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
                var result = await _addressService.UpdateAddressAsync(userId, addressDto);
                return Ok(result);
            }
            catch(SqlException e) { return BadRequest(e.InnerException); }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        [HttpDelete("address")]
        public async Task<IActionResult> DeleteAddress()
        {
            try
            {
                var userId = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
                await _addressService.DeleteAddressByUserIdAsync(userId);
                return Ok();
            }
            catch (SqlException e) { return BadRequest(e.InnerException); }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteUser(id);
                return Ok();
            }
            catch (KeyNotFoundException e) { return NotFound(e.Message); }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        [HttpGet("roles")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _userService.GetRolesAsync();
            return Ok(result);
        }
    }
}
