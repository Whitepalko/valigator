using Valigator.Validators;

namespace Valigator.Extensions.Validators.Common;

/// <summary>
/// Validator that ensures a value is present, enforcing it as required.
/// If the value is a string, it can optionally allow empty strings based on the configuration of <paramref name="allowEmptyStrings"/>.
/// Typically used to validate that a property has a non-null or non-empty value.
/// </summary>
/// <param name="allowEmptyStrings">If true, empty strings are considered valid values; otherwise, they are treated as missing values.</param>
[Validator]
[ValidatorDescription("is required")]
[AttributeUsage(AttributeTargets.Property)]
public class RequiredAttribute(bool allowEmptyStrings = false) : Attribute
{
	private static readonly ValidationMessage RequiredMessage =
		new("A value is required.", "Valigator.Validations.Required");

	/// <summary>
	/// Validate the value
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public ValidationMessage? IsValid(string? value)
	{
		if (value is null || (string.IsNullOrEmpty(value) && !allowEmptyStrings))
		{
			return RequiredMessage;
		}

		return null;
	}

	/// <summary>
	/// Validate the value
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public ValidationMessage? IsValid<T>(T? value)
	{
		if (value is null)
		{
			return RequiredMessage;
		}

		return null;
	}
}
