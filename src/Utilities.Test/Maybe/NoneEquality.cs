namespace Utilities.Test.Maybe;

using System.Text;
using Utilities;

public class NoneEquality
{
    private readonly Maybe<string> none = Maybe.None;

    [Test]
    public async ValueTask None_ShouldBeEqualToSelf()
    {
        using (Assert.Multiple())
        {
            await Assert.That(none.Equals(none)).IsTrue();
            await Assert.That(none.Equals((object)none)).IsTrue();
        }
    }

    [Test]
    public async ValueTask None_ShouldBeEqualToOtherNone()
    {
        using (Assert.Multiple())
        {
            await Assert.That(none.Equals(Maybe.None)).IsTrue();
            await Assert.That(none.Equals((object)default(Maybe<string>))).IsTrue();
        }
    }

    [Test]
    public async ValueTask None_ShouldBeEqualToDefaultValue()
    {
        using (Assert.Multiple())
        {
            await Assert.That(none.Equals(default)).IsTrue();
            await Assert.That(none.Equals((object)default(Maybe<string>))).IsTrue();
        }
    }

    [Test]
    public async ValueTask None_ShouldNotBeEqualToSome()
    {
        using (Assert.Multiple())
        {
            await Assert.That(none.Equals(Maybe.Some("value"))).IsFalse();
            await Assert.That(none.Equals((object)Maybe.Some("value"))).IsFalse();
        }
    }

    [Test]
    public async ValueTask None_ShouldNotBeEqualToSomeOtherType()
    {
        await Assert.That(none.Equals(Maybe.Some(new StringBuilder()))).IsFalse();
    }
}
