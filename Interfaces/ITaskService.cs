using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SOTaskManagement.Dtos;
using SOTaskManagement.Models;
using SOTaskManagement.ViewModels;

namespace SOTaskManagement.Interfaces;

public interface ITaskService
{
    Task<PaginatedData<UserTaskVm>> GetAllTasks(PaginatedQuery query);
    Task<UserTaskVm> GetTaskById(int id);
    Task<UserTaskVm> CreateTask(UserTaskDto dto);
    Task<UserTaskVm> UpdateTask(int id, UserTaskDto dto);
    Task<bool> DeleteTask(int id);

}
