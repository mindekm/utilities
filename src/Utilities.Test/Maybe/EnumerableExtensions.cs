namespace Utilities.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities;
    using Extensions;
    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    public class EnumerableExtensions
    {
        private static readonly List<string> EmptyList = new List<string>();

        private static readonly List<string> NonEmptyList = new List<string> { "Test1", "test2" };

        private static readonly List<string> SingleItemList = new List<string> { "Test" };

        [Test]
        public void FirstOrNone_ShouldReturnSomeForNonEmptySequence()
        {
            NonEmptyList.FirstOrNone().IsSome.ShouldBeTrue();
        }

        [Test]
        public void FirstOrNone_ShouldReturnNoneForEmptySequence()
        {
            EmptyList.FirstOrNone().IsNone.ShouldBeTrue();
        }

        [Test]
        public void LastOrNone_ShouldReturnSomeForNonEmptySequence()
        {
            NonEmptyList.LastOrNone().IsSome.ShouldBeTrue();
        }

        [Test]
        public void LastOrNone_ShouldReturnNoneForEmptySequence()
        {
            EmptyList.LastOrNone().IsNone.ShouldBeTrue();
        }

        [Test]
        public void SingleOrNone_ShouldReturnSomeForSingleItemSequence()
        {
            SingleItemList.SingleOrNone().IsSome.ShouldBeTrue();
        }

        [Test]
        public void SingleOrNone_ShouldReturnNoneForEmptySequence()
        {
            EmptyList.SingleOrNone().IsNone.ShouldBeTrue();
        }

        [Test]
        public void SingleOrNone_ShouldThrowForSequenceWithMoreThanOneItem()
        {
            Should.Throw<InvalidOperationException>(() => NonEmptyList.SingleOrNone());
        }

        [Test]
        public void ElementAtOrNone_ShouldReturnSomeForValidIndex()
        {
            NonEmptyList.ElementAtOrNone(1).IsSome.ShouldBeTrue();
        }

        [Test]
        public void ElementAtOrNone_ShouldReturnNoneForInvalidIndex()
        {
            EmptyList.ElementAtOrNone(1).IsNone.ShouldBeTrue();
        }

        [Test]
        public void Values_ShouldReturnSomeValues()
        {
            var listSome = new List<Maybe<string>> { "Test1", "Test2", "Test3" };
            var listNone = new List<Maybe<string>> { Maybe.None<string>(), Maybe.None<string>() };

            var result = new List<Maybe<string>>();
            result.AddRange(listSome);
            result.AddRange(listNone);

            result.GetValues().ShouldBe(listSome.Select(m => m.GetValue()));
        }
    }
}
