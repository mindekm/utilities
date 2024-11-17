namespace Utilities.Test.Either;

using System;
using Either = Utilities.Either;

public class ValueRetrieval
{
    [Test]
    public async ValueTask Either_GetLeft_ShouldReturnValueForLeftCase()
    {
        Either<string, string> either = Either.Left("left value");

        await Assert.That(either.UnwrapLeft()).IsEqualTo("left value");
    }

    [Test]
    public async ValueTask Either_GetLeft_ShouldThrowForRightCase()
    {
        Either<string, string> either = Either.Right("right value");

        await Assert.That(() => either.UnwrapLeft()).Throws<InvalidOperationException>();
    }

    [Test]
    public async ValueTask Either_GetRight_ShouldReturnValueForRightCase()
    {
        Either<string, string> either = Either.Right("right value");

        await Assert.That(either.UnwrapRight()).IsEqualTo("right value");
    }

    [Test]
    public async ValueTask Either_GetRight_ShouldThrowForLeftCase()
    {
        Either<string, string> either = Either.Left("left value");

        await Assert.That(() => either.UnwrapRight()).Throws<InvalidOperationException>();
    }

    [Test]
    public async ValueTask Either_TryGetLeft_ShouldReturnTrueAndValueForLeftCase()
    {
        Either<string, string> either = Either.Left("left value");

        using (Assert.Multiple())
        {
            await Assert.That(either.TryUnwrapLeft(out var result)).IsTrue();
            await Assert.That(result).IsEqualTo("left value");
        }
    }

    [Test]
    public async ValueTask Either_TryGetLeft_ShouldReturnFalseAndDefaultForRightCase()
    {
        Either<string, string> either = Either.Right("right value");

        using (Assert.Multiple())
        {
            await Assert.That(either.TryUnwrapLeft(out var result)).IsFalse();
            await Assert.That(result).IsDefault();
        }
    }

    [Test]
    public async ValueTask Either_TryGetRight_ShouldReturnTrueAndValueForRightCase()
    {
        Either<string, string> either = Either.Right("right value");

        using (Assert.Multiple())
        {
            await Assert.That(either.TryUnwrapRight(out var result)).IsTrue();
            await Assert.That(result).IsEqualTo("right value");
        }
    }

    [Test]
    public async ValueTask Either_TryGetRight_ShouldReturnFalseAndDefaultForLeftCase()
    {
        Either<string, string> either = Either.Left("left value");

        using (Assert.Multiple())
        {
            await Assert.That(either.TryUnwrapRight(out var result)).IsFalse();
            await Assert.That(result).IsDefault();
        }
    }

    [Test]
    public async ValueTask Either_GetLeftOrDefault_ShouldReturnValueForLeftCase()
    {
        Either<string, string> either = Either.Left("left value");

        await Assert.That(either.UnwrapLeftOrDefault()).IsEqualTo("left value");
    }

    [Test]
    public async ValueTask Either_GetLeftOrDefault_ShouldReturnDefaultForRightCase()
    {
        Either<string, string> either = Either.Right("right value");

        await Assert.That(either.UnwrapLeftOrDefault()).IsDefault();
    }

    [Test]
    public async ValueTask Either_GetRightOrDefault_ShouldReturnValueForRightCase()
    {
        Either<string, string> either = Either.Right("right value");

        await Assert.That(either.UnwrapRightOrDefault()).IsEqualTo("right value");
    }

    [Test]
    public async ValueTask Either_GetRightOrDefault_ShouldReturnDefaultForLeftCase()
    {
        Either<string, string> either = Either.Left("left value");

        await Assert.That(either.UnwrapRightOrDefault()).IsDefault();
    }

    [Test]
    public async ValueTask Either_Match_ShouldMatchOnLeftCase()
    {
        Either<string, string> either = Either.Left("left value");

        await Assert.That(either.Match(left => left, right => right)).IsEqualTo("left value");
    }

    [Test]
    public async ValueTask Either_Match_ShouldMatchOnRightCase()
    {
        Either<string, string> either = Either.Right("right value");

        await Assert.That(either.Match(left => left, right => right)).IsEqualTo("right value");
    }
}
