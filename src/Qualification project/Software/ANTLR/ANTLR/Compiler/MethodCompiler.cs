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
			// Sagatavojam metodi
			_method = new();
			_method.generate = true;
			_urlFound = false;

			var methodBody = context.fieldDefinition().attributeDefinition();
			var argumentBody = context.fieldDefinition().methodDefinition();
			
			uint line = (uint)context.Start.Line; // Nosaka rindu, kurā ir kļūda, ja tādu atrod.
			if (context.annotation().Length != 0) { line = (uint)context.annotation()[context.annotation().Length - 1].Start.Line; }
			

			if (methodBody != null)
			{
				// Pārbauda, vai metodei ir aizsardzība
				if (methodBody.fieldProtection() != null) 
				{
					line = (uint)methodBody.fieldProtection().Stop.Line;
					VisitMethodProtection(methodBody.fieldProtection()); 
				}
				else { _method.Protection = "public"; }

				// Pārbauda, vai metodei ir datu tips un/vai vārds
				if (methodBody.attribute() != null)
				{
					// Pārbauda, vai metodei ir datu tips
					if (methodBody.attribute().fieldDataType() != null)
					{
						line = (uint)methodBody.attribute().fieldDataType().Stop.Line;
						VisitMethodDataType(methodBody.attribute().fieldDataType());
					}
					else { Errors.Add("At line " + line + ": Missing datatype for method!"); }

					// Pārbauda, vai metodei ir argumentu definīcija
					if (argumentBody != null) { VisitMethodDefinition(argumentBody); }
					else { Errors.Add("At line " + methodBody.Stop.Line + ": Missing arguemnt definition for method!"); }

					// Pārbauda, vai metodei ir vārds
					if (methodBody.attribute().fieldName() != null)
					{
						line = (uint)methodBody.attribute().fieldName().Stop.Line;
						VisitMethodName(methodBody.attribute().fieldName());
					}
					else { Errors.Add("At line " + line + ": Missing name for method!"); }
				}
				else { Errors.Add("At line " + line + ": Missing datatype and name for method!"); }
			}
			else { Errors.Add("At line " + line + ": Missing datatype and name for method!"); }


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

			if (_method.Name != null) 
			{
				_method.Line = (uint)context.fieldDefinition().attributeDefinition().attribute().fieldName().Start.Line;
				_class._methods.Add(_method);
			}

			return null;
		}

		/// <summary>
		/// Apstaigājam metodes aizsardzību
		/// </summary>
		public object VisitMethodProtection([NotNull] FieldProtectionContext context)
		{
			_method.Protection = context.GetText();
			
			return null;
		}

		/// <summary>
		/// Apstaigājam metodes datu tipu
		/// </summary>
		public object VisitMethodDataType([NotNull] FieldDataTypeContext context)
		{
			_method.Type = context.GetText();

			if (_method.Type == null) { Errors.Add("At line " + context.Start.Line + ": '" + context.GetText() + "' is not a valid data type!"); }
			
			return null;
		}

		/// <summary>
		/// Apstaigājam metodes vārdu
		/// </summary>
		public object VisitMethodName([NotNull] FieldNameContext context)
		{
			// Pārbauda, vai metodes vārds sakrīt ar klases vārdu
			if (context.GetText() == _class.ClassName)
			{
				Errors.Add("At line " + context.Start.Line + ": A method cannot be named after class name!");
				return null;
			}

			// Pārbauda, vai metodes vārds sakrīt ar rezervētajiem vārdiem
			foreach (var r in Reserved)
			{
				if (context.GetText() == r)
				{
					Errors.Add("At line " + context.Start.Line + ": A method cannot be named '" + r + "'!");
					return null;
				}
			}

			if (checkMethodName(context, _class, false) == true)
			{
				var sc = _class.SuperClass;
				while (sc != null)
				{
					if (checkMethodName(context, sc, true) == false) { return null; }
					sc = sc.SuperClass;

				}
				_method.Name = context.GetText();
			}

			return null;
		}

		/// <summary>
		/// Pārbauda metodes vārda esamību klasē/virsklasē
		/// </summary>
		public bool checkMethodName([NotNull] FieldNameContext context, Class _checkClass, bool isSuperClass) 
		{
			string message;

			if (isSuperClass == false) { message = "At line " + context.Start.Line + ": a field with name '" + context.GetText() + "' already exists in class '" + _checkClass.ClassName + "'! Check line "; }
			else { message = "At line " + context.Start.Line + ": a field with name '" + context.GetText() + "' already exists in superclass '" + _checkClass.ClassName + "'! Check line "; }

			

			// Pārbauda, vai metodes vārds atkārtojas klasē starp asociācijām
			foreach (var ae in _checkClass._associationEnds)
			{
				if (ae.RoleName == context.GetText())
				{
					Errors.Add(message + Associations[(int)ae.ID].Line + "!");
					return false;
				}
			}

			if (isSuperClass == false)
			{
				// Pārbauda, vai metodes vārds atkārtojas klasē starp atribūtiem
				foreach (var v in _checkClass._attributes)
				{
					if (v.Name == context.GetText())
					{
						Errors.Add(message + v.Line + "!");
						return false;
					}
				}

				// Pārbauda, vai metodes vārds atkārtojas klasē starp citām metodēm
				foreach (var m in _checkClass._methods)
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
				// Pārbauda, vai metodes vārds atkārtojas klasē starp atribūtiem
				foreach (var v in _checkClass._attributes)
				{
					if (v.Name == context.GetText() && v.Protection == "public")
					{
						Errors.Add(message + v.Line + "!");
						return false;
					}
				}

				// Pārbauda, vai metode ir sastopama virsklasē
				foreach (var m in _checkClass._methods)
				{
					// Pārbauda, vai metožu vārdi sakrīt
					if (m.Name == context.GetText() && m.Protection == "public")
					{
						if (m.Type != _method.Type) 
						{ 
							Errors.Add("At line " + context.Start.Line + ": Method '" + context.GetText() + "' , that exists in superclass '" + _checkClass.ClassName + "' , does not have the same datatype! Check line " + m.Line + "!"); 
						}

						// Pārbauda, vai metožu argumentu skaits sakrīt
						if (m._arguments.Count == _method._arguments.Count)
						{
							// Pārbauda, vai metožu argumentu datu tipi un vārdi sakrīt
							for (int x = 0; x < m._arguments.Count; x++)
							{
								if (m._arguments[x].Type != null && _method._arguments[x].Type != null)
								{
									if (m._arguments[x].Type != _method._arguments[x].Type) 
									{
										Errors.Add("At line " + _method._arguments[x].Line + ": Argument No. " + (x + 1) + ", does not have the same datatype as in '" + _checkClass.ClassName + "'! Check line " + m.Line + "!");
									}
								}
							}
						}
						else 
						{ 
							Errors.Add("At line " + context.Start.Line + ": Method '" + context.GetText() + "' , that exists in superclass '" + _checkClass.ClassName + "' does not have equal amount of arguments! Check line " + m.Line + "!"); 
						}
						return false;
					}
				}
			}
			return true;
		}
	}
}