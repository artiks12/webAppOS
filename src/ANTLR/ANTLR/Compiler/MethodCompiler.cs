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
		Method _method;

		/// <summary>
		/// Izveidojam metodes objektu
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public object VisitMethod([NotNull] FieldContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			_method = new();
			_method.Line = (uint)context.Start.Line;

			
			if (context.annotation().Length > 0)
			{
				foreach (var a in context.annotation()) 
				{
					VisitAnnotation(a);
				}
			}
			else { Errors.Add("At line " + context.Start.Line + ": Missing method URL!"); }

			var methodBody = context.fieldDefinition();

			if (methodBody.fieldProtection() != null) { VisitMethodProtection(methodBody.fieldProtection()); }

			if (methodBody.fieldDataType() != null) { VisitMethodDataType(methodBody.fieldDataType()); }
			else { Errors.Add("At line " + context.Start.Line + ": Missing datatype!"); }

			if (methodBody.fieldName() != null) { VisitMethodName(methodBody.fieldName()); }
			else { Errors.Add("At line " + context.Start.Line + ": Missing name!"); }

			if (methodBody.methodDefinition() != null) { VisitMethodDefinition(methodBody.methodDefinition()); }
			else { Errors.Add("At line " + context.Start.Line + ": Missing arguemnt definition!"); }

			/*
			if (_method.Name != " ") 
			{
				if (_class._superClasses.Count > 0)
				{
					foreach (var sc in _class._superClasses)
					{
						foreach (var m in sc._methods)
						{
							if (m.Name == _method.Name) 
							{
								if (m._arguments.Count == _method._arguments.Count)
								{
									bool found = false;
									for (int x = 0; x < m._arguments.Count; x++) 
									{
										if (m._arguments[x].Name != _method._arguments[x].Name) 
										{
											Errors.Add("At line " + _method.Line + ": Argument No. " + x+1 + ", does not have the same name as in " + sc.className + "! Check line " + m.Line + "!");
											found = true;
										}
										if (m._arguments[x].Type != _method._arguments[x].Type)
										{
											Errors.Add("At line " + _method.Line + ": Argument No. " + x+1 + ", does not have the same datatype as in " + sc.className + "! Check line " + m.Line + "!");
											found = true;
										}
									}
								}
								else { Errors.Add("At line " + context.Start.Line + ": Method " + _method.Name + ", that exists in superclass " + sc.className + " does not have equal amount of arguments!"); }
							}
						}
					}
				}
				else
				{
					_class._methods.Add(_method);
				}
			}
			else 
			{
				_class._methods.Add(_method);
			}
			*/

			_class._methods.Add(_method);
			return null;
		}

		/// <summary>
		/// Iegūstam metodes aizsardzības līmeni, ja tāds ir
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public object VisitMethodProtection([NotNull] FieldProtectionContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			var p = context.GetText();
			_method.Protection = p;
			return null;
		}

		/// <summary>
		/// Iegūstam metodes datu tipu
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public object VisitMethodDataType([NotNull] FieldDataTypeContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");

			var t = context.GetText();
			_method.Type = t;
			// Pārbaudam, vai ir pareizs datu tips
			if (_method.Type == null)
			{
				Errors.Add("At line " + context.Start.Line + ": Unsupported datatype '" + context.GetText() + "' was given for method!");
			}
			return null;
		}

		/// <summary>
		/// Iegūstam metodes vārdu
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public object VisitMethodName([NotNull] FieldNameContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			var n = context.GetText();
			// Pārbauda, vai metodes vārds sakrīt ar klases vārdu
			if (context.GetText() == _class.className)
			{
				Errors.Add("At line " + context.Start.Line + ": A method cannot be named after class name!");
				_method.Name = " ";
			}
			else
			{
				bool found = false;
				// Pārbauda, vai metodes vārds sakrīt ar rezervētajiem vārdiem
				foreach (var r in Reserved)
				{
					if (context.GetText() == r)
					{
						Errors.Add("At line " + context.Start.Line + ": A method cannot be named '" + r + "'!");
						_method.Name = " ";
						found = true;
						break;
					}
				}
				if (found == false)
				{
					// Pārbauda, vai metodes vārds atkārtojas klasē starp citām metodēm
					foreach (var m in _class._methods)
					{
						if (m.Name == n)
						{
							Errors.Add("At line " + context.Start.Line + ": a field with name '" + n + "' already exists! Check line " + m.Line + "!");
							_method.Name = " ";
							found = true;
							break;
						}
					}
					if (found == false)
					{
						// Pārbauda, vai metodes vārds atkārtojas klasē starp mainīgajiem
						foreach (var v in _class._variables)
						{
							if (v.Name == n)
							{
								Errors.Add("At line " + context.Start.Line + ": a field with name '" + n + "' already exists! Check line " + v.Line + "!");
								_method.Name = " ";
								found = true;
								break;
							}
						}
						if (found == false) { _method.Name = n; }
					}
				}
			}
			return null;
		}
	}
}
