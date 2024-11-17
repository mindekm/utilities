namespace Utilities.Test.Maybe;

using System;
using Utilities;

public class ValueRetrieval
{
    [Test]
    public async ValueTask Maybe_TryUnwrap_ShouldReturnTrueForSome()
    {
        const string Expected = "Test";

        using (Assert.Multiple())
        {
            await Assert.That(Maybe.Some(Expected).TryUnwrap(out var result)).IsTrue();
            await Assert.That(result).IsEqualTo(Expected);
        }
    }

    [Test]
    public async ValueTask Maybe_TryUnwrap_ShouldReturnFalseForNone()
    {
        using (Assert.Multiple())
        {
            await Assert.That(default(Maybe<string>).TryUnwrap(out var result)).IsFalse();
            await Assert.That(result).IsDefault();
        }
    }

    [Test]
    public async ValueTask Maybe_Unwrap_ShouldThrowForNone()
    {
        await Assert.That(() => default(Maybe<string>).Unwrap()).Throws<InvalidOperationException>();
    }

    [Test]
    public async ValueTask Maybe_Unwrap_ShouldReturnValueForSome()
    {
        const string expected = "Test";

        await Assert.That(Maybe.Some(expected).Unwrap()).IsEqualTo(expected);
    }

    [Test]
    public async ValueTask Maybe_UnwrapOrDefault_ShouldReturnValueForSome()
    {
        const string expected = "Test";

        await Assert.That(Maybe.Some(expected).UnwrapOrDefault()).IsEqualTo(expected);
    }

    [Test]
    public async ValueTask Maybe_UnwrapOrDefault_ShouldReturnDefaultForNone()
    {
        await Assert.That(default(Maybe<string>).UnwrapOrDefault()).IsDefault();
    }

    [Test]
    public async ValueTask Maybe_UnwrapOrElse_ShouldReturnValueForSome()
    {
        var some = Maybe.Some(10);

        var result = some.UnwrapOrElse(() => 20);

        await Assert.That(result).IsEqualTo(10);
    }

    [Test]
    public async ValueTask Maybe_UnwrapOrElse_ShouldUseValueFactoryForNone()
    {
        Maybe<int> none = Maybe.None;

        var result = none.UnwrapOrElse(() => 20);

        await Assert.That(result).IsEqualTo(20);
    }

    [Test]
    public async ValueTask Maybe_UnwrapOr_ShouldReturnValueOnSome()
    {
        var some = Maybe.Some(10);

        var result = some.UnwrapOr(20);

        await Assert.That(result).IsEqualTo(10);
    }

    [Test]
    public async ValueTask Maybe_UnwrapOr_ShouldUseAlternativeOnNone()
    {
        Maybe<int> none = Maybe.None;

        var result = none.UnwrapOr(20);

        await Assert.That(result).IsEqualTo(20);
    }
}
