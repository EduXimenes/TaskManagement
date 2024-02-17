using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.ViewModels;
using TaskManagement.Core.Entities;
using TaskManagement.Infrastructure.Persistence.Repositories;

namespace TaskManagement.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<UserViewModel>> GetAllUsers()
        {
            var users = await _repository.GetAllUsers();

            var viewModel = _mapper.Map<List<UserViewModel>>(users);
            return viewModel;
        }
        public async Task<UserViewModel?> GetUser(Guid idUser)
        {
            var user = await _repository.GetUser(idUser);

            var viewModel = _mapper.Map<UserViewModel>(user);
            return viewModel;
        }
        public async Task<UserViewModel> AddUser(User input)
        {
            var user = await _repository.AddUser(input);

            var viewModel = _mapper.Map<UserViewModel>(user);
            return viewModel;
        }
        public async Task DeleteUser(Guid idUser)
        {
            await _repository.DeleteUser(idUser);
        }
    }
}
