using BoardApp.BLL.Validators.Base;
using BoardApp.Common.Models;
using FluentValidation;

namespace BoardApp.BLL.Validators
{
    public class BoardValidator : BaseValidator<BoardDto>
    {
        public BoardValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(30);
        }
    }
}
