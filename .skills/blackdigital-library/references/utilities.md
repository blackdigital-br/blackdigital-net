# Utilities and Serialization Reference

## JsonCast

`JsonCast` is the shared JSON configuration point for the library.

Default behavior includes:
- case-insensitive property names
- ignoring null values when writing
- `JsonStringEnumConverter`
- `TimeSpanConverter`
- `IdConverter`

Do not describe serialization as plain default `System.Text.Json` behavior without checking `src/JsonCast.cs`.

## IdConverter

`IdConverter`:
- reads numeric JSON tokens into numeric `Id` values when not in legacy mode
- writes numeric IDs as numbers when possible
- falls back to string when the value is not numeric

Only mention legacy string-only behavior when a custom `IdConverter(legacy: true)` is intentionally configured.

## TimeSpanConverter

`TimeSpanConverter` serializes `TimeSpan` using the standard `"c"` format.

## BusinessException

`BusinessException` standardizes business-layer errors with HTTP-like codes.

Useful points:
- static factories such as `New(...)`
- static throw helpers like `ThrowNotFound()`
- null-check helpers like `ThrowNotFoundIfNull(...)`

## ErrorApiResponse

Use it when documenting standardized API error payloads created from messages or exceptions.

## DataBuilder

Do not omit `DataBuilder` when describing the library.

Main types:
- `TypeBuilder`
- `PropertyBuilder`
- `DataType`
- `DataTypeTable`

Use `DataBuilder` for:
- dynamic forms
- admin metadata
- model-driven UI generation

Important `PropertyBuilder` behaviors:
- resolves display metadata
- resolves validation metadata
- infers a component type from CLR types when `DataTypeAttribute` is absent
- supports `Show(object value)` for visibility rules

## SecurityFilter

`SecurityFilter` is a small shared abstraction for auth context:
- `UserId`
- `Roles`

Do not oversell it as a full authentication system. It is a base abstraction.
