using System;
using System.Linq;
using Antix.Services.Validation;
using Antix.Services.Validation.Predicates;
using Antix.Services.Validation.Rules;
using Xunit;

namespace Antix.Tests.Services.Validation
{
    public class ValidationTests
    {
        #region setup

        class ModelA
        {
            public string Name { get; set; }
            public int Size { get; set; }
            public ModelB B { get; set; }
        }

        class ModelB
        {
            public ModelC[] Cs { get; set; }
        }

        class ModelC
        {
            public string Name { get; set; }
        }

        readonly IStandardValidationPredicates _is
            = new StandardValidationPredicates();

        IValidationRule<ModelA> GetRule(IValidationRuleBuilder<ModelA> builder)
        {
            return new ValidationRule<ModelA>(builder);
        }

        IValidationRuleBuilder<ModelA> GetBuilder()
        {
            return new ValidationRuleBuilder<ModelA>();
        }

#endregion

        [Fact]
        public void fail_model_is_null_in_collection()
        {
            var builder = GetBuilder();
            var rule = GetRule(builder);
            rule
                .ForEach(m => m.B.Cs)
                .Assert(_is.NotNull);

            var result = new ValidationRuleValidator<ModelA>(builder)
                .Validate(new ModelA
                {
                    B = new ModelB
                    {
                        Cs = new[]
                        {
                            new ModelC(),
                            null
                        }
                    }
                });

            foreach (var error in result)
                Console.WriteLine(error);

            Assert.Equal("Cs.B[1]:not-null",
                Assert.Single(result)
                );
        }

        [Fact]
        public void fail_model_name_is_null_in_collection_allowing_for_null_model()
        {
            var builder = GetBuilder();
            var rule = GetRule(builder);
            rule
                .ForEach(m => m.B.Cs)
                .When(_is.NotNull)
                .For(m => m.Name)
                .Assert(_is.NotNull);

            var result = new ValidationRuleValidator<ModelA>(builder)
                .Validate(new ModelA
                {
                    B = new ModelB
                    {
                        Cs = new[]
                        {
                            null,
                            new ModelC {Name = "Name"},
                            new ModelC()
                        }
                    }
                });

            foreach (var error in result)
                Console.WriteLine(error);

            Assert.Equal("Cs.B[2].Name:not-null",
                Assert.Single(result)
                );
        }

        [Fact]
        public void fail_model_is_null()
        {
            var builder = GetBuilder();
            GetRule(builder)
                .Assert(_is.NotNull);

            var result = new ValidationRuleValidator<ModelA>(builder)
                .Validate(null);

            foreach (var error in result)
                Console.WriteLine(error);

            Assert.Equal(":not-null",
                Assert.Single(result)
                );
        }

        [Fact]
        public void fail_model_name_is_null()
        {
            var builder = GetBuilder();
            GetRule(builder)
                .For(m => m.Name)
                .Assert(_is.NotNull);

            var result = new ValidationRuleValidator<ModelA>(builder)
                .Validate(new ModelA
                {
                    Name = null
                });

            foreach (var error in result)
                Console.WriteLine(error);

            Assert.Equal("Name:not-null",
                Assert.Single(result)
                );
        }

        [Fact]
        public void fail_model_name_is_empty()
        {
            var builder = GetBuilder();
            GetRule(builder)
                .For(m => m.Name)
                .Assert(_is.NotNull)
                .Assert(_is.NotEmpty);

            var result = new ValidationRuleValidator<ModelA>(builder)
                .Validate(new ModelA
                {
                    Name = string.Empty
                });

            foreach (var error in result)
                Console.WriteLine(error);

            Assert.Equal("Name:not-empty",
                Assert.Single(result)
                );
        }

        [Fact]
        public void fail_model_name_is_empty_and_size_is_0()
        {
            var builder = GetBuilder();
            var rule = GetRule(builder);
            rule
                .For(m => m.Name)
                .Assert(_is.NotNull)
                .Assert(_is.NotEmpty);
            rule
                .For(m => m.Size)
                .Assert(_is.Min(1));

            var result = new ValidationRuleValidator<ModelA>(builder)
                .Validate(new ModelA
                {
                    Name = string.Empty,
                    Size = 0
                });

            foreach (var error in result)
                Console.WriteLine(error);

            Assert.True(result.ElementAt(0)
                .StartsWith("Name:not-empty"));
            Assert.True(result.ElementAt(1)
                .StartsWith("Size:min"));
        }

        [Fact]
        public void validate_with_method()
        {
            var builder = GetBuilder();
            var rule = GetRule(builder);
            rule
                .For(m => m.Name)
                .Assert(validate_with_method_name);
            rule
                .For(m => m.Size)
                .Assert(validate_with_method_size);

            var result = new ValidationRuleValidator<ModelA>(builder)
                .Validate(new ModelA
                {
                    Name = string.Empty,
                    Size = 0
                });

            foreach (var error in result)
                Console.WriteLine(error);

            Assert.True(result.ElementAt(0)
                .StartsWith("Name:not-empty"));
            Assert.True(result.ElementAt(1)
                .StartsWith("Size:min"));
        }

        void validate_with_method_name(
            IValidationRule<string> rule)
        {
            rule
                .Assert(_is.NotNull)
                .Assert(_is.NotEmpty);
        }

        void validate_with_method_size(
            IValidationRule<int> rule)
        {
            rule.Assert(_is.Min(1));
        }

        [Fact]
        public void validate_with_method_predicate()
        {
            var builder = GetBuilder();
            var rule = GetRule(builder);
            rule
                .For(m => m.Name)
                .Assert("a-function", validate_with_method_predicate_name);

            var result = new ValidationRuleValidator<ModelA>(builder)
                .Validate(new ModelA
                {
                    Name = string.Empty,
                    Size = 0
                });

            foreach (var error in result)
                Console.WriteLine(error);

            Assert.True(result.ElementAt(0)
                .StartsWith("Name:a-function"));
        }

        static bool validate_with_method_predicate_name(string arg)
        {
            return false;
        }

        [Fact]
        public void when_first_assert_fails_stop()
        {
            var builder = GetBuilder();
            var rule = GetRule(builder);
            rule
                .For(m => m.Name)
                .Assert(_is.NotEmpty)
                .Assert(_is.Email);

            var result = new ValidationRuleValidator<ModelA>(builder)
                .Validate(new ModelA
                {
                    Name = "NOT EMPTY"
                });

            Assert.Equal(new[] {"Name:email"}, result);

            result = new ValidationRuleValidator<ModelA>(builder)
                .Validate(new ModelA
                {
                    Name = string.Empty
                });

            Assert.Equal(new[] {"Name:not-empty"}, result);
        }

        [Fact]
        public void if_not_empty_then_is_email()
        {
            var builder = GetBuilder();
            var rule = GetRule(builder);
            rule
                .For(m => m.Name)
                .When(_is.NotEmpty)
                .Assert(_is.Email);

            var result = new ValidationRuleValidator<ModelA>(builder)
                .Validate(new ModelA
                {
                    Name = "NOT EMPTY"
                });

            Assert.Equal(new[] {"Name:email"}, result);

            result = new ValidationRuleValidator<ModelA>(builder)
                .Validate(new ModelA
                {
                    Name = string.Empty
                });

            Assert.Equal(new string[] {}, result);
        }

        //        [Fact]
        //        public void when_3_chars_xxx_else_4_chars_yyyy()
        //        {
        //            var builder = GetService2();
        //            builder
        //                .For(m => m.Name)
        //                .When(_is.Length(3, 3))
        //                .Assert(_is.Email)

        //                .Else()
        //                .When(_is.Length(4, 4))
        //                .Assert(_is.Equals("xxxx"))
        //                .Else()
        //                .Assert(_is.Empty);
        //;

        //            var result = new RuleValidator<ModelA>(builder)
        //                .Validate(new ModelA
        //                {
        //                    Name = "AAA"
        //                });

        //            Assert.Equal(new[] { "Name:email" }, result);

        //            result = new RuleValidator<ModelA>(builder)
        //                .Validate(new ModelA
        //                {
        //                    Name = "AAAA"
        //                });

        //            Assert.Equal(new [] { "Name:equal" }, result);
        //        }

        //[Fact]
        //public void reuse_builder_in_then_fail_on_size_when_name_not_empty()
        //{
        //    var builder = GetService2();
        //    builder
        //        .For(m => m.Name)
        //        .Assert(_is.NotEmpty)
        //        .Then()
        //            .Parent()
        //            .For(m => m.Size)
        //                .Assert(_is.Min(1)));

        //    var result = new RuleValidator<ModelA>(builder)
        //        .Validate(new ModelA
        //    {
        //        Name = "NOT EMPTY",
        //        Size = 0
        //    });

        //    Assert.Equal(new[] {"Size:min"}, result);

        //    result = builder.Build(new ModelA
        //    {
        //        Name = string.Empty,
        //        Size = 0
        //    });

        //    Assert.Equal(new[] {"Name:not-empty"}, result);
        //}
    }
}