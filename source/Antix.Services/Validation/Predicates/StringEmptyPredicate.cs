namespace Antix.Services.Validation.Predicates
{
    public class StringEmptyPredicate : ValidationPredicateBase<string>
    {
        public override bool Is(string model)
        {
            return model.Length == 0;
        }
    }
}