namespace Antix.Services.Validation.Predicates
{
    public class StringNotNullOrWhiteSpacePredicate : ValidationPredicateBase<string>
    {
        public override bool Is(string model)
        {
            return !string.IsNullOrWhiteSpace(model);
        }
    }
}