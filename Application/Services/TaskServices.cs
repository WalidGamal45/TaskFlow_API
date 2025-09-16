using Application.DTOs;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services
{
    public class TaskServices : IRepositoryTaskFlow<Task1,TaskDto>
    {
        private readonly DBContext _dbContext;

        public TaskServices(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Task1>> GetAllAsync()
        {
            return await _dbContext.Tasks
                                   .Include(t => t.User)
                                   .Include(t => t.Category)
                                   .ToListAsync();
        }

        public async Task<Task1> GetByIdAsync(int id)
        {
            var task = await _dbContext.Tasks
                                       .Include(t => t.User)
                                       .Include(t => t.Category)
                                       .FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
                throw new KeyNotFoundException($"Task with Id {id} was not found.");

            return task;
        }

        public async Task AddAsync(Task1 entity)
        {
            await _dbContext.Tasks.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaskDto newEntity)
        {
            var oldEntity = await _dbContext.Tasks.FindAsync(newEntity.Id);
            if (oldEntity == null)
                throw new KeyNotFoundException($"Task with Id {newEntity.Id} was not found.");

            oldEntity.Title = newEntity.Title;
            oldEntity.Description = newEntity.Description;
            oldEntity.Deadline = newEntity.Deadline;
            oldEntity.IsCompleted = newEntity.IsCompleted;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var task = await _dbContext.Tasks.FindAsync(id);
            if (task == null)
                throw new KeyNotFoundException($"Task with Id {id} was not found.");

            _dbContext.Tasks.Remove(task);
            await _dbContext.SaveChangesAsync();
        }
    }
}
