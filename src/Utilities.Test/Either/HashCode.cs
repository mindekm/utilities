namespace Utilities.Test.Either;

using Either = Utilities.Either;

public class HashCode
{
    [Test]
    public async ValueTask Either_GetHashCode_LeftAndRightHashCodesShouldNotBeTheSame()
    {
        Either<string, string> left = Either.Left("value");
        Either<string, string> right = Either.Right("value");

        using (Assert.Multiple())
        {
            await Assert.That(left.GetHashCode()).IsNotEqualTo(right.GetHashCode());
            await Assert.That(left).IsNotEqualTo(right);
        }
    }

    [Test]
    public async ValueTask Either_GetHashCode_LeftHashCodeShouldBeEqualToItsCopy()
    {
        Either<string, string> left = Either.Left("left value");
        Either<string, string> copy = Either.Left("left value");

        using (Assert.Multiple())
        {
            await Assert.That(left.GetHashCode()).IsEqualTo(copy.GetHashCode());
            await Assert.That(left).IsEqualTo(copy);
        }
    }

    [Test]
    public async ValueTask Either_GetHashCode_RightHashCodeShouldBeEqualToItsCopy()
    {
        Either<string, string> right = Either.Right("right value");
        Either<string, string> copy = Either.Right("right value");

        using (Assert.Multiple())
        {
            await Assert.That(right.GetHashCode()).IsEqualTo(copy.GetHashCode());
            await Assert.That(right).IsEqualTo(copy);
        }
    }

    [Test]
    public async ValueTask Either_GetHashCode_UninitializedValuesShouldBeEqual()
    {
        Either<string, string> default1 = default;
        Either<string, string> default2 = default;

        using (Assert.Multiple())
        {
            await Assert.That(default1.GetHashCode()).IsEqualTo(default2.GetHashCode());
            await Assert.That(default1).IsEqualTo(default2);
        }
    }
}
