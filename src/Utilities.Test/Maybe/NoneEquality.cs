namespace Utilities.Test.Maybe
{
    using System.Text;
    using Utilities;
    using Shouldly;
    using Xunit;

    public class NoneEquality
    {
        private readonly Maybe<string> none = Maybe.None;

        [Fact]
        public void None_ShouldBeEqualToSelf()
        {
            none.Equals(none).ShouldBeTrue();
            none.Equals((object)none).ShouldBeTrue();
        }

        [Fact]
        public void None_ShouldBeEqualToOtherNone()
        {
            none.Equals(Maybe.None).ShouldBeTrue();
            none.Equals((object)default(Maybe<string>)).ShouldBeTrue();
        }

        [Fact]
        public void None_ShouldBeEqualToDefaultValue()
        {
            none.Equals(default).ShouldBeTrue();
            none.Equals((object)default(Maybe<string>)).ShouldBeTrue();
        }

        [Fact]
        public void None_ShouldNotBeEqualToSome()
        {
            none.Equals(Maybe.Some("value")).ShouldBeFalse();
            none.Equals((object)Maybe.Some("value")).ShouldBeFalse();
        }

        [Fact]
        public void None_ShouldNotBeEqualToSomeOtherType()
        {
            none.Equals(Maybe.Some(new StringBuilder())).ShouldBeFalse();
        }
    }
}