namespace Utilities.Test
{
    using System;
    using Utilities;
    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    public class ValueRetrieval
    {
        [Test]
        public void Maybe_TryGet_ShouldReturnTrueForSome()
        {
            const string Expected = "Test";

            Maybe.Some(Expected).TryGetValue(out var result).ShouldBeTrue();
            result.ShouldBe(Expected);
        }

        [Test]
        public void Maybe_TryGet_ShouldReturnFalseForNone()
        {
            Maybe.None<string>().TryGetValue(out var result).ShouldBeFalse();
            result.ShouldBe(default);
        }

        [Test]
        public void Maybe_Value_ShouldThrowForNone()
        {
            Should.Throw<InvalidOperationException>(() => Maybe.None<string>().GetValue());
        }

        [Test]
        public void Maybe_Value_ShouldReturnValueForSome()
        {
            const string Expected = "Test";

            Maybe.Some(Expected).GetValue().ShouldBe(Expected);
        }

        [Test]
        public void Maybe_ExplicitCast_ShouldThrowForNone()
        {
            Should.Throw<InvalidCastException>(() =>
            {
                var result = (string)Maybe.None<string>();
            });
        }

        [Test]
        public void Maybe_ExplicitCast_ShouldReturnValueForSome()
        {
            const string Expected = "Test";

            ((string)Maybe.Some(Expected)).ShouldBe(Expected);
        }

        [Test]
        public void Maybe_OrDefault_ShouldReturnValueForSome()
        {
            const string Expected = "Test";

            Maybe.Some(Expected).GetValueOrDefault().ShouldBe(Expected);
        }

        [Test]
        public void Maybe_OrDefault_ShouldReturnDefaultForNone()
        {
            Maybe.None<string>().GetValueOrDefault().ShouldBe(default);
        }
    }
}