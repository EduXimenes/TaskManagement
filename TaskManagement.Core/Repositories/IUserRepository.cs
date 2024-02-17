using TaskManagement.Core.Entities;

namespace TaskManagement.Infrastructure.Persistence.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task DeleteUser(Guid idUser);
        Task<List<User>> GetAllUsers();
        Task<User?> GetUser(Guid idUser);
    }
}