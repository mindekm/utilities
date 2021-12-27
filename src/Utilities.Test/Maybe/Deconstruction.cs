namespace Utilities.Test.Maybe;

using Shouldly;
using Xunit;
using Maybe = Utilities.Maybe;

public class Deconstruction
{
    [Fact]
    public void Maybe_Deconstruct_ShouldCorrectlyDeconstructNone()
    {
        Maybe<string> none = Maybe.None;

        var (isSome, value) = none;

        isSome.ShouldBeFalse();
        value.ShouldBeNull();
    }

    [Fact]
    public void Maybe_Deconstruct_ShouldCorrectlyDeconstructSome()
    {
        var guid = Guid.NewGuid();
        var some = Maybe.Some(guid);

        var (isSome, value) = some;

        isSome.ShouldBeTrue();
        value.ShouldBe(guid);
    }
}