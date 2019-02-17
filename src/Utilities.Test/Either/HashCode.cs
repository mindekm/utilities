namespace Utilities.Test.Either
{
    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    public class HashCode
    {
        [Test]
        public void Either_GetHashCode_LeftAndRightHashCodesShouldNotBeTheSame()
        {
            var left = Either<string, string>.Left("value");
            var right = Either<string, string>.Right("value");

            left.GetHashCode().ShouldNotBe(right.GetHashCode());
        }

        [Test]
        public void Either_GetHashCode_LeftHashCodeShouldBeEqualToItsCopy()
        {
            var left = Either<string, string>.Left("left value");
            var copy = left;

            left.GetHashCode().ShouldBe(copy.GetHashCode());
        }

        [Test]
        public void Either_GetHashCode_RightHashCodeShouldBeEqualToItsCopy()
        {
            var right = Either<string, string>.Right("right value");
            var copy = right;

            right.GetHashCode().ShouldBe(copy.GetHashCode());
        }

        [Test]
        public void Either_GetHashCode_NeitherHashCodeShouldBeZero()
        {
            Either<string, string> neither = default;

            neither.GetHashCode().ShouldBe(0);
        }
    }
}
