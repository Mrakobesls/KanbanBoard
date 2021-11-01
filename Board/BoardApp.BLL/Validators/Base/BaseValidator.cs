using System.Globalization;
using FluentValidation;

namespace BoardApp.BLL.Validators.Base
{
    public class BaseValidator<T> : AbstractValidator<T>
    {
        public BaseValidator()
        {
            ValidatorOptions.Global.DisplayNameResolver = ValidatorOptions.Global.PropertyNameResolver;
            ValidatorOptions.Global.LanguageManager.Culture = CultureInfo.InvariantCulture;
        }
    }
}
