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
		Variable _variable;

		public object VisitVariable([NotNull] FieldContext context) 
		{
			_variable = new();
			_variable.Line = (uint)context.Start.Line;

			var variableBody = context.fieldDefinition();

			if (variableBody.fieldProtection() != null) { VisitVariableProtection(variableBody.fieldProtection()); }

			if (variableBody.fieldDataType() != null) { VisitVariableDataType(variableBody.fieldDataType()); }
			else { Errors.Add("At line " + context.Start.Line + ": Missing datatype!"); }

			if (variableBody.fieldName() != null) { VisitVariableName(variableBody.fieldName()); }
			else { Errors.Add("At line " + context.Start.Line + ": Missing name!"); }

			_class._variables.Add(_variable);
			
			return null;
		}

		public object VisitVariableProtection([NotNull] FieldProtectionContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			var p = context.GetText();
			_variable.Protection = p;
			return null;
		}
		public object VisitVariableDataType([NotNull] FieldDataTypeContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			// Pārbauda, vai mainīgajam ir dots datu tips
			if (context.GetText() == "") { Errors.Add("At line " + context.Start.Line + ": Missing variable datatype!"); }
			else
			{
				_variable.Type = context.GetText();
				// Pārbauda, vai mainīgajam ir dots pareizs datu tips
				if (_variable.Type == null)
				{
					Errors.Add("At line " + context.Start.Line + ": Unsupported datatype '" + context.GetText() + "' was given for variable!");
				}
			}
			return VisitChildren(context);
		}
		public object VisitVariableName([NotNull] FieldNameContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			var n = context.GetText();
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
					// Pārbauda, vai mainīgā vārds atkārtojas klasē starp citām metodēm
					foreach (var m in _class._methods)
					{
						if (m.Name == n)
						{
							Errors.Add("At line " + context.Start.Line + ": a field with name '" + n + "' already exists! Check line " + m.Line + "!");
							_variable.Name = " ";
							found = true;
							break;
						}
					}
					if (found == false)
					{
						// Pārbauda, vai mainīgā vārds atkārtojas klasē starp mainīgajiem
						foreach (var v in _class._variables)
						{
							if (v.Name == n)
							{
								Errors.Add("At line " + context.Start.Line + ": a field with name '" + n + "' already exists! Check line " + v.Line + "!");
								_variable.Name = " ";
								found = true;
								break;
							}
						}
						if (found == false) 
						{
							foreach (var ae in _class._associationEnds)
							{
								if (ae.RoleName == n)
								{
									Errors.Add("At line " + context.Start.Line + ": a field with name '" + n + "' already exists in class! Check line " + Associations[(int)ae.ID].Line + "!");
									_variable.Name = " ";
									found = true;
									break;
								}
							}
							if (found == false) 
							{
								if (_class.SuperClass != null)
								{
									foreach (var v in _class.SuperClass._variables)
									{
										if (v.Name == n)
										{
											Errors.Add("At line " + context.Start.Line + ": a field with name '" + n + "' already exists in super class! Check line " + v.Line + "!");
											_variable.Name = " ";
											found = true;
											break;
										}
									}
									if (found == false)
									{
										foreach (var m in _class.SuperClass._methods)
										{
											if (m.Name == n)
											{
												Errors.Add("At line " + context.Start.Line + ": a field with name '" + n + "' already exists in super class! Check line " + m.Line + "!");
												_variable.Name = " ";
												found = true;
												break;
											}
										}
										if (found == false)
										{
											foreach (var ae in _class.SuperClass._associationEnds)
											{
												if (ae.RoleName == n)
												{
													Errors.Add("At line " + context.Start.Line + ": a field with name '" + n + "' already exists in super class! Check line " + Associations[(int)ae.ID].Line + "!");
													_variable.Name = " ";
													found = true;
													break;
												}
											}
											if (found == false)
											{
												_variable.Name = n;
											}
										}
									}
								}
								else 
								{
									_variable.Name = n;
								}
							}
						}
					}
				}
			}
			return null;
		}
	}
}