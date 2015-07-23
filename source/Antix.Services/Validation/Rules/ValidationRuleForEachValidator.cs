using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Antix.Services.Validation.Rules
{
    public class ValidationRuleForEachValidator<TModel, TProperty> :
        IValidator<TModel>
    {
        readonly Expression<Func<TModel, IEnumerable<TProperty>>> _propertyExpression;
        readonly IValidationRuleBuilder<TProperty> _builder;

        public ValidationRuleForEachValidator(
            Expression<Func<TModel, IEnumerable<TProperty>>> propertyExpression,
            IValidationRuleBuilder<TProperty> builder)
        {
            _propertyExpression = propertyExpression;
            _builder = builder;
        }

        public async Task<string[]> ExecuteAsync(
            ValidateRequest<TModel> request)
        {
            var models = _propertyExpression.Compile()(request.Model);
            var path = ConcatPath(request.Path, _propertyExpression);

            var validators = _builder.Build();
            var errors = new List<string>();

            var index = 0;
            foreach (var model in models)
            {
                var forRequest = new ValidateRequest<TProperty>(
                    model,
                    string.Format("{0}[{1}]", path, index));
                index++;

                foreach (var validator in validators)
                {
                    errors.AddRange(
                        await validator.ExecuteAsync(forRequest));
                }
            }

            return errors.ToArray();
        }

        static string ConcatPath(string path, Expression propertyExpression)
        {
            return string.Format("{0}{1}{2}",
                path,
                string.IsNullOrEmpty(path) ? string.Empty : ".",
                ExpressionPathVisitor.GetPath(propertyExpression));
        }
    }
}