namespace Utilities.Test;

using System;
using System.Collections.Generic;

public class GuardTestSuite
{
    [Test]
    public async ValueTask Guard_NotNull_ShouldThrowOnNullInput()
    {
        await Assert.That(() => Guard.NotNull(null)).Throws<ArgumentNullException>();
    }

    [Test]
    public async ValueTask Guard_NotNull_ShouldNotThrowOnNonNullInput()
    {
        await Assert.That(() => Guard.NotNull(string.Empty)).ThrowsNothing();
    }

    [Test]
    public async ValueTask Guard_NotDefault_ShouldThrowOnUninitializedInput()
    {
        await Assert.That(() => Guard.NotDefault(default(int))).Throws<ArgumentException>();
    }

    [Test]
    public async ValueTask Guard_NotDefault_ShouldNotThrowOnInitializedInput()
    {
        await Assert.That(() => Guard.NotDefault(1)).ThrowsNothing();
    }

    [Test]
    public async ValueTask Guard_HasValue_ShouldThrowOnNullableWithoutValue()
    {
        await Assert.That(() => Guard.HasValue(new int?())).Throws<ArgumentException>();
    }

    [Test]
    public async ValueTask Guard_HasValue_ShouldNotThrowOnNullableWithValue()
    {
        await Assert.That(() => Guard.HasValue(new int?(1))).ThrowsNothing();
    }

    [Test]
    public async ValueTask Guard_HasElements_ShouldThrowOnNullCollection()
    {
        await Assert.That(() => Guard.HasElements((List<string>)null)).Throws<ArgumentNullException>();
    }

    [Test]
    public async ValueTask Guard_HasElements_ShouldThrowOnEmptyCollection()
    {
        await Assert.That(() => Guard.HasElements(new List<string>())).Throws<ArgumentException>();
    }

    [Test]
    public async ValueTask Guard_HasElements_ShouldNotThrowOnCollectionWithElements()
    {
        await Assert.That(() => Guard.HasElements(new List<string>{"Test"})).ThrowsNothing();
    }

    [Test]
    public async ValueTask Guard_NotNullOrEmpty_ShouldThrowOnNullString()
    {
        await Assert.That(() => Guard.NotNullOrEmpty(null)).Throws<ArgumentNullException>();
    }

    [Test]
    public async ValueTask Guard_NotNullOrEmpty_ShouldThrowOnEmptyString()
    {
        await Assert.That(() => Guard.NotNullOrEmpty(string.Empty)).Throws<ArgumentException>();
    }

    [Test]
    [Arguments(" ")]
    [Arguments("Test")]
    public async ValueTask Guard_NotNullOrEmpty_ShouldNotThrowOnNonEmptyString(string value)
    {
        await Assert.That(() => Guard.NotNullOrEmpty(value)).ThrowsNothing();
    }

    [Test]
    public async ValueTask Guard_NotNullOrWhitespace_ShouldThrowOnNullString()
    {
        await Assert.That(() => Guard.NotNullOrWhitespace(null)).Throws<ArgumentNullException>();
    }

    [Test]
    [Arguments("")]
    [Arguments(" ")]
    public async ValueTask Guard_NotNullOrWhitespace_ShouldThrowOnEmptyOrWhitespaceString(string value)
    {
        await Assert.That(() => Guard.NotNullOrWhitespace(value)).Throws<ArgumentException>();
    }

    [Test]
    public async ValueTask Guard_NotNullOrWhitespace_ShouldNotThrowOnNonEmptyOrWhitespaceString()
    {
        await Assert.That(() => Guard.NotNullOrWhitespace("Test")).ThrowsNothing();
    }
}
