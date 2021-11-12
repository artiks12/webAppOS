using ANTLR;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using ANTLR.Grammar;
using static ANTLR.Grammar.LanguageParser;
using Antlr4.Runtime.Tree;

namespace AntlrCSharp
{
	public partial class Compiler : LanguageParserBaseVisitor<object>
	{
		public List<Class> Classes; // Saraksts ar klasēm.
		public List<Association> Associations; // Saraksts ar asociācijām.
		public List<string> Errors; // Saraksts ar kļūdām.
		public List<string> Reserved = new() { "class", "association", "Integer", "String", "Boolean", "Real", "URL", "private", "public", "BaseObject" }; // Saraksts ar rezervētajiem vārdiem.

		private List<string> AnnotationTypes = new() { }; // Saraksts ar anotāciju tipiem
		private List<string> URLProtocols = new() { "java", "dotnet" }; // Saraksts ar URL Valodu protokoliem
		private List<string> URLlocations = new() { "local", "remote"}; // Saraksts ar URL lokāciju vērtībām


		/// <summary>
		/// Kļūdainie mezgli (apstrādā visas pārējās kļūdas)
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public override object VisitErrorNode([NotNull] IErrorNode node)
		{
			///		Console.WriteLine(node.GetType() + "\n" + node.GetText() + "\n\n");
			
			Errors.Add("At line " + node.Symbol.Line + ": Unexpected '" + node.Symbol.Text + "'!");
			return base.VisitErrorNode(node);
		}

		/// <summary>
		/// Apstaigajam kodu
		/// </summary>
		/// <returns></returns>
		public override object VisitCode([NotNull] CodeContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			
			// Sākam kompilēšanu ar sarakstu iestatīsanu
			Classes = new();
			Associations = new();
			Errors = new();

			return VisitChildren(context);
		}

		/// <summary>
		/// Apstaigajam blokus
		/// </summary>
        public override object VisitBlocks([NotNull] BlocksContext context)
        {
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");

			uint line = (uint)context.Start.Line; // Nosaka rindu, kurā ir kļūda, ja tādu atrod.

			// Pārbauda, vai blokam ir "galva" jeb tips.
			if (context.blockType() == null)
			{
				if (context.blockBody().webMemoryClass() != null) { Errors.Add("At line " + line + ": Missing keyword 'class'!"); }
				else { Errors.Add("At line " + line + ": Missing keyword 'association'!"); }
			}
			else 
			{
                line = (uint)context.blockType().Stop.Line; 
				VisitBlockType(context.blockType()); 
			}

			// Pārbauda, vai blokam ir ķermenis
			if (context.blockBody() == null) { Errors.Add("At line " + line + ": Missing " + context.blockType().GetText() + " body!"); }
			else { VisitBlockBody(context.blockBody()); }

			return null;
        }

		/// <summary>
		/// Kompilēšanas pamtfunkcija
		/// </summary>
		public void Compile(LanguageParser.CodeContext context) { Visit(context); }
	}
}