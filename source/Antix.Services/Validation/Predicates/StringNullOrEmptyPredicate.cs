namespace Antix.Services.Validation.Predicates
{
    public class StringNullOrEmptyPredicate : ValidationPredicateBase<string>
    {
        public override bool Is(string model)
        {
            return string.IsNullOrEmpty(model);
        }
    }
}