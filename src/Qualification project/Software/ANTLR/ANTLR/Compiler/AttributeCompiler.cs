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
		Attribute _attribute; // Pagaidu mainīgais

		/// <summary>
		/// Apstaigājam atribūtu
		/// </summary>
		public object VisitAttribute([NotNull] AttributeDefinitionContext context) 
		{
			// Sagatavojam īpašību
			_attribute = new();
			_attribute.Line = (uint)context.Start.Line;

			uint line = (uint)context.Start.Line; // Nosaka rindu, kurā ir kļūda, ja tādu atrod.

			// Pārbauda, vai atribūtam ir aizsardzība
			if (context.fieldProtection() != null)
			{
				line = (uint)context.fieldProtection().Stop.Line;
				VisitattributeProtection(context.fieldProtection());
			}
			else { _attribute.Protection = "public"; }

			// Pārbauda, vai atribūtam ir datu tips un/vai vārds
			if (context.attribute() != null) 
			{
				// Pārbauda, vai atribūtam ir datu tips
				if (context.attribute().fieldDataType() != null) 
				{
					line = (uint)context.attribute().fieldDataType().Stop.Line;
					VisitattributeDataType(context.attribute().fieldDataType()); 
				}
				else { Errors.Add("At line " + line + ": Missing datatype for property!"); }

				// Pārbauda, vai atribūtam ir vārds
				if (context.attribute().fieldName() != null) 
				{
					VisitattributeName(context.attribute().fieldName()); 
				}
				else { Errors.Add("At line " + line + ": Missing name for property!"); }
			}
			else { Errors.Add("At line " + line + ": Missing datatype and name for property!"); }

			_class._attributes.Add(_attribute); // Pievienojam atribūtu klasē mainīgo sarakstā
			
			return null;
		}

		/// <summary>
		/// Apstaigājam atribūta aizsardzību
		/// </summary>
		public object VisitattributeProtection([NotNull] FieldProtectionContext context)
		{
			_attribute.Protection = context.GetText();
			
			return null;
		}

		/// <summary>
		/// Apstaigājam atribūta datu tipu
		/// </summary>
		public object VisitattributeDataType([NotNull] FieldDataTypeContext context)
		{
			_attribute.Type = context.GetText();

			if (_attribute.Type == null || _attribute.Type == "void") { Errors.Add("At line " + context.Start.Line + ": '" + context.GetText() + "' is not a valid data type for attribute!"); }

			return null;
		}

		/// <summary>
		/// Apstaigājam atribūta vārdu
		/// </summary>
		public object VisitattributeName([NotNull] FieldNameContext context)
		{
			_attribute.Name = context.GetText();

			// Pārbauda, vai atribūta vārds sakrīt ar klases vārdu
			if (_attribute.Name == _class.ClassName)
			{
				Errors.Add("At line " + context.Start.Line + ": A attribute cannot be named after class name!");
				return null;
			}
			// Pārbauda, vai atribūta vārds sakrīt ar rezervētajiem vārdiem
			foreach (var r in Reserved)
			{
				if (_attribute.Name == r)
				{
					Errors.Add("At line " + context.Start.Line + ": A attribute cannot be named '" + r + "'!");
					return null;
				}
			}

			if (checkattributeName(context,_class,false) == true) 
			{
				// Pārbauda, vai klasei ir virsklase
				if (_class.SuperClass != null)
				{
					checkattributeName(context, _class.SuperClass, true);
				}
			}
			
			return null;
		}

		/// <summary>
		/// Pārbauda atribūta vārda esamību klasē/virsklasē
		/// </summary>
		public bool checkattributeName([NotNull] FieldNameContext context, Class _class, bool isSuperClass)
		{
			string message;

			if (isSuperClass == false) { message = "At line " + context.Start.Line + ": a field with name '" + context.GetText() + "' already exists in class " + _class.ClassName + "! Check line "; }
			else { message = "At line " + context.Start.Line + ": a field with name '" + context.GetText() + "' already exists in superclass " + _class.ClassName + "! Check line "; }
			
			// Pārbauda, vai atribūta vārds atkārtojas klasē starp metodēm
			foreach (var m in _class._methods)
			{
				if (m.Name == context.GetText())
				{
					Errors.Add(message + m.Line + "!");
					return false;
				}
			}

			// Pārbauda, vai atribūta vārds atkārtojas klasē starp asociācijām
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
				// Pārbauda, vai atribūta vārds atkārtojas klasē starp citiem atribūtiem
				foreach (var v in _class._attributes)
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
				// Pārbauda, vai atribūta vārds atkārtojas virsklasē starp citiem atribūtiem
				foreach (var v in _class._attributes)
				{
					if (v.Name == context.GetText())
					{
						if (v.primitiveType == _attribute.primitiveType)
						{
							Errors.Add("At line " + context.Start.Line + ": attribute " + _attribute.Name + ", that exists in superclass " + _class.ClassName + " does not have the same datatype! Check line " + v.Line + "!");
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