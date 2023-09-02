namespace Utilities.Test.Maybe;

using System;
using Utilities;
using Shouldly;
using Xunit;

public class ValueRetrieval
{
    [Fact]
    public void Maybe_TryUnwrap_ShouldReturnTrueForSome()
    {
        const string Expected = "Test";

        Maybe.Some(Expected).TryUnwrap(out var result).ShouldBeTrue();
        result.ShouldBe(Expected);
    }

    [Fact]
    public void Maybe_TryUnwrap_ShouldReturnFalseForNone()
    {
        default(Maybe<string>).TryUnwrap(out var result).ShouldBeFalse();
        result.ShouldBe(default);
    }

    [Fact]
    public void Maybe_Unwrap_ShouldThrowForNone()
    {
        Should.Throw<InvalidOperationException>(() => default(Maybe<string>).Unwrap());
    }

    [Fact]
    public void Maybe_Unwrap_ShouldReturnValueForSome()
    {
        const string expected = "Test";

        Maybe.Some(expected).Unwrap().ShouldBe(expected);
    }

    [Fact]
    public void Maybe_UnwrapOrDefault_ShouldReturnValueForSome()
    {
        const string expected = "Test";

        Maybe.Some(expected).UnwrapOrDefault().ShouldBe(expected);
    }

    [Fact]
    public void Maybe_UnwrapOrDefault_ShouldReturnDefaultForNone()
    {
        default(Maybe<string>).UnwrapOrDefault().ShouldBe(default);
    }

    [Fact]
    public void Maybe_UnwrapOrElse_ShouldReturnValueForSome()
    {
        var some = Maybe.Some(10);

        var result = some.UnwrapOrElse(() => 20);

        result.ShouldBe(10);
    }

    [Fact]
    public void Maybe_UnwrapOrElse_ShouldUseValueFactoryForNone()
    {
        Maybe<int> none = Maybe.None;

        var result = none.UnwrapOrElse(() => 20);

        result.ShouldBe(20);
    }

    [Fact]
    public void Maybe_UnwrapOr_ShouldReturnValueOnSome()
    {
        var some = Maybe.Some(10);

        var result = some.UnwrapOr(20);

        result.ShouldBe(10);
    }

    [Fact]
    public void Maybe_UnwrapOr_ShouldUseAlternativeOnNone()
    {
        Maybe<int> none = Maybe.None;

        var result = none.UnwrapOr(20);

        result.ShouldBe(20);
    }
}
