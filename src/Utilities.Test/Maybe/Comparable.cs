namespace Utilities.Test.Maybe;

using Utilities;
using Shouldly;
using Xunit;

public class Comparable
{
    [Fact]
    public void Maybe_CompareTo_SomeShouldBeGreaterThanNone()
    {
        var some = Maybe.Some(10);
        Maybe<int> none = Maybe.None;

        some.CompareTo(none).ShouldBe(1);
        (some > none).ShouldBeTrue();
    }

    [Fact]
    public void Maybe_CompareTo_NoneShouldBeSmallerThanSome()
    {
        var some = Maybe.Some(10);
        Maybe<int> none = Maybe.None;

        none.CompareTo(some).ShouldBe(-1);
        (none < some).ShouldBeTrue();
    }

    [Fact]
    public void Maybe_CompareTo_SomeShouldBeEqualToSomeIfUnderlyingValuesAreEqual()
    {
        var some1 = Maybe.Some(10);
        var some2 = Maybe.Some(10);

        some1.CompareTo(some2).ShouldBe(0);
        (some1 >= some2).ShouldBeTrue();
        (some1 <= some2).ShouldBeTrue();
    }
}
