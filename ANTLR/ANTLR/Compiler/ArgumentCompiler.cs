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
		Variable _argument; // Pagaidu arguments

		/// <summary>
		/// Apstaigājam argumentus
		/// </summary>
        public override object VisitArguments([NotNull] ArgumentsContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			
			// Pārbauda, vai iekavās ir kaut kas ierakstīts
			if (context.children != null) 
			{
				bool needComa = false; // Vai ir vajadzīgs komats
				uint line = (uint)context.Start.Line;  // Nosaka rindu, kurā ir kļūda, ja tādu atrod.

				// Pārbaudam visu, kas ir ierakstīts iekavās
				foreach (var c in context.children)
				{
					// Ir divi gadījumi - ir argumenta definīcija vai komats
					switch (c.GetType().ToString())
					{
						// Komats
						case "ANTLR.LanguageParser+ComaContext":
							if (needComa == false) { Errors.Add("At line " + line + ": argument expected!"); }
							else 
							{
								needComa = false; 
								
							}
						break;
						
						// Argumenta definīcija
						case "ANTLR.LanguageParser+ArgumentContext":
							if (needComa == false) 
							{
								needComa = true;
								line = (uint)((ArgumentContext)c).Stop.Line;
							}
							else { Errors.Add("At line " + line + ": Syntax error! Missing ','!"); }
							VisitArgument((ArgumentContext)c);
						break;
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Apstaigājam argumentu
		/// </summary>
        public override object VisitArgument([NotNull] ArgumentContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			
			// Sagatavojam argumentu
			_argument = new();
			_argument.Line = (uint)context.Start.Line;

			uint line = (uint)context.Start.Line;  // Nosaka rindu, kurā ir kļūda, ja tādu atrod.
												  
			// Pārbauda, vai mainīgajam ir datu tips
			if (context.argumentDataType() != null)
			{
				line = (uint)context.argumentDataType().Stop.Line;
				VisitArgumentDataType(context.argumentDataType());
			}
			else { Errors.Add("At line " + line + ": Missing datatype for argument!"); }


			// Pārbauda, vai mainīgajam ir datu tips
			if (context.argumentName() != null)
			{
				line = (uint)context.argumentName().Stop.Line;
				VisitArgumentName(context.argumentName());
			}
			else { Errors.Add("At line " + line + ": Missing name for argument!"); }

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

			bool found = false;

			// Pārbauda, vai argumenta vārds sakrīt ar rezervētajiem vārdiem
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
				// Pārbauda, vai arguments ar doto vārdu jau ir definēts starp citiem argumentiem
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