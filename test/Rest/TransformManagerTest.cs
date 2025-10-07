using BlackDigital.Rest.Transforms;

namespace BlackDigital.Test.Rest;

public class TransformManagerTest
{
    private readonly TransformManager _manager;
    private readonly MockTransformRule _mockRule1;
    private readonly MockTransformRule _mockRule2;
    private readonly MockTransformRule _mockRule3;

    public TransformManagerTest()
    {
        _manager = new TransformManager();
        _mockRule1 = new MockTransformRule("Rule1");
        _mockRule2 = new MockTransformRule("Rule2");
        _mockRule3 = new MockTransformRule("Rule3");
    }

    #region AddRule Tests

    [Fact]
    public void AddRule_WithTransformKey_ShouldAddRule()
    {
        // Arrange
        var key = new TransformKey("test", "2024-01-01", TransformDirection.Input);

        // Act
        var result = _manager.AddRule(key, _mockRule1);

        // Assert
        Assert.Same(_manager, result);
        Assert.True(_manager.HasRule(key));
    }

    [Fact]
    public void AddRule_WithStringParameters_ShouldAddRule()
    {
        // Arrange & Act
        var result = _manager.AddRule("test", "2024-01-01", _mockRule1, TransformDirection.Input);

        // Assert
        Assert.Same(_manager, result);
        Assert.True(_manager.HasRule("test", "2024-01-01", TransformDirection.Input));
    }

    [Fact]
    public void AddRule_WithMultipleKeys_ShouldAddRuleToAllKeys()
    {
        // Arrange
        var key1 = new TransformKey("test1", "2024-01-01", TransformDirection.Input);
        var key2 = new TransformKey("test2", "2024-01-01", TransformDirection.Output);
        var key3 = new TransformKey("test3", "2024-01-01", TransformDirection.Both);

        // Act
        var result = _manager.AddRule(_mockRule1, key1, key2, key3);

        // Assert
        Assert.Same(_manager, result);
        Assert.True(_manager.HasRule(key1));
        Assert.True(_manager.HasRule(key2));
        Assert.True(_manager.HasRule(key3));
    }

    [Fact]
    public void AddRule_WithDuplicateKey_ShouldThrowArgumentException()
    {
        // Arrange
        var key = new TransformKey("test", "2024-01-01", TransformDirection.Input);
        _manager.AddRule(key, _mockRule1);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _manager.AddRule(key, _mockRule2));
        Assert.Contains("already exists", exception.Message);
    }

    [Fact]
    public void AddRule_WithNoKeys_ShouldThrowArgumentException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _manager.AddRule(_mockRule1));
        Assert.Contains("At least one key must be provided", exception.Message);
    }

    #endregion

    #region HasRule Tests

    [Fact]
    public void HasRule_WithExistingKey_ShouldReturnTrue()
    {
        // Arrange
        var key = new TransformKey("test", "2024-01-01", TransformDirection.Input);
        _manager.AddRule(key, _mockRule1);

        // Act & Assert
        Assert.True(_manager.HasRule(key));
        Assert.True(_manager.HasRule("test", "2024-01-01", TransformDirection.Input));
    }

    [Fact]
    public void HasRule_WithNonExistingKey_ShouldReturnFalse()
    {
        // Arrange
        var key = new TransformKey("test", "2024-01-01", TransformDirection.Input);

        // Act & Assert
        Assert.False(_manager.HasRule(key));
        Assert.False(_manager.HasRule("test", "2024-01-01", TransformDirection.Input));
    }

    #endregion

    #region GetRule Tests

    [Fact]
    public void GetRule_WithExistingKey_ShouldReturnRule()
    {
        // Arrange
        var key = new TransformKey("test", "2024-01-01", TransformDirection.Input);
        _manager.AddRule(key, _mockRule1);

        // Act
        var result1 = _manager.GetRule(key);
        var result2 = _manager.GetRule("test", "2024-01-01", TransformDirection.Input);

        // Assert
        Assert.Same(_mockRule1, result1);
        Assert.Same(_mockRule1, result2);
    }

    [Fact]
    public void GetRule_WithNonExistingKey_ShouldReturnNull()
    {
        // Arrange
        var key = new TransformKey("test", "2024-01-01", TransformDirection.Input);

        // Act
        var result1 = _manager.GetRule(key);
        var result2 = _manager.GetRule("test", "2024-01-01", TransformDirection.Input);

        // Assert
        Assert.Null(result1);
        Assert.Null(result2);
    }

    [Fact]
    public void GetRequiredRule_WithExistingKey_ShouldReturnRule()
    {
        // Arrange
        var key = new TransformKey("test", "2024-01-01", TransformDirection.Input);
        _manager.AddRule(key, _mockRule1);

        // Act
        var result1 = _manager.GetRequiredRule(key);
        var result2 = _manager.GetRequiredRule("test", "2024-01-01", TransformDirection.Input);

        // Assert
        Assert.Same(_mockRule1, result1);
        Assert.Same(_mockRule1, result2);
    }

    [Fact]
    public void GetRequiredRule_WithNonExistingKey_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        var key = new TransformKey("test", "2024-01-01", TransformDirection.Input);

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => _manager.GetRequiredRule(key));
        Assert.Throws<KeyNotFoundException>(() => _manager.GetRequiredRule("test", "2024-01-01", TransformDirection.Input));
    }

    [Fact]
    public void TryGetRule_WithExistingKey_ShouldReturnTrueAndRule()
    {
        // Arrange
        var key = new TransformKey("test", "2024-01-01", TransformDirection.Input);
        _manager.AddRule(key, _mockRule1);

        // Act
        var result1 = _manager.TryGetRule(key, out var rule1);
        var result2 = _manager.TryGetRule("test", "2024-01-01", out var rule2, TransformDirection.Input);

        // Assert
        Assert.True(result1);
        Assert.Same(_mockRule1, rule1);
        Assert.True(result2);
        Assert.Same(_mockRule1, rule2);
    }

    [Fact]
    public void TryGetRule_WithNonExistingKey_ShouldReturnFalseAndNull()
    {
        // Arrange
        var key = new TransformKey("test", "2024-01-01", TransformDirection.Input);

        // Act
        var result1 = _manager.TryGetRule(key, out var rule1);
        var result2 = _manager.TryGetRule("test", "2024-01-01", out var rule2, TransformDirection.Input);

        // Assert
        Assert.False(result1);
        Assert.Null(rule1);
        Assert.False(result2);
        Assert.Null(rule2);
    }

    #endregion

    #region TryAddRule Tests

    [Fact]
    public void TryAddRule_WithNewKey_ShouldReturnTrueAndAddRule()
    {
        // Arrange
        var key = new TransformKey("test", "2024-01-01", TransformDirection.Input);

        // Act
        var result1 = _manager.TryAddRule(key, _mockRule1);
        var result2 = _manager.TryAddRule("test2", "2024-01-01", _mockRule2, TransformDirection.Input);

        // Assert
        Assert.True(result1);
        Assert.True(_manager.HasRule(key));
        Assert.True(result2);
        Assert.True(_manager.HasRule("test2", "2024-01-01", TransformDirection.Input));
    }

    [Fact]
    public void TryAddRule_WithExistingKey_ShouldReturnFalse()
    {
        // Arrange
        var key = new TransformKey("test", "2024-01-01", TransformDirection.Input);
        _manager.AddRule(key, _mockRule1);

        // Act
        var result1 = _manager.TryAddRule(key, _mockRule2);
        var result2 = _manager.TryAddRule("test", "2024-01-01", _mockRule3, TransformDirection.Input);

        // Assert
        Assert.False(result1);
        Assert.False(result2);
        Assert.Same(_mockRule1, _manager.GetRule(key)); // Original rule should remain
    }

    #endregion

    #region ReplaceRule Tests

    [Fact]
    public void ReplaceRule_WithExistingKey_ShouldReplaceRule()
    {
        // Arrange
        var key = new TransformKey("test", "2024-01-01", TransformDirection.Input);
        _manager.AddRule(key, _mockRule1);

        // Act
        var result1 = _manager.ReplaceRule(key, _mockRule2);
        var result2 = _manager.ReplaceRule("test2", "2024-01-01", _mockRule3, TransformDirection.Input);

        // Assert
        Assert.Same(_manager, result1);
        Assert.Same(_mockRule2, _manager.GetRule(key));
        Assert.Same(_manager, result2);
        Assert.Same(_mockRule3, _manager.GetRule("test2", "2024-01-01", TransformDirection.Input));
    }

    [Fact]
    public void ReplaceRule_WithNonExistingKey_ShouldCreateNewRule()
    {
        // Arrange
        var key = new TransformKey("test", "2024-01-01", TransformDirection.Input);

        // Act
        var result = _manager.ReplaceRule(key, _mockRule1);

        // Assert
        Assert.Same(_manager, result);
        Assert.Same(_mockRule1, _manager.GetRule(key));
    }

    #endregion

    #region RemoveRule Tests

    [Fact]
    public void RemoveRule_WithExistingKey_ShouldReturnTrueAndRemoveRule()
    {
        // Arrange
        var key = new TransformKey("test", "2024-01-01", TransformDirection.Input);
        _manager.AddRule(key, _mockRule1);

        // Act
        var result1 = _manager.RemoveRule(key);
        var result2 = _manager.RemoveRule("test2", "2024-01-01", TransformDirection.Input);

        // Assert
        Assert.True(result1);
        Assert.False(_manager.HasRule(key));
        Assert.False(result2); // This key was never added
    }

    [Fact]
    public void RemoveRule_WithNonExistingKey_ShouldReturnFalse()
    {
        // Arrange
        var key = new TransformKey("test", "2024-01-01", TransformDirection.Input);

        // Act
        var result1 = _manager.RemoveRule(key);
        var result2 = _manager.RemoveRule("test", "2024-01-01", TransformDirection.Input);

        // Assert
        Assert.False(result1);
        Assert.False(result2);
    }

    #endregion

    #region GetRequiredRules Tests

    [Fact]
    public void GetRequiredRules_WithVersionsGreaterThanSpecified_ShouldReturnOrderedRules()
    {
        // Arrange
        _manager.AddRule("test", "2024-06-15", _mockRule2, TransformDirection.Input);
        _manager.AddRule("test", "2024-01-01", _mockRule1, TransformDirection.Input);
        _manager.AddRule("test", "2025-01-01", _mockRule3, TransformDirection.Input);

        var key = new TransformKey("test", "2024-01-01", TransformDirection.Input);

        // Act
        var result1 = _manager.GetRequiredRules(key);
        var result2 = _manager.GetRequiredRules("test", "2024-01-01", TransformDirection.Input);

        // Assert
        Assert.Equal(2, result1.Count);
        Assert.Same(_mockRule2, result1[0]); // 2024-06-15 comes first
        Assert.Same(_mockRule3, result1[1]); // 2025-01-01 comes second

        Assert.Equal(2, result2.Count);
        Assert.Same(_mockRule2, result2[0]);
        Assert.Same(_mockRule3, result2[1]);
    }

    [Fact]
    public void GetRequiredRules_WithDifferentDirections_ShouldOnlyReturnMatchingDirection()
    {
        // Arrange
        _manager.AddRule("test", "2024-06-15", _mockRule1, TransformDirection.Input);
        _manager.AddRule("test", "2024-06-15", _mockRule2, TransformDirection.Output);
        _manager.AddRule("test", "2025-01-01", _mockRule3, TransformDirection.Input);

        var inputKey = new TransformKey("test", "2024-01-01", TransformDirection.Input);
        var outputKey = new TransformKey("test", "2024-01-01", TransformDirection.Output);

        // Act
        var inputRules = _manager.GetRequiredRules(inputKey);
        var outputRules = _manager.GetRequiredRules(outputKey);

        // Assert
        Assert.Equal(2, inputRules.Count);
        Assert.Contains(_mockRule1, inputRules);
        Assert.Contains(_mockRule3, inputRules);

        Assert.Single(outputRules);
        Assert.Contains(_mockRule2, outputRules);
    }

    [Fact]
    public void GetRequiredRules_WithNoMatchingRules_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        var key = new TransformKey("test", "2025-01-01", TransformDirection.Input);

        // Act & Assert
        var exception1 = Assert.Throws<KeyNotFoundException>(() => _manager.GetRequiredRules(key));
        var exception2 = Assert.Throws<KeyNotFoundException>(() => _manager.GetRequiredRules("test", "2025-01-01", TransformDirection.Input));

        Assert.Contains("No rules found", exception1.Message);
        Assert.Contains("No rules found", exception2.Message);
    }

    [Fact]
    public void GetRequiredRules_WithVersionOrdering_ShouldOrderCorrectly()
    {
        // Arrange - Add rules in random order
        _manager.AddRule("test", "2024-12-31", _mockRule3, TransformDirection.Input);
        _manager.AddRule("test", "2024-02-01", _mockRule1, TransformDirection.Input);
        _manager.AddRule("test", "2024-06-15", _mockRule2, TransformDirection.Input);

        var key = new TransformKey("test", "2024-01-01", TransformDirection.Input);

        // Act
        var result = _manager.GetRequiredRules(key);

        // Assert - Should be ordered by version
        Assert.Equal(3, result.Count);
        Assert.Same(_mockRule1, result[0]); // 2024-02-01
        Assert.Same(_mockRule2, result[1]); // 2024-06-15
        Assert.Same(_mockRule3, result[2]); // 2024-12-31
    }

    #endregion

    #region Transform Tests

    [Fact]
    public void Transform_WithSingleRule_ShouldApplyTransformation()
    {
        // Arrange
        _manager.AddRule("test", "2024-06-15", _mockRule1, TransformDirection.Input);
        var key = new TransformKey("test", "2024-01-01", TransformDirection.Input);
        var input = "input";

        // Act
        var result1 = _manager.Transform(key, input);
        var result2 = _manager.Transform("test", "2024-01-01", input, TransformDirection.Input);

        // Assert
        Assert.Equal("input_Rule1", result1);
        Assert.Equal("input_Rule1", result2);
    }

    [Fact]
    public void Transform_WithMultipleRules_ShouldApplyPipeline()
    {
        // Arrange
        _manager.AddRule("test", "2024-06-15", _mockRule2, TransformDirection.Input);
        _manager.AddRule("test", "2024-02-01", _mockRule1, TransformDirection.Input);
        _manager.AddRule("test", "2024-12-31", _mockRule3, TransformDirection.Input);

        var key = new TransformKey("test", "2024-01-01", TransformDirection.Input);
        var input = "input";

        // Act
        var result = _manager.Transform(key, input);

        // Assert
        // Should apply in order: Rule1 -> Rule2 -> Rule3
        Assert.Equal("input_Rule1_Rule2_Rule3", result);
    }

    [Fact]
    public async Task TransformAsync_WithSingleRule_ShouldApplyTransformation()
    {
        // Arrange
        _manager.AddRule("test", "2024-06-15", _mockRule1, TransformDirection.Input);
        var key = new TransformKey("test", "2024-01-01", TransformDirection.Input);
        var input = "input";

        // Act
        var result1 = await _manager.TransformAsync(key, input);
        var result2 = await _manager.TransformAsync("test", "2024-01-01", input, TransformDirection.Input);

        // Assert
        Assert.Equal("input_Rule1_Async", result1);
        Assert.Equal("input_Rule1_Async", result2);
    }

    [Fact]
    public async Task TransformAsync_WithMultipleRules_ShouldApplyPipeline()
    {
        // Arrange
        _manager.AddRule("test", "2024-06-15", _mockRule2, TransformDirection.Input);
        _manager.AddRule("test", "2024-02-01", _mockRule1, TransformDirection.Input);
        _manager.AddRule("test", "2024-12-31", _mockRule3, TransformDirection.Input);

        var key = new TransformKey("test", "2024-01-01", TransformDirection.Input);
        var input = "input";

        // Act
        var result = await _manager.TransformAsync(key, input);

        // Assert
        // Should apply in order: Rule1 -> Rule2 -> Rule3 (all async)
        Assert.Equal("input_Rule1_Async_Rule2_Async_Rule3_Async", result);
    }

    [Fact]
    public void Transform_WithNullValue_ShouldHandleNull()
    {
        // Arrange
        _manager.AddRule("test", "2024-06-15", _mockRule1, TransformDirection.Input);
        var key = new TransformKey("test", "2024-01-01", TransformDirection.Input);

        // Act
        var result = _manager.Transform(key, null);

        // Assert
        Assert.Equal("null_Rule1", result);
    }

    #endregion

    #region Mock Classes

    private class MockTransformRule : ITransformRule
    {
        private readonly string _suffix;

        public MockTransformRule(string suffix)
        {
            _suffix = suffix;
        }

        public object? Transform(object? value)
        {
            var input = value?.ToString() ?? "null";
            return $"{input}_{_suffix}";
        }

        public Task<object?> TransformAsync(object? value)
        {
            var input = value?.ToString() ?? "null";
            return Task.FromResult<object?>($"{input}_{_suffix}_Async");
        }
    }

    #endregion
}