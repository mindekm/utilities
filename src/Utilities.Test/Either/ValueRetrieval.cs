namespace Utilities.Test.Either;

using System;
using Shouldly;
using Xunit;
using Either = Utilities.Either;

public class ValueRetrieval
{
    [Fact]
    public void Either_GetLeft_ShouldReturnValueForLeftCase()
    {
        Either<string, string> either = Either.Left("left value");
        either.UnwrapLeft().ShouldBe("left value");
    }

    [Fact]
    public void Either_GetLeft_ShouldThrowForRightCase()
    {
        Either<string, string> either = Either.Right("right value");
        Should.Throw<InvalidOperationException>(() => either.UnwrapLeft());
    }

    [Fact]
    public void Either_GetRight_ShouldReturnValueForRightCase()
    {
        Either<string, string> either = Either.Right("right value");
        either.UnwrapRight().ShouldBe("right value");
    }

    [Fact]
    public void Either_GetRight_ShouldThrowForLeftCase()
    {
        Either<string, string> either = Either.Left("left value");
        Should.Throw<InvalidOperationException>(() => either.UnwrapRight());
    }

    [Fact]
    public void Either_TryGetLeft_ShouldReturnTrueAndValueForLeftCase()
    {
        Either<string, string> either = Either.Left("left value");

        either.TryUnwrapLeft(out var result).ShouldBeTrue();
        result.ShouldBe("left value");
    }

    [Fact]
    public void Either_TryGetLeft_ShouldReturnFalseAndDefaultForRightCase()
    {
        Either<string, string> either = Either.Right("right value");

        either.TryUnwrapLeft(out var result).ShouldBeFalse();
        result.ShouldBe(default);
    }

    [Fact]
    public void Either_TryGetRight_ShouldReturnTrueAndValueForRightCase()
    {
        Either<string, string> either = Either.Right("right value");

        either.TryUnwrapRight(out var result).ShouldBeTrue();
        result.ShouldBe("right value");
    }

    [Fact]
    public void Either_TryGetRight_ShouldReturnFalseAndDefaultForLeftCase()
    {
        Either<string, string> either = Either.Left("left value");

        either.TryUnwrapRight(out var result).ShouldBeFalse();
        result.ShouldBe(default);
    }

    [Fact]
    public void Either_GetLeftOrDefault_ShouldReturnValueForLeftCase()
    {
        Either<string, string> either = Either.Left("left value");
        either.UnwrapLeftOrDefault().ShouldBe("left value");
    }

    [Fact]
    public void Either_GetLeftOrDefault_ShouldReturnDefaultForRightCase()
    {
        Either<string, string> either = Either.Right("right value");
        either.UnwrapLeftOrDefault().ShouldBe(default);
    }

    [Fact]
    public void Either_GetRightOrDefault_ShouldReturnValueForRightCase()
    {
        Either<string, string> either = Either.Right("right value");
        either.UnwrapRightOrDefault().ShouldBe("right value");
    }

    [Fact]
    public void Either_GetRightOrDefault_ShouldReturnDefaultForLeftCase()
    {
        Either<string, string> either = Either.Left("left value");
        either.UnwrapRightOrDefault().ShouldBe(default);
    }

    [Fact]
    public void Either_Match_ShouldMatchOnLeftCase()
    {
        Either<string, string> either = Either.Left("left value");
        
        either.Match(left => left, right => right).ShouldBe("left value");
    }

    [Fact]
    public void Either_Match_ShouldMatchOnRightCase()
    {
        Either<string, string> either = Either.Right("right value");
        
        either.Match(left => left, right => right).ShouldBe("right value");
    }
}