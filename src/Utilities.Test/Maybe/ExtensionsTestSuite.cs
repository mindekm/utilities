namespace Utilities;

public sealed class ExtensionsTestSuite
{
    [Test]
    public async ValueTask MaybeExtensions_Add_ShouldAddSomeValueToCollection()
    {
        var list = new List<string>();
        var some = Maybe.Some("test");

        list.Add(some);

        using (Assert.Multiple())
        {
            await Assert.That(list.Count).IsEqualTo(1);
            await Assert.That(list[0]).IsEqualTo(some.Unwrap());
        }
    }

    [Test]
    public async ValueTask MaybeExtensions_Add_ShouldNotAddNoneValueToCollection()
    {
        var list = new List<string>();
        Maybe<string> none = Maybe.None;

        list.Add(none);

        await Assert.That(list.Count).IsEqualTo(0);
    }

    [Test]
    [Arguments(null)]
    public async ValueTask MaybeExtensions_ToMaybe_Null_ShouldReturnNoneWhenInputIsNull(string input)
    {
        var result = input.ToMaybe(NoneWhen.Null);

        await Assert.That(result.IsNone).IsTrue();
    }

    [Test]
    [Arguments("")]
    [Arguments(" ")]
    [Arguments("test")]
    public async ValueTask MaybeExtensions_ToMaybe_Null_ShouldReturnSomeWhenInputIsNotNull(string input)
    {
        var result = input.ToMaybe(NoneWhen.Null);

        await Assert.That(result.IsSome).IsTrue();
    }

    [Test]
    [Arguments(null)]
    [Arguments("")]
    public async ValueTask MaybeExtensions_ToMaybe_NullOrEmpty_ShouldReturnNoneWhenInputIsNullOrEmpty(string input)
    {
        var result = input.ToMaybe(NoneWhen.NullOrEmpty);

        await Assert.That(result.IsNone).IsTrue();
    }

    [Test]
    [Arguments(" ")]
    [Arguments("test")]
    public async ValueTask MaybeExtensions_ToMaybe_NullOrEmpty_ShouldReturnSomeWhenInputIsNotNullOrEmpty(string input)
    {
        var result = input.ToMaybe(NoneWhen.NullOrEmpty);

        await Assert.That(result.IsSome).IsTrue();
    }

    [Test]
    [Arguments(null)]
    [Arguments("")]
    [Arguments(" ")]
    public async ValueTask MaybeExtensions_ToMaybe_NullOrWhitespace_ShouldReturnNoneWhenInputIsNullOrWhitespace(string input)
    {
        var result = input.ToMaybe(NoneWhen.NullOrWhitespace);

        await Assert.That(result.IsNone).IsTrue();
    }

    [Test]
    [Arguments("test")]
    public async ValueTask MaybeExtensions_ToMaybe_NullOrWhitespace_ShouldReturnSomeWhenInputIsNotNullOrWhitespace(string input)
    {
        var result = input.ToMaybe(NoneWhen.NullOrWhitespace);

        await Assert.That(result.IsSome).IsTrue();
    }
}
