﻿using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestHelper;

namespace PullThroughDoc.Test
{
	/// <summary>
	/// Holds a bunch of tests specific to base classes
	/// </summary>
	[TestClass]
	public class BaseClassDocumentationTests : PullThroughDocCodeFixVerifier
	{

		[TestMethod]
		public void BaseClass_Documentation_PullsThrough()
		{
			var test = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace ConsoleApplication1
    {
		class BaseClass 
		{
			/// <summary>Does A Thing </summary>
			public virtual string DoThing() { }
		}
        class TypeName : BaseClass
        {   
			public override string DoThing() {}
        }
    }";
			var expected = new DiagnosticResult
			{
				Id = "PullThroughDoc",
				Message = String.Format("Pull through documentation for {0}.", "DoThing"),
				Severity = DiagnosticSeverity.Info,
				Locations =
					new[] {
							new DiagnosticResultLocation("Test0.cs", 18, 27)
						}
			};

			VerifyCSharpDiagnostic(test, expected);

			var fixtest = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace ConsoleApplication1
    {
		class BaseClass 
		{
			/// <summary>Does A Thing </summary>
			public virtual string DoThing() { }
		}
        class TypeName : BaseClass
        {   
			/// <summary>Does A Thing </summary>
			public override string DoThing() {}
        }
    }";
			VerifyCSharpFix(test, fixtest);
		}


		[TestMethod]
		public void BaseClass_GetSetProperty_DocumentationPulledThrough()
		{
			var test = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace ConsoleApplication1
    {
		class BaseClass 
		{
			/// <summary>Gets A Thing </summary>
			public virtual string GetsThing { get; set; }
		}
        class TypeName : BaseClass
        {   
			public override string GetsThing { get; set; }
        }
    }";
			var expected = new DiagnosticResult
			{
				Id = "PullThroughDoc",
				Message = String.Format("Pull through documentation for {0}.", "GetsThing"),
				Severity = DiagnosticSeverity.Info,
				Locations =
					new[] {
							new DiagnosticResultLocation("Test0.cs", 18, 27)
						}
			};

			VerifyCSharpDiagnostic(test, expected);

			var fixtest = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace ConsoleApplication1
    {
		class BaseClass 
		{
			/// <summary>Gets A Thing </summary>
			public virtual string GetsThing { get; set; }
		}
        class TypeName : BaseClass
        {   
			/// <summary>Gets A Thing </summary>
			public override string GetsThing { get; set; }
        }
    }";
			VerifyCSharpFix(test, fixtest);
		}

		[TestMethod]
		public void BaseClass_GetterOnlyProperty_DocumentationPulledThrough()
		{
			var test = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace ConsoleApplication1
    {
		class BaseClass 
		{
			/// <summary>Gets A Thing </summary>
			public virtual string GetsThing { get => null; }
		}
        class TypeName : BaseClass
        {   
			public override string GetsThing { get => null; }
        }
    }";
			var expected = new DiagnosticResult
			{
				Id = "PullThroughDoc",
				Message = String.Format("Pull through documentation for {0}.", "GetsThing"),
				Severity = DiagnosticSeverity.Info,
				Locations =
					new[] {
							new DiagnosticResultLocation("Test0.cs", 18, 27)
						}
			};

			VerifyCSharpDiagnostic(test, expected);

			var fixtest = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace ConsoleApplication1
    {
		class BaseClass 
		{
			/// <summary>Gets A Thing </summary>
			public virtual string GetsThing { get => null; }
		}
        class TypeName : BaseClass
        {   
			/// <summary>Gets A Thing </summary>
			public override string GetsThing { get => null; }
        }
    }";
			VerifyCSharpFix(test, fixtest);
		}


	}
}
