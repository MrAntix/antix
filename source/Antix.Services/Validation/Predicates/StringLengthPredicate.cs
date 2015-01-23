namespace Antix.Services.Validation.Predicates
{
    public class StringLengthPredicate : ValidationPredicateBase<string>
    {
        readonly int _min;
        readonly int _max;

        public StringLengthPredicate(int min, int max)
        {
            _min = min;
            _max = max;
        }

        public override bool Is(string model)
        {
            return model.Length >= _min
                   && model.Length <= _max;
        }

        public override string ToString()
        {
            return NameFormat("min", _min, "max", _max);
        }
    }
}