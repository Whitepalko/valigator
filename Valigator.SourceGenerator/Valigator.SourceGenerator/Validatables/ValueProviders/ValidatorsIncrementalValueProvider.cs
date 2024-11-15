using Microsoft.CodeAnalysis;
using Valigator.SourceGenerator.Utils;
using Valigator.SourceGenerator.Utils.Mapping;
using Valigator.SourceGenerator.Utils.Symbols;
using Valigator.SourceGenerator.Validatables.Dtos;

namespace Valigator.SourceGenerator.Validatables.ValueProviders;

internal static class ValidatorsIncrementalValueProvider
{
	public static IncrementalValueProvider<EquatableArray<ValidatorProperties>> Get(
		IncrementalGeneratorInitializationContext initContext
	)
	{
		var referencedValidators = initContext
			.CompilationProvider.SelectMany(
				(compilation, cancellationToken) =>
					compilation.SourceModule.ReferencedAssemblySymbols.SelectMany(assemblySymbol =>
						SelectValidatorsProperties(assemblySymbol, cancellationToken)
					)
			)
			.Collect();

		var projectValidators = initContext.CompilationProvider.Select(
			(compilation, cancellationToken) =>
				SelectValidatorsProperties(compilation.SourceModule.ContainingAssembly, cancellationToken)
		);

		var allValidators = referencedValidators
			.Combine(projectValidators)
			.Select((tuple, _) => new EquatableArray<ValidatorProperties>(tuple.Left.Concat(tuple.Right).ToArray()));

		return allValidators;
	}

	private static IEnumerable<ValidatorProperties> SelectValidatorsProperties(
		IAssemblySymbol assemblySymbol,
		CancellationToken cancellationToken
	)
	{
		return CustomSymbolVisitor
			.GetNamedTypes(
				assemblySymbol,
				cancellationToken,
				t =>
					t.GetAttributes()
						.Any(attr => attr.AttributeClass?.GetQualifiedName() == Consts.ValidatorAttributeQualifiedName)
			)
			.Select(GetValidatorProperties);
	}

	private static ValidatorProperties GetValidatorProperties(INamedTypeSymbol typeSymbol)
	{
		// ImmutableArray<IMethodSymbol> constructors = typeSymbol.Constructors;
		IMethodSymbol? isValidMethod =
			typeSymbol.GetMembers(Consts.IsValidMethodName).FirstOrDefault() as IMethodSymbol;

		return new ValidatorProperties
		{
			QualifiedName = typeSymbol.GetQualifiedName(),
			IsValidMethod = isValidMethod is null
				? EmptyIsValidMethodProperties
				: SymbolMapper.MapMethod(isValidMethod),
		};
	}

	private static readonly MethodProperties EmptyIsValidMethodProperties =
		new()
		{
			MethodName = Consts.IsValidMethodName,
			ReturnType = "void",
			ReturnTypeGenericArgument = null,
			Dependencies = new EquatableArray<string>(),
			IsAsync = false,
			Awaitable = false,
			RequiresInjection = false,
		};
}
