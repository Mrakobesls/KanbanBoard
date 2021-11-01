using BoardApp.BLL.Validators.Base;
using BoardApp.Common.Models;
using FluentValidation;

namespace BoardApp.BLL.Validators
{
    public class ColumnValidator : BaseValidator<ColumnDto>
    {
        public ColumnValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(30);
        }
    }
}
