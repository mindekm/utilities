namespace Utilities.Test.Either
{
    using System;
    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    public class ValueRetrieval
    {
        [Test]
        public void Either_GetLeft_ShouldReturnValueForLeftCase()
        {
            Either<string, string>.Left("left value").GetLeft().ShouldBe("left value");
        }

        [Test]
        public void Either_GetLeft_ShouldThrowForRightCase()
        {
            Should.Throw<InvalidOperationException>(() => Either<string, string>.Right("right value").GetLeft());
        }

        [Test]
        public void Either_GetRight_ShouldReturnValueForRightCase()
        {
            Either<string, string>.Right("right value").GetRight().ShouldBe("right value");
        }

        [Test]
        public void Either_GetRight_ShouldThrowForLeftCase()
        {
            Should.Throw<InvalidOperationException>(() => Either<string, string>.Left("left value").GetRight());
        }

        [Test]
        public void Either_TryGetLeft_ShouldReturnTrueAndValueForLeftCase()
        {
            var either = Either<string, string>.Left("left value");

            either.TryGetLeft(out var result).ShouldBeTrue();
            result.ShouldBe("left value");
        }

        [Test]
        public void Either_TryGetLeft_ShouldReturnFalseAndDefaultForRightCase()
        {
            var either = Either<string, string>.Right("right value");

            either.TryGetLeft(out var result).ShouldBeFalse();
            result.ShouldBe(default);
        }

        [Test]
        public void Either_TryGetRight_ShouldReturnTrueAndValueForRightCase()
        {
            var either = Either<string, string>.Right("right value");

            either.TryGetRight(out var result).ShouldBeTrue();
            result.ShouldBe("right value");
        }

        [Test]
        public void Either_TryGetRight_ShouldReturnFalseAndDefaultForLeftCase()
        {
            var either = Either<string, string>.Left("left value");

            either.TryGetRight(out var result).ShouldBeFalse();
            result.ShouldBe(default);
        }

        [Test]
        public void Either_GetLeftOrDefault_ShouldReturnValueForLeftCase()
        {
            Either<string, string>.Left("left value").GetLeftOrDefault().ShouldBe("left value");
        }

        [Test]
        public void Either_GetLeftOrDefault_ShouldReturnDefaultForRightCase()
        {
            Either<string, string>.Right("right value").GetLeftOrDefault().ShouldBe(default);
        }

        [Test]
        public void Either_GetRightOrDefault_ShouldReturnValueForRightCase()
        {
            Either<string, string>.Right("right value").GetRightOrDefault().ShouldBe("right value");
        }

        [Test]
        public void Either_GetRightOrDefault_ShouldReturnDefaultForLeftCase()
        {
            Either<string, string>.Left("left value").GetRightOrDefault().ShouldBe(default);
        }

        [Test]
        public void Either_GetLeftOr_ShouldReturnValueForLeftCase()
        {
            Either<string, string>.Left("left value").GetLeftOr(() => "other value").ShouldBe("left value");
        }

        [Test]
        public void Either_GetLeftOr_ShouldReturnFactoryResultForRightCase()
        {
            Either<string, string>.Right("right value").GetLeftOr(() => "other value").ShouldBe("other value");
        }

        [Test]
        public void Either_GetRightOr_ShouldReturnValueForRightCase()
        {
            Either<string, string>.Right("right value").GetRightOr(() => "other value").ShouldBe("right value");
        }

        [Test]
        public void Either_GetRightOr_ShouldReturnFactoryResultForLeftCase()
        {
            Either<string, string>.Left("left value").GetRightOr(() => "other value").ShouldBe("other value");
        }

        [Test]
        public void Either_ExplicitCast_ShouldReturnCorrectValues()
        {
            ((string)Either<string, int>.Left("left value")).ShouldBe("left value");

            ((string)Either<int, string>.Right("right value")).ShouldBe("right value");
        }

        [Test]
        public void Either_ExplicitLeftCast_ShouldThrowForRightCase()
        {
            Should.Throw<InvalidCastException>(() =>
            {
                var result = (string)Either<string, int>.Right(0);
            });
        }

        [Test]
        public void Either_ExplicitRightCast_ShouldThrowForLeftCase()
        {
            Should.Throw<InvalidCastException>(() =>
            {
                var result = (string)Either <int, string>.Left(0);
            });
        }
    }
}
