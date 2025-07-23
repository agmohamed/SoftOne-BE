using Microsoft.EntityFrameworkCore;
using SOTaskManagement.Dtos;
using SOTaskManagement.Enums;
using SOTaskManagement.Interfaces;
using SOTaskManagement.Models;
using SOTaskManagement.Persistence;
using SOTaskManagement.ViewModels;

namespace SOTaskManagement.Services;

public class TaskService(ApplicationDbContext context) : ITaskService
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<PaginatedData<UserTaskVm>> GetAllTasks(PaginatedQuery query)
    {
        try
        {
            var queryable = _context.UserTasks.AsQueryable();
            if (!string.IsNullOrEmpty(query.Keyword))
            {
                var search = query.Keyword?.ToLower();
                queryable = queryable.Where(x => x.Title.ToLower().Contains(search) || x.AssignedTo.ToLower().Contains(search));

            }
            if (query.Status > 0)
            {
                queryable = queryable.Where(x => x.Status == (Enums.TaskStatus)query.Status);
            }

            if (query.Priority > 0)
            {
                queryable = queryable.Where(x => x.Priority == (TaskPriority)query.Priority);
            }

            if (query.SortDirection.Equals("desc", StringComparison.OrdinalIgnoreCase))
            {
                queryable = queryable.OrderByDescending(x => x.Id);
            }
            else
            {
                queryable = queryable.OrderBy(x => x.Id);
            }

            var count = await queryable.CountAsync();
            var items = await queryable
                                .Skip((query.Page - 1) * query.PerPage)
                                .Take(query.PerPage)
                                .ToListAsync();

            var result = new PaginatedData<UserTaskVm>
            {
                TotalCount = count,
                Data = [.. items.Select(i => MapToViewModel(i))],
                CurrentPage = query.Page,
                TotalPages = (int)Math.Ceiling((double)count / query.PerPage)
            };
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }


    public async Task<UserTaskVm> GetTaskById(int id)
    {
        try
        {
            var existingTask = await GetTaskByIdAsync(id);
            return MapToViewModel(existingTask);
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }

    }

    public async Task<UserTaskVm> CreateTask(UserTaskDto dto)
    {
        try
        {
            var task = MapToModel(dto);
            await _context.UserTasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return MapToViewModel(task);
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }



    public async Task<UserTaskVm> UpdateTask(int id, UserTaskDto dto)
    {
        try
        {
            var existingTask = await GetTaskByIdAsync(id);
            var task = MapToModel(dto);
            task.Id = existingTask.Id;
            await _context.SaveChangesAsync();
            return MapToViewModel(task);
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }

    }

    public async Task<bool> DeleteTask(int id)
    {
        try
        {
            var existingTask = await GetTaskByIdAsync(id);
            _context.UserTasks.Remove(existingTask);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }


    private static UserTaskVm MapToViewModel(UserTask task)
    {
        var result = new UserTaskVm
        {
            Id = task.Id,
            Title = task.Title,
            AssignedTo = task.AssignedTo,
            Description = task.Description,
            StartDate = task.StartDate,
            DueDate = task.DueDate,
            Priority = task.Priority,
            Status = task.Status

        };
        return result;

    }
    private static UserTask MapToModel(UserTaskDto dto)
    {
        return new UserTask
        {
            Title = dto.Title,
            AssignedTo = dto.AssignedTo,
            Description = dto.Description,
            StartDate = dto.StartDate,
            DueDate = dto.DueDate,
            Priority = dto.Priority,
            Status = dto.Status
        };
    }

    private async Task<UserTask> GetTaskByIdAsync(int id)
    {
        return await _context.UserTasks.FindAsync(id) ?? throw new KeyNotFoundException($"Task with ID {id} not found.");
    }
}
