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
		Method _method; // Pagaidu metode

		/// <summary>
		/// Apstaigājam metodi
		/// </summary>
		public object VisitMethod([NotNull] FieldContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			
			// Sagatavojam metodi
			_method = new();
			_method.Line = (uint)context.fieldDefinition().Start.Line;
			_urlFound = false;

			var methodBody = context.fieldDefinition().variableDefinition();
			var argumentBody = context.fieldDefinition().methodDefinition();

			uint line = (uint)context.Start.Line; // Nosaka rindu, kurā ir kļūda, ja tādu atrod.

			if (methodBody != null)
			{
				// Pārbauda, vai mainīgajam ir aizsardzība
				if (methodBody.fieldProtection() != null) 
				{
					line = (uint)methodBody.fieldProtection().Stop.Line;
					VisitMethodProtection(methodBody.fieldProtection()); 
				}

				// Pārbauda, vai mainīgajam ir datu tips un/vai vārds
				if (methodBody.variable() != null)
				{
					// Pārbauda, vai mainīgajam ir datu tips
					if (methodBody.variable().fieldDataType() != null)
					{
						line = (uint)methodBody.variable().fieldDataType().Stop.Line;
						VisitMethodDataType(methodBody.variable().fieldDataType());
					}
					else { Errors.Add("At line " + line + ": Missing datatype for method!"); }

					// Pārbauda, vai mainīgajam ir vārds
					if (methodBody.variable().fieldName() != null)
					{
						VisitMethodName(methodBody.variable().fieldName());
					}
					else { Errors.Add("At line " + line + ": Missing name for method!"); }
				}
				else { Errors.Add("At line " + line + ": Missing datatype and name for method!"); }
			}
			else { Errors.Add("At line " + line + ": Missing datatype and name for method!"); }

			// Pārbauda, vai metodei ir argumentu definīcija
			if (argumentBody != null) { VisitMethodDefinition(argumentBody); }
			else { Errors.Add("At line " + line + ": Missing arguemnt definition for method!"); }

			// Pārbauda metodes anotācijas
			if (context.annotation().Length > 0)
			{
				foreach (var a in context.annotation())
				{
					VisitAnnotation(a);
				}
				if (_urlFound == false) { Errors.Add("At line " + context.Start.Line + ": Missing method URL!"); }
			}
			else { Errors.Add("At line " + context.Start.Line + ": Missing method URL!"); }

			_class._methods.Add(_method);

			return null;
		}

		/// <summary>
		/// Apstaigājam metodes aizsardzību
		/// </summary>
		public object VisitMethodProtection([NotNull] FieldProtectionContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			
			_method.Protection = context.GetText();
			
			return null;
		}

		/// <summary>
		/// Apstaigājam metodes datu tipu
		/// </summary>
		public object VisitMethodDataType([NotNull] FieldDataTypeContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");

			_method.Type = context.GetText();

			if (_method.Type == null) { Errors.Add("At line " + context.Start.Line + ": '" + context.GetText() + "' is not a valid data type!"); }
			
			return null;
		}

		/// <summary>
		/// Apstaigājam metodes vārdu
		/// </summary>
		public object VisitMethodName([NotNull] FieldNameContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");

			_method.Name = context.GetText();

			// Pārbauda, vai metodes vārds sakrīt ar klases vārdu
			if (_method.Name == _class.ClassName)
			{
				Errors.Add("At line " + context.Start.Line + ": A method cannot be named after class name!");
				return null;
			}

			// Pārbauda, vai metodes vārds sakrīt ar rezervētajiem vārdiem
			foreach (var r in Reserved)
			{
				if (_method.Name == r)
				{
					Errors.Add("At line " + context.Start.Line + ": A method cannot be named '" + r + "'!");
					return null;
				}
			}

			if (checkMethodName(context, _class, false) == true)
			{
				// Pārbauda, vai klasei ir virsklase
				if (_class.SuperClass != null)
				{
					checkMethodName(context, _class.SuperClass, true);
				}
			}

			return null;
		}

		public bool checkMethodName([NotNull] FieldNameContext context, Class _class, bool isSuperClass) 
		{
			string message;

			if (isSuperClass == false) { message = "At line " + context.Start.Line + ": a field with name '" + context.GetText() + "' already exists in class " + _class.ClassName + "! Check line "; }
			else { message = "At line " + context.Start.Line + ": a field with name '" + context.GetText() + "' already exists in superclass " + _class.ClassName + "! Check line "; }

			// Pārbauda, vai metodes vārds atkārtojas klasē starp mainīgajiem
			foreach (var v in _class._variables)
			{
				if (v.Name == context.GetText())
				{
					Errors.Add(message + v.Line + "!");
					return false;
				}
			}

			// Pārbauda, vai metodes vārds atkārtojas klasē starp asociācijām
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
				// Pārbauda, vai metodes vārds atkārtojas klasē starp citām metodēm
				foreach (var m in _class._methods)
				{
					if (m.Name == context.GetText())
					{
						Errors.Add(message + m.Line + "!");
						return false;
					}
				}
			}
			else 
			{
				// Pārbauda, vai metode ir sastopama virsklasē
				foreach (var m in _class._methods)
				{
					// Pārbauda, vai metožu vārdi sakrīt
					if (m.Name == _method.Name)
					{
						if (m.primitiveType == _method.primitiveType) { Errors.Add("At line " + context.Start.Line + ": Method " + _variable.Name + ", that exists in superclass " + _class.ClassName + " does not have the same datatype!"); }
						
						// Pārbauda, vai metožu argumentu skaits sakrīt
						if (m._arguments.Count == _method._arguments.Count)
						{
							// Pārbauda, vai metožu argumentu datu tipi un vārdi sakrīt
							for (int x = 0; x < m._arguments.Count; x++)
							{
								if (m._arguments[x].Name != _method._arguments[x].Name)
								{
									Errors.Add("At line " + _method.Line + ": Argument No. " + (x + 1) + ", does not have the same name as in " + _class.ClassName + "! Check line " + m.Line + "!");
								}
								if (m._arguments[x].Type != _method._arguments[x].Type)
								{
									Errors.Add("At line " + _method.Line + ": Argument No. " + (x + 1) + ", does not have the same datatype as in " + _class.ClassName + "! Check line " + m.Line + "!");
								}
							}
						}
						else { Errors.Add("At line " + context.Start.Line + ": Method " + _method.Name + ", that exists in superclass " + _class.ClassName + " does not have equal amount of arguments!"); }
						return true;
					}
				}
			}
			return true;
		}
	}
}