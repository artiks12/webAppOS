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
			if (context.children != null) 
			{
				if (context.children.Count > 0)
				{
					bool needComa = false;
					foreach (var c in context.children)
					{
						switch (c.GetType().ToString())
						{
							case "ANTLR.LanguageParser+ComaContext":
								if (needComa == false)
								{
									Errors.Add("At line " + ((ComaContext)c).Start.Line + ": argument expected!");
								}
								else { needComa = false; }
								break;
							case "ANTLR.LanguageParser+ArgumentContext":
								if (needComa == false)
								{
									needComa = true;
								}
								else
								{
									Errors.Add("At line " + ((ArgumentContext)c).Start.Line + ": Syntax error! Missing ','!");
								}
								VisitArgument((ArgumentContext)c);
								break;
						}
					}
				}
			}
			return null;
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
			_argument = new();
			_argument.Line = (uint)context.Start.Line;
			VisitChildren(context);
			if (context.argumentName() == null) { Errors.Add("At line " + context.Start.Line + ": Missing argument name!"); }
			_method._arguments.Add(_argument);
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
			_argument.Type = context.GetText();
			// Pārbauda, vai argumentam ir dots pareizs datu tips
			if (_argument.Type == null)
			{
				Errors.Add("At line " + context.Start.Line + ": Unsupported datatype '" + context.GetText() + "' was given for argument!");
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
			bool found = false;

			foreach (var r in Reserved) 
			{
				if (r == context.GetText()) 
				{
					Errors.Add("At line " + context.Start.Line + ": Argument cannot be named '" + r + "'!");
					_argument.Name = " ";
					found = true;
					break;
				}
			}
			if (found == false) 
			{
				// Pārbauda, vai arguments ar doto vārdu jau ir definēts
				foreach (var arg in _method._arguments)
				{
					if (arg.Name == context.GetText())
					{
						Errors.Add("At line " + context.Start.Line + ": Argument with name '" + context.GetText() + "' already exists! Check line " + arg.Line + "!");
						_argument.Name = " ";
						found = true;
						break;
					}
				}
				if (found == false) { _argument.Name = context.GetText(); }
			}
			return null;
		}
	}
}