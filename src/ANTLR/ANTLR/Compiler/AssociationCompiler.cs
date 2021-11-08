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
		Association _association;
		Class _source;

		public override object VisitAssociation([NotNull] AssociationContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			_association = new();
			_source = new();
			_association.Line = (uint)context.Start.Line;
			VisitChildren(context);
			_source._associations.Add(_association);
			return null;
		}
		public override object VisitAssociationType([NotNull] AssociationTypeContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			if (context.GetText() != "association") { Errors.Add("At line " + context.Start.Line + ": " + context.GetText() + " unrecognized! Did you mean to write 'association' instead?"); }
			return VisitChildren(context);
		}
		public override object VisitAssociationSourceName([NotNull] AssociationSourceNameContext context)
        {
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n"); 
			_association.SourceName = context.GetText();
			return VisitChildren(context);
		}
		public override object VisitSourceClass([NotNull] SourceClassContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n"); 

			bool found = false;
			foreach (var c in Classes)
			{
				if (c.className == context.GetText())
				{
					_source = c;
					found = true;
					break;
				}
			}
			if (found == false) { Errors.Add("At line " + context.Start.Line + ": there is no class" + context.GetText() + " to use as source class!"); }
			return VisitChildren(context);
		}
		public override object VisitAssociationTargetName([NotNull] AssociationTargetNameContext context)
        {
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n"); 
			_association.TargetName = context.GetText();
			return VisitChildren(context);
		}
        public override object VisitTargetClass([NotNull] TargetClassContext context)
        {
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n"); 
			
			bool found = false;
			foreach (var c in Classes)
			{
				if (c.className == context.GetText())
				{
					_association.TargetClass = context.GetText();
					found = true;
					break;
				}
			}
			if (found == false) { Errors.Add("At line " + context.Start.Line + ": there is no class" + context.GetText() + " to use as source class!"); }

			return VisitChildren(context);
		}
	}
}