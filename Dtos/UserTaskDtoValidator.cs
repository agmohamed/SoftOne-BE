using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace SOTaskManagement.Dtos;

public class UserTaskDtoValidator:AbstractValidator<UserTaskDto>
{
    public UserTaskDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
        RuleFor(x => x.AssignedTo).NotEmpty().WithMessage("AssignedTo is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(x => x.StartDate).NotEmpty().WithMessage("StartDate is required.");
        RuleFor(x => x.DueDate).NotEmpty().WithMessage("DueDate is required.");
    }
}

