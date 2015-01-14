using System;
using System.Linq;
using Antix.Services.Validation;
using Antix.Services.Validation.Predicates;
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

            Assert.Equal("Name:not-null", validator.Validate(model).Single());
        }

        [Fact]
        public void validate_sub_model()
        {
            var validator = GetBValidator();

            var model = new B {A = new A()};

            Assert.Equal("A.Name:not-null", validator.Validate(model).Single());
        }

        static AValidator GetAValidator()
        {
            return new AValidator(
                new StandardValidationPredicates(),
                () => new ValidationRuleBuilder<A>()
                );
        }

        static BValidator GetBValidator(AValidator aValidator = null)
        {
            return new BValidator(
                new StandardValidationPredicates(),
                () => new ValidationRuleBuilder<B>(),
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
                Func<IValidationRuleBuilder<A>> getRulesBuilder) :
                    base(@is, getRulesBuilder)
            {
            }

            protected override void Validate(IValidationRuleBuilder<A> rules)
            {
                rules.For(a => a.Name)
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
                Func<IValidationRuleBuilder<B>> getRulesBuilder,
                IValidator<A> aValidator) :
                    base(@is, getRulesBuilder)
            {
                _aValidator = aValidator;
            }

            protected override void Validate(IValidationRuleBuilder<B> rules)
            {
                rules.For(b => b.A)
                    .Validate(_aValidator);
            }
        }
    }
}