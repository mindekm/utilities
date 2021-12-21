namespace Utilities.Test.Either;

using Shouldly;
using Xunit;
using Either = Utilities.Either;

public class Equality
{
    [Fact]
    public void Either_LeftCaseShouldBeEqualToItsCopy()
    {
        Either<string, string> either = Either.Left("left value");
        Either<string, string> copy = Either.Left("left value");

        either.Equals(copy).ShouldBeTrue();
        (either == copy).ShouldBeTrue();
        either.Equals((object)copy).ShouldBeTrue();
    }

    [Fact]
    public void Either_RightCaseShouldBeEqualToItsCopy()
    {
        Either<string, string> either = Either.Right("right value");
        Either<string, string> copy = Either.Right("right value");

        either.Equals(copy).ShouldBeTrue();
        (either == copy).ShouldBeTrue();
        either.Equals((object)copy).ShouldBeTrue();
    }

    [Fact]
    public void Either_LeftCaseShouldNotBeEqualToRightCase()
    {
        Either<string, string> left = Either.Left("value");
        Either<string, string> right = Either.Right("value");

        left.Equals(right).ShouldBeFalse();
        (left != right).ShouldBeTrue();
        left.Equals((object)right).ShouldBeFalse();

        right.Equals(left).ShouldBeFalse();
        (right != left).ShouldBeTrue();
        right.Equals((object)left).ShouldBeFalse();
    }

    [Fact]
    public void Either_GetHashCode_ShouldNotHaveACollisionBetweenLeftAndRightStates()
    {
        Either<string, string> left = Either.Left("value");
        Either<string, string> right = Either.Right("value");
        
        left.GetHashCode().ShouldNotBe(right.GetHashCode());
    }
}