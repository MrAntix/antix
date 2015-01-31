using System.Linq;

namespace Antix.Services.Validation.Predicates
{
    public abstract class ValidationPredicateBase<TModel> :
        IValidationPredicate<TModel>
    {
        const string Suffix = "Predicate";
        const string StringPrefix = "String";
        const string NumberPrefix = "Number";

        readonly string _name;

        protected ValidationPredicateBase(string name)
        {
            _name = name;
        }

        protected ValidationPredicateBase()
        {
            var typeName = GetType().Name;
            typeName = typeName
                .TrimEnd(Suffix)
                .TrimStart(StringPrefix)
                .TrimStart(NumberPrefix);

            _name = string.Join(
                "",
                typeName
                    .Select((c, i) => char.IsUpper(c)
                        ? (i > 0 ? "-" : "") + char.ToLower(c)
                        : char.ToString(c))
                );
        }

        public abstract bool Is(TModel model);

        public string Name
        {
            get { return _name; }
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