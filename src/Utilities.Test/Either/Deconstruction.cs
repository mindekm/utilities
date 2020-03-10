namespace Utilities.Test.Either
{
    using Shouldly;
    using Xunit;
    using Either = Utilities.Either;

    public class Deconstruction
    {
        [Fact]
        public void Either_Deconstruct_ShouldCorrectlyDeconstructLeftCase()
        {
            Either<string, string> either = Either.Left("left value");
            var (isLeft, left, isRight, right) = either;

            isLeft.ShouldBeTrue();
            left.ShouldBe("left value");

            isRight.ShouldBeFalse();
            right.ShouldBe(default);
        }

        [Fact]
        public void Either_Deconstruct_ShouldCorrectlyDeconstructRightCase()
        {
            Either<int, string> either = Either.Right("right value");
            var (isLeft, left, isRight, right) = either;

            isLeft.ShouldBeFalse();
            left.ShouldBe(default);

            isRight.ShouldBeTrue();
            right.ShouldBe("right value");
        }
    }
}
