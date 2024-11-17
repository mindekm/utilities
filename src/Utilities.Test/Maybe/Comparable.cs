namespace Utilities.Test.Maybe;

using Utilities;

public class Comparable
{
    [Test]
    public async ValueTask Maybe_CompareTo_SomeShouldBeGreaterThanNone()
    {
        var some = Maybe.Some(10);
        Maybe<int> none = Maybe.None;

        using (Assert.Multiple())
        {
            await Assert.That(some.CompareTo(none)).IsEqualTo(1);
            await Assert.That(some.CompareTo((object)none)).IsEqualTo(1);
            await Assert.That(some > none).IsTrue();
            await Assert.That(none < some).IsTrue();
        }
    }

    [Test]
    public async ValueTask Maybe_CompareTo_NoneShouldBeSmallerThanSome()
    {
        var some = Maybe.Some(10);
        Maybe<int> none = Maybe.None;

        using (Assert.Multiple())
        {
            await Assert.That(none.CompareTo(some)).IsEqualTo(-1);
            await Assert.That(none.CompareTo((object)some)).IsEqualTo(-1);
            await Assert.That(none < some).IsTrue();
            await Assert.That(some > none).IsTrue();
        }
    }

    [Test]
    public async ValueTask Maybe_CompareTo_SomeShouldBeEqualToSomeIfUnderlyingValuesAreEqual()
    {
        var some1 = Maybe.Some(10);
        var some2 = Maybe.Some(10);

        using (Assert.Multiple())
        {
            await Assert.That(some1.CompareTo(some2)).IsEqualTo(0);
            await Assert.That(some1.CompareTo((object)some2)).IsEqualTo(0);
            await Assert.That(some1 >= some2).IsTrue();
            await Assert.That(some1 <= some2).IsTrue();
        }
    }

    [Test]
    public async ValueTask Maybe_CompareTo_ShouldThrowForIncompatibleTypes()
    {
        var some = Maybe.Some(10);

        await Assert.That(() => some.CompareTo(5)).Throws<ArgumentException>();
    }
}
