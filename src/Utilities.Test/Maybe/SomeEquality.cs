namespace Utilities.Test.Maybe;

using System.Text;
using Utilities;
using Shouldly;
using Xunit;

public class SomeEquality
{
    private readonly Maybe<string> some = Maybe.Some("test");

    [Fact]
    public void Some_ShouldBeEqualToSelf()
    {
        some.Equals(some).ShouldBeTrue();
        some.Equals((object)some).ShouldBeTrue();
    }

    [Fact]
    public void Some_ShouldBeEqualToOtherSome()
    {
        some.Equals(Maybe.Some(some.Unwrap())).ShouldBeTrue();
        some.Equals((object)Maybe.Some(some.Unwrap())).ShouldBeTrue();
    }

    [Fact]
    public void Some_ShouldNotBeEqualToNull()
    {
        some.Equals(null).ShouldBeFalse();
    }

    [Fact]
    public void Some_ShouldNotBeEqualToDefaultValue()
    {
        some.Equals(default).ShouldBeFalse();
        some.Equals((object)default(Maybe<string>)).ShouldBeFalse();
    }

    [Fact]
    public void Some_ShouldNotBeEqualToNone()
    {
        some.Equals(Maybe.None).ShouldBeFalse();
        some.Equals((object)default(Maybe<string>)).ShouldBeFalse();
    }

    [Fact]
    public void Some_ShouldNotBeEqualToSomeOtherType()
    {
        some.Equals(Maybe.Some(new StringBuilder())).ShouldBeFalse();
        some.Equals(new StringBuilder()).ShouldBeFalse();
    }
}