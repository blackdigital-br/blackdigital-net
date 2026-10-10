# Data Builder

## Introduction

The `BlackDigital.DataBuilder` namespace provides metadata inspection helpers for models. It is useful when you need to build dynamic forms, admin screens, low-code editors, or API-driven UI definitions based on existing .NET attributes.

Instead of manually describing each field twice, you can inspect a type and reuse metadata that already exists in attributes such as `Display`, `DataType`, `Range`, `MaxLength`, `MinLength`, `Editable`, and BlackDigital visibility attributes.

## Main Components

### TypeBuilder

`TypeBuilder` inspects a .NET type and exposes its writable/readable properties as `PropertyBuilder` instances.

**Main members:**
- `Create<T>()` - Creates a builder for a model type
- `Properties` - Original property list
- `OrderProperties` - Properties ordered by `DisplayAttribute.Order`

```csharp
var builder = TypeBuilder.Create<ProductForm>();

foreach (var property in builder.OrderProperties)
{
    Console.WriteLine($"{property.PropertyName} -> {property.ComponentType}");
}
```

### PropertyBuilder

`PropertyBuilder` wraps a reflected property and exposes a UI-friendly view of its metadata.

**Main members:**
- `PropertyName` - Raw CLR property name
- `Name` - Display name or property name
- `Description` - `DisplayAttribute.Description`
- `Prompt` - `DisplayAttribute.Prompt`
- `Order` - Display order
- `ComponentType` - Resolved field type for UI generation
- `CurrentDataType` - Parsed `DataType` enum value when available
- `MinLength` / `MaxLength`
- `MinValue` / `MaxValue`
- `AllowEdit`
- `PropertyType`
- `Show(object value)` - Evaluates visibility rules based on `ShowAttribute`
- `GetValue(object model)` / `SetValue(object model, object value)`

```csharp
var builder = TypeBuilder.Create<ProductForm>();
var nameProperty = builder.Properties.First(p => p.PropertyName == "Name");

Console.WriteLine(nameProperty.Name);
Console.WriteLine(nameProperty.MaxLength);
Console.WriteLine(nameProperty.AllowEdit);
```

## Type Inference

When a property does not declare a `DataTypeAttribute`, `PropertyBuilder` infers a `DataType` from the CLR type.

**Built-in mappings include:**
- `bool` -> `Boolean`
- `short`, `int`, `long`, `sbyte` -> `Integer`
- `byte`, `ushort`, `uint`, `ulong` -> `UnsignedInteger`
- `float`, `double`, `decimal` -> `Decimal`
- `DateTime`, `DateTimeOffset` -> `DateTime`
- `DateOnly` -> `Date`
- `TimeOnly` -> `Time`
- `TimeSpan` -> `TimeSpan`
- enums -> `Enumeration`
- collections -> `List`
- everything else -> `Text`

This mapping is centralized in `DataTypeTable`, which makes the behavior predictable for UI generation.

## Practical Example

```csharp
using System.ComponentModel.DataAnnotations;
using BlackDigital.DataAnnotations;
using BlackDigital.DataBuilder;

public class ProductForm
{
    [Display(Name = "Product name", Order = 1, Prompt = "Type the product name")]
    [MaxLength(120)]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Price", Order = 2)]
    [Range(typeof(decimal), "0", "999999")]
    public decimal Price { get; set; }

    [Display(Name = "Category", Order = 3)]
    public ProductCategory Category { get; set; }

    [ShowIfProperty("Category", ProductCategory.Service)]
    public int? DurationInMinutes { get; set; }
}

var builder = TypeBuilder.Create<ProductForm>();

foreach (var property in builder.OrderProperties)
{
    Console.WriteLine($"{property.Name} ({property.ComponentType})");
}
```

Possible output:

```text
Product name (Text)
Price (Decimal)
Category (Enumeration)
DurationInMinutes (Integer)
```

## Dynamic Form Scenario

One common usage is converting a model into a generic field definition:

```csharp
var fields = TypeBuilder.Create<ProductForm>()
    .OrderProperties
    .Select(property => new
    {
        property.PropertyName,
        property.Name,
        property.ComponentType,
        Required = property.MinLength.HasValue && property.MinLength.Value > 0,
        property.MinLength,
        property.MaxLength,
        property.MinValue,
        property.MaxValue,
        property.AllowEdit
    })
    .ToList();
```

This is especially useful when:
- building backoffice screens
- exposing model metadata to a frontend
- generating forms from shared contracts
- centralizing validation and display rules

## Notes

- `TypeBuilder` only includes properties that can be read and written
- `ComponentType` prefers `DataTypeAttribute` when it exists
- visibility rules can reuse BlackDigital `ShowAttribute` derivatives
- `GetValue` and `SetValue` allow generic read/write flows without extra reflection code

## Conclusion

`DataBuilder` is a lightweight bridge between model metadata and UI generation. It helps you keep validation, display, and field-shape rules close to the model while still enabling dynamic experiences.
