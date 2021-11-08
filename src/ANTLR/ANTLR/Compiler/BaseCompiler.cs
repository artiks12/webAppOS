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
		public List<Class> Classes; // Saraksts ar klasēm.
		public List<string> Errors; // Saraksts ar kļūdām.
		public List<string> Reserved = new() { "class", "association", "Integer", "String", "Boolean", "Real", "URL", "private", "public", "BaseObject" }; // Saraksts ar rezervētajiem vārdiem.

		private List<string> AnnotationTypes = new() { };
		private List<string> URLProtocols = new() { "java", "dotnet" };
		private List<string> URLlocations = new() { "local", "remote"};


		/// <summary>
		/// Funkcijai VisitErrorNode meklē simbolu virkni, kuras trūkst.
		/// </summary>
		/// <param name="missing"></param>
		/// <returns></returns>
		private string getMissingText(string missing)
		{
			string result = "";
			for (int x = 10; ; x++)
			{
				if (missing[x] == '\'') { break; }
				result += missing[x];
			}
			return result;
		}
		/// <summary>
		/// Kļūdainie mezgli (apstrādā visas pārējās kļūdas)
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public override object VisitErrorNode([NotNull] IErrorNode node)
		{
			///		Console.WriteLine(node.GetType() + "\n" + node.GetText() + "\n\n");
			// Trūkstošs simbols
			if ((node.Symbol.Text).StartsWith("<missing '"))
			{
				Errors.Add("At line " + node.Symbol.Line + ": Syntax error! Missing '" + getMissingText(node.Symbol.Text) + "'!");
			}
			// Negaidīts simbols
			else if (!(node.Symbol.Text).StartsWith("<missing "))
			{
				Errors.Add("At line " + node.Symbol.Line + ": Syntax error! Unexpected '" + node.Symbol.Text + "'!");
			}
			return base.VisitErrorNode(node);
		}
		/// <summary>
		/// Sākam kompilēšanu ar sarakstu un skaitītāju iestatīsanu
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override object VisitCode([NotNull] CodeContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			Classes = new();
			Errors = new();

			if (context.children != null) 
			{
				foreach (var c in context.children)
				{
					switch (c.GetType().ToString()) 
					{
						case "ANTLR.LanguageParser+BlocksContext":
							VisitBlocks((BlocksContext)c);
						break;
						case "ANTLR.LanguageParser+AssociationContext":
							Errors.Add("At line " + ((AssociationContext)c).Start.Line + ": Syntax error! Missing keyword 'association'!");
							_association = new();
							VisitAssociation((AssociationContext)c);
						break;
						case "ANTLR.LanguageParser+ClassBodyContext":
							Errors.Add("At line " + ((ClassBodyContext)c).Start.Line + ": Syntax error! Missing keyword 'class'!");
							Errors.Add("At line " + ((ClassBodyContext)c).Start.Line + ": Syntax error! Missing class name!");
							_class = new();
							VisitClassBody((ClassBodyContext)c);
						break;
					}
				}
			}
			return null;
		}
		public void Compile(LanguageParser.CodeContext context)
        {
            try
            {
                Visit(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }

        public override object VisitBlocks([NotNull] BlocksContext context)
        {
			var type = context.blockType();
			var block = context.blockBody().children[0];

			if (block.GetType().ToString() == "ANTLR.LanguageParser+WebMemoryClassContext") 
			{
				if (block.GetText() == "") 
				{
					switch (type.GetText()) 
					{
						case "class":
							Errors.Add("At line " + type.Stop.Line + ": there is no name and body for class!");
						break;
						case "association":
							Errors.Add("At line " + type.Stop.Line + ": there is no body for association!");
						break;
						default:
							Errors.Add("At line " + type.Stop.Line + ": " + context.GetText() + " unrecognized!");
						break;
					}
				}
				else 
				{
					if (type.GetText() != "class") { Errors.Add("At line " + type.Start.Line + ": " + context.GetText() + " unrecognized! Did you mean to write 'class' instead?"); }
					VisitWebMemoryClass((WebMemoryClassContext)block);
				}
			}
			else 
			{
				VisitAssociation((AssociationContext)block);
			}
			return null;
        }
    }
}
