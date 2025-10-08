# BlackDigital DataAnnotations System

## Introduction

The BlackDigital DataAnnotations system provides a collection of custom attributes for data validation and property display control. This system extends the standard .NET functionalities with specific validations and conditional visibility controls.

## Basic Concepts

### Attribute Types

The system is divided into two main categories:

1. **Validation Attributes**: Check if data meets specific criteria
2. **Display Attributes**: Control when and how properties should be displayed

### DataValidator

The `DataValidator` is the core of the system, providing static methods for validation:

```csharp
public static class DataValidator
{
    // Email validation using regex
    public static bool IsEmail(string email)
    
    // Phone validation using regex  
    public static bool IsPhone(string phone)
    
    // Email or phone validation
    public static bool IsEmailOrPhone(string emailOrPhone)
    
    // Value comparison using symbols
    public static bool ValidateData(object? value, object? otherValue, Symbol symbol)
}
```

## Validation Attributes

### 1. EmailAttribute

Validates if the value is a valid email address.

```csharp
public class User
{
    [Email(ErrorMessage = "Invalid email")]
    public string Email { get; set; }
}
```

**Usage example:**
```csharp
var user = new User { Email = "test@example.com" }; // ✅ Valid
var user2 = new User { Email = "invalid-email" };   // ❌ Invalid
```

### 2. MobileAttribute

Validates if the value is a valid phone number.

```csharp
public class Contact
{
    [Mobile(ErrorMessage = "Invalid phone number")]
    public string Phone { get; set; }
}
```

**Usage example:**
```csharp
var contact = new Contact { Phone = "+5511999999999" }; // ✅ Valid
var contact2 = new Contact { Phone = "123" };           // ❌ Invalid
```

### 3. EmailMobileAttribute

Validates if the value is a valid email OR phone.

```csharp
public class Registration
{
    [EmailMobile(ErrorMessage = "Enter a valid email or phone")]
    public string Contact { get; set; }
}
```

**Usage example:**
```csharp
var registration1 = new Registration { Contact = "test@example.com" };  // ✅ Valid (email)
var registration2 = new Registration { Contact = "+5511999999999" };     // ✅ Valid (phone)
var registration3 = new Registration { Contact = "invalid" };            // ❌ Invalid
```

### 4. RequiredIfAttribute<T>

Makes a property required based on a custom function.

```csharp
public class Order
{
    public PaymentType PaymentType { get; set; }
    
    [RequiredIf<Order>(p => p.PaymentType == PaymentType.CreditCard, 
                       ErrorMessage = "Card number is required")]
    public string CardNumber { get; set; }
}
```

**Advanced example with custom validation:**
```csharp
public class Employee
{
    public bool IsManager { get; set; }
    public decimal Salary { get; set; }
    
    [RequiredIf<Employee>((employee, context) => 
    {
        if (employee.IsManager && employee.Salary < 5000)
        {
            return new ValidationResult("Managers must have minimum salary of $5,000");
        }
        return ValidationResult.Success;
    })]
    public string ManagementCode { get; set; }
}
```

### 5. RequiredIfPropertyAttribute

Makes a property required based on another property's value.

```csharp
public class Address
{
    public string Country { get; set; }
    
    [RequiredIfProperty("Country", "Brazil", ErrorMessage = "ZIP code is required for Brazilian addresses")]
    public string ZipCode { get; set; }
    
    [RequiredIfProperty("Country", "Brazil", "Argentina", ErrorMessage = "State is required")]
    public string State { get; set; }
}
```

**Example with inversion:**
```csharp
public class Product
{
    public bool IsDigital { get; set; }
    
    [RequiredIfProperty("IsDigital", false, IsInverted = true, 
                        ErrorMessage = "Weight is required for physical products")]
    public decimal? Weight { get; set; }
}
```

### 6. CompareValueAttribute

Compares the property value with another property using comparison symbols.

```csharp
public class UserRegistration
{
    public string Password { get; set; }
    
    [CompareValue("Password", Symbol.Equal, ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    [CompareValue("BirthDate", Symbol.LessThan, ErrorMessage = "Registration date must be after birth date")]
    public DateTime RegistrationDate { get; set; }
}
```

## Comparison Symbols

The `Symbol` enum defines the available comparison operators:

```csharp
public enum Symbol
{
    Equal,              // ==
    NotEqual,           // !=
    LessThan,           // <
    LessThanOrEqual,    // <=
    GreaterThan,        // >
    GreaterThanOrEqual  // >=
}
```

**Practical examples:**
```csharp
public class Event
{
    public DateTime StartDate { get; set; }
    
    [CompareValue("StartDate", Symbol.GreaterThanOrEqual, 
                  ErrorMessage = "End date must be after or equal to start date")]
    public DateTime EndDate { get; set; }
    
    public decimal MinPrice { get; set; }
    
    [CompareValue("MinPrice", Symbol.GreaterThan, 
                  ErrorMessage = "Max price must be greater than min price")]
    public decimal MaxPrice { get; set; }
}
```

## Display Attributes

### 1. ShowAttribute (Base Class)

Abstract class that defines the base for visibility control.

```csharp
public abstract class ShowAttribute : Attribute
{
    public abstract bool Show(object value);
}
```

### 2. ShowIfAttribute<T>

Controls visibility based on a custom function.

```csharp
public class AdvancedConfiguration
{
    public UserLevel Level { get; set; }
    
    [ShowIf<AdvancedConfiguration>(config => config.Level == UserLevel.Administrator)]
    public string AdminConfiguration { get; set; }
    
    [ShowIf<AdvancedConfiguration>(config => config.Level >= UserLevel.Moderator)]
    public bool AllowModeration { get; set; }
}
```

### 3. ShowIfPropertyAttribute

Controls visibility based on another property's value.

```csharp
public class ContactForm
{
    public ContactType Type { get; set; }
    
    [ShowIfProperty("Type", ContactType.Company, ContactType.Supplier)]
    public string TaxId { get; set; }
    
    [ShowIfProperty("Type", ContactType.Individual)]
    public string SocialSecurityNumber { get; set; }
    
    [ShowIfProperty("Type", ContactType.Company, IsInverted = true)]
    public string FullName { get; set; }
}
```

**Example with multiple values:**
```csharp
public class OrderStatus
{
    public HttpStatusCode Status { get; set; }
    
    [ShowIfProperty("Status", HttpStatusCode.OK, HttpStatusCode.Created, 
                    HttpStatusCode.Accepted, HttpStatusCode.NoContent)]
    public string SuccessMessage { get; set; }
    
    [ShowIfProperty("Status", HttpStatusCode.OK, HttpStatusCode.Created, 
                    IsInverted = true)]
    public string ErrorDetails { get; set; }
}
```

### 4. NotShow

Always hides the property (useful for properties that should never be displayed).

```csharp
public class User
{
    public string Name { get; set; }
    
    [NotShow]
    public string PasswordHash { get; set; }
    
    [NotShow]
    public string InternalKey { get; set; }
}
```

## Complex Examples

### Complete Registration Form

```csharp
public class CompleteRegistration
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    
    [EmailMobile(ErrorMessage = "Enter a valid email or phone")]
    public string Contact { get; set; }
    
    public PersonType PersonType { get; set; }
    
    [RequiredIfProperty("PersonType", PersonType.Individual, ErrorMessage = "SSN is required")]
    [ShowIfProperty("PersonType", PersonType.Individual)]
    public string SSN { get; set; }
    
    [RequiredIfProperty("PersonType", PersonType.Company, ErrorMessage = "Tax ID is required")]
    [ShowIfProperty("PersonType", PersonType.Company)]
    public string TaxId { get; set; }
    
    public bool HasAddress { get; set; }
    
    [RequiredIfProperty("HasAddress", true, ErrorMessage = "ZIP code is required")]
    [ShowIfProperty("HasAddress", true)]
    public string ZipCode { get; set; }
    
    [ShowIfProperty("HasAddress", true)]
    public string Address { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    [CompareValue("BirthDate", Symbol.GreaterThan, 
                  ErrorMessage = "Registration date must be after birth date")]
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
    
    [NotShow]
    public string InternalToken { get; set; }
}
```

### Conditional Configuration System

```csharp
public class SystemConfiguration
{
    public bool EnableNotifications { get; set; }
    
    [ShowIfProperty("EnableNotifications", true)]
    [RequiredIfProperty("EnableNotifications", true, ErrorMessage = "Email is required for notifications")]
    [Email(ErrorMessage = "Invalid email")]
    public string NotificationEmail { get; set; }
    
    [ShowIfProperty("EnableNotifications", true)]
    public int IntervalMinutes { get; set; }
    
    public AuthenticationType AuthType { get; set; }
    
    [ShowIfProperty("AuthType", AuthenticationType.LDAP, AuthenticationType.ActiveDirectory)]
    [RequiredIfProperty("AuthType", AuthenticationType.LDAP, AuthenticationType.ActiveDirectory)]
    public string AuthServer { get; set; }
    
    [ShowIfProperty("AuthType", AuthenticationType.OAuth)]
    [RequiredIfProperty("AuthType", AuthenticationType.OAuth)]
    public string ClientId { get; set; }
    
    [ShowIfProperty("AuthType", AuthenticationType.OAuth)]
    [NotShow] // Never display, but validate if necessary
    public string ClientSecret { get; set; }
}
```

## How It Works Internally

### Validation Process

1. **Validation Attributes**: Inherit from `ValidationAttribute` and implement `IsValid()`
2. **DataValidator**: Provides static methods for specific validations
3. **Reflection**: Used to access related properties at runtime
4. **ValidationContext**: Provides context about the object being validated

### Display Process

1. **ShowAttribute**: Defines the base contract with `Show()` method
2. **Reflection**: Used to access related property values
3. **Conditional Evaluation**: Determines if the property should be displayed

### Execution Flow

```
Object → Validation → Attributes → DataValidator → Result
                   ↓
               Display → ShowAttribute → Evaluation → Visibility
```

## System Advantages

1. **Flexibility**: Conditional validations and displays based on custom logic
2. **Reusability**: Attributes can be applied to multiple properties
3. **Separation of Concerns**: Validation and display are handled separately
4. **Integration**: Works with the standard .NET validation system
5. **Extensibility**: Easy creation of new custom attributes

## Best Practices

1. **Clear Error Messages**: Always provide descriptive error messages
2. **Consistent Validation**: Use the same patterns throughout the system
3. **Performance**: Avoid complex logic in attributes that are executed frequently
4. **Testing**: Always test validations with valid and invalid cases
5. **Documentation**: Document complex validations for easier maintenance

## Test Example

```csharp
[Fact]
public void ShowIfPropertyAttribute_ShouldShowWhenStatusIsCorrect()
{
    var showAttribute = new ShowIfPropertyAttribute("HttpStatus", 
        HttpStatusCode.OK, HttpStatusCode.Created);
    
    var model = new SimpleModel { HttpStatus = HttpStatusCode.OK };
    
    Assert.True(showAttribute.Show(model));
}

[Fact]
public void EmailAttribute_ShouldValidateCorrectEmail()
{
    var emailAttribute = new EmailAttribute();
    
    Assert.True(emailAttribute.IsValid("test@example.com"));
    Assert.False(emailAttribute.IsValid("invalid-email"));
}
```

This DataAnnotations system provides a solid foundation for validation and display control in .NET applications, offering flexibility and ease of use for complex conditional validation scenarios.