namespace Utilities.Test.Either
{
    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    public class Deconstruction
    {
        [Test]
        public void Either_Deconstruct_ShouldCorrectlyDeconstructLeftCase()
        {
            var (isLeft, left, isRight, right) = Either<string, string>.Left("left value");

            isLeft.ShouldBeTrue();
            left.ShouldBe("left value");

            isRight.ShouldBeFalse();
            right.ShouldBe(default);
        }

        [Test]
        public void Either_Deconstruct_ShouldCorrectlyDeconstructRightCase()
        {
            var (isLeft, left, isRight, right) = Either<string, string>.Right("right value");

            isLeft.ShouldBeFalse();
            left.ShouldBe(default);

            isRight.ShouldBeTrue();
            right.ShouldBe("right value");
        }
    }
}
