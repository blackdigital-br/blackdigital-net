# Documentation - Model

## Introduction

The `Model` folder contains utility classes for working with entities, filters, and identifiers in the BlackDigital system. Unlike other modules, these classes are more independent of each other, offering specific functionalities for different aspects of development.

## Identification Interfaces

### IId and Id

**Purpose**: Standardize working with unique identifiers in entities.

#### IId
Generic interface that defines `Id` properties for different key types:
- `IId` - Generic Id
- `IId<TKey>` - Typed Id (byte, short, int, long, Guid, etc.)

#### Id (struct)
Structure that encapsulates different types of identifiers and allows automatic conversions.

**Usage example**:
```csharp
public class User : IId
{
    public Id Id { get; set; }
    public string Name { get; set; }
}

// Automatic conversions
Id id1 = new Id("123");
Id id2 = new Id(Guid.NewGuid());
Id id3 = new Id(456);

// Verification
if (id1.HasId)
{
    Console.WriteLine($"ID: {id1}");
}
```

## Audit Interfaces

### IActive
**Purpose**: Control whether an entity is active or inactive.

```csharp
public class Product : IActive
{
    public Id Id { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; }
}
```

### ICreated
**Purpose**: Track when an entity was created.

```csharp
public class Order : ICreated
{
    public Id Id { get; set; }
    public DateTime Created { get; set; }
}
```

### IUpdated
**Purpose**: Track when an entity was updated (inherits from ICreated).

```csharp
public class Customer : IUpdated
{
    public Id Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
```

### IDeleted
**Purpose**: Implement logical deletion (soft delete) - inherits from IUpdated.

```csharp
public class Document : IDeleted
{
    public Id Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public DateTime? Deleted { get; set; }
}
```

## Filter Classes

### BaseFilter
**Purpose**: Base class for pagination and sorting of queries.

**Main properties**:
- `Skip` - Number of records to skip
- `Take` - Number of records to return
- `Sort` - List of sorting criteria

```csharp
var filter = new BaseFilter
{
    Skip = 10,
    Take = 20,
    Sort = new List<SortItem>
    {
        new SortItem("Name", true), // Ascending
        new SortItem("Created", false) // Descending
    }
};

var result = query.ApplyFilter(filter);
```

### ActiveFilter
**Purpose**: Filter entities by active/inactive status.

```csharp
// Search only active records
var active = query.FilterActive(true);

// Search only inactive records
var inactive = query.FilterActive(false);

// Search all (active and inactive)
var all = query.FilterActive(null);
```

### CreatedFilter
**Purpose**: Filter entities by creation date.

```csharp
// Search by specific date
var byDate = query.FindCreated(DateTime.Today);

// Search by period
var byPeriod = query.FilterCreatedRange(
    DateTime.Today.AddDays(-30), 
    DateTime.Today
);

// Search created after a date
var recent = query.FilterMinCreated(DateTime.Today.AddDays(-7));
```

### UpdatedFilter
**Purpose**: Filter entities by update date.

```csharp
// Search updated today
var updatedToday = query.FindUpdated(DateTime.Today);

// Search updated in the last month
var lastMonth = query.FilterUpdatedRange(
    DateTime.Today.AddMonths(-1),
    DateTime.Today
);
```

### DeletedFilter
**Purpose**: Filter entities by deletion status.

```csharp
// Search only non-deleted records
var notDeleted = query.HasNotDeleted();

// Search only deleted records
var deleted = query.HasDeleted();

// Search deleted in a period
var recentlyDeleted = query.FilterDeletedRange(
    DateTime.Today.AddDays(-30),
    DateTime.Today
);
```

### IdFilter
**Purpose**: Filter entities by identifier.

```csharp
// Search by specific ID
var byId = query.FindId(new Id(123));

// Filter by ID (if provided)
var filtered = query.FilterId(optionalId);

// For typed IDs
var byTypedId = query.FindId<User, int>(456);
```

### BaseFilterFilter
**Purpose**: Apply BaseFilter filters (pagination and sorting) to queries.

```csharp
// Apply complete filter (sorting + pagination)
var result = query.ApplyFilter(filter);

// Apply only sorting
var sorted = query.ApplyOnlyOrderBy(filter);

// Apply only pagination
var paginated = query.ApplyOnlyFilter(filter);
```

## Option Classes

### OptionItem
**Purpose**: Represent an option item with identifier, label, and connections.

**Properties**:
- `Id` - Unique identifier
- `Label` - Display text
- `Code` - Optional code
- `Description` - Optional description
- `Connections` - Dictionary of connections with other entities

```csharp
var option = new OptionItem
{
    Id = new Id(1),
    Label = "Category A",
    Code = "CAT_A",
    Description = "First category",
    Connections = new Dictionary<string, ListId>
    {
        ["subcategories"] = new ListId(new[] { 10, 11, 12 })
    }
};
```

### Options
**Purpose**: Collection of OptionItem with search and filter methods.

```csharp
var options = new Options(optionList);

// Search by ID
var option = options[1];

// Check if exists
bool exists = options.ContainsKey(1);

// Filter by connections
var filtered = options.FilterConnections("category", new Id(5));

// Search by text
var found = options.FindText("product");

// Filter by label
var byLabel = options.FilterLabel("category");
```

## Auxiliary Classes

### ListId
**Purpose**: Specialized list for working with identifiers, with automatic conversions.

```csharp
// Creation in different ways
var list1 = new ListId(new[] { "1", "2", "3" });
var list2 = new ListId(new[] { 1, 2, 3 });
var list3 = new ListId(new[] { Guid.NewGuid(), Guid.NewGuid() });

// Implicit conversions
ListId fromArray = new int[] { 1, 2, 3 };
ListId fromList = new List<string> { "a", "b", "c" };

// Conversions to other types
int[] toArray = list1;
List<Guid> toGuidList = list1;
```

### SortItem
**Purpose**: Define sorting criteria for queries.

```csharp
// Ascending sort
var sortAsc = new SortItem("Name", true);

// Descending sort
var sortDesc = new SortItem("Created", false);

// Default sort (ascending)
var sortDefault = new SortItem("Id");

// Usage in BaseFilter
var filter = new BaseFilter
{
    Sort = new List<SortItem>
    {
        new SortItem("Category"),
        new SortItem("Created", false)
    }
};
```

## System Advantages

### 1. **Standardization**
- Consistent interfaces for auditing and identification
- Uniform patterns throughout the application

### 2. **Flexibility**
- Support for different types of identifiers
- Composable and reusable filters

### 3. **Ease of Use**
- Automatic conversions between types
- Intuitive extension methods

### 4. **Maintainability**
- Organized and modular code
- Easy extension and customization

## Complete Practical Example

```csharp
// Define entity
public class Product : IId, IActive, ICreated, IUpdated, IDeleted
{
    public Id Id { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public DateTime? Deleted { get; set; }
}

// Use filters
var filter = new BaseFilter
{
    Skip = 0,
    Take = 10,
    Sort = new List<SortItem> { new SortItem("Name") }
};

var products = query
    .HasNotDeleted()           // Not deleted
    .FilterActive(true)        // Only active
    .FilterMinCreated(DateTime.Today.AddDays(-30)) // Created in the last 30 days
    .ApplyFilter(filter);      // Pagination and sorting

// Work with options
var productOptions = new Options(products.Select(p => new OptionItem
{
    Id = p.Id,
    Label = p.Name,
    Code = p.Id.ToString()
}));

var foundProduct = productOptions.FindText("notebook");
```

This Model system provides a solid and flexible foundation for working with entities, filters, and identifiers, keeping the code organized and reusable.