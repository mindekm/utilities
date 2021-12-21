namespace Utilities.Test;

using System;
using System.Collections.Generic;
using Shouldly;
using Xunit;

public class GuardTestSuite
{
    [Fact]
    public void Guard_NotNull_ShouldThrowOnNullInput()
    {
        Should.Throw<ArgumentNullException>(() => Guard.NotNull(null));
    }

    [Fact]
    public void Guard_NotNull_ShouldNotThrowOnNonNullInput()
    {
        Should.NotThrow(() => Guard.NotNull(string.Empty));
    }

    [Fact]
    public void Guard_NotDefault_ShouldThrowOnUninitializedInput()
    {
        Should.Throw<ArgumentException>(() => Guard.NotDefault(default(int)));
    }

    [Fact]
    public void Guard_NotDefault_ShouldNotThrowOnInitializedInput()
    {
        Should.NotThrow(() => Guard.NotDefault(1));
    }

    [Fact]
    public void Guard_HasValue_ShouldThrowOnNullableWithoutValue()
    {
        Should.Throw<ArgumentException>(() => Guard.HasValue(new int?()));
    }

    [Fact]
    public void Guard_HasValue_ShouldNotThrowOnNullableWithValue()
    {
        Should.NotThrow(() => Guard.HasValue(new int?(1)));
    }

    [Fact]
    public void Guard_HasElements_ShouldThrowOnNullCollection()
    {
        Should.Throw<ArgumentNullException>(() => Guard.HasElements((List<string>) null));
    }

    [Fact]
    public void Guard_HasElements_ShouldThrowOnEmptyCollection()
    {
        Should.Throw<ArgumentException>(() => Guard.HasElements(new List<string>()));
    }

    [Fact]
    public void Guard_HasElements_ShouldNotThrowOnCollectionWithElements()
    {
        Should.NotThrow(() => Guard.HasElements(new List<string> { "Test" }));
    }

    [Fact]
    public void Guard_NotNullOrEmpty_ShouldThrowOnNullString()
    {
        Should.Throw<ArgumentNullException>(() => Guard.NotNullOrEmpty(null));
    }

    [Fact]
    public void Guard_NotNullOrEmpty_ShouldThrowOnEmptyString()
    {
        Should.Throw<ArgumentException>(() => Guard.NotNullOrEmpty(string.Empty));
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("Test")]
    public void Guard_NotNullOrEmpty_ShouldNotThrowOnNonEmptyString(string value)
    {
        Should.NotThrow(() => Guard.NotNullOrEmpty(value));
    }

    [Fact]
    public void Guard_NotNullOrWhitespace_ShouldThrowOnNullString()
    {
        Should.Throw<ArgumentNullException>(() => Guard.NotNullOrWhitespace(null));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Guard_NotNullOrWhitespace_ShouldThrowOnEmptyOrWhitespaceString(string value)
    {
        Should.Throw<ArgumentException>(() => Guard.NotNullOrWhitespace(value));
    }

    [Fact]
    public void Guard_NotNullOrWhitespace_ShouldNotThrowOnNonEmptyOrWhitespaceString()
    {
        Should.NotThrow(() => Guard.NotNullOrWhitespace("Test"));
    }
}