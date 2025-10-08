# Utility Classes - BlackDigital

## Introduction

This document presents the utility classes that are directly in the `src` folder of the BlackDigital project. These classes are independent and provide essential auxiliary functionalities for application development, including exception handling, data manipulation, validations, and various support operations.

## Exception Handling

### BusinessException

Custom class for business exceptions that maps HTTP codes to different error types.

**Main Features:**
- Inherits from `Exception`
- Contains `Code` property with HTTP code
- Static methods for quick exception creation
- Validation methods that throw exceptions when necessary

**Usage Example:**
```csharp
// Throw specific exception
BusinessException.ThrowNotFound();

// Validate and throw if null
var user = BusinessException.ThrowNotFoundIfNull(userRepository.GetById(id));

// Create custom exception
throw BusinessException.New("User does not have permission", 403);
```

### BusinessExceptionType

Enum that defines business exception types with their respective HTTP codes.

**Available Values:**
- `BadRequest = 400`
- `Forbidden = 403`
- `NotFound = 404`
- `Conflict = 409`
- `Gone = 410`
- `PreconditionFailed = 412`
- `RangeNotSatisfiable = 416`
- `ExpectationFailed = 417`
- `PreconditionRequired = 428`

### ErrorApiResponse

Class to standardize error responses in APIs.

**Main Methods:**
- `Create(string message, List<string>? errors)` - Create custom error response
- `Create(Exception exception)` - Create response based on exception

**Usage Example:**
```csharp
// Create simple error response
var errorResponse = ErrorApiResponse.Create("Invalid data");

// Create response based on exception
try 
{
    // code that may fail
}
catch (Exception ex)
{
    var errorResponse = ErrorApiResponse.Create(ex);
    return BadRequest(errorResponse);
}
```

## Data Helpers

### DataValidator

Static class for validating common data like email and phone.

**Main Methods:**
- `IsEmail(string email)` - Validates email format
- `IsPhone(string phone)` - Validates phone format
- `IsEmailOrPhone(string value)` - Validates if it's email or phone
- `ValidateData(object? value, object? otherValue, Symbol symbol)` - Compares values

**Usage Example:**
```csharp
// Validate email
bool isValid = DataValidator.IsEmail("user@example.com");

// Validate phone
bool isValidPhone = DataValidator.IsPhone("+5511999999999");

// Compare values
bool isEqual = DataValidator.ValidateData(10, 10, Symbol.Equal);
```

### DateTimeHelper

Utilities for date and time manipulation.

**Main Methods:**
- `ToMonthDate(DateTime date)` - Converts to first day of month
- `TryToDateTime(string datetime, out DateTime result)` - Tries to convert string to DateTime
- `ToDateTime(string datetime, DateTime defaultValue)` - Converts with default value
- `ToDateTimeUnspecified(DateTime date)` - Removes timezone information

**Usage Example:**
```csharp
// Convert to first day of month
DateTime firstDay = DateTime.Now.ToMonthDate();

// Convert string to DateTime
DateTime date = "2023-12-25".ToDateTime();

// Deconstruct TimeSpan
var timespan = TimeSpan.FromHours(25);
var (days, hours, minutes, seconds) = timespan;
```

### DisplayHelper

Helper for working with `Display` attributes and obtaining display information.

**Main Methods:**
- `GetDisplay(PropertyInfo property)` - Gets Display attribute
- `GetDisplayName<T>(Expression<Func<T>> expression)` - Gets display name
- `GetDescription<T>(Expression<Func<T>> expression)` - Gets description
- `GetOrder<T>(Expression<Func<T>> expression)` - Gets display order

**Usage Example:**
```csharp
// Get display name of a property
string displayName = DisplayHelper.GetDisplayName(() => user.Name);

// Get description
string description = DisplayHelper.GetDescription(() => user.Email);
```

### ExpressionHelper

Utilities for working with LINQ expressions.

**Main Methods:**
- `Filter<TEntity, TProperty>(IQueryable<TEntity> query, Expression<Func<TEntity, TProperty>> property, TProperty value)` - Filters query by property

**Usage Example:**
```csharp
// Filter users by name
var filteredUsers = ExpressionHelper.Filter(users, u => u.Name, "John");
```

### HttpHelper

Utilities for working with HTTP and query string conversions.

**Main Methods:**
- `IsSuccess(HttpStatusCode status)` - Checks if status is successful
- `IsClientError(HttpStatusCode status)` - Checks if it's client error
- `FromQueryString<T>(string queryString)` - Converts query string to object
- `ToUrlQueryString<T>(T item)` - Converts object to query string

**Usage Example:**
```csharp
// Check HTTP status
bool isSuccess = HttpStatusCode.OK.IsSuccess();

// Convert query string to object
var filter = "name=John&age=30".FromQueryString<UserFilter>();

// Convert object to query string
string queryString = userFilter.ToUrlQueryString();
```

### JsonCast

Utilities for JSON serialization and deserialization with standardized configurations.

**Main Methods:**
- `To<T>(string json)` - Deserializes JSON to specific type
- `ToJson(object item)` - Serializes object to JSON
- `ConfigureJson(JsonSerializerOptions options)` - Configures default options

**Usage Example:**
```csharp
// Serialize object
string json = user.ToJson();

// Deserialize JSON
var user = jsonString.To<User>();

// Configure custom options
var options = new JsonSerializerOptions().ConfigureJson();
```

### ObjectHelper

General utilities for object manipulation.

**Main Methods:**
- `CreateId()` - Generates unique ID
- `ToUrlQueryString<T>(T item)` - Converts object to query string
- `CloneObject<T>(T item)` - Clones object via JSON
- `CastObject<TCast, TOrig>(TOrig item)` - Converts type via JSON
- `GetResourceValue(string? value, Type? resourceType)` - Gets resource value

**Usage Example:**
```csharp
// Generate unique ID
string id = ObjectHelper.CreateId();

// Clone object
var userCopy = user.CloneObject();

// Convert type
var userDto = user.CastObject<UserDto, User>();
```

### ReflectionHelper

Advanced utilities for reflection and attribute manipulation.

**Main Methods:**
- `GetAttributes<TAttribute>(Type type)` - Gets attributes from a type
- `GetSingleAttribute<TAttribute>(PropertyInfo property)` - Gets single attribute
- `GetDisplayName(PropertyInfo property)` - Gets display name
- `CreateCustomAttribute<TAttribute>()` - Creates custom attribute

**Usage Example:**
```csharp
// Get attributes from a property
var attributes = property.GetAttributes<DisplayAttribute>();

// Get display name
string displayName = property.GetDisplayName();
```

### UriHelper

Utilities for URI manipulation.

**Main Methods:**
- `Append(Uri uri, params string[] paths)` - Adds paths to URI
- `GetQueryString(Uri uri)` - Extracts query string as dictionary

**Usage Example:**
```csharp
// Add paths to URI
var baseUri = new Uri("https://api.example.com");
var fullUri = baseUri.Append("users", "123", "profile");

// Get query string
var queryParams = uri.GetQueryString();
```

## Miscellaneous Utilities

### LocalRandom

Random string generator with different character types.

**Main Methods:**
- `GenerateString(int length, bool useUpper, bool useLower, bool useNumbers, bool useSpecial)` - Generates random string
- `GenerateNumber(int length)` - Generates random numeric string

**Usage Example:**
```csharp
// Generate random password
string password = LocalRandom.GenerateString(12, true, true, true, true);

// Generate numeric code
string code = LocalRandom.GenerateNumber(6);
```

### ModelList<T>

Class to represent paginated lists with pagination metadata.

**Main Properties:**
- `Itens` - List of items
- `TotalItens` - Total available items
- `PageNumber` - Current page number
- `PageSize` - Page size
- `TotalPages` - Total pages
- `HasPreviousPage` - Indicates if there's a previous page
- `HasNextPage` - Indicates if there's a next page

**Usage Example:**
```csharp
var pagedUsers = new ModelList<User>
{
    Itens = users.Skip(skip).Take(take),
    TotalItens = totalUsers,
    PageNumber = pageNumber,
    PageSize = pageSize
};

// Check if there are more pages
if (pagedUsers.HasNextPage)
{
    // load next page
}
```

### Symbol

Enum that defines comparison symbols for validations.

**Available Values:**
- `Equal` - Equal
- `NotEqual` - Not equal
- `LessThan` - Less than
- `LessThanOrEqual` - Less than or equal
- `GreaterThan` - Greater than
- `GreaterThanOrEqual` - Greater than or equal

**Usage Example:**
```csharp
// Use in validations
bool isValid = DataValidator.ValidateData(age, 18, Symbol.GreaterThanOrEqual);
```

## Advantages of Utility Classes

### **🔧 Ready-to-Use Functionalities**
- Common methods already implemented and tested
- Reduces repetitive code
- Standardizes frequent operations

### **⚡ Performance**
- Optimized implementations
- Code reuse
- Less development overhead

### **🛡️ Reliability**
- Tested and validated code
- Handling of edge cases
- Consistent validations

### **📚 Ease of Use**
- Simple and intuitive APIs
- Clear documentation
- Practical examples

## Best Practices

1. **Use DataValidator validations** to ensure consistency
2. **Leverage conversion helpers** to avoid repetitive code
3. **Use BusinessException** to standardize error handling
4. **Configure JsonCast** once and use throughout the project
5. **Combine classes** for more robust solutions

These utility classes form a solid foundation for efficient development, providing essential functionalities in a standardized and reliable way.