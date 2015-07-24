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
        public void with_asserts_all_predicates()
        {
            var builder = GetBuilder();
            GetRule(builder)
                .For(m => m.Name)
                .Assert(_is.NotNull, _is.Email);

            var result = new ValidationRuleValidator<ModelA>(builder)
                .Validate(new ModelA
                {
                    Name = null
                });

            foreach (var error in result)
                Console.WriteLine(error);

            Assert.Equal(new[] {"Name:not-null", "Name:email"}, result);
        }

        [Fact]
        public void with_method()
        {
            var builder = GetBuilder();
            var rule = GetRule(builder);
            rule
                .For(m => m)
                .Assert(with_method_build);
            rule
                .For(m => m.Size)
                .Assert(with_method_build_size);

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
        public void with_method_stops_on_first_assert_fail_but_runs_next_rule()
        {
            var builder = GetBuilder();
            var rule = GetRule(builder);
            rule
                .Assert(with_method_build)
                .For(m => m.Name)
                .Assert(_is.NotEmpty);

            rule
                .For(m => m.B)
                .Assert(_is.NotNull);

            var result = new ValidationRuleValidator<ModelA>(builder)
                .Validate(new ModelA
                {
                    Name = string.Empty,
                    Size = 0
                });

            foreach (var error in result)
                Console.WriteLine(error);

            Assert.Equal(new[] { "Name:not-empty", "B:not-null" }, result);
        }

        void with_method_build(
            IValidationRule<ModelA> rule)
        {
            rule
                .For(m => m.Name)
                .Assert(_is.NotNull)
                .Assert(_is.NotEmpty);
        }

        void with_method_build_size(
            IValidationRule<int> rule)
        {
            rule.Assert(_is.Min(1));
        }

        [Fact]
        public void with_method_predicate()
        {
            var builder = GetBuilder();
            var rule = GetRule(builder);
            rule
                .For(m => m.Name)
                .Assert("a-function", with_method_predicate_name);

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

        static bool with_method_predicate_name(string model)
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

        [Fact]
        public void validate_accross_graph_hierarchy()
        {
            var builder = GetBuilder();
            var rule = GetRule(builder);
            rule
                .When(b =>
                    b.For(m => m.Name)
                        .Assert(_is.NotNullOrEmpty)
                )
                .Assert(b =>
                    b.For(m => m.B)
                        .Assert(_is.NotNull)
                );

            var result = new ValidationRuleValidator<ModelA>(builder)
                .Validate(new ModelA
                {
                    Name = string.Empty
                });

            Assert.Equal(new string[] {}, result);

            result = new ValidationRuleValidator<ModelA>(builder)
                .Validate(new ModelA
                {
                    Name = "NOT EMPTY",
                    B = null
                });

            Assert.Equal(new[] {"B:not-null"}, result);
        }

        [Fact]
        public void with_else()
        {
          
        }
    }
}