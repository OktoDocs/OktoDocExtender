﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using TestHelper;

namespace PullThroughDoc.Test
{
	public class PullThroughDocCodeFixVerifier : CodeFixVerifier
	{
		protected CodeFixProvider CodeFixProvider { get; set; }

		protected void ExpectPullThroughDiagnosticAt(string text, string member, int line, int col)
		{
			var expectedDiagnostic = new DiagnosticResult
			{
				Id = PullThroughDocAnalyzer.PullThroughDocDiagId,
				Message = String.Format("Pull through documentation for {0}.", member),
				Severity = DiagnosticSeverity.Hidden,
				Locations =
					new[] {
							new DiagnosticResultLocation("Test0.cs", line, col)
						}
			};

			VerifyCSharpDiagnostic(text, expectedDiagnostic);
		}

		protected override CodeFixProvider GetCSharpCodeFixProvider()
		{
			return CodeFixProvider;
		}

		protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
		{
			return new PullThroughDocAnalyzer();
		}
	}
}
