using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SOTaskManagement.Enums;

namespace SOTaskManagement.Dtos;

public class UserTaskDto
{
    public string Title { get; set; }

    public string AssignedTo { get; set; }

    public string Description { get; set; }

    public string StartDate { get; set; }

    public string DueDate { get; set; }
    public TaskPriority Priority { get; set; } = TaskPriority.Low;
    public Enums.TaskStatus Status { get; set; } = Enums.TaskStatus.Pending;

}
