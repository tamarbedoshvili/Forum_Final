using AutoMapper;
using Final.Domain.Dto;
using Final.Dto;
using Final.Entities;
using Final.Enum;
using Final.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Final.Services
{

    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {

            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<List<User>> GetUsers()
        {
            return await Task.FromResult(_userManager.Users.ToList());

        }
        public async Task<EUserRole> GetUserRole(string email)
        {
            var user = await GetUser(email);
            if (user != null)
            {
                return user.Role;
            }
            return EUserRole.User;
        }
        public async Task AddUser(AddUserDto userDto)
        {
            if (await _userManager.FindByEmailAsync(userDto.Email) != null)
            {
                throw new Exception("User already exists");
            }

            var user = _mapper.Map<User>(userDto);
            user.IsBanned = false;
            user.UserName = userDto.Email;

            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (!result.Succeeded)
            {
                var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to create user: {errorMessages}");
            }


            var roleName = "User";
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            var identityUser = await _userManager.FindByEmailAsync(user.Email);
            if (identityUser != null)
            {
                await _userManager.AddToRoleAsync(identityUser, roleName);
            }
        }


        public async Task DeleteUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public async Task UpdateUser(UpdateUserDto updateUser)
        {
            var user = _mapper.Map<User>(updateUser);
            await _userManager.UpdateAsync(user);
        }


        public async Task<User> GetUser(string email)
        {
            return await _userManager.FindByEmailAsync(email);


        }

        public async Task<User> GetUser(string email, string password)
        {
            var user = (await _userManager.FindByEmailAsync(email));
            if (user == null)
            {
                throw new Exception("Provide valid email");
            }
            var result = await _userManager.CheckPasswordAsync(user, password);
            if (!result)
            {
                throw new Exception("Provide valid password");
            }
            return user;

        }

        public async Task BanUser(BanUserDto banUserDto, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user.Role != EUserRole.Administrator)
            {
                throw new Exception("You dont have permission to ban user");
            }

            var userToBan = await _userManager.FindByIdAsync(banUserDto.BannedUserId);
            if (userToBan == null)
            {
                throw new Exception("User not found");
            }
            userToBan.IsBanned = true;

            await _userManager.UpdateAsync(user);

        }


        public async Task UnBanUser(BanUserDto banUserDto, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user.Role != EUserRole.Administrator)
            {
                throw new Exception("You dont have permission to Unban user");
            }

            var userToBan = await _userManager.FindByIdAsync(banUserDto.BannedUserId);
            if (userToBan == null)
            {
                throw new Exception("User not found");
            }
            userToBan.IsBanned = false;

            await _userManager.UpdateAsync(user);

        }
        public async Task ChangeUserRole(ChangeUserRoleDto changeUserRoleDto)
        {
            var identityUser = await _userManager.FindByIdAsync(changeUserRoleDto.UserId);
            if (identityUser == null)
            {
                throw new ArgumentException("User not found", nameof(changeUserRoleDto.UserId));
            }

            var currentRoles = await _userManager.GetRolesAsync(identityUser);
            await _userManager.RemoveFromRolesAsync(identityUser, currentRoles);

            string newRole = "Administrator";

            if (!await _roleManager.RoleExistsAsync(newRole))
            {
                await _roleManager.CreateAsync(new IdentityRole(newRole));
            }
            identityUser.Role = changeUserRoleDto.Role;
            await _userManager.UpdateAsync(identityUser);
            await _userManager.AddToRoleAsync(identityUser, newRole);
        }
    }
}
