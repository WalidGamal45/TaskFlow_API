using Application.DTOs;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services
{
    public class UserServices : IRepositoryTaskFlow<User, UserDto>
    {
        private readonly DBContext _dbContext;

        public UserServices(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User entity)
        {
            await _dbContext.Users.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with Id {id} was not found.");

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with Id {id} was not found.");

            return user;
        }

        public async Task UpdateAsync(UserDto newEntity)
        {
            var oldEntity = await _dbContext.Users.FindAsync(newEntity.Id);
            if (oldEntity == null)
                throw new KeyNotFoundException($"User with Id {newEntity.Id} was not found.");

            oldEntity.UserName = newEntity.UserName;
            oldEntity.PassWord = newEntity.PassWord;
            oldEntity.Email = newEntity.Email;
            oldEntity.PhoneNumber = newEntity.PhoneNumber;

            await _dbContext.SaveChangesAsync();
        }
    }
}
