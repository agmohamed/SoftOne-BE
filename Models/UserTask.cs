using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SOTaskManagement.Enums;

namespace SOTaskManagement.Models;

public class UserTask
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string AssignedTo { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string StartDate { get; set; } = string.Empty;
    public string DueDate { get; set; } = string.Empty;
    public TaskPriority Priority { get; set; } = TaskPriority.Low;
    public Enums.TaskStatus Status { get; set; }

}

