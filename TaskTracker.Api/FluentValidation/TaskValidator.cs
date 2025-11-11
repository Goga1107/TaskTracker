using FluentValidation;
using TaskTracker.Models.Models;
using TaskTracker.Api.Dtos;

namespace TaskTracker.Api.FluentValidation
{
    public class TaskValidator : AbstractValidator<TaskItemDto>
    {
        public TaskValidator()
        {
            RuleFor(x=> x.Title).NotEmpty().MinimumLength(3);
            RuleFor(u => u.UserId).GreaterThan(0);
            RuleFor(s => s.Status).IsInEnum();
        }
    }
}
