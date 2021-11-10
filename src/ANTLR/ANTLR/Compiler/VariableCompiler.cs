﻿using ANTLR;
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
		Variable _variable; // Pagaidu mainīgais

		/// <summary>
		/// Apstaigājam mainīgo
		/// </summary>
		public object VisitVariable([NotNull] FieldContext context) 
		{
			// Sagatavojam īpašību
			_variable = new();
			_variable.Line = (uint)context.Start.Line;

			uint line = (uint)context.Start.Line; // Nosaka rindu, kurā ir kļūda, ja tādu atrod.

			var variableBody = context.fieldDefinition().variableDefinition(); // Mainīgā ķermenis

			// Pārbauda, vai mainīgajam ir aizsardzība
			if (variableBody.fieldProtection() != null) 
			{
				line = (uint)variableBody.fieldProtection().Stop.Line;
				VisitVariableProtection(variableBody.fieldProtection()); 
			}

			// Pārbauda, vai mainīgajam ir datu tips un/vai vārds
			if (variableBody.variable() != null) 
			{
				// Pārbauda, vai mainīgajam ir datu tips
				if (variableBody.variable().fieldDataType() != null) 
				{
					line = (uint)variableBody.variable().fieldDataType().Stop.Line;
					VisitVariableDataType(variableBody.variable().fieldDataType()); 
				}
				else { Errors.Add("At line " + line + ": Missing datatype for property!"); }

				// Pārbauda, vai mainīgajam ir vārds
				if (variableBody.variable().fieldName() != null) 
				{
					VisitVariableName(variableBody.variable().fieldName()); 
				}
				else { Errors.Add("At line " + line + ": Missing name for property!"); }
			}
			else { Errors.Add("At line " + line + ": Missing datatype and name for property!"); }

			_class._variables.Add(_variable); // Pievienojam mainīgo klasē mainīgo sarakstā
			
			return null;
		}

		/// <summary>
		/// Apstaigājam mainīgā aizsardzību
		/// </summary>
		public object VisitVariableProtection([NotNull] FieldProtectionContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			
			_variable.Protection = context.GetText();
			
			return null;
		}

		/// <summary>
		/// Apstaigājam mainīgā datu tipu
		/// </summary>
		public object VisitVariableDataType([NotNull] FieldDataTypeContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");

			_variable.Type = context.GetText();
			
			return null;
		}

		/// <summary>
		/// Apstaigājam mainīgā vārdu
		/// </summary>
		public object VisitVariableName([NotNull] FieldNameContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			
			// Pārbauda, vai mainīgā vārds sakrīt ar klases vārdu
			if (context.GetText() == _class.ClassName)
			{
				Errors.Add("At line " + context.Start.Line + ": A variable cannot be named after class name!");
				_variable.Name = " ";
			}
			else
			{
				bool found = false;
				// Pārbauda, vai mainīgā vārds sakrīt ar rezervētajiem vārdiem
				foreach (var r in Reserved)
				{
					if (context.GetText() == r)
					{
						Errors.Add("At line " + context.Start.Line + ": A variable cannot be named '" + r + "'!");
						_variable.Name = " ";
						found = true;
						break;
					}
				}
				if (found == false)
				{
					// Pārbauda, vai mainīgā vārds atkārtojas klasē starp metodēm
					foreach (var m in _class._methods)
					{
						if (m.Name == context.GetText())
						{
							Errors.Add("At line " + context.Start.Line + ": a field with name '" + context.GetText() + "' already exists! Check line " + m.Line + "!");
							_variable.Name = " ";
							found = true;
							break;
						}
					}
					if (found == false)
					{
						// Pārbauda, vai mainīgā vārds atkārtojas klasē starp citiem mainīgajiem
						foreach (var v in _class._variables)
						{
							if (v.Name == context.GetText())
							{
								Errors.Add("At line " + context.Start.Line + ": a field with name '" + context.GetText() + "' already exists! Check line " + v.Line + "!");
								_variable.Name = " ";
								found = true;
								break;
							}
						}
						if (found == false) 
						{
							// Pārbauda, vai mainīgā vārds atkārtojas klasē starp asociācijām
							foreach (var ae in _class._associationEnds)
							{
								if (ae.RoleName == context.GetText())
								{
									Errors.Add("At line " + context.Start.Line + ": a field with name '" + context.GetText() + "' already exists in class! Check line " + Associations[(int)ae.ID].Line + "!");
									_variable.Name = " ";
									found = true;
									break;
								}
							}
							if (found == false) 
							{
								// Pārbauda, vai klasei ir virsklase
								if (_class.SuperClass != null)
								{
									// Pārbauda, vai mainīgā vārds atkārtojas virsklasē starp citiem mainīgajiem
									foreach (var v in _class.SuperClass._variables)
									{
										if (v.Name == context.GetText())
										{
											Errors.Add("At line " + context.Start.Line + ": a field with name '" + context.GetText() + "' already exists in super class! Check line " + v.Line + "!");
											_variable.Name = " ";
											found = true;
											break;
										}
									}
									if (found == false)
									{
										// Pārbauda, vai mainīgā vārds atkārtojas virsklasē starp metodēm
										foreach (var m in _class.SuperClass._methods)
										{
											if (m.Name == context.GetText())
											{
												Errors.Add("At line " + context.Start.Line + ": a field with name '" + context.GetText() + "' already exists in super class! Check line " + m.Line + "!");
												_variable.Name = " ";
												found = true;
												break;
											}
										}
										if (found == false)
										{
											// Pārbauda, vai mainīgā vārds atkārtojas virsklasē starp asociācijām
											foreach (var ae in _class.SuperClass._associationEnds)
											{
												if (ae.RoleName == context.GetText())
												{
													Errors.Add("At line " + context.Start.Line + ": a field with name '" + context.GetText() + "' already exists in super class! Check line " + Associations[(int)ae.ID].Line + "!");
													_variable.Name = " ";
													found = true;
													break;
												}
											}
											if (found == false) { _variable.Name = context.GetText(); }
										}
									}
								}
								else  { _variable.Name = context.GetText(); }
							}
						}
					}
				}
			}
			return null;
		}
	}
}