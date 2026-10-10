# Model Reference

## Identity

- `IId` is an interface exposing `Id Id { get; }`.
- `IId<TKey>` and typed aliases such as `IdInt`, `IdGuid`, and `IdLong` exist for strongly typed identifiers.
- `Id` is a struct that accepts strings, numeric values, and GUIDs.
- `Id` supports implicit conversions and helper methods such as `TryToGuid()`, `TryToInt()`, and `HasId`.

Prefer using `Id` in examples unless the task is explicitly about typed alternatives.

## Audit types

Important naming trap:

- `IActive` is an interface
- `ICreated` is a base class
- `IUpdated` is a base class that inherits `ICreated`
- `IDeleted` is a base class that inherits `IUpdated`

Do not write examples that implement `ICreated`, `IUpdated`, or `IDeleted` as if they were interfaces.

Correct shape:

```csharp
public class User : IId, IActive
{
    public Id Id { get; set; }
    public bool Active { get; set; }
}

public class AuditedUser : IDeleted, IActive
{
    public Id Id { get; set; }
    public bool Active { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public DateTime? Deleted { get; set; }
}
```

## Filter helpers

Filter classes are extension-based helpers, not stateful objects.

Examples:
- `ActiveFilter.FilterActive(...)`
- `CreatedFilter.FilterCreatedRange(...)`
- `UpdatedFilter.FilterUpdatedRange(...)`
- `DeletedFilter.HasDeleted()` and `HasNotDeleted()`
- `IdFilter.FindId(...)`
- `BaseFilterFilter.ApplyFilter(...)`

Avoid examples that instantiate `ActiveFilter<T>` or put unsupported properties on `BaseFilter`.

## BaseFilter

`BaseFilter` contains:
- `Skip`
- `Take`
- `Sort`

Do not invent fields like `Search` unless they are added in source code first.

## Options

Key types:
- `OptionItem`
- `Options`
- `ListId`
- `SortItem`

Useful behaviors:
- `Options.ContainsKey(...)`
- indexer access by integer key
- `FilterConnections(...)`
- `FilterConnectionsId(...)`
- `FilterLabel(...)`
- `FindText(...)`

## Pagination helper

`ModelList<T>` represents paged results and exposes:
- `Itens`
- `TotalItens`
- `PageNumber`
- `PageSize`
- `TotalPages`
- `HasPreviousPage`
- `HasNextPage`
