namespace Antix.Services.Validation.Predicates
{
    public class StringEmptyPredicate : ValidationPredicateBase<string>
    {
        public override bool Is(string model)
        {
            return model.Length == 0;
        }
    }

    public class StringNullOrEmptyPredicate : ValidationPredicateBase<string>
    {
        public override bool Is(string model)
        {
            return string.IsNullOrEmpty(model);
        }
    }

    public class StringNullOrWhiteSpacePredicate : ValidationPredicateBase<string>
    {
        public override bool Is(string model)
        {
            return string.IsNullOrWhiteSpace(model);
        }
    }

    public class StringNotEmptyPredicate : ValidationPredicateBase<string>
    {
        public override bool Is(string model)
        {
            return model.Length > 0;
        }
    }

    public class StringNotNullOrEmptyPredicate : ValidationPredicateBase<string>
    {
        public override bool Is(string model)
        {
            return !string.IsNullOrEmpty(model);
        }
    }

    public class StringNotNullOrWhiteSpacePredicate : ValidationPredicateBase<string>
    {
        public override bool Is(string model)
        {
            return !string.IsNullOrWhiteSpace(model);
        }
    }

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