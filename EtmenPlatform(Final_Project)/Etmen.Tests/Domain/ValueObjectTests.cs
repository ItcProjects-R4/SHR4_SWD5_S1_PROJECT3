using Etmen.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Etmen.Tests.Domain;

/// <summary>Unit tests for the RiskScore value object.</summary>
public sealed class RiskScoreTests
{
    [Theory]
    [InlineData(0.0)]
    [InlineData(0.5)]
    [InlineData(1.0)]
    public void Constructor_WithValidValue_ShouldSucceed(double value)
    {
        var score = new RiskScore(value);
        score.Value.Should().Be(value);
    }

    [Theory]
    [InlineData(-0.01)]
    [InlineData(1.01)]
    [InlineData(-100)]
    public void Constructor_WithOutOfRangeValue_ShouldThrow(double value)
    {
        var act = () => new RiskScore(value);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void ImplicitConversion_ToDouble_ShouldWork()
    {
        var score  = new RiskScore(0.75);
        double val = score;
        val.Should().Be(0.75);
    }

    [Fact]
    public void ToString_ShouldReturnPercentFormat()
    {
        var score = new RiskScore(0.75);
        score.ToString().Should().Contain("%");
    }

    [Fact]
    public void TwoScoresWithSameValue_ShouldBeEqual()
    {
        var a = new RiskScore(0.5);
        var b = new RiskScore(0.5);
        a.Should().Be(b);
    }
}

/// <summary>Unit tests for the BloodPressure value object.</summary>
public sealed class BloodPressureTests
{
    [Fact]
    public void Constructor_WithValidValues_ShouldSucceed()
    {
        var bp = new BloodPressure(120, 80);
        bp.Systolic.Should().Be(120);
        bp.Diastolic.Should().Be(80);
    }

    [Theory]
    [InlineData(0,  80)]
    [InlineData(120, 0)]
    [InlineData(-10, 80)]
    public void Constructor_WithInvalidValues_ShouldThrow(double sys, double dia)
    {
        var act = () => new BloodPressure(sys, dia);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void IsHypertensive_WhenSystolicAbove140_ShouldBeTrue()
    {
        var bp = new BloodPressure(145, 90);
        bp.IsHypertensive.Should().BeTrue();
    }

    [Fact]
    public void IsHypertensive_WhenNormal_ShouldBeFalse()
    {
        var bp = new BloodPressure(118, 76);
        bp.IsHypertensive.Should().BeFalse();
    }

    [Fact]
    public void ToString_ShouldReturnCorrectFormat()
    {
        var bp = new BloodPressure(120, 80);
        bp.ToString().Should().Be("120/80 mmHg");
    }
}
