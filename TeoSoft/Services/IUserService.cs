using TeoSoft.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TeoSoft.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(string id, User user);
        Task<bool> DeleteUserAsync(string id);
        Task<string> GetUserByIdAsync(string id);
        Task<bool> CanDeleteUserAsync(string id);
    }
}