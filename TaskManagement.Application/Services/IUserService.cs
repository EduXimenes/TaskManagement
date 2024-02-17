using TaskManagement.Application.ViewModels;
using TaskManagement.Core.Entities;

namespace TaskManagement.Application.Services
{
    public interface IUserService
    {
        Task<UserViewModel> AddUser(User input);
        Task<List<UserViewModel>> GetAllUsers();
        Task<UserViewModel?> GetUser(Guid idUser);
        Task DeleteUser(Guid idUser);
    }
}