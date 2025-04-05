using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TicketMaster.DataContext.Context;
using TicketMaster.DataContext.Models;
using TicketMaster.DataContext.UnitsOfWork;
using TicketMaster.Services.DTOs;

namespace TicketMaster.Services
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetUsersAsync();
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<UserDTO> RegisterAsync(UserRegisterDTO UserDTO);
        Task<string> LoginAsync(UserLoginDTO UserDTO);
        Task<UserDTO> UpdateProfileAsync(int userId, UserUpdateDTO UserDTO);
        Task<UserWithAddressDTO> UpdateAddressAsync(int userId, AddressPutDTO addressDto);
        Task<IList<RoleDTO>> UpdateAddressAsync();
        Task DeleteUser(int id);
    }
    public class UserService :IUserService
    {
        private readonly AppDbContext _context;
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(AppDbContext context, UnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<List<UserDTO>> GetUsersAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAsync(includedProperties: ["Roles"]);
            return _mapper.Map<List<UserDTO>>(users);
        }
        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id, includedCollections: ["Roles"]);
            if (user == null) throw new KeyNotFoundException("User not found");
            return _mapper.Map<UserDTO>(user);
        }


        public async Task<UserDTO> RegisterAsync(UserRegisterDTO UserDTO)
        {
            var user = _mapper.Map<User>(UserDTO);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(UserDTO.Password);
            user.Roles = new List<Role>();

            if (UserDTO.RoleIds != null)
            {
                foreach (var roleId in UserDTO.RoleIds)
                {
                    var existingRole = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
                    if (existingRole != null)
                    {
                        user.Roles.Add(existingRole);
                    }
                }
            }

            if (!user.Roles.Any())
            {
                user.Roles.Add(await GetDefaultCustomerRoleAsync());
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDTO>(user);
        }

        private async Task<Role> GetDefaultCustomerRoleAsync()
        {
            var customerRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer");
            if (customerRole == null)
            {
                customerRole = new Role { Name = "Customer" };
                await _context.Roles.AddAsync(customerRole);
                await _context.SaveChangesAsync();
            }
            return customerRole;
        }

        public async Task<string> LoginAsync(UserLoginDTO UserDTO)
        {
            var user = await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(x => x.Email == UserDTO.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(UserDTO.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            return await GenerateToken(user);
        }

        private async Task<string> GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])); 
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"]));
            //var expires = DateTime.Now.AddDays(Convert.ToDouble(15)); //15nap

            var id = await GetClaimsIdentity(user);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], id.Claims, expires: expires, signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name), // Fix for CS1061: Changed user.UserName to user.Name
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.AuthTime, DateTime.Now.ToString(CultureInfo.InvariantCulture))
            };

            if (user.Roles != null && user.Roles.Any())
            {
                claims.AddRange(user.Roles.Select(role => new Claim("roleIds", Convert.ToString(role.Id))));
                claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));
            }

            return new ClaimsIdentity(claims, "Token");
        }

        public async Task<UserDTO> UpdateProfileAsync(int userId, UserUpdateDTO UserUpdateDTO)
        {
            var user = await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            _mapper.Map(UserUpdateDTO, user);

            if (UserUpdateDTO.RoleIds != null && UserUpdateDTO.RoleIds.Any())
            {
                user.Roles.Clear();

                foreach (var roleId in UserUpdateDTO.RoleIds)
                {
                    var existingRole = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
                    if (existingRole != null)
                    {
                        user.Roles.Add(existingRole);
                    }
                }
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDTO>(user);
        }

        public async Task DeleteUser(int id)
        {
            if ((await _unitOfWork.UserRepository.GetByIdAsync(id)) == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            await _unitOfWork.UserRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<UserWithAddressDTO> UpdateAddressAsync(int userId, AddressPutDTO dto)
        {
            await _unitOfWork.UserRepository.GetByIdAsync(userId, includedCollections: ["Roles"]);
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, includedReferences: ["Address"]);
            //var user = await _context.Users.Include(u => u.Address).Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }
            dto.UserId = user.Id;

            _mapper.Map(dto, user.Address);

            await _context.SaveChangesAsync();

            return _mapper.Map<UserWithAddressDTO>(user);
        }

        public async Task<IList<RoleDTO>> GetRolesAsync()
        {
            var roles = await _context.Roles.ToListAsync();
            return _mapper.Map<IList<RoleDTO>>(roles);
        }
    }
}
