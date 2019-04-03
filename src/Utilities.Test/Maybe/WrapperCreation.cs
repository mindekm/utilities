namespace Utilities.Test
{
    using System;
    using Utilities;
    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    public class WrapperCreation
    {
        [Test]
        public void Maybe_Some_ShouldCreateSome()
        {
            Maybe.Some("test").IsSome.ShouldBeTrue();
        }

        [Test]
        public void Maybe_Some_ShouldNotAcceptNull()
        {
            Should.Throw<ArgumentNullException>(() => Maybe.Some((string)null));
        }

        [Test]
        public void Maybe_None_ShouldCreateNone()
        {
            Maybe.None<string>().IsNone.ShouldBeTrue();
        }

        [Test]
        public void Maybe_ImplicitCast_ShouldCreateSomeFromNonNullvalue()
        {
            Maybe<string> test = "test";
            test.IsSome.ShouldBeTrue();
        }

        [Test]
        public void Maybe_ImplicitCase_ShouldCreateNoneFromNull()
        {
            Maybe<string> test = null;
            test.IsNone.ShouldBeTrue();
        }
    }
}