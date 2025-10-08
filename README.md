# BlackDigital Framework

![Logo](doc/images/Logo128.png)

[![NuGet Version](https://img.shields.io/nuget/v/BlackDigital)](https://www.nuget.org/packages/BlackDigital)
[![.NET](https://img.shields.io/badge/.NET-8.0-blue)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![License](https://img.shields.io/github/license/blackdigital-br/blackdigital-net)](LICENSE)

A comprehensive .NET framework providing essential utilities, automatic REST service generation, advanced data annotations, and model helpers for modern application development.

## 🚀 Key Features

### 🌐 **Automatic REST Services Generation** ⭐
The flagship feature of BlackDigital! Create REST clients automatically through interfaces decorated with attributes. No more manual HTTP calls - just define your interface and let the framework handle the rest.

```csharp
[Service("api/users", version: "2025-01-15")]
public interface IUserService
{
    [Action]
    Task<List<User>> GetAllUsers();
    
    [Action("{id}", Method = RestMethod.GET)]
    Task<User> GetUserById([Path] int id);
    
    [Action(Method = RestMethod.POST)]
    Task<User> CreateUser([Body] User user);
}

// Usage
var restClient = new RestClient("https://api.example.com");
var userService = new RestService<IUserService>(restClient);
var users = await userService.GetAllUsers();
```

### 📝 **Advanced Data Annotations**
Powerful validation and display control attributes that extend .NET's built-in capabilities:

```csharp
public class UserRegistration
{
    [EmailMobile(ErrorMessage = "Enter a valid email or phone")]
    public string Contact { get; set; }
    
    [RequiredIfProperty("UserType", UserType.Business)]
    [ShowIfProperty("UserType", UserType.Business)]
    public string CompanyName { get; set; }
    
    [CompareValue("Password", Symbol.Equal)]
    public string ConfirmPassword { get; set; }
}
```

### 🏗️ **Model Utilities**
Base classes and interfaces for common patterns:
- `IId`, `ICreated`, `IUpdated`, `IActive`, `IDeleted`
- Filtering and sorting helpers
- Option lists and data builders

### 🛠️ **Utility Classes**
Essential helpers for everyday development:
- `HttpHelper` - HTTP utilities and extensions
- `DateTimeHelper` - Date/time manipulation
- `ReflectionHelper` - Reflection utilities
- `ObjectHelper` - Object manipulation
- `UriHelper` - URI building and manipulation

## 📦 Installation

Install via NuGet Package Manager:

```bash
dotnet add package BlackDigital
```

Or via Package Manager Console:

```powershell
Install-Package BlackDigital
```

## 🏃‍♂️ Quick Start

### 1. REST Services Example

```csharp
using BlackDigital.Rest;

// Define your API interface
[Service("api/products", version: "2025-01-15")]
public interface IProductService
{
    [Action]
    Task<List<Product>> GetProducts([Query] string? search = null);
    
    [Action("{id}")]
    Task<Product> GetProduct([Path] int id);
    
    [Action(Method = RestMethod.POST)]
    Task<Product> CreateProduct([Body] Product product);
    
    [Action("{id}", Method = RestMethod.PUT)]
    Task<Product> UpdateProduct([Path] int id, [Body] Product product);
    
    [Action("{id}", Method = RestMethod.DELETE)]
    Task DeleteProduct([Path] int id);
}

// Configure and use
var client = new RestClient("https://api.mystore.com");
var productService = new RestService<IProductService>(client);

// Make calls
var products = await productService.GetProducts("laptop");
var product = await productService.GetProduct(1);
```

### 2. Data Annotations Example

```csharp
using BlackDigital.DataAnnotations;

public class ContactForm
{
    [Required]
    public string Name { get; set; }
    
    [EmailMobile(ErrorMessage = "Enter a valid email or phone number")]
    public string Contact { get; set; }
    
    public ContactType Type { get; set; }
    
    [RequiredIfProperty("Type", ContactType.Business)]
    [ShowIfProperty("Type", ContactType.Business)]
    public string CompanyName { get; set; }
    
    [ShowIfProperty("Type", ContactType.Personal)]
    public DateTime? BirthDate { get; set; }
}
```

### 3. Model Utilities Example

```csharp
using BlackDigital.Model;

public class User : IId, ICreated, IUpdated, IActive
{
    public int Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public bool Active { get; set; }
    
    public string Name { get; set; }
    public string Email { get; set; }
}

// Use with filters
var filter = new ActiveFilter<User> { Active = true };
var userFilter = new BaseFilter { Search = "john", Take = 10 };
```

## 📚 Documentation

### Core Documentation
- **[REST Services](doc/rest-services.md)** - Complete guide to automatic REST client generation
- **[Data Annotations](doc/data-annotations.md)** - Advanced validation and display attributes
- **[Model Utilities](doc/model.md)** - Base classes and model helpers
- **[Utilities](doc/utilities.md)** - Helper classes and extensions

### Portuguese Documentation
- **[Serviços REST](doc/rest-services.pt.md)** - Guia completo de geração automática de clientes REST
- **[Data Annotations](doc/data-annotations.pt.md)** - Atributos avançados de validação e exibição
- **[Utilitários de Modelo](doc/model.pt.md)** - Classes base e helpers de modelo
- **[Utilitários](doc/utilities.pt.md)** - Classes auxiliares e extensões

## 🏗️ Architecture

The BlackDigital Framework is organized into several key components:

```
BlackDigital/
├── Rest/                    # Automatic REST service generation
│   ├── RestService<T>      # Main service class
│   ├── RestClient          # HTTP client wrapper
│   └── Attributes/         # Service and action attributes
├── DataAnnotations/        # Advanced validation attributes
│   ├── Validation/         # Email, Mobile, RequiredIf, etc.
│   └── Display/           # ShowIf, NotShow, etc.
├── Model/                  # Base interfaces and classes
│   ├── Interfaces/         # IId, ICreated, IUpdated, etc.
│   └── Filters/           # BaseFilter, ActiveFilter, etc.
└── Utilities/             # Helper classes
    ├── HttpHelper         # HTTP utilities
    ├── DateTimeHelper     # Date/time helpers
    └── ReflectionHelper   # Reflection utilities
```

## 🤝 Contributing

We welcome contributions! Please feel free to submit a Pull Request. For major changes, please open an issue first to discuss what you would like to change.

### Development Setup

1. Clone the repository
2. Open `BlackDigital.sln` in Visual Studio or your preferred IDE
3. Build the solution
4. Run tests to ensure everything works

### Guidelines

- Follow existing code style and conventions
- Add tests for new features
- Update documentation as needed
- Ensure all tests pass before submitting PR

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🔗 Links

- [NuGet Package](https://www.nuget.org/packages/BlackDigital)
- [GitHub Repository](https://github.com/blackdigital-br/blackdigital-net)
- [Issues](https://github.com/blackdigital-br/blackdigital-net/issues)

## 📞 Support

If you encounter any issues or have questions, please:

1. Check the [documentation](doc/)
2. Search [existing issues](https://github.com/blackdigital-br/blackdigital-net/issues)
3. Create a new issue if needed

---

**BlackDigital Framework** - Simplifying .NET development with powerful utilities and automatic REST service generation.