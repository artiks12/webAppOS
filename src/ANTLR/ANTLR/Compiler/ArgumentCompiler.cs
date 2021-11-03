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
		Variable _argument;

        public override object VisitArguments([NotNull] ArgumentsContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			return VisitChildren(context);
		}

		/// <summary>
		/// Izveidojam argumenta objektu, ja arguments ir definēts
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
        public override object VisitArgument([NotNull] ArgumentContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			// Pārbauda, vai arguments ir definēts
			if (context.GetText() == "") { Errors.Add("At line " + context.Start.Line + ": Missing argument!");}
			else 
			{
				_argument = new();
				_argument.Line = (uint)context.Start.Line;
				VisitChildren(context);
				if (context.argumentName() == null) { Errors.Add("At line " + context.Start.Line + ": Missing argument name!"); }
				_method._arguments.Add(_argument);
			}
			return null;
        }

		/// <summary>
		/// Iegūstam argumenta datu tipu
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
        public override object VisitArgumentDataType([NotNull] ArgumentDataTypeContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			// Pārbauda, vai argumentam ir dots datu tips
			if (context.GetText() == "") { Errors.Add("At line " + context.Start.Line + ": Missing argument datatype!"); }
			else
			{
				_argument.Type = context.GetText();
				// Pārbauda, vai argumentam ir dots pareizs datu tips
				if (_argument.Type == null) 
				{
					Errors.Add("At line " + context.Start.Line + ": Unsupported datatype '" + context.GetText() + "' was given for argument!");
				}
			}
			return VisitChildren(context);
		}

		/// <summary>
		/// Iegūst argumenta vārdu
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
        public override object VisitArgumentName([NotNull] ArgumentNameContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			// Pārbauda, vai argumentam ir dots vārds
			if (context.GetText().StartsWith("<missing")) { Errors.Add("At line " + context.Start.Line + ": Missing argument name!"); }
			else 
			{
				bool found = false;
				// Pārbauda, vai arguments ar doto vārdu jau ir definēts
				foreach (var arg in _method._arguments)
				{
					if (arg.Name == context.GetText())
					{
						Errors.Add("At line " + context.Start.Line + ": Argument with given name already exists! Check line " + arg.Line + "!");
						_argument.Name = " ";
						found = true;
						break;
					}
				}
				if (found == false) { _argument.Name = context.GetText(); }
			}
			return VisitChildren(context);
		}
	}
}