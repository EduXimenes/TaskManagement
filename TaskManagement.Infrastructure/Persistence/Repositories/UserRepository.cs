using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Entities;

namespace TaskManagement.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly TaskDbContext _context;
        public readonly IMapper _mapper;
        public UserRepository(TaskDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.Users
                .Where(d => !d.isDeleted)
                .ToListAsync();

            return users;
        }
        public async Task<User?> GetUser(Guid idUser)
        {
            var user = await _context.Users
                    .SingleOrDefaultAsync(d => d.Id == idUser);
            return user;
        }

        public async Task<User> AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task DeleteUser(Guid idUser)
        {
            var user = await _context.Users
                            .SingleOrDefaultAsync(d => d.Id == idUser);
            if (user == null)
            {
                throw new Exception("Identificador de usuário inválido.");
            }
            user.isDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
