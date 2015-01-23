namespace Antix.Services.Validation.Predicates
{
    public class StringMaxLengthPredicate : ValidationPredicateBase<string>
    {
        readonly int _max;

        public StringMaxLengthPredicate(int max)
        {
            _max = max;
        }

        public override bool Is(string model)
        {
            return model.Length <= _max;
        }

        public override string ToString()
        {
            return NameFormat("max", _max);
        }
    }
}