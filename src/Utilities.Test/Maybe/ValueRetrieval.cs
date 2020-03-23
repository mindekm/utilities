namespace Utilities.Test.Maybe
{
    using System;
    using Utilities;
    using Shouldly;
    using Xunit;

    public class ValueRetrieval
    {
        [Fact]
        public void Maybe_TryGet_ShouldReturnTrueForSome()
        {
            const string Expected = "Test";

            Maybe.Some(Expected).TryUnwrap(out var result).ShouldBeTrue();
            result.ShouldBe(Expected);
        }

        [Fact]
        public void Maybe_TryGet_ShouldReturnFalseForNone()
        {
            default(Maybe<string>).TryUnwrap(out var result).ShouldBeFalse();
            result.ShouldBe(default);
        }

        [Fact]
        public void Maybe_Value_ShouldThrowForNone()
        {
            Should.Throw<InvalidOperationException>(() => default(Maybe<string>).Unwrap());
        }

        [Fact]
        public void Maybe_Value_ShouldReturnValueForSome()
        {
            const string Expected = "Test";

            Maybe.Some(Expected).Unwrap().ShouldBe(Expected);
        }

        [Fact]
        public void Maybe_OrDefault_ShouldReturnValueForSome()
        {
            const string Expected = "Test";

            Maybe.Some(Expected).UnwrapOrDefault().ShouldBe(Expected);
        }

        [Fact]
        public void Maybe_OrDefault_ShouldReturnDefaultForNone()
        {
            default(Maybe<string>).UnwrapOrDefault().ShouldBe(default);
        }
    }
}