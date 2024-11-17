namespace Utilities.Test.Maybe;

using Utilities;

public class SomeEquality
{
    [Test]
    public async ValueTask Some_ShouldBeEqualToSelf()
    {
        var first = Maybe.Some(Guid.NewGuid().ToString());
        var second = first;

        using (Assert.Multiple())
        {
            await Assert.That(first.Equals(second)).IsTrue();
            await Assert.That(first.Equals((object)second)).IsTrue();
            await Assert.That(first == second).IsTrue();
            await Assert.That(first != second).IsFalse();
        }
    }

    [Test]
    public async ValueTask Some_ShouldBeEqualToOtherSome()
    {
        var value = Guid.NewGuid().ToString();
        var first = Maybe.Some(value);
        var second = Maybe.Some(value);

        using (Assert.Multiple())
        {
            await Assert.That(first.Equals(second)).IsTrue();
            await Assert.That(first.Equals((object)second)).IsTrue();
            await Assert.That(first == second).IsTrue();
            await Assert.That(first != second).IsFalse();
        }
    }

    [Test]
    public async ValueTask Some_ShouldNotBeEqualToNull()
    {
        var some = Maybe.Some(Guid.NewGuid().ToString());

        await Assert.That(some.Equals(null)).IsFalse();
    }

    [Test]
    public async ValueTask Some_ShouldNotBeEqualToDefaultValue()
    {
        var some = Maybe.Some(Guid.NewGuid().ToString());

        using (Assert.Multiple())
        {
            await Assert.That(some.Equals(default)).IsFalse();
            await Assert.That(some.Equals((object)default(Maybe<string>))).IsFalse();
        }
    }

    [Test]
    public async ValueTask Some_ShouldNotBeEqualToNone()
    {
        var some = Maybe.Some(Guid.NewGuid().ToString());

        using (Assert.Multiple())
        {
            await Assert.That(some.Equals(Maybe.None)).IsFalse();
            await Assert.That(some.Equals((object)default(Maybe<string>))).IsFalse();
        }
    }

    [Test]
    public async ValueTask Some_ShouldNotBeEqualToNoneForDefaultValueType()
    {
        var some = Maybe.Some(default(int));

        using (Assert.Multiple())
        {
            await Assert.That(some.Equals(Maybe.None)).IsFalse();
            await Assert.That(some.Equals((object)default(Maybe<int>))).IsFalse();
        }
    }

    [Test]
    public async ValueTask Some_ShouldNotBeEqualToSomeOtherType()
    {
        var value = Guid.NewGuid().ToString();
        var some = Maybe.Some(value);

        using (Assert.Multiple())
        {
            await Assert.That(some.Equals(Maybe.Some(10))).IsFalse();
            await Assert.That(some.Equals(value)).IsFalse();
        }
    }
}
