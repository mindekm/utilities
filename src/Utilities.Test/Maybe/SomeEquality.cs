namespace Utilities.Test.Maybe;

using AutoFixture;
using Utilities;
using Shouldly;
using Xunit;

public class SomeEquality
{
    private readonly Fixture fixture = new Fixture();

    [Fact]
    public void Some_ShouldBeEqualToSelf()
    {
        var first = Maybe.Some(fixture.Create<string>());
        var second = first;

        first.Equals(second).ShouldBeTrue();
        first.Equals((object)second).ShouldBeTrue();
        (first == second).ShouldBeTrue();
        (first != second).ShouldBeFalse();
    }

    [Fact]
    public void Some_ShouldBeEqualToOtherSome()
    {
        var value = fixture.Create<string>();
        var first = Maybe.Some(value);
        var second = Maybe.Some(value);

        first.Equals(second).ShouldBeTrue();
        first.Equals((object)second).ShouldBeTrue();
        (first == second).ShouldBeTrue();
        (first != second).ShouldBeFalse();
    }

    [Fact]
    public void Some_ShouldNotBeEqualToNull()
    {
        var some = Maybe.Some(fixture.Create<string>());

        some.Equals(null).ShouldBeFalse();
        some.Equals((object)null).ShouldBeFalse();
        (some == null).ShouldBeFalse();
        (some != null).ShouldBeTrue();
    }

    [Fact]
    public void Some_ShouldNotBeEqualToDefaultValue()
    {
        var some = Maybe.Some(fixture.Create<string>());

        some.Equals(default).ShouldBeFalse();
        some.Equals((object)default(Maybe<string>)).ShouldBeFalse();
    }

    [Fact]
    public void Some_ShouldNotBeEqualToNone()
    {
        var some = Maybe.Some(fixture.Create<string>());
        
        some.Equals(Maybe.None).ShouldBeFalse();
        some.Equals((object)default(Maybe<string>)).ShouldBeFalse();
    }

    [Fact]
    public void Some_ShouldNotBeEqualToNoneForDefaultValueType()
    {
        var some = Maybe.Some(default(int));

        some.Equals(Maybe.None).ShouldBeFalse();
        some.Equals((object)default(Maybe<int>)).ShouldBeFalse();
    }

    [Fact]
    public void Some_ShouldNotBeEqualToSomeOtherType()
    {
        var value = fixture.Create<string>();
        var some = Maybe.Some(value);

        some.Equals(Maybe.Some(10)).ShouldBeFalse();
        some.Equals(value).ShouldBeFalse();
    }
}
