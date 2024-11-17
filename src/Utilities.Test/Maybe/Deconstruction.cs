namespace Utilities.Test.Maybe;

using Utilities;

public class Deconstruction
{
    [Test]
    public async ValueTask Maybe_Deconstruct_ShouldCorrectlyDeconstructNone()
    {
        Maybe<string> none = Maybe.None;

        var (isSome, value) = none;

        using (Assert.Multiple())
        {
            await Assert.That(isSome).IsFalse();
            await Assert.That(value).IsNull();
        }
    }

    [Test]
    public async ValueTask Maybe_Deconstruct_ShouldCorrectlyDeconstructSome()
    {
        var guid = Guid.NewGuid();
        var some = Maybe.Some(guid);

        var (isSome, value) = some;

        using (Assert.Multiple())
        {
            await Assert.That(isSome).IsTrue();
            await Assert.That(value).IsEqualTo(guid);
        }
    }
}
