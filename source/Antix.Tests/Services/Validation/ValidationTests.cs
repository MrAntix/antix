using System;
using System.Linq;
using Antix.Services.Validation;
using Antix.Services.Validation.Predicates;
using Xunit;

namespace Antix.Tests.Services.Validation
{
    public class ValidationTests
    {
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

        static IValidationRuleBuilder<ModelA> GetService()
        {
            return new ValidationRuleBuilder<ModelA>();
        } 

        [Fact]
        public void fail_model_is_null_in_collection()
        {
            var builder = GetService();
            builder
                .ForEach(m => m.B.Cs)
                .Assert(_is.NotNull);

            var result = builder.Build(new ModelA
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
            var builder = GetService();
            builder
                .ForEach(m => m.B.Cs)
                .When(_is.NotNull)
                .For(m => m.Name, b => b
                    .Assert(_is.NotNull)
                );

            var result = builder.Build(new ModelA
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
            var builder = GetService();
            builder
                .Assert(_is.NotNull);

            var result = builder.Build(null);

            foreach (var error in result)
                Console.WriteLine(error);

            Assert.Equal(":not-null",
                Assert.Single(result)
                );
        }

        [Fact]
        public void fail_model_name_is_null()
        {
            var builder = GetService();
            builder
                .For(m => m.Name)
                .Assert(_is.NotNull);

            var result = builder.Build(new ModelA
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
            var builder = GetService();
            builder
                .For(m => m.Name)
                .Assert(_is.NotNull)
                .Assert(_is.NotEmpty);

            var result = builder.Build(new ModelA
            {
                Name = string.Empty
            });

            foreach (var error in result)
                Console.WriteLine(error);

            Assert.Equal("Name:string-not-empty",
                Assert.Single(result)
                );
        }

        [Fact]
        public void fail_model_name_is_empty_and_size_is_0()
        {
            var builder = GetService();
            builder
                .For(m => m.Name)
                .Assert(_is.NotNull)
                .Assert(_is.NotEmpty);
            builder
                .For(m => m.Size)
                .Assert(_is.Min(1));

            var result = builder.Build(new ModelA
            {
                Name = string.Empty,
                Size = 0
            });

            foreach (var error in result)
                Console.WriteLine(error);

            Assert.True(result.ElementAt(0)
                .StartsWith("Name:string-not-empty"));
            Assert.True(result.ElementAt(1)
                .StartsWith("Size:number-min"));
        }

        [Fact]
        public void validate_with_method()
        {
            var builder = GetService();
            builder
                .For(m => m.Name, validate_with_method_name)
                .For(m => m.Size, validate_with_method_size);

            var result = builder.Build(new ModelA
            {
                Name = string.Empty,
                Size = 0
            });

            foreach (var error in result)
                Console.WriteLine(error);

            Assert.True(result.ElementAt(0)
                .StartsWith("Name:string-not-empty"));
            Assert.True(result.ElementAt(1)
                .StartsWith("Size:number-min"));
        }

        void validate_with_method_name(
            IValidationRuleBuilder<string> ruleBuilder)
        {
            ruleBuilder
                .Assert(_is.NotNull)
                .Assert(_is.NotEmpty);
        }

        void validate_with_method_size(
            IValidationRuleBuilder<int> ruleBuilder)
        {
            ruleBuilder.Assert(_is.Min(1));
        }

        [Fact]
        public void validate_with_method_predicate()
        {
            var builder = GetService();
            builder
                .For(m => m.Name)
                .Assert("a-function", validate_with_method_predicate_name);

            var result = builder.Build(new ModelA
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
    }
}