namespace Antix.Services.Validation.Predicates
{
    public class StringMinLengthPredicate :
        ValidationPredicateBase<string>
    {
        readonly int _min;

        public StringMinLengthPredicate(int min)
        {
            _min = min;
        }

        public override bool Is(string model)
        {
            return model.Length >= _min;
        }

        public override string ToString()
        {
            return NameFormat("min", _min);
        }
    }
}