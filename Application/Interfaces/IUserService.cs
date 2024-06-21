using Final.Domain.Dto;
using Final.Dto;
using Final.Entities;
using Final.Enum;

namespace Final.Interfaces
{
    public interface IUserService
    {
        public Task<List<User>> GetUsers();
        public Task<EUserRole> GetUserRole(string email);

        public Task AddUser(AddUserDto user);
        public Task ChangeUserRole(ChangeUserRoleDto user);
        public Task BanUser(BanUserDto user);
        public Task UnBanUser(BanUserDto user);

        public Task DeleteUser(string email);

        public Task UpdateUser(UpdateUserDto user);

        public Task<User> GetUser(string email);
        public Task<User> GetUser(string email, string password);
    }
}
