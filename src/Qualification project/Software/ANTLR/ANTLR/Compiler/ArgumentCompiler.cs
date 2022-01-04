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
		Attribute _argument; // Pagaidu arguments

		/// <summary>
		/// Apstaigājam argumentus
		/// </summary>
        public override object VisitArguments([NotNull] ArgumentsContext context)
		{
			// Pārbauda, vai iekavās ir kaut kas ierakstīts
			if (context.children != null) 
			{
				bool needComa = false; // Vai ir vajadzīgs komats
				var start = (MethodDefinitionContext)context.Parent; // Rindas fiksēsanu sākam no iekavam
				uint line = (uint)start.Start.Line; // Nosaka rindu, kurā ir kļūda, ja tādu atrod.

				var c = context.children;
				// Pārbaudam visu, kas ir ierakstīts iekavās
				for (int x = 0; x < c.Count; x++) 
				{
					// Ir divi gadījumi - ir argumenta definīcija vai komats
					switch (c[x].GetType().Name)
					{
						// Komats
						case "ComaContext":
							// Pēc komata nevajag vēl vienu komatu
							if (needComa == false) { Errors.Add("At line " + line + ": Argument expected!"); }
							else { needComa = false; }

							// Skatamies, lai iekavās pēdējais ierakstītais elements nav komats
							if (x + 1 == c.Count) { Errors.Add("At line " + (uint)((ComaContext)c[x]).Stop.Line + ": Argument expected!"); }

							line = (uint)((ComaContext)c[x]).Stop.Line;
						break;

						// Argumenta definīcija
						case "ArgumentContext":
							// Pēc argumenta vajag komatu
							if (needComa == false) { needComa = true; }
							else { Errors.Add("At line " + line + ": Syntax error! Missing ','!"); }
							
							VisitArgument((ArgumentContext)c[x]);
							line = (uint)((ArgumentContext)c[x]).Stop.Line;
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
			// Sagatavojam argumentu
			_argument = new();

			uint line = (uint)context.Start.Line;  // Nosaka rindu, kurā ir kļūda, ja tādu atrod.
												  
			// Pārbauda, vai argumentam ir datu tips
			if (context.argumentDataType() != null)
			{
				line = (uint)context.argumentDataType().Stop.Line;
				VisitArgumentDataType(context.argumentDataType());
			}
			else { Errors.Add("At line " + line + ": Missing datatype for argument!"); }

			// Pārbauda, vai argumentam ir vārds
			if (context.argumentName() != null) { VisitArgumentName(context.argumentName()); }
			else { Errors.Add("At line " + line + ": Missing name for argument!"); }

			// Ja argumentam ir vārds, tad vārda rindu izmanto kā argumenta rindu, citādāk izmanto datu tipa rindu
			if (_argument.Name != null) { _argument.Line = (uint)context.argumentName().Start.Line; }
			else { _argument.Line = (uint)context.Start.Line; }
			_method.Arguments.Add(_argument);

			return null;
		}

		/// <summary>
		/// Iegūstam argumenta datu tipu
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
        public override object VisitArgumentDataType([NotNull] ArgumentDataTypeContext context)
		{
			_argument.Type = context.GetText();

			// Pārbaudam, vai argumentam ir pareizs datu tips
			if (_argument.Type == null || _argument.Type == "void") { Errors.Add("At line " + context.Start.Line + ": '" + context.GetText() + "' is not a valid data type for argument!"); }

			return VisitChildren(context);
		}

		/// <summary>
		/// Iegūst argumenta vārdu
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
        public override object VisitArgumentName([NotNull] ArgumentNameContext context)
		{
			// Pārbauda, vai argumenta vārds sakrīt ar rezervētajiem vārdiem
			foreach (var r in Reserved) 
			{
				if (r == context.GetText()) 
				{
					Errors.Add("At line " + context.Start.Line + ": Argument cannot be named '" + r + "'!");
					return null;
				}
			}

			// Pārbauda, vai arguments ar doto vārdu jau ir definēts starp citiem argumentiem
			foreach (var arg in _method.Arguments)
			{
				if (arg.Name == context.GetText())
				{
					Errors.Add("At line " + context.Start.Line + ": Argument with name '" + context.GetText() + "' already exists! Check line " + arg.Line + "!");
					return null;
				}
			}

			_argument.Name = context.GetText();

			return null;
		}
	}
}