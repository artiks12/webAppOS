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
		Class _class; // Pagaidu klase

		/// <summary>
		/// Apstaigājam klasi
		/// </summary>
		public override object VisitWebMemoryClass([NotNull] WebMemoryClassContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");

			uint line = (uint)context.Start.Line; // Nosaka rindu, kurā ir kļūda, ja tādu atrod.
												  
			// Izveidojam klases instanci
			_class = new();
			_class.Line = (uint)context.Start.Line;

			// Pārbaudam, vai klasei ir "galva".
			if (context.classHead() == null) { Errors.Add("At line " + line + ": Missing class head!"); }
			else 
			{
				line = (uint)context.classHead().Stop.Line;
				VisitClassHead(context.classHead());
			}

			// Pārbaudam, vai klasei ir "ķermenis".
			if (context.classBody() == null) { Errors.Add("At line " + line + ": Missing class body!"); }
			else
			{
				VisitClassBody(context.classBody());
			}
			
			Classes.Add(_class); // Pievienojam klasi sarakstā
			return null;
		}

		/// <summary>
		/// Apstaigājam klases galvu
		/// </summary>
		public override object VisitClassHead([NotNull] ClassHeadContext context)
        {
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");

			// Pārbaudam, vai klasei ir vards.
			if (context.className() == null) { Errors.Add("At line " + context.Start.Line + ": Missing class name!"); }

			return VisitChildren(context);
		}

        /// <summary>
        /// Apstaigājam virsklases lauku
        /// </summary>
        public override object VisitSuperClass([NotNull] SuperClassContext context)
        {
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			
			// Pārbaudam, vai ir dota virsklase
			if (context.superClassName() == null) { Errors.Add("At line " + context.Stop.Line + ": Missing super class!"); }
			
			return VisitChildren(context);
        }

        /// <summary>
        /// Iegūstam klases vārdu
        /// </summary>
        public override object VisitClassName([NotNull] ClassNameContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			
			bool found = false;
			// Klases vārds nedrīkst būt rezervētais vārds
			foreach (var r in Reserved) 
			{
				if (context.GetText() == r) 
				{ 
					Errors.Add("At line " + context.Start.Line + ": A class cannot be named '" + r + "'!");
					_class.ClassName = " ";
					found = true; 
					break; 
				}
			}

			if (found == false)
			{
				// Pārbaudam, vai eksistē klase ar doto vārdu
				foreach (var c in Classes)
				{
					if (context.GetText() == c.ClassName) 
					{ 
						Errors.Add("At line " + context.Start.Line + ": A class '" + context.GetText() + "' already exists! Check line " + c.Line + "!");
						_class.ClassName = " ";
						found = true; 
						break; 
					}
				}
				if (found == false) { _class.ClassName = context.GetText(); }
			}

			return null;
		}

		/// <summary>
		/// Iegūstam virsklasi
		/// </summary>
		public override object VisitSuperClassName([NotNull] SuperClassNameContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			
			bool found = false;
			// Vispirms pārbaudam, vai virsklasei ir tāds pats vārds, kā pamatklasei
			if (context.GetText() == _class.ClassName) 
			{
				Errors.Add("At line " + context.Start.Line + ": Cannot inherit from class of the same name!"); 
				found = true;
			}
			if (found == false) 
			{
				// Tad meklē, vai eksistē klase ar doto vārdu.
				if (found == false)
				{
					foreach (var c in Classes)
					{
						if (context.GetText() == c.ClassName)
						{
							_class.SuperClass = c;
							c.isSuperClass = true;
							found = true;
							break;
						}
					}
					if (found == false)
					{
						Errors.Add("At line " + context.Start.Line + ": There is no class '" + context.GetText() + "' for class '" + _class.ClassName + "' to inherit from!");
					}
				}
			}
			return null;
		}
	}
}