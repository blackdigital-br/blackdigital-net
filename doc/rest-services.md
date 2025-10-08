# Automatic REST Services Generation System

## Introduction

BlackDigital.Rest is an innovative system that allows automatic creation of REST clients through interfaces decorated with attributes. Instead of manually writing all HTTP calls, you define an interface with the desired methods and the system automatically generates all the necessary code to perform the requests.

## Basic Concepts

### How It Works

The system uses **Reflection** and **Expression Trees** to analyze interfaces decorated with special attributes and automatically generate:
- Request URLs
- HTTP headers
- Request body
- Query parameters
- Path parameters

### Main Components

1. **RestService<T>**: Main class that executes the calls
2. **RestClient**: HTTP client that performs the requests
3. **Attributes**: Decorators that define the behavior of calls
4. **RestCallConfig**: Additional configurations for calls

## Basic Example

Let's start with a simple example of how to create a REST service:

### 1. Defining the Interface

```csharp
using BlackDigital.Rest;

[Service("api/users")]
public interface IUserService
{
    [Action]
    Task<List<User>> GetAllUsers();
    
    [Action("{id}")]
    Task<User> GetUserById([Path] int id);
}
```

### 2. Using the Service

```csharp
// Configuring the client
var restClient = new RestClient("https://api.example.com");
var userService = new RestService<IUserService>(restClient);

// Making the calls
var users = await userService.CallAsync(s => s.GetAllUsers());
var user = await userService.CallAsync(s => s.GetUserById(123));
```

## Available Attributes

### [Service] - Service Definition

Defines the base route and global service configurations.

```csharp
[Service("api/users", authorize: true, version: "2025-10-08")]
public interface IUserService
{
    // methods...
}
```

**Parameters:**
- `baseRoute`: Service base route
- `authorize`: If requires authorization (default: false)
- `version`: API version in date format (e.g., "2025-10-08"). This version is automatically sent in the HTTP header `x-api-version` so the backend can handle different API versions when necessary

### [Action] - Action Definition

Defines the specific behavior of each method.

```csharp
[Action("search", method: RestMethod.Post, authorize: false)]
Task<List<User>> SearchUsers([Body] SearchRequest request);
```

**Parameters:**
- `route`: Specific action route
- `method`: HTTP method (Get, Post, Put, Delete, Patch)
- `authorize`: If requires authorization
- `returnIsSuccess`: If returns only success/failure
- `version`: Specific action version in date format (e.g., "2025-10-08"). Overrides the version defined in [Service]

### [Path] - Route Parameters

Marks parameters that will be inserted into the URL.

```csharp
[Action("users/{id}/posts/{postId}")]
Task<Post> GetUserPost([Path] int id, [Path] int postId);

// Generates: GET /api/users/123/posts/456
```

### [Query] - Query Parameters

Marks parameters that will be added as query string.

```csharp
[Action("users")]
Task<List<User>> GetUsers([Query] int page, [Query] int size);

// Generates: GET /api/users?page=1&size=10
```

### [Body] - Request Body

Marks the parameter that will be serialized as JSON in the request body.

```csharp
[Action("users", method: RestMethod.Post)]
Task<User> CreateUser([Body] CreateUserRequest request);
```

### [Header] - HTTP Headers

Marks parameters that will be sent as HTTP headers.

```csharp
[Action("users")]
Task<List<User>> GetUsers([Header] string authorization, [Header] string userAgent);
```

## Progressive Examples

### Example 1: Basic CRUD

```csharp
[Service("api/products")]
public interface IProductService
{
    // GET /api/products
    [Action]
    Task<List<Product>> GetAll();
    
    // GET /api/products/{id}
    [Action("{id}")]
    Task<Product> GetById([Path] int id);
    
    // POST /api/products
    [Action(method: RestMethod.Post)]
    Task<Product> Create([Body] CreateProductRequest request);
    
    // PUT /api/products/{id}
    [Action("{id}", method: RestMethod.Put)]
    Task<Product> Update([Path] int id, [Body] UpdateProductRequest request);
    
    // DELETE /api/products/{id}
    [Action("{id}", method: RestMethod.Delete)]
    Task<bool> Delete([Path] int id);
}
```

### Example 2: Search with Filters

```csharp
[Service("api/products")]
public interface IProductService
{
    [Action("search")]
    Task<PagedResult<Product>> Search(
        [Query] string? name,
        [Query] decimal? minPrice,
        [Query] decimal? maxPrice,
        [Query] int page = 1,
        [Query] int size = 10);
}

// Usage:
var products = await productService.CallAsync(s => 
    s.Search("notebook", 1000, 5000, 1, 20));
// Generates: GET /api/products/search?name=notebook&minPrice=1000&maxPrice=5000&page=1&size=20
```

### Example 3: File Upload

```csharp
[Service("api/files")]
public interface IFileService
{
    [Action("upload", method: RestMethod.Post)]
    Task<FileUploadResult> UploadFile(
        [Header("Content-Type")] string contentType,
        [Body] byte[] fileContent);
}
```

### Example 4: Authentication and Versioning

```csharp
[Service("api/users", authorize: true, version: "2025-10-08")]
public interface IUserService
{
    [Action("profile")]
    Task<UserProfile> GetProfile([Header("Authorization")] string token);
    
    [Action("profile", method: RestMethod.Put, version: "2025-12-15")]
    Task<UserProfile> UpdateProfile(
        [Header("Authorization")] string token,
        [Body] UpdateProfileRequest request);
}
```

**How versioning works:**
- The version defined in `[Service]` ("2025-10-08") will be sent in the `x-api-version` header for all actions
- The `UpdateProfile` action overrides the version to "2025-12-15", sending this value in the header
- The backend can check the `x-api-version` header and handle different API versions as needed

### Example 5: Complex Parameters

```csharp
public class SearchFilters
{
    public string? Name { get; set; }
    public int? CategoryId { get; set; }
    public bool? IsActive { get; set; }
}

[Service("api/products")]
public interface IProductService
{
    [Action("advanced-search")]
    Task<List<Product>> AdvancedSearch([Query] SearchFilters filters);
}

// The object will be automatically serialized as query parameters
```

## Advanced Configurations

### RestCallConfig

Allows adding extra configurations for specific calls:

```csharp
var config = RestCallConfig.Create()
    .AddHeader("X-Custom-Header", "value")
    .AddQueryParameter("debug", "true")
    .AddVersion("2025-10-08");

var result = await service.CallAsync(s => s.GetData(), config);
```

### Error Handling

```csharp
var restClient = new RestClient("https://api.example.com");
restClient.ThownType = RestThownType.OnlyBusiness; // Only business errors
// or
restClient.ThownType = RestThownType.All; // All errors
// or
restClient.ThownType = RestThownType.None; // Don't throw exceptions

// Events for custom handling
restClient.Unauthorized += (args) => {
    // Handle 401 error
};

restClient.Forbidden += (args) => {
    // Handle 403 error
};
```

## How It Works Internally

### Execution Flow

1. **Expression Analysis**: The `RestService` receives an expression tree that represents the method call
2. **Metadata Extraction**: Uses Reflection to get the `[Service]` and `[Action]` attributes
3. **Parameter Processing**: Analyzes each parameter and its attributes (`[Path]`, `[Query]`, `[Body]`, `[Header]`)
4. **URL Generation**: Combines the service base route with the action route, replacing path parameters
5. **Header Assembly**: Collects all parameters marked with `[Header]`
6. **Body Creation**: Serializes the parameter marked with `[Body]` to JSON
7. **Query Parameters Addition**: Adds query parameters to the URL
8. **Request Execution**: Calls the `RestClient` to execute the HTTP request

### Transformation Example

```csharp
// Defined interface:
[Service("api/users", version: "2025-10-08")]
public interface IUserService
{
    [Action("{id}/posts", method: RestMethod.Post)]
    Task<Post> CreatePost([Path] int id, [Header] string auth, [Body] CreatePostRequest request);
}

// Call:
await service.CallAsync(s => s.CreatePost(123, "Bearer token", new CreatePostRequest()));

// Generated result:
// POST /api/users/123/posts
// Headers: { 
//   "auth": "Bearer token",
//   "x-api-version": "2025-10-08"
// }
// Body: { "title": "...", "content": "..." } (JSON serialized)
```

### Detailed Versioning Example

```csharp
[Service("api/products", version: "2025-10-08")]
public interface IProductService
{
    // Uses service version: "2025-10-08"
    [Action]
    Task<List<Product>> GetAll();
    
    // Overrides with specific version: "2025-12-15"
    [Action("search", version: "2025-12-15")]
    Task<List<Product>> Search([Query] string term);
}

// Generated requests:
// GET /api/products
// Headers: { "x-api-version": "2025-10-08" }

// GET /api/products/search?term=notebook
// Headers: { "x-api-version": "2025-12-15" }
```

## System Advantages

1. **Productivity**: Eliminates boilerplate code for HTTP calls
2. **Type Safety**: Compile-time type checking
3. **Maintainability**: API changes automatically reflect in the client
4. **Testability**: Easy mock creation for testing
5. **Consistency**: Standardization in how REST calls are made
6. **IntelliSense**: Full IDE support for autocompletion

## Best Practices

### 1. Interface Organization

```csharp
// Separate by domain
public interface IUserService { }
public interface IProductService { }
public interface IOrderService { }
```

### 2. Consistent Naming

```csharp
[Service("api/users")]
public interface IUserService
{
    [Action] Task<List<User>> GetAll();           // GET /api/users
    [Action("{id}")] Task<User> GetById([Path] int id);  // GET /api/users/{id}
    [Action(method: RestMethod.Post)] Task<User> Create([Body] User user);
}
```

### 3. Versioning

```csharp
[Service("api/users", version: "2020-05-10")]
public interface IUserServiceV1 { }

[Service("api/users", version: "2025-10-08")]
public interface IUserServiceV2 { }
```

**Important about versioning:**
- Always use dates in "YYYY-MM-DD" format for versions
- The version is automatically sent in the HTTP header `x-api-version`
- The backend can use this header to determine which API version to process
- More specific versions in actions override the service version

### 4. Error Handling

```csharp
try
{
    var user = await userService.CallAsync(s => s.GetById(id));
}
catch (BusinessException ex) when (ex.Code == 404)
{
    // User not found
}
```

## Conclusion

The BlackDigital automatic REST services generation system offers an elegant and productive way to consume REST APIs in .NET applications. Through simple interfaces decorated with attributes, you can eliminate much of the repetitive code needed for HTTP calls, while maintaining type safety and ease of maintenance.

The system is flexible enough to handle everything from simple scenarios to complex corporate API requirements, always keeping the code clean and expressive.