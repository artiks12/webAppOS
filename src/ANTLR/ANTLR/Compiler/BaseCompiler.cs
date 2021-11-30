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
		/// Metode, kas pārbauda, vai vārdtelpa ir sintaktiski pareiza
		/// </summary>
		private static bool checkNamespace(string _namespace)
		{
			// Pārbauda, vai pirmais simbols ir burts vai apakšsvītra
			if (_namespace[0] == '_' || (_namespace[0] >= 'a' && (int)_namespace[0] <= 'z') || (_namespace[0] >= 'A' && (int)_namespace[0] <= 'Z'))
			{
				// Pārbauda vai visi pārejie simboli ir burti, cipari vai apakšsvītras
				for (int x = 1; x < _namespace.Length; x++)
				{
					if (!(_namespace[x] == '_' || (_namespace[x] >= '0' && (int)_namespace[x] <= '9') || (_namespace[x] >= 'a' && (int)_namespace[x] <= 'z') || (_namespace[x] >= 'A' && (int)_namespace[x] <= 'Z')))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

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
			else 
			{
				if (context.blockType() != null) 
				{
					if (context.blockType().GetText() == "class")
					{
						if (context.blockBody().webMemoryClass() == null) { Errors.Add("At line " + line + ": class definition is given as association definition!"); }
						else { VisitBlockBody(context.blockBody()); }
					}
					else if (context.blockType().GetText() == "association")
					{
						if (context.blockBody().association() == null) { Errors.Add("At line " + line + ": association definition is given as class definition!"); }
						else { VisitBlockBody(context.blockBody()); }
					}
				}
			}

			return null;
        }

		/// <summary>
		/// Apstaigā bloka tipu
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
        public override object VisitBlockType([NotNull] BlockTypeContext context)
        {
			if (context.BLOCKTYPE() == null) { Errors.Add("At line " + context.Start.Line + ": '" + context.GetText() + "' is not a block type! Use 'class' or 'association' instead!"); }
            
			return null;
        }

        /// <summary>
        /// Kompilēšanas pamtfunkcija
        /// </summary>
        public void Compile(LanguageParser.CodeContext context, string _namespace) 
		{
			// Pārbauda, vai padotā vārdtelpa ir sintaktiski pareizs
			if (!checkNamespace(_namespace))
			{
				Errors.Add("'" + _namespace + "' is in incorrect format!");
			}

			// Apstaigā kodu
			Visit(context);

			// Pārbauda, vai kodā nav kļūdu. Ja nav, tad ģenerējam starpkodu
			if (Errors.Count != 0)
			{
				Console.WriteLine("");
				Console.WriteLine("Compilation unsuccessful! Errors encountered:");
				foreach (var error in Errors)
				{
					Console.WriteLine(error);
				}
			}
			else 
			{ 
				Program.generate(_namespace);

				Console.WriteLine("");
				Console.WriteLine("Compilation successful!");
			}
		}
	}
}