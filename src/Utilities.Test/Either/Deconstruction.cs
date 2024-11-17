namespace Utilities.Test.Either;

using Either = Utilities.Either;

public class Deconstruction
{
    [Test]
    public async ValueTask Either_DeconstructBasic_ShouldCorrectlyDeconstructLeftCase()
    {
        Either<string, string> either = Either.Left("left value");
        var (isLeft, isRight) = either;

        using (Assert.Multiple())
        {
            await Assert.That(isLeft).IsTrue();
            await Assert.That(isRight).IsFalse();
        }
    }

    [Test]
    public async ValueTask Either_DeconstructBasic_ShouldCorrectlyDeconstructRightCase()
    {
        Either<string, string> either = Either.Right("right value");
        var (isLeft, isRight) = either;

        using (Assert.Multiple())
        {
            await Assert.That(isLeft).IsFalse();
            await Assert.That(isRight).IsTrue();
        }
    }

    [Test]
    public async ValueTask Either_DeconstructBasic_ShouldCorrectlyDeconstructUninitializedCase()
    {
        Either<string, string> either = default;
        var (isLeft, isRight) = either;

        using (Assert.Multiple())
        {
            await Assert.That(isLeft).IsFalse();
            await Assert.That(isRight).IsFalse();
        }
    }

    [Test]
    public async ValueTask Either_Deconstruct_ShouldCorrectlyDeconstructLeftCase()
    {
        Either<string, string> either = Either.Left("left value");
        var (isLeft, left, isRight, right) = either;

        using (Assert.Multiple())
        {
            await Assert.That(isLeft).IsTrue();
            await Assert.That(left).IsEqualTo("left value");

            await Assert.That(isRight).IsFalse();
            await Assert.That(right).IsDefault();
        }
    }

    [Test]
    public async ValueTask Either_Deconstruct_ShouldCorrectlyDeconstructRightCase()
    {
        Either<int, string> either = Either.Right("right value");
        var (isLeft, left, isRight, right) = either;

        using (Assert.Multiple())
        {
            await Assert.That(isLeft).IsFalse();
            await Assert.That(left).IsDefault();

            await Assert.That(isRight).IsTrue();
            await Assert.That(right).IsEqualTo("right value");
        }
    }

    [Test]
    public async ValueTask Either_Deconstruct_ShouldCorrectlyDeconstructUninitializedCase()
    {
        Either<int, string> either = default;
        var (isLeft, left, isRight, right) = either;

        using (Assert.Multiple())
        {
            await Assert.That(isLeft).IsFalse();
            await Assert.That(left).IsDefault();

            await Assert.That(isRight).IsFalse();
            await Assert.That(right).IsNull();
        }
    }
}
