namespace Antix.Services.Validation.Predicates
{
    public class StringNullOrWhiteSpacePredicate : ValidationPredicateBase<string>
    {
        public override bool Is(string model)
        {
            return string.IsNullOrWhiteSpace(model);
        }
    }
}