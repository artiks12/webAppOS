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
		Variable _variable; // Pagaidu mainīgais

		/// <summary>
		/// Apstaigājam mainīgo
		/// </summary>
		public object VisitVariable([NotNull] VariableDefinitionContext context) 
		{
			// Sagatavojam īpašību
			_variable = new();
			_variable.Line = (uint)context.Start.Line;

			uint line = (uint)context.Start.Line; // Nosaka rindu, kurā ir kļūda, ja tādu atrod.

			// Pārbauda, vai mainīgajam ir aizsardzība
			if (context.fieldProtection() != null) 
			{
				line = (uint)context.fieldProtection().Stop.Line;
				VisitVariableProtection(context.fieldProtection()); 
			}

			// Pārbauda, vai mainīgajam ir datu tips un/vai vārds
			if (context.variable() != null) 
			{
				// Pārbauda, vai mainīgajam ir datu tips
				if (context.variable().fieldDataType() != null) 
				{
					line = (uint)context.variable().fieldDataType().Stop.Line;
					VisitVariableDataType(context.variable().fieldDataType()); 
				}
				else { Errors.Add("At line " + line + ": Missing datatype for property!"); }

				// Pārbauda, vai mainīgajam ir vārds
				if (context.variable().fieldName() != null) 
				{
					VisitVariableName(context.variable().fieldName()); 
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

			if (_variable.Type == null || _variable.Type == "void") { Errors.Add("At line " + context.Start.Line + ": '" + context.GetText() + "' is not a valid data type for variable!"); }

			return null;
		}

		/// <summary>
		/// Apstaigājam mainīgā vārdu
		/// </summary>
		public object VisitVariableName([NotNull] FieldNameContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");

			_variable.Name = context.GetText();

			// Pārbauda, vai mainīgā vārds sakrīt ar klases vārdu
			if (_variable.Name == _class.ClassName)
			{
				Errors.Add("At line " + context.Start.Line + ": A variable cannot be named after class name!");
				return null;
			}
			// Pārbauda, vai mainīgā vārds sakrīt ar rezervētajiem vārdiem
			foreach (var r in Reserved)
			{
				if (_variable.Name == r)
				{
					Errors.Add("At line " + context.Start.Line + ": A variable cannot be named '" + r + "'!");
					return null;
				}
			}

			if (checkVariableName(context,_class,false) == true) 
			{
				// Pārbauda, vai klasei ir virsklase
				if (_class.SuperClass != null)
				{
					checkVariableName(context, _class.SuperClass, true);
				}
			}
			
			return null;
		}

		public bool checkVariableName([NotNull] FieldNameContext context, Class _class, bool isSuperClass)
		{
			string message;

			if (isSuperClass == false) { message = "At line " + context.Start.Line + ": a field with name '" + context.GetText() + "' already exists in class " + _class.ClassName + "! Check line "; }
			else { message = "At line " + context.Start.Line + ": a field with name '" + context.GetText() + "' already exists in superclass " + _class.ClassName + "! Check line "; }
			
			// Pārbauda, vai mainīgā vārds atkārtojas klasē starp metodēm
			foreach (var m in _class._methods)
			{
				if (m.Name == context.GetText())
				{
					Errors.Add(message + m.Line + "!");
					return false;
				}
			}

			// Pārbauda, vai mainīgā vārds atkārtojas klasē starp asociācijām
			foreach (var ae in _class._associationEnds)
			{
				if (ae.RoleName == context.GetText())
				{
					Errors.Add(message + Associations[(int)ae.ID].Line + "!");
					return false;
				}
			}

			if (isSuperClass == false)
			{
				// Pārbauda, vai mainīgā vārds atkārtojas klasē starp citiem mainīgajiem
				foreach (var v in _class._variables)
				{
					if (v.Name == context.GetText())
					{
						Errors.Add(message + v.Line + "!");
						return false;
					}
				}
			}
			else 
			{
				// Pārbauda, vai mainīgā vārds atkārtojas klasē starp citiem mainīgajiem
				foreach (var v in _class._variables)
				{
					if (v.Name == context.GetText())
					{
						if (v.primitiveType == _variable.primitiveType)
						{
							Errors.Add("At line " + context.Start.Line + ": Variable " + _variable.Name + ", that exists in superclass " + _class.ClassName + " does not have the same datatype!");
							return false;
						}
						return true;
					}
				}
			}
			return true;
		}
	}
}