using System.Threading.Tasks;

namespace Antix.Services.Validation
{
    public static class ValidationExtensions
    {
        public static async Task<string[]> ValidateAsync<TModel>(
            this IValidator<TModel> validator,
            TModel model)
        {
            return await validator.ValidateAsync(model, string.Empty);
        }

        public static async Task<string[]> ValidateAsync<TModel>(
            this IValidator<TModel> validator,
            TModel model,
            string path)
        {
            return await validator.ExecuteAsync(
                new ValidateRequest<TModel>(model, path));
        }

        public static string[] Validate<TModel>(
            this IValidator<TModel> validator,
            TModel model)
        {
            return validator.Validate(model, string.Empty);
        }

        public static string[] Validate<TModel>(
            this IValidator<TModel> validator,
            TModel model,
            string path)
        {
            return validator.ExecuteAsync(
                new ValidateRequest<TModel>(model, path))
                .Result;
        }
    }
}