namespace Utilities;

using Shouldly;
using Xunit;

public sealed class ExtensionsTestSuite
{
    [Fact]
    public void MaybeExtensions_Add_ShouldAddSomeValueToCollection()
    {
        var list = new List<string>();
        var some = Maybe.Some("test");

        list.Add(some);

        list.Count.ShouldBe(1);
        list[0].ShouldBe(some.Unwrap());
    }

    [Fact]
    public void MaybeExtensions_Add_ShouldNotAddNoneValueToCollection()
    {
        var list = new List<string>();
        Maybe<string> none = Maybe.None;

        list.Add(none);

        list.Count.ShouldBe(0);
    }

    [Theory]
    [InlineData(null)]
    public void MaybeExtensions_ToMaybe_Null_ShouldReturnNoneWhenInputIsNull(string input)
    {
        var result = input.ToMaybe(NoneWhen.Null);

        result.IsNone.ShouldBeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("test")]
    public void MaybeExtensions_ToMaybe_Null_ShouldReturnSomeWhenInputIsNotNull(string input)
    {
        var result = input.ToMaybe(NoneWhen.Null);

        result.IsSome.ShouldBeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void MaybeExtensions_ToMaybe_NullOrEmpty_ShouldReturnNoneWhenInputIsNullOrEmpty(string input)
    {
        var result = input.ToMaybe(NoneWhen.NullOrEmpty);

        result.IsNone.ShouldBeTrue();
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("test")]
    public void MaybeExtensions_ToMaybe_NullOrEmpty_ShouldReturnSomeWhenInputIsNotNullOrEmpty(string input)
    {
        var result = input.ToMaybe(NoneWhen.NullOrEmpty);

        result.IsSome.ShouldBeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void MaybeExtensions_ToMaybe_NullOrWhitespace_ShouldReturnNoneWhenInputIsNullOrWhitespace(string input)
    {
        var result = input.ToMaybe(NoneWhen.NullOrWhitespace);

        result.IsNone.ShouldBeTrue();
    }

    [Theory]
    [InlineData("test")]
    public void MaybeExtensions_ToMaybe_NullOrWhitespace_ShouldReturnSomeWhenInputIsNotNullOrWhitespace(string input)
    {
        var result = input.ToMaybe(NoneWhen.NullOrWhitespace);

        result.IsSome.ShouldBeTrue();
    }
}
