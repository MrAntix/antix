namespace Antix.Services.Validation.Predicates
{
    public static class ValidationPredicateExtensions
    {
        public static bool Is<TModel>(
            this IValidationPredicate<TModel> predicate,
            TModel model)
        {
            return predicate.IsAsync(model).Result;
        }
    }
}