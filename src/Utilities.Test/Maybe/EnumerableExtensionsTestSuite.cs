namespace Utilities.Test.Maybe
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoFixture;
    using Utilities;
    using Shouldly;
    using Xunit;

    public class EnumerableExtensionsTestSuite
    {
        private static readonly Fixture Fixture = new Fixture();

        public static IEnumerable<object[]> EmptyEnumerableData()
        {
            yield return new object[] { new List<string>() };
            yield return new object[] { GetEnumerable() };

            IEnumerable<string> GetEnumerable()
            {
                yield break;
            }
        }

        public static IEnumerable<object[]> NonEmptyEnumerableData()
        {
            yield return new object[] { Fixture.CreateMany<string>().ToList() };
            yield return new object[] { GetEnumerable() };

            IEnumerable<string> GetEnumerable()
            {
                yield return Fixture.Create<string>();
                yield return Fixture.Create<string>();
            }
        }

        public static IEnumerable<object[]> SingleElementEnumerableData()
        {
            yield return new object[] { new List<string> { Fixture.Create<string>() } };
            yield return new object[] { GetEnumerable() };

            IEnumerable<string> GetEnumerable()
            {
                yield return Fixture.Create<string>();
            }
        }

        [Theory]
        [MemberData(nameof(NonEmptyEnumerableData))]
        public void FirstOrNone_ShouldReturnSomeForNonEmptySequence(IEnumerable<string> source)
        {
            source.FirstOrNone().IsSome.ShouldBeTrue();
        }

        [Theory]
        [MemberData(nameof(EmptyEnumerableData))]
        public void FirstOrNone_ShouldReturnNoneForEmptySequence(IEnumerable<string> source)
        {
            source.FirstOrNone().IsNone.ShouldBeTrue();
        }

        [Fact]
        public void FirstOrNone_ShouldReturnSomeForFirstElementSatisfyingThePredicate()
        {
            var list = new List<string> { "Test1", "Test2", "Test3", "Test22" };

            var maybe = list.FirstOrNone(e => e.StartsWith("Test2"));
            maybe.IsSome.ShouldBeTrue();
            maybe.Unwrap().ShouldBe("Test2");
        }

        [Fact]
        public void FirstOrNone_ShouldReturnNoneIfSequenceDoesNotContainElementSatisfyingThePredicate()
        {
            var list = new List<string> { "Test1", "Test2", "Test3" };

            list.FirstOrNone(e => e == "Test4").IsNone.ShouldBeTrue();
        }

        [Theory]
        [MemberData(nameof(NonEmptyEnumerableData))]
        public void LastOrNone_ShouldReturnSomeForNonEmptySequence(IEnumerable<string> source)
        {
            source.LastOrNone().IsSome.ShouldBeTrue();
        }

        [Theory]
        [MemberData(nameof(EmptyEnumerableData))]
        public void LastOrNone_ShouldReturnNoneForEmptySequence(IEnumerable<string> source)
        {
            source.LastOrNone().IsNone.ShouldBeTrue();
        }

        [Fact]
        public void LastOrNone_ShouldReturnSomeForLastElementSatisfyingThePredicate()
        {
            var list = new List<string> { "Test1", "Test2", "Test3", "Test22" };

            var maybe = list.LastOrNone(e => e.StartsWith("Test2"));
            maybe.IsSome.ShouldBeTrue();
            maybe.Unwrap().ShouldBe("Test22");
        }

        [Fact]
        public void LastOrNone_ShouldReturnNoneIfSequenceDoesNotContainElementSatisfyingThePredicate()
        {
            var list = new List<string> { "Test1", "Test2", "Test3" };

            list.LastOrNone(e => e == "Test4").IsNone.ShouldBeTrue();
        }

        [Theory]
        [MemberData(nameof(SingleElementEnumerableData))]
        public void SingleOrNone_ShouldReturnSomeForSingleItemSequence(IEnumerable<string> source)
        {
            source.SingleOrNone().IsSome.ShouldBeTrue();
        }

        [Theory]
        [MemberData(nameof(EmptyEnumerableData))]
        public void SingleOrNone_ShouldReturnNoneForEmptySequence(IEnumerable<string> source)
        {
            source.SingleOrNone().IsNone.ShouldBeTrue();
        }

        [Theory]
        [MemberData(nameof(NonEmptyEnumerableData))]
        public void SingleOrNone_ShouldThrowForSequenceWithMoreThanOneItem(IEnumerable<string> source)
        {
            Should.Throw<InvalidOperationException>(() => source.SingleOrNone());
        }

        [Fact]
        public void SingeOrNone_ShouldReturnSomeForSingleElementSatisfyingThePredicate()
        {
            var list = new List<string> { "Test1", "Test2", "Test3", "Test22" };

            var maybe = list.SingleOrNone(e => e.StartsWith("Test3"));
            maybe.IsSome.ShouldBeTrue();
            maybe.Unwrap().ShouldBe("Test3");
        }

        [Fact]
        public void SingleOrNone_ShouldReturnNoneIfSequenceDoesNotContainElementSatisfyingThePredicate()
        {
            var list = new List<string> { "Test1", "Test2", "Test22" };

            list.SingleOrNone(e => e == "Test4").IsNone.ShouldBeTrue();
        }

        [Fact]
        public void SingleOrNone_ShouldThrowIfSequenceContainsMoreThanOneElementSatisfyingThePredicate()
        {
            var list = new List<string> { "Test1", "Test2", "Test3", "Test22" };

            Should.Throw<InvalidOperationException>(() => list.SingleOrNone(e => e.StartsWith("Test2")));
        }

        [Theory]
        [MemberData(nameof(NonEmptyEnumerableData))]
        public void ElementAtOrNone_ShouldReturnSomeForValidIndex(IEnumerable<string> source)
        {
            source.ElementAtOrNone(1).IsSome.ShouldBeTrue();
        }

        [Theory]
        [MemberData(nameof(EmptyEnumerableData))]
        public void ElementAtOrNone_ShouldReturnNoneForInvalidIndex(IEnumerable<string> source)
        {
            source.ElementAtOrNone(1).IsNone.ShouldBeTrue();
        }
    }
}
