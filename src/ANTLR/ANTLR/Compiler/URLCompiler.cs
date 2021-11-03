using ANTLR;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using static ANTLR.LanguageParser;
using Antlr4.Runtime.Tree;

namespace AntlrCSharp
{
	public partial class Compiler : LanguageParserBaseVisitor<object>
	{
		public override object VisitUrl([NotNull] UrlContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			return VisitChildren(context);
		}

		/// <summary>
		/// Iegūstam metodes URL
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override object VisitUrlDefinition([NotNull] UrlDefinitionContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			_method.URL = context.GetText();
			return VisitChildren(context);
		}
        public override object VisitUrlClassName([NotNull] UrlClassNameContext context)
        {
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n"); 
			return VisitChildren(context);
		}
        public override object VisitUrlMethodName([NotNull] UrlMethodNameContext context)
        {
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n"); 
			return VisitChildren(context);
		}
		public override object VisitUrlPackageName([NotNull] UrlPackageNameContext context)
        {
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n"); 
			return VisitChildren(context);
		}
        public override object VisitUrlType([NotNull] UrlTypeContext context)
        {
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n"); 
			return VisitChildren(context);
		}
		public override object VisitProtocolName([NotNull] ProtocolNameContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n"); 
			return VisitChildren(context);
		}
	}
}