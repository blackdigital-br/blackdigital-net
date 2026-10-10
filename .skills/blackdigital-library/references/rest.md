# REST Reference

## Core behavior

- `RestService<TService>` builds requests from expression trees.
- Calls are made through `CallAsync(...)`.
- The service reads `[Service]`, `[Action]`, `[Path]`, `[Query]`, `[Header]`, and `[Body]`.
- URLs, headers, query string, and request body are assembled in `src/Rest/RestService.cs`.

## Example shape

Correct style:

```csharp
var service = new RestService<IUserService>(client);
var user = await service.CallAsync(s => s.GetUserById(10));
```

Do not generate examples like:

```csharp
await service.GetUserById(10);
```

## QueryAttribute rules

`QueryAttribute` defaults to `QueryParameterType.Raw`.

### Use `Parameter`

Use `QueryParameterType.Parameter` for simple values that must become `name=value`.

Typical cases:
- `page`
- `size`
- `search`
- `sort`
- booleans and enums used as simple filters

Example:

```csharp
Task<List<User>> GetUsers(
    [Query(QueryParameterType.Parameter)] int page,
    [Query(QueryParameterType.Parameter)] int size);
```

Result:

```text
?page=1&size=20
```

### Use `Raw`

Use `QueryParameterType.Raw` for objects, nested objects, and collections that should expand through `ToUrlQueryString()`.

Example:

```csharp
Task<List<User>> Search([Query] UserFilter filter);
```

If `filter = new UserFilter { Name = "John" }`, the result is a flattened query string such as:

```text
?Name=John
```

For nested objects, keys use dotted paths. Collections are indexed.

Do not use `Parameter` for complex objects unless you explicitly want `name=value.ToString()`.

## Version rules

- `[Service(version: ...)]` and `[Action(version: ...)]` produce the `x-api-version` header.
- `RestCallConfig.AddVersion(...)` adds `api-version` to the query string.

These are not interchangeable.

## Error handling and resiliency

`RestClient` supports:
- `ThownType`
- `Unauthorized`
- `Forbidden`
- `ConnectionError`
- `ServerError`
- `RetryConection`
- `TimeRetryConnection`

When documenting or explaining these features, describe them from `src/Rest/RestClient.cs`, not from assumptions about a generic HTTP wrapper.
