using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SOTaskManagement.Dtos;
using SOTaskManagement.Interfaces;
using SOTaskManagement.Models;
using SOTaskManagement.ViewModels;

namespace SOTaskManagement.Controllers;

[ApiController]
[Route("api/tasks")]
public class TaskController(ITaskService taskService) : ControllerBase
{
    private readonly ITaskService _taskService = taskService;

    [HttpGet]
    public async Task<ActionResult<PaginatedData<UserTaskVm>>> GetTasks([FromQuery] PaginatedQuery query)
    {
        try
        {

            var tasks = await _taskService.GetAllTasks(query);
            return Ok(tasks);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"{ex.Message}");
        }
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<UserTaskVm>> GetbyId([Required] int id)
    {
        try
        {
            var result = await _taskService.GetTaskById(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"{ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<UserTaskVm>> CreateTask([FromBody] UserTaskDto dto)
    {
        try
        {
            var createdTask = await _taskService.CreateTask(dto);
            return CreatedAtAction(nameof(GetTasks), new { id = createdTask.Id }, createdTask);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"{ex.Message}");
        }
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<UserTaskVm>> UpdateTask([Required] int id, [FromBody] UserTaskDto dto)
    {
        try
        {
            var result = await _taskService.UpdateTask(id, dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"{ex.Message}");
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<UserTaskVm>> DeleteTask([Required] int id)
    {
        try
        {
            await _taskService.DeleteTask(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"{ex.Message}");
        }
    }
}
