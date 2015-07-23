using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Antix.Services.Validation
{
    public static class ValidationExtensions
    {
        public static async Task<string[]> ValidateAsync<TModel>(
            this IValidator<TModel> validator,
            TModel model)
        {
            return await validator.ValidateAsync(model, string.Empty);
        }

        public async static Task<string[]> ValidateAsync<TModel>(
            this IValidator<TModel> validator,
            TModel model,
            string path)
        {
            return await validator.ExecuteAsync(
                new ValidateRequest<TModel>(model, path));
        }

        public static string[] Validate<TModel>(
            this IValidator<TModel> validator,
            TModel model)
        {
            return validator.Validate(model, string.Empty);
        }

        public static string[] Validate<TModel>(
            this IValidator<TModel> validator,
            TModel model,
            string path)
        {
            return validator.ExecuteAsync(
                new ValidateRequest<TModel>(model, path))
                .Result;
        }

        public static string[] Build<TModel>(
            this IValidationBuilder<TModel> builder,
            TModel model
            )
        {
            return builder.Build(model, string.Empty);
        }

        public static string[] Build<TModel>(
            this IValidationBuilder<TModel> builder,
            TModel model,
            string path
            )
        {
            var state = new ValidationBuildState();
            builder.Build(state, model, path);

            return state.Errors.ToArray();
        }

        public static void Build<TModel, TProperty>(
            this IValidationBuilder<TProperty> builder,
            ValidationBuildState state,
            TModel model,
            Expression<Func<TModel, TProperty>> propertyExpression,
            string path)
        {
            var subPath = ConcatPath(path, propertyExpression);
            var subModel = propertyExpression.Compile()(model);

            builder.Build(state, subModel, subPath);
        }

        public static void BuildEach<TParentModel, TModel>(
            this IValidationBuilder<TModel> builder,
            ValidationBuildState state,
            TParentModel model,
            Expression<Func<TParentModel, IEnumerable<TModel>>> propertyExpression,
            string path)
        {
            var subPath = ConcatPath(path, propertyExpression);
            var subModels = propertyExpression.Compile()(model);

            var index = 0;
            foreach (var subModel in subModels)
            {
                builder.Build(state, 
                    subModel, string.Format("{0}[{1}]", subPath, index));
                index++;
            }
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