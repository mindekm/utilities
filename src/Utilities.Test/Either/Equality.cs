namespace Utilities.Test.Either;

using Either = Utilities.Either;

public class Equality
{
    [Test]
    public async ValueTask Either_LeftCaseShouldBeEqualToItsCopy()
    {
        Either<string, string> either = Either.Left("left value");
        Either<string, string> copy = Either.Left("left value");

        using (Assert.Multiple())
        {
            await Assert.That(either.Equals(copy)).IsTrue();
            await Assert.That(either == copy).IsTrue();
            await Assert.That(either.Equals((object)copy)).IsTrue();
        }
    }

    [Test]
    public async ValueTask Either_RightCaseShouldBeEqualToItsCopy()
    {
        Either<string, string> either = Either.Right("right value");
        Either<string, string> copy = Either.Right("right value");

        using (Assert.Multiple())
        {
            await Assert.That(either.Equals(copy)).IsTrue();
            await Assert.That(either == copy).IsTrue();
            await Assert.That(either.Equals((object)copy)).IsTrue();
        }
    }

    [Test]
    public async ValueTask Either_LeftCaseShouldNotBeEqualToRightCase()
    {
        Either<string, string> left = Either.Left("value");
        Either<string, string> right = Either.Right("value");

        using (Assert.Multiple())
        {
            await Assert.That(left.Equals(right)).IsFalse();
            await Assert.That(left != right).IsTrue();
            await Assert.That(left.Equals((object)right)).IsFalse();

            await Assert.That(right.Equals(left)).IsFalse();
            await Assert.That(right != left).IsTrue();
            await Assert.That(right.Equals((object)left)).IsFalse();
        }
    }

    [Test]
    public async ValueTask Either_GetHashCode_ShouldNotHaveACollisionBetweenLeftAndRightStates()
    {
        Either<string, string> left = Either.Left("value");
        Either<string, string> right = Either.Right("value");

        await Assert.That(left.GetHashCode()).IsNotEqualTo(right.GetHashCode());
    }
}
