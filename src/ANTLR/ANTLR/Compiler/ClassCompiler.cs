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
		Class _class;

		/// <summary>
		/// Izveidojam klases objektu
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override object VisitWebMemoryClass([NotNull] WebMemoryClassContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			_class = new();
			_class.Line = (uint)context.Start.Line;
			VisitChildren(context);
			Classes.Add(_class);
			return null;
		}

		/// <summary>
		/// Pārbaudam klases atribūtus
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
        /*public override object VisitClassAttributes([NotNull] ClassAttributesContext context)
        {
			if (context.classType() != null) { VisitClassType(context.classType()); }
			else { Errors.Add("At line " + context.Start.Line + ": Missing keyword 'class'!"); }
			if (context.className() != null) { VisitClassName(context.className()); }
			else { Errors.Add("At line " + context.Start.Line + ": Missing class name!"); }
			return null;
		}*/

		/// <summary>
		/// Pārbaudam nepieciešamību pēc virsklašu pārbaudes
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
        public override object VisitSuperClass([NotNull] SuperClassContext context)
        {
			var col = context.COLON();
			var sc = context.superClassName();

			if (sc.Length > 0)
			{
				if (col == null) { Errors.Add("At line " + context.Start.Line + ": Syntax error! Missing ':'!"); }
				VisitSuperClassName(sc[0]);
				if (sc.Length > 1)  { Errors.Add("At line " + sc[1].Start.Line + ": Class '" + _class.ClassName + "' cannot have multiple clases to inherit from!"); }
			}
			else if (col != null) { Errors.Add("At line " + context.Start.Line + ": Syntax error! Unnecessary ':'!"); }
			return null;
        }

        /// <summary>
        /// Iegūstam klases vārdu
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object VisitClassName([NotNull] ClassNameContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			bool found = false;
			// Klases vārds nedrīkst būt rezervētais vārds
			foreach (var r in Reserved) 
			{
				if (context.GetText() == r) 
				{ 
					Errors.Add("At line " + context.Start.Line + ": A class cannot be named '"+r+"'!");
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
			return VisitChildren(context);
		}

		/// <summary>
		/// Iegūstam virsklasi
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
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