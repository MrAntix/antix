using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Antix.Services.Validation
{
    public static class ValidationExtensions
    {
        public static string[] Validate<TModel>(
            this IValidator<TModel> validator,
            TModel model)
        {
            return validator.Validate(model, string.Empty);
        }

        public static string[] Build<TModel>(
            this IValidationBuilder<TModel> builder,
            TModel model
            )
        {
            return builder.Build(model, string.Empty);
        }

        public static IEnumerable<string> Build<TModel, TProperty>(
            this IValidationBuilder<TProperty> builder,
            TModel model,
            Expression<Func<TModel, TProperty>> propertyExpression,
            string path)
        {
            var subPath = ConcatPath(path, propertyExpression);
            var subModel = propertyExpression.Compile()(model);

            return builder.Build(subModel, subPath);
        }

        public static string[] BuildEach<TParentModel, TModel>(
            this IValidationBuilder<TModel> builder,
            TParentModel model,
            Expression<Func<TParentModel, IEnumerable<TModel>>> propertyExpression,
            string path)
        {
            var subPath = ConcatPath(path, propertyExpression);
            var subModels = propertyExpression.Compile()(model);

            return subModels.SelectMany(
                (subModel, index)
                    => builder.Build(
                        subModel,
                        string.Format("{0}[{1}]", subPath, index))
                ).ToArray();
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