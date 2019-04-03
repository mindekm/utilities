namespace Utilities.Test.Either
{
    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    public class Equality
    {
        [Test]
        public void Either_LeftCaseShouldBeEqualToItsCopy()
        {
            var either = Either<string, string>.Left("left value");
            var copy = either;

            either.Equals(copy).ShouldBeTrue();
            (either == copy).ShouldBeTrue();
            either.Equals((object)copy).ShouldBeTrue();
        }

        [Test]
        public void Either_RightCaseShouldBeEqualToItsCopy()
        {
            var either = Either<string, string>.Right("right value");
            var copy = either;

            either.Equals(copy).ShouldBeTrue();
            (either == copy).ShouldBeTrue();
            either.Equals((object)copy).ShouldBeTrue();
        }

        [Test]
        public void Either_NeitherCaseShouldBeEqualToItsCopy()
        {
            Either<string, string> either = default;
            var copy = either;

            either.Equals(copy).ShouldBeTrue();
            (either == copy).ShouldBeTrue();
            either.Equals((object)copy).ShouldBeTrue();
        }

        [Test]
        public void Either_LeftCaseShouldNotBeEqualToRightCase()
        {
            var left = Either<string, string>.Left("value");
            var right = Either<string, string>.Right("value");

            left.Equals(right).ShouldBeFalse();
            (left != right).ShouldBeTrue();
            left.Equals((object)right).ShouldBeFalse();

            right.Equals(left).ShouldBeFalse();
            (right != left).ShouldBeTrue();
            right.Equals((object)left).ShouldBeFalse();
        }

        [Test]
        public void Either_ShouldNotEqualItsUnderlyingValue()
        {
            var either = Either<string, string>.Left("left value");

            either.Equals((object)"left value").ShouldBeFalse();

            var nullEither = Either<string, string>.Right(null);

            nullEither.Equals((object)null).ShouldBeFalse();
        }
    }
}
