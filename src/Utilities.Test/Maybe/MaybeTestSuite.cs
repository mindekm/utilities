namespace Utilities.Test.Maybe
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoFixture;
    using Utilities;
    using Shouldly;
    using Xunit;

    public class MaybeTestSuite
    {
        private readonly Fixture fixture = new Fixture();

        [Fact]
        public void Maybe_Some_ShouldCreateSome()
        {
            Maybe.Some(fixture.Create<string>()).IsSome.ShouldBeTrue();
        }

        [Fact]
        public void Maybe_None_ShouldCreateNone()
        {
            Maybe<string> maybe = Maybe.None;

            maybe.IsNone.ShouldBeTrue();
        }

        [Fact]
        public void Maybe_IsSome_ShouldReturnTrueForSome()
        {
            var maybe = Maybe.Some(fixture.Create<string>());

            maybe.IsSome.ShouldBeTrue();
            maybe.IsNone.ShouldBeFalse();
        }

        [Fact]
        public void Maybe_IsNone_ShouldReturnTrueForNone()
        {
            Maybe<string> maybe = Maybe.None;

            maybe.IsSome.ShouldBeFalse();
            maybe.IsNone.ShouldBeTrue();
        }

        [Fact]
        public void Maybe_ToMaybe_ShouldCreateSomeFromNonNullReferenceType()
        {
            var maybe = fixture.Create<string>().ToMaybe();

            maybe.IsSome.ShouldBeTrue();
        }

        [Fact]
        public void Maybe_ToMaybe_ShouldCreateNoneFromNullReferenceType()
        {
            var maybe = ((string)null).ToMaybe();

            maybe.IsNone.ShouldBeTrue();
        }

        [Fact]
        public void Maybe_ToMaybe_ShouldCreateSomeFromNullableTypeWithValue()
        {
            int? value = 1;
            var maybe = value.ToMaybe();

            maybe.IsSome.ShouldBeTrue();
        }

        [Fact]
        public void Maybe_ToMaybe_ShouldCreateNoneFromNullableTypeWithoutValue()
        {
            int? value = default;
            var maybe = value.ToMaybe();

            maybe.IsNone.ShouldBeTrue();
        }

        [Fact]
        public void Maybe_ToNullable_ShouldCreateNullableWithValueFromSome()
        {
            Maybe.Some(1).ToNullable().HasValue.ShouldBeTrue();
        }

        [Fact]
        public void Maybe_ToNullable_ShouldCreateNullableWithoutValueFromNone()
        {
            default(Maybe<int>).ToNullable().HasValue.ShouldBeFalse();
        }

        [Fact]
        public void Maybe_Flatten_ShouldReturnSomeForInnerSome()
        {
            var inner = Maybe.Some(fixture.Create<string>());

            Maybe.Some(inner).Flatten().ShouldBe(inner);
        }

        [Fact]
        public void Maybe_Flatten_ShouldReturnNoneForInnerNone()
        {
            default(Maybe<Maybe<string>>).Flatten().ShouldBe(Maybe.None);
        }

        [Fact]
        public void Maybe_GetValues_ShouldValidateParameters()
        {
            Should.Throw<ArgumentNullException>(() => ((List<Maybe<string>>) null).GetValues().ToList());
        }

        [Fact]
        public void Maybe_GetValues_ShouldReturnValuesForSomeCases()
        {
            var list = new List<Maybe<string>> { Maybe.Some(fixture.Create<string>()), Maybe.Some(fixture.Create<string>()) };

            list.GetValues().Count().ShouldBe(2);
        }

        [Fact]
        public void Maybe_GetValues_ShouldReturnEmptyForNoneCases()
        {
            var list = new List<Maybe<string>> { Maybe.None, Maybe.None };

            list.GetValues().Count().ShouldBe(0);
        }

        [Fact]
        public void Maybe_GetValues_ShouldReturnValuesFromMixedCases()
        {
            var list = new List<Maybe<string>>
            {
                Maybe.Some("Test1"),
                Maybe.None,
                Maybe.Some("Test2"),
                Maybe.None,
                Maybe.Some(fixture.Create<string>()),
            };

            list.GetValues().Count().ShouldBe(3);
        }

        [Fact]
        public void Maybe_GetHashCode_ShouldReturnUnderlyingHashCodeForSome()
        {
            var test = fixture.Create<string>();

            Maybe.Some(test).GetHashCode().ShouldBe(test.GetHashCode());
        }

        [Fact]
        public void Maybe_GetHashCode_ShouldReturnZeroForNone()
        {
            default(Maybe<string>).GetHashCode().ShouldBe(0);
        }
    }
}