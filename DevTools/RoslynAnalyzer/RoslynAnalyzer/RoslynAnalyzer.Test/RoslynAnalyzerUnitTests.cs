using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestHelper;

namespace RoslynAnalyzer.Test
{
    [TestClass]
    public class UnitTest : CodeFixVerifier
    {

        //No diagnostics expected to show up
        [TestMethod]
        public void Analyzer_WhenEntityValid_ShouldDoNotReturnDiagnostic()
        {
            var test = @"
    namespace Test.My.Entities
    {
        [DataContract]
        public class TypeName
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }";

            VerifyCSharpDiagnostic(test);
        }

        [TestMethod]
        public void Analyzer_WhenClassIsNotPublic_ShouldReturnDiagnostic()
        {
            var test = @"
    namespace Test.My.Entities
    {
        [DataContract]
        class TypeName
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "EntitiesRule",
                Message = String.Format("Type name '{0}' is not public or hasn't public properties Id,Name or hasn't DataContract attribute.", "TypeName"),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 5, 15)
                        }
            };

            VerifyCSharpDiagnostic(test, expected);
        }

        [TestMethod]
        public void Analyzer_WhenPropertyIdIsNotPublic_ShouldReturnDiagnostic()
        {
            var test = @"
    namespace Test.My.Entities
    {
        [DataContract]
        public class TypeName
        {
            int Id { get; set; }
            public string Name { get; set; }
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "EntitiesRule",
                Message = String.Format("Type name '{0}' is not public or hasn't public properties Id,Name or hasn't DataContract attribute.", "TypeName"),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 5, 22)
                        }
            };

            VerifyCSharpDiagnostic(test, expected);
        }

        [TestMethod]
        public void Analyzer_WhenPropertyNameIsNotPublic_ShouldReturnDiagnostic()
        {
            var test = @"
    namespace Test.My.Entities
    {
        [DataContract]
        public class TypeName
        {
            public int Id { get; set; }
            string Name { get; set; }
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "EntitiesRule",
                Message = String.Format("Type name '{0}' is not public or hasn't public properties Id,Name or hasn't DataContract attribute.", "TypeName"),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 5, 22)
                        }
            };

            VerifyCSharpDiagnostic(test, expected);
        }

        [TestMethod]
        public void Analyzer_WhenClassHasNotDataContractAttribute_ShouldReturnDiagnostic()
        {
            var test = @"
    namespace Test.My.Entities
    {
        public class TypeName
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }";
            var expected = new DiagnosticResult
            {
                Id = "EntitiesRule",
                Message = String.Format("Type name '{0}' is not public or hasn't public properties Id,Name or hasn't DataContract attribute.", "TypeName"),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 4, 22)
                        }
            };

            VerifyCSharpDiagnostic(test, expected);
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new RoslynAnalyzerAnalyzer();
        }
    }
}
