namespace Antix.Services.Validation.Predicates
{
    public class NullPredicate : ValidationPredicateBase<object>
    {
        public override bool Is(object model)
        {
            return Equals(model, null);
        }
    }

    public class NotNullPredicate : ValidationPredicateBase<object>
    {
        public override bool Is(object model)
        {
            return !Equals(model, null);
        }
    }
}