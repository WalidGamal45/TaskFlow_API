using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CategoryServices : IRepositoryTaskFlow<Category>
    {
        private readonly DBContext _dbContext;

        public CategoryServices(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category == null)
                throw new KeyNotFoundException($"Category with Id {id} was not found.");

            return category;
        }

        public async Task AddAsync(Category entity)
        {
            await _dbContext.Categories.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category newEntity)
        {
            var oldEntity = await _dbContext.Categories.FindAsync(newEntity.Id);
            if (oldEntity == null)
                throw new KeyNotFoundException($"Category with Id {newEntity.Id} was not found.");

            oldEntity.Name = newEntity.Name;
            oldEntity.Description = newEntity.Description;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category == null)
                throw new KeyNotFoundException($"Category with Id {id} was not found.");

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
        }
    }
}
