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
			var start = (BlocksContext)context.Parent.Parent; // Rindas fiksēšanu sākam no bloka tipa.
			uint line = (uint)start.Start.Line; // Nosaka rindu, kurā ir kļūda, ja tādu atrod.

			_class = new(); // Izveidojam klases instanci

			// Pārbaudam, vai klasei ir "galva".
			if (context.classHead() == null) { Errors.Add("At line " + line + ": Missing class head!"); }
			else 
			{
				line = (uint)context.classHead().Stop.Line;
				VisitClassHead(context.classHead());
			}

			// Pārbaudam, vai klasei ir "ķermenis".
			if (context.classBody() == null) { Errors.Add("At line " + line + ": Missing class body!");}
			else
			{
				VisitClassBody(context.classBody());
			}

			// Ja klasei ir vārds vai klase neatkārtojas, tad klase tiek saglabāta kompilatorā
			if (_class.ClassName != null) 
			{ 
				_class.Line = (uint)context.classHead().className().Start.Line;
				if (_class.SuperClass != null) { _class.SuperClass.SubClasses.Add(_class); } // Ja klasei ir virsklase, tad tajā saglabājam šo klasi kā apakšklasi.
				Classes.Add(_class); // Pievienojam klasi sarakstā
			}
			
			return null;
		}

		/// <summary>
		/// Apstaigājam klases galvu
		/// </summary>
		public override object VisitClassHead([NotNull] ClassHeadContext context)
        {
			var start = (BlocksContext)context.Parent.Parent.Parent; // Rindas fiksēšanu sākam no bloka tipa.
			uint line = (uint)start.Start.Line; // Nosaka rindu, kurā ir kļūda, ja tādu atrod.

			// Pārbaudam, vai klasei ir vards.
			if (context.className() == null) { Errors.Add("At line " + line + ": Missing class name!"); }

			return VisitChildren(context);
		}

        /// <summary>
        /// Apstaigājam virsklases lauku
        /// </summary>
        public override object VisitSuperClass([NotNull] SuperClassContext context)
        {
			// Pārbaudam, vai ir dota virsklase
			if (context.superClassName() == null) { Errors.Add("At line " + context.Stop.Line + ": Missing super class!"); }
			
			return VisitChildren(context);
        }

        /// <summary>
        /// Iegūstam klases vārdu
        /// </summary>
        public override object VisitClassName([NotNull] ClassNameContext context)
		{
			// Pārbaudam, vai klases vards sakrīt ar rezervētajiem vārdiem
			foreach (var r in Reserved) 
			{
				if (context.GetText() == r) 
				{ 
					Errors.Add("At line " + context.Start.Line + ": A class cannot be named '" + r + "'!");
					_class.ClassName = null;
					return null;
				}
			}

			// Pārbaudam, vai eksistē klase ar doto vārdu
			foreach (var c in Classes)
			{
				if (context.GetText() == c.ClassName)
				{
					Errors.Add("At line " + context.Start.Line + ": A class '" + context.GetText() + "' already exists! Check line " + c.Line + "!");
					_class.ClassName = null;
					return null;
				}
			}

			_class.ClassName = context.GetText();

			return null;
		}

		/// <summary>
		/// Iegūstam virsklasi
		/// </summary>
		public override object VisitSuperClassName([NotNull] SuperClassNameContext context)
		{
			// Pārbaudam, vai virsklasei ir tāds pats vārds, kā pamatklasei
			if (context.GetText() == _class.ClassName)
			{
				Errors.Add("At line " + context.Start.Line + ": Cannot inherit from class of the same name!");
			}
			else 
			{
				// Pārbaudam, vai klases vards sakrīt ar rezervētajiem vārdiem
				foreach (var r in Reserved)
				{
					if (context.GetText() == r)
					{
						Errors.Add("At line " + context.Start.Line + ": A class cannot be named '" + r + "'!");
						return null;
					}
				}

				bool found = false; // Nosaka, vai virsklase ir atrasta

				// Pārbaudam, vai eksistē klase, kuru var likt kā virsklasi
				foreach (var c in Classes)
				{
					if (context.GetText() == c.ClassName)
					{
						_class.SuperClass = c;
						found = true;
						break;
					}
				}

				// Ja virsklase nav atrasta, tad saglabajam kļūdu
				if (found == false) { Errors.Add("At line " + context.Start.Line + ": There is no class '" + context.GetText() + "' for class '" + _class.ClassName + "' to inherit from!"); }
			}
			return null;
		}
	}
}