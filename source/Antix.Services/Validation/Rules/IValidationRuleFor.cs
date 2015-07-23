using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Antix.Services.Validation.Rules
{
    public interface IValidationRuleFor<TModel>
    {
        IValidationRule<TProperty> For<TProperty>(
            Expression<Func<TModel, TProperty>> propertyExpression);

        IValidationRule<TProperty> ForEach<TProperty>(
            Expression<Func<TModel, IEnumerable<TProperty>>> propertyExpression);
    }
}