namespace Utilities.Test
{
    using System.Text;
    using Utilities;
    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    public class SomeEquality
    {
        private readonly Maybe<string> some = Maybe.Some("test");

        [Test]
        public void Some_ShouldBeEqualToSelf()
        {
            some.Equals(some).ShouldBeTrue();
            some.Equals((object)some).ShouldBeTrue();
        }

        [Test]
        public void Some_ShouldBeEqualToOtherSome()
        {
            some.Equals(Maybe.Some(some.GetValue())).ShouldBeTrue();
            some.Equals((object)Maybe.Some(some.GetValue())).ShouldBeTrue();
        }

        [Test]
        public void Some_ShouldNotBeEqualToNull()
        {
            some.Equals(null).ShouldBeFalse();
            some.Equals((object)null).ShouldBeFalse();
        }

        [Test]
        public void Some_ShouldNotBeEqualToDefaultValue()
        {
            some.Equals(default(Maybe<string>)).ShouldBeFalse();
            some.Equals((object)default(Maybe<string>)).ShouldBeFalse();
        }

        [Test]
        public void Some_ShouldBeEqualToUnderlyingValue()
        {
            some.Equals(some.GetValue()).ShouldBeTrue();
            some.Equals((object)some.GetValue()).ShouldBeTrue();
        }

        [Test]
        public void Some_ShouldNotBeEqualToNone()
        {
            some.Equals(Maybe.None<string>()).ShouldBeFalse();
            some.Equals((object)Maybe.None<string>()).ShouldBeFalse();
        }

        [Test]
        public void Some_ShouldNotBeEqualToSomeOtherType()
        {
            some.Equals(Maybe.Some(new StringBuilder())).ShouldBeFalse();
            some.Equals(new StringBuilder()).ShouldBeFalse();
        }
    }
}