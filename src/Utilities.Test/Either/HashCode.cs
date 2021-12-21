namespace Utilities.Test.Either;

using Shouldly;
using Xunit;
using Either = Utilities.Either;

public class HashCode
{
    [Fact]
    public void Either_GetHashCode_LeftAndRightHashCodesShouldNotBeTheSame()
    {
        Either<string, string> left = Either.Left("value");
        Either<string, string> right = Either.Right("value");

        left.GetHashCode().ShouldNotBe(right.GetHashCode());
        left.ShouldNotBe(right);
    }

    [Fact]
    public void Either_GetHashCode_LeftHashCodeShouldBeEqualToItsCopy()
    {
        Either<string, string> left = Either.Left("left value");
        Either<string, string> copy = Either.Left("left value");

        left.GetHashCode().ShouldBe(copy.GetHashCode());
        left.ShouldBe(copy);
    }

    [Fact]
    public void Either_GetHashCode_RightHashCodeShouldBeEqualToItsCopy()
    {
        Either<string, string> right = Either.Right("right value");
        Either<string, string> copy = Either.Right("right value");

        right.GetHashCode().ShouldBe(copy.GetHashCode());
        right.ShouldBe(copy);
    }

    [Fact]
    public void Either_GetHashCode_UninitializedValuesShouldBeEqual()
    {
        Either<string, string> default1 = default;
        Either<string, string> default2 = default;

        default1.GetHashCode().ShouldBe(default2.GetHashCode());
        default1.ShouldBe(default2);
    }
}