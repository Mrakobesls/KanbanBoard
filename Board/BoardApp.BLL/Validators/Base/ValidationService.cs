using System.Linq;

namespace BoardApp.BLL.Validators.Base
{
    public class ValidationService : IValidationService
    {
        public ValidationModel Validate<TValidator, T>(T entity) where TValidator : BaseValidator<T>, new()
        {
            var validationModel = new ValidationModel();
            TValidator validator = new TValidator();
            var validationResult = validator.Validate(entity);

            validationModel.IsValid = validationResult.IsValid;
            var errors = validationResult.Errors.Select(x => x.ErrorMessage);
            var result = string.Join("\n", errors);
            validationModel.Message = result;
            return validationModel;
        }
    }
}
