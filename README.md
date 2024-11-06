# Valigator

This repository contains a powerful, efficient, and highly customizable validation library for .NET, leveraging the capabilities of C# Source Generators to provide compile-time validation logic generation. The library is designed to simplify model validation in .NET applications by automatically generating validation code based on attributes and custom rules, reducing runtime overhead and enhancing code maintainability.

## Key Features
- Compile-Time Code Generation: Uses C# Source Generators to create validation code at compile-time, eliminating the need for runtime reflection and improving performance.
- Attribute-Based Validation: Define validation rules using simple attributes directly on your models, allowing clear and maintainable validation rules.
- Property and Object-Level Validation: Supports validation of individual properties as well as the model as a whole, enabling both fine-grained and aggregate validations.
- Customizable Validation Logic: Extend the library with custom validation attributes and rules to meet unique validation requirements.
- Detailed Validation Results: Provides rich validation results, including per-property error messages and model-wide validation summaries.
- Seamless Integration: Designed to work smoothly in .NET applications, with support for dependency injection and easy configuration.

## Getting Started
To start using the library, add it to your project as a NuGet package. Decorate your model properties with validation attributes and let the source generator handle the rest. Generated code will include optimized validation methods that you can call to validate instances of your models.

## Example Usage
Define validation rules using attributes on your model properties:

```csharp
[Validatable]
public class UserModel
{
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string Name { get; set; }

    [Range(18, 99)]
    public int Age { get; set; }
}
```
The source generator will automatically create efficient, compile-time-validated code to handle these checks.

## Installation
Install the package via NuGet:
```bash
dotnet add package Valigator
```

Install the source generator package to enable compile-time validation code generation:
```bash
dotnet add package Valigator.SourceGenerator
```

Install the package with default validators:
```bash
dotnet add package Valigator.Validators
```

## Contributions
Contributions are welcome! Feel free to open issues, submit pull requests, or suggest enhancements to improve the library further.

## License
This project is licensed under the MIT License.

---
This .NET validation library aims to streamline validation in .NET applications, leveraging modern C# features for performance and ease of use. It’s ideal for developers looking to minimize boilerplate code and maximize performance in their validation routines.