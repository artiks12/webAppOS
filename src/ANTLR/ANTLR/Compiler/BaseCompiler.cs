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
        public List<string> Reserved; // Saraksts ar rezervētajiem vārdiem.

        private void checkSuperClassMethods() 
        {

        }

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
			Reserved = new() { "class", "association", "Integer", "String", "Boolean", "Real", "URL", "private", "public", "BaseObject" };
			return VisitChildren(context);
		}
		public void Compile(LanguageParser.CodeContext context)
        {
            try
            {
                Visit(context);
                checkSuperClassMethods();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
    }
}
