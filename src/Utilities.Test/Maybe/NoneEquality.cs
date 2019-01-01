namespace Utilities.Test
{
    using System.Text;
    using Utilities;
    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    public class NoneEquality
    {
        private readonly Maybe<string> none = Maybe.None<string>();

        [Test]
        public void None_ShouldBeEqualToSelf()
        {
            none.Equals(none).ShouldBeTrue();
            none.Equals((object)none).ShouldBeTrue();
        }

        [Test]
        public void None_ShouldBeEqualToOtherNone()
        {
            none.Equals(Maybe.None<string>()).ShouldBeTrue();
            none.Equals((object)Maybe.None<string>()).ShouldBeTrue();
        }

        [Test]
        public void None_ShouldBeEqualToNull()
        {
            none.Equals(null).ShouldBeTrue();
            none.Equals((object)null).ShouldBeTrue();
        }

        [Test]
        public void None_ShouldBeEqualToDefaultValue()
        {
            none.Equals(default(Maybe<string>)).ShouldBeTrue();
            none.Equals((object)default(Maybe<string>)).ShouldBeTrue();
        }

        [Test]
        public void None_ShouldBeEqualToDefaultUnderlyingValue()
        {
            none.Equals(default).ShouldBeTrue();
            none.Equals((object)default(string)).ShouldBeTrue();
        }

        [Test]
        public void None_ShouldNotBeEqualToSome()
        {
            none.Equals(Maybe.Some("value")).ShouldBeFalse();
            none.Equals((object)Maybe.Some("value")).ShouldBeFalse();
        }

        [Test]
        public void None_ShouldNotBeEqualToSomeOtherType()
        {
            none.Equals(Maybe.Some(new StringBuilder())).ShouldBeFalse();
        }

        [Test]
        public void None_ShouldNotBeEqualToSomeUnderlying()
        {
            none.Equals("value").ShouldBeFalse();
            none.Equals((object)"value").ShouldBeFalse();
        }

        [Test]
        public void None_ShouldNotBeEqualToSomeOtherUnderlying()
        {
            none.Equals(1).ShouldBeFalse();
        }
    }
}