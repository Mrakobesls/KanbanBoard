using BoardApp.BLL.Validators.Base;
using BoardApp.Common.Models;
using FluentValidation;

namespace BoardApp.BLL.Validators
{
    public class LabelValidator : BaseValidator<LabelDto>
    {
        public LabelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(30);
        }
    }
}
