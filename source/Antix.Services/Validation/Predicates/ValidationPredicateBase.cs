using System.Linq;

namespace Antix.Services.Validation.Predicates
{
    public abstract class ValidationPredicateBase<TModel> :
        IValidationPredicate<TModel>
    {
        const string SUFFIX = "Predicate";
        readonly string _name;

        protected ValidationPredicateBase(string name)
        {
            _name = name;
        }

        protected ValidationPredicateBase()
        {
            var typeName = GetType().Name;
            if (typeName.EndsWith(SUFFIX))
                typeName = typeName.Substring(0, typeName.Length - SUFFIX.Length);

            _name = string.Join(
                "",
                typeName
                    .Select((c, i) => char.IsUpper(c)
                        ? (i > 0 ? "-" : "") + char.ToLower(c)
                        : char.ToString(c))
                );
        }

        public abstract bool Is(TModel model);

        public override string ToString()
        {
            return _name;
        }

        protected string NameFormat(params object[] parameters)
        {
            return string.Format("{0}[{1}]",
                _name,
                string.Join("",
                    parameters.Select(
                        (p, i) => (i%2 == 0 ? p : string.Concat(":\'", p, "',")))
                    ).TrimEnd(',')
                );
        }
    }
}