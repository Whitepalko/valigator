namespace Valigator.SourceGenerator.Validatables.ObjectDtos;

public enum BeforeValidateReturnType
{
	Void,
	Task,
	Enumerable,
	AsyncEnumerable,
	ValidationResult,
	TaskValidationResult,
}
