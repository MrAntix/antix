using System;
using System.Linq;
using System.Threading.Tasks;

namespace Antix.Services.Validation.Predicates
{
    public abstract class ValidationPredicate
    {
        const string Suffix = "Predicate";
        const string StringPrefix = "String";
        const string NumberPrefix = "Number";

        public static string GetDefaultName(Type type)
        {
            var typeName = type.Name;
            typeName = typeName
                .TrimEnd(Suffix)
                .TrimStart(StringPrefix)
                .TrimStart(NumberPrefix);

            return string.Join(
                "",
                typeName
                    .Select((c, i) => char.IsUpper(c)
                        ? (i > 0 ? "-" : "") + char.ToLower(c)
                        : char.ToString(c))
                );
        }
    }

    public abstract class ValidationPredicateBase<TModel> :
        ValidationPredicate, IValidationPredicate<TModel>
    {
        readonly string _name;

        protected ValidationPredicateBase(string name)
        {
            _name = name;
        }

        protected ValidationPredicateBase()
        {
            _name = GetDefaultName(GetType());
        }

        public abstract Task<bool> IsAsync(TModel model);

        public string Name
        {
            get { return _name; }
        }

        protected string NameFormat(params object[] parameters)
        {
            return string.Format("{0}[{1}]",
                Name,
                string.Join("",
                    parameters.Select(
                        (p, i) => (i%2 == 0 ? p : string.Concat(":\'", p, "',")))
                    ).TrimEnd(',')
                );
        }
    }
}