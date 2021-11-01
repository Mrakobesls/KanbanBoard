namespace BoardApp.BLL.Validators.Base
{
    public interface IValidationService
    {
        ValidationModel Validate<TValidator, T>(T entity) where TValidator : BaseValidator<T>, new();
    }
}
