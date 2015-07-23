using System;
using System.Linq;
using Antix.Services.Validation;
using Antix.Services.Validation.Predicates;
using Antix.Services.Validation.Rules;
using Xunit;

namespace Antix.Tests.Services.Validation
{
    public class ValidatorBaseTest
    {
        [Fact]
        public void validate_model()
        {
            var validator = GetAValidator();

            var model = new A();

            Assert.Equal("Name:not-null", validator.ValidateAsync(model).Result.Single());
        }

        [Fact]
        public void validate_sub_model()
        {
            var validator = GetBValidator();

            var model = new B {A = new A()};

            Assert.Equal("A.Name:not-null", validator.ValidateAsync(model).Result.Single());
        }

        static AValidator GetAValidator()
        {
            return new AValidator(
                new StandardValidationPredicates(),
                () => new ValidationRules<A>()
                );
        }

        static BValidator GetBValidator(AValidator aValidator = null)
        {
            return new BValidator(
                new StandardValidationPredicates(),
                () => new ValidationRules<B>(),
                aValidator ?? GetAValidator()
                );
        }

        class A
        {
            public string Name { get; set; }
        }

        class AValidator : ValidatorBase<A>
        {
            public AValidator(
                IStandardValidationPredicates @is,
                Func<IValidationRules<A>> getRules) :
                    base(@is, getRules)
            {
            }

            protected override void Validate(IValidationRule<A> validationRules)
            {
                validationRules.For(a => a.Name)
                    .Assert(Is.NotNull);
            }
        }

        class B
        {
            public A A { get; set; }
        }

        class BValidator : ValidatorBase<B>
        {
            readonly IValidator<A> _aValidator;

            public BValidator(
                IStandardValidationPredicates @is,
                Func<IValidationRules<B>> getRules,
                IValidator<A> aValidator) :
                    base(@is, getRules)
            {
                _aValidator = aValidator;
            }

            protected override void Validate(IValidationRule<B> validationRules)
            {
                validationRules.For(b => b.A)
                    .Assert(_aValidator);
            }
        }
    }
}