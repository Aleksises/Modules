using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RoslynAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class RoslynAnalyzerAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "EntitiesRule";

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Naming";

        private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.NamedType);
        }

        private static bool IsIdAndNamePublic(INamedTypeSymbol symbol)
        {
            var members = symbol.GetMembers().Where(x => x.Kind == SymbolKind.Property);
            if (members.FirstOrDefault(x => x.Name == "Id")?.DeclaredAccessibility != Accessibility.Public
                || members.FirstOrDefault(x => x.Name == "Name")?.DeclaredAccessibility != Accessibility.Public)
            {
                return false;
            }

            return true;
        }

        private static bool HasDataContractAttribute(INamedTypeSymbol symbol)
        {
            var dataContractAttribute = symbol.GetAttributes().FirstOrDefault(x => x.AttributeClass.Name == "DataContract");

            return dataContractAttribute == null ? false : true;
        }

        private static void AnalyzeSymbol(SymbolAnalysisContext context)
        {
            var namedTypeSymbol = (INamedTypeSymbol)context.Symbol;

            if (namedTypeSymbol.TypeKind == TypeKind.Class && namedTypeSymbol.ContainingNamespace.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).EndsWith("My.Entities"))
            {
                if (namedTypeSymbol.DeclaredAccessibility != Accessibility.Public || !IsIdAndNamePublic(namedTypeSymbol) || !HasDataContractAttribute(namedTypeSymbol))
                {
                    var diagnostic = Diagnostic.Create(Rule, namedTypeSymbol.Locations[0], namedTypeSymbol.Name);

                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }
}
