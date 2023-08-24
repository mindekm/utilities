namespace Utilities.Test.Maybe;

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

    [Fact]
    public void Maybe_Expect_ShouldThrowWithTheProvidedMessageOnNone()
    {
        Maybe<string> maybe = Maybe.None;
        var message = fixture.Create<string>();

        var exception = Should.Throw<InvalidOperationException>(() => maybe.Expect(message));
        exception.Message.ShouldBe(message);
    }

    [Fact]
    public void Maybe_Expect_ShouldReturnValueOnSome()
    {
        var testValue = fixture.Create<string>();
        var maybe = Maybe.Some(testValue);

        var value = Should.NotThrow(() => maybe.Expect(fixture.Create<string>()));
        value.ShouldBe(testValue);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(4)]
    [InlineData(6)]
    public void Maybe_IsSomeAnd_ShouldReturnTrueWhenSomeAndPredicateIsTrue(int input)
    {
        var maybe = Maybe.Some(input);

        var result = maybe.IsSomeAnd(v => v % 2 == 0);

        result.ShouldBeTrue();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    public void Maybe_IsSomeAnd_ShouldReturnFalseWhenSomeAndPredicateIsFalse(int input)
    {
        var maybe = Maybe.Some(input);

        var result = maybe.IsSomeAnd(v => v % 2 == 0);

        result.ShouldBeFalse();
    }

    [Fact]
    public void Maybe_IsSomeAnd_ShouldReturnFalseWhenNone()
    {
        Maybe<int> maybe = Maybe.None;

        var result = maybe.IsSomeAnd(v => v % 2 == 0);

        result.ShouldBeFalse();
    }

    [Fact]
    public void Maybe_Bind_ShouldExecuteBinderWhenInitialIsSome()
    {
        var value = fixture.Create<int>();
        var inital = Maybe.Some(value);

        var result = inital.Bind(v => Maybe.Some(v + 1));

        result.IsSome.ShouldBeTrue();
        result.Unwrap().ShouldBe(value + 1);
    }

    [Fact]
    public void Maybe_Bind_ShouldReturnNoneWhenInitialIsNone()
    {
        Maybe<int> initial = Maybe.None;

        var result = initial.Bind(v => Maybe.Some(v + 1));

        result.IsNone.ShouldBeTrue();
    }

    [Theory]
    [InlineData(2)]
    [InlineData(4)]
    [InlineData(6)]
    public void Maybe_Filter_ShouldReturnSomeWhenInitialIsSomeAndPredicateIsTrue(int input)
    {
        var initial = Maybe.Some(input);

        var result = initial.Filter(v => v % 2 == 0);

        result.IsSome.ShouldBeTrue();
        result.Unwrap().ShouldBe(input);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    public void Maybe_Filter_ShouldReturnNoneWhenInitialIsSomeAndPredicateIsFalse(int input)
    {
        var initial = Maybe.Some(input);

        var result = initial.Filter(v => v % 2 == 0);

        result.IsNone.ShouldBeTrue();
    }

    [Fact]
    public void Maybe_Filter_ShouldReturnNoneWhenInitialIsNone()
    {
        Maybe<string> initial = Maybe.None;

        var result = initial.Filter(_ => true);

        result.IsNone.ShouldBeTrue();
    }

    [Fact]
    public void Maybe_Map_ShouldExecuteMapperWhenInitialIsSome()
    {
        var value = fixture.Create<int>();
        var inital = Maybe.Some(value);

        var result = inital.Map(v => v + 1);

        result.IsSome.ShouldBeTrue();
        result.Unwrap().ShouldBe(value + 1);
    }

    [Fact]
    public void Maybe_Map_ShouldReturnNoneWhenInitialIsNone()
    {
        Maybe<int> initial = Maybe.None;

        var result = initial.Map(v => v + 1);

        result.IsNone.ShouldBeTrue();
    }

    [Fact]
    public void Maybe_And_ShouldReturnOtherWhenInitialIsSome()
    {
        var first = Maybe.Some(1);
        var second = Maybe.Some(2);

        var result = first.And(second);

        result.IsSome.ShouldBeTrue();
        result.ShouldBe(second);
    }

    [Fact]
    public void Maybe_And_ShouldReturnNoneWhenInitialIsNone()
    {
        Maybe<int> first = Maybe.None;
        var second = Maybe.Some(2);

        var result = first.And(second);

        result.IsNone.ShouldBeTrue();
    }

    [Fact]
    public void Maybe_Or_ShouldReturnSelfWhenSome()
    {
        var first = Maybe.Some(1);
        var second = Maybe.Some(2);

        var result = first.Or(second);

        result.IsSome.ShouldBeTrue();
        result.ShouldBe(first);
    }

    [Fact]
    public void Maybe_Or_ShouldReturnOtherWhenNone()
    {
        Maybe<int> first = Maybe.None;
        var second = Maybe.Some(2);

        var result = first.Or(second);

        result.IsSome.ShouldBeTrue();
        result.ShouldBe(second);
    }

    [Fact]
    public void Maybe_OrElse_ShouldReturnSelfWhenSome()
    {
        var first = Maybe.Some(1);
        var second = Maybe.Some(2);

        var result = first.OrElse(() => second);

        result.IsSome.ShouldBeTrue();
        result.ShouldBe(first);
    }

    [Fact]
    public void Maybe_OrElse_ShouldExecuteValueFactoryWhenNone()
    {
        Maybe<int> first = Maybe.None;
        var second = Maybe.Some(2);

        var result = first.OrElse(() => second);

        result.IsSome.ShouldBeTrue();
        result.ShouldBe(second);
    }

    [Fact]
    public void Maybe_Xor_ShouldReturnSelfWhenSomeAndOtherIsNone()
    {
        var first = Maybe.Some(1);
        Maybe<int> second = Maybe.None;

        var result = first.Xor(second);

        result.IsSome.ShouldBeTrue();
        result.ShouldBe(first);
    }

    [Fact]
    public void Maybe_Xor_ShouldReturnOtherWhenNoneAndOtherIsSome()
    {
        Maybe<int> first = Maybe.None;
        var second = Maybe.Some(2);

        var result = first.Xor(second);

        result.IsSome.ShouldBeTrue();
        result.ShouldBe(second);
    }

    [Fact]
    public void Maybe_Xor_ShouldReturnNoneWhenNoneAndOtherIsNone()
    {
        Maybe<int> first = Maybe.None;
        Maybe<int> second = Maybe.None;

        var result = first.Xor(second);

        result.IsNone.ShouldBeTrue();
    }
}
