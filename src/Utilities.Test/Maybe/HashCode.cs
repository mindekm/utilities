namespace Utilities.Test
{
    using Utilities;
    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    public class HashCode
    {
        [Test]
        public void Maybe_GetHashCode_ShouldReturnUnderlyingHashCodeForSome()
        {
            const string Test = "Test";

            Maybe.Some(Test).GetHashCode().ShouldBe(Test.GetHashCode());
        }

        [Test]
        public void Maybe_GetHashCode_ShouldReturnZeroForNone()
        {
            Maybe.None<string>().GetHashCode().ShouldBe(0);
        }
    }
}