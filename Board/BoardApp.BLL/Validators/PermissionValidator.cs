using BoardApp.BLL.Validators.Base;
using BoardApp.Common.Models;
using FluentValidation;

namespace BoardApp.BLL.Validators
{
    public class PermissionValidator : BaseValidator<PermissionDto>
    {
        public PermissionValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(30);
        }
    }
}
