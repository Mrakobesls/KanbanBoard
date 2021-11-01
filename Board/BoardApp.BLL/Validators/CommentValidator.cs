using System;
using BoardApp.BLL.Validators.Base;
using BoardApp.Common.Models;
using FluentValidation;

namespace BoardApp.BLL.Validators
{
    public class CommentValidator : BaseValidator<CommentDto>
    {
        public CommentValidator()
        {
            RuleFor(x => x.Text).NotEmpty().MaximumLength(30);
            RuleFor(x => x.DateTime).GreaterThan(DateTime.MinValue);
        }
    }
}
