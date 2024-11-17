namespace Utilities.Test.Maybe;

using System;
using System.Collections.Generic;
using Utilities;

public class EnumerableExtensionsTestSuite
{
    public static IEnumerable<Func<IEnumerable<string>>> EmptyEnumerableData()
    {
        yield return () => new List<string>();
        yield return GetEnumerable;
        yield break;

        static IEnumerable<string> GetEnumerable()
        {
            yield break;
        }
    }

    public static IEnumerable<Func<IEnumerable<string>>> NonEmptyEnumerableData()
    {
        yield return () => [Guid.NewGuid().ToString(), Guid.NewGuid().ToString()];
        yield return GetEnumerable;
        yield break;

        static IEnumerable<string> GetEnumerable()
        {
            yield return Guid.NewGuid().ToString();
            yield return Guid.NewGuid().ToString();
        }
    }

    public static IEnumerable<Func<IEnumerable<string>>> SingleElementEnumerableData()
    {
        yield return () => [Guid.NewGuid().ToString()];
        yield return GetEnumerable;
        yield break;

        static IEnumerable<string> GetEnumerable()
        {
            yield return Guid.NewGuid().ToString();
        }
    }

    [Test]
    [MethodDataSource(nameof(NonEmptyEnumerableData))]
    public async ValueTask FirstOrNone_ShouldReturnSomeForNonEmptySequence(IEnumerable<string> source)
    {
        await Assert.That(source.FirstOrNone().IsSome).IsTrue();
    }

    [Test]
    [MethodDataSource(nameof(EmptyEnumerableData))]
    public async ValueTask FirstOrNone_ShouldReturnNoneForEmptySequence(IEnumerable<string> source)
    {
        await Assert.That(source.FirstOrNone().IsNone).IsTrue();
    }

    [Test]
    public async ValueTask FirstOrNone_ShouldReturnSomeForFirstElementSatisfyingThePredicate()
    {
        var list = new List<string> { "Test1", "Test2", "Test3", "Test22" };

        var maybe = list.FirstOrNone(e => e.StartsWith("Test2", StringComparison.Ordinal));

        using (Assert.Multiple())
        {
            await Assert.That(maybe.IsSome).IsTrue();
            await Assert.That(maybe.Unwrap()).IsEqualTo("Test2");
        }
    }

    [Test]
    public async ValueTask FirstOrNone_ShouldReturnNoneIfSequenceDoesNotContainElementSatisfyingThePredicate()
    {
        var list = new List<string> { "Test1", "Test2", "Test3" };

        await Assert.That(list.FirstOrNone(e => e == "Test4").IsNone).IsTrue();
    }

    [Test]
    [MethodDataSource(nameof(NonEmptyEnumerableData))]
    public async ValueTask LastOrNone_ShouldReturnSomeForNonEmptySequence(IEnumerable<string> source)
    {
        await Assert.That(source.LastOrNone().IsSome).IsTrue();
    }

    [Test]
    [MethodDataSource(nameof(EmptyEnumerableData))]
    public async ValueTask LastOrNone_ShouldReturnNoneForEmptySequence(IEnumerable<string> source)
    {
        await Assert.That(source.LastOrNone().IsNone).IsTrue();
    }

    [Test]
    public async ValueTask LastOrNone_ShouldReturnSomeForLastElementSatisfyingThePredicate()
    {
        var list = new List<string> { "Test1", "Test2", "Test3", "Test22" };

        var maybe = list.LastOrNone(e => e.StartsWith("Test2", StringComparison.Ordinal));

        using (Assert.Multiple())
        {
            await Assert.That(maybe.IsSome).IsTrue();
            await Assert.That(maybe.Unwrap()).IsEqualTo("Test22");
        }
    }

    [Test]
    public async ValueTask LastOrNone_ShouldReturnNoneIfSequenceDoesNotContainElementSatisfyingThePredicate()
    {
        var list = new List<string> { "Test1", "Test2", "Test3" };

        await Assert.That(list.LastOrNone(e => e == "Test4").IsNone).IsTrue();
    }

    [Test]
    [MethodDataSource(nameof(SingleElementEnumerableData))]
    public async ValueTask SingleOrNone_ShouldReturnSomeForSingleItemSequence(IEnumerable<string> source)
    {
        await Assert.That(source.SingleOrNone().IsSome).IsTrue();
    }

    [Test]
    [MethodDataSource(nameof(EmptyEnumerableData))]
    public async ValueTask SingleOrNone_ShouldReturnNoneForEmptySequence(IEnumerable<string> source)
    {
        await Assert.That(source.SingleOrNone().IsNone).IsTrue();
    }

    [Test]
    [MethodDataSource(nameof(NonEmptyEnumerableData))]
    public async ValueTask SingleOrNone_ShouldThrowForSequenceWithMoreThanOneItem(IEnumerable<string> source)
    {
        await Assert.That(source.SingleOrNone).Throws<InvalidOperationException>();
    }

    [Test]
    public async ValueTask SingeOrNone_ShouldReturnSomeForSingleElementSatisfyingThePredicate()
    {
        var list = new List<string> { "Test1", "Test2", "Test3", "Test22" };

        var maybe = list.SingleOrNone(e => e.StartsWith("Test3", StringComparison.Ordinal));

        using (Assert.Multiple())
        {
            await Assert.That(maybe.IsSome).IsTrue();
            await Assert.That(maybe.Unwrap()).IsEqualTo("Test3");
        }
    }

    [Test]
    public async ValueTask SingleOrNone_ShouldReturnNoneIfSequenceDoesNotContainElementSatisfyingThePredicate()
    {
        var list = new List<string> { "Test1", "Test2", "Test22" };

        await Assert.That(list.SingleOrNone(e => e == "Test4").IsNone).IsTrue();
    }

    [Test]
    public async ValueTask SingleOrNone_ShouldThrowIfSequenceContainsMoreThanOneElementSatisfyingThePredicate()
    {
        var list = new List<string> { "Test1", "Test2", "Test3", "Test22" };

        await Assert
            .That(() => list.SingleOrNone(e => e.StartsWith("Test2", StringComparison.Ordinal)))
            .Throws<InvalidOperationException>();
    }

    [Test]
    [MethodDataSource(nameof(NonEmptyEnumerableData))]
    public async ValueTask ElementAtOrNone_ShouldReturnSomeForValidIndex(IEnumerable<string> source)
    {
        await Assert.That(source.ElementAtOrNone(1).IsSome).IsTrue();
    }

    [Test]
    [MethodDataSource(nameof(EmptyEnumerableData))]
    public async ValueTask ElementAtOrNone_ShouldReturnNoneForInvalidIndex(IEnumerable<string> source)
    {
        await Assert.That(source.ElementAtOrNone(1).IsNone).IsTrue();
    }
}
