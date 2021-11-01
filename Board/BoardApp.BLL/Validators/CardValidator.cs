using BoardApp.BLL.Validators.Base;
using BoardApp.Common.Models;
using FluentValidation;

namespace BoardApp.BLL.Validators
{
    public class CardValidator : BaseValidator<CardDto>
    {
        public CardValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(30);
        }
    }
}
