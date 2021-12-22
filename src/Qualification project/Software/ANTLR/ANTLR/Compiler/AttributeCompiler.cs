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
		Attribute _attribute; // Pagaidu mainīgais

		/// <summary>
		/// Apstaigājam atribūtu
		/// </summary>
		public object VisitAttribute([NotNull] AttributeDefinitionContext context) 
		{
			// Sagatavojam īpašību
			_attribute = new();
			_attribute.generate = true;

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
				else { Errors.Add("At line " + line + ": Missing datatype for attribute!"); }

				// Pārbauda, vai atribūtam ir vārds
				if (context.attribute().fieldName() != null) 
				{
					VisitattributeName(context.attribute().fieldName()); 
				}
				else { Errors.Add("At line " + line + ": Missing name for attribute!"); }
			}
			else { Errors.Add("At line " + line + ": Missing datatype and name for attribute!"); }

			// Ja atribūtam ir dots vārds vai atribūts neatkārtojas, tad tas tiek saglabāts klasē
			if (_attribute.Name != null) 
			{
				_attribute.Line = (uint)context.attribute().fieldName().Start.Line;
				_class.Attributes.Add(_attribute); // Pievienojam atribūtu klasē mainīgo sarakstā
			}
			
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

			// Pārbaudam, vai atribūtam ir dots pareizs datu tips
			if (_attribute.Type == null || _attribute.Type == "void") { Errors.Add("At line " + context.Start.Line + ": '" + context.GetText() + "' is not a valid data type for attribute!"); }

			return null;
		}

		/// <summary>
		/// Apstaigājam atribūta vārdu
		/// </summary>
		public object VisitattributeName([NotNull] FieldNameContext context)
		{
			// Pārbauda, vai atribūta vārds sakrīt ar klases vārdu
			if (context.GetText() == _class.ClassName)
			{
				Errors.Add("At line " + context.Start.Line + ": A attribute cannot be named after class name!");
				return null;
			}
			// Pārbauda, vai atribūta vārds sakrīt ar rezervētajiem vārdiem
			foreach (var r in Reserved)
			{
				if (context.GetText() == r)
				{
					Errors.Add("At line " + context.Start.Line + ": A attribute cannot be named '" + r + "'!");
					return null;
				}
			}

			// Pārbauda, vai atribūts sākas ar "_constructor_"
			if (context.GetText().StartsWith("_constructor_")) { Errors.Add("At line " + context.Start.Line + ": An attribute cannot start with '_constructor_'!"); }

			// Pārbauda, vai atribūta vārds jau ir sastopams klasē
			if (checkAttributeNameInClass(context, _class) == true)
			{
				// Pārbauda, vai atribūta vārds jau ir sastopams virsklasēs
				var sc = _class.SuperClass;
				while (sc != null)
				{
					if (checkAttributeNameInSuperClass(context, sc) == false) { return null; }
					sc = sc.SuperClass;
				}
				_attribute.Name = context.GetText();
			}

			return null;
		}

		/// <summary>
		/// Pārbauda atribūta vārda esamību klasē
		/// </summary>
		/// <returns>Atgriež true, ja atribūta vārds klasē neeksistē, citādi atgriež false</returns>
		public bool checkAttributeNameInClass([NotNull] FieldNameContext context, Class _checkClass)
		{
			string message = "At line " + context.Start.Line + ": a field with name '" + context.GetText() + "' already exists in class '" + _checkClass.ClassName + "'! Check line ";

			// Pārbauda, vai atribūta vārds atkārtojas klasē starp metodēm
			foreach (var m in _checkClass.Methods)
			{
				if (m.Name == context.GetText())
				{
					Errors.Add(message + m.Line + "!");
					return false;
				}
			}

			// Pārbauda, vai atribūta vārds atkārtojas klasē starp citiem atribūtiem
			foreach (var v in _checkClass.Attributes)
			{
				if (v.Name == context.GetText())
				{
					Errors.Add(message + v.Line + "!");
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Pārbauda atribūta vārda esamību virsklasē
		/// </summary>
		/// <returns>Atgriež true, ja atribūta vārds virsklasē neeksistē, citādi atgriež false</returns>
		public bool checkAttributeNameInSuperClass([NotNull] FieldNameContext context, Class _checkClass)
		{
			string message = "At line " + context.Start.Line + ": a field with name '" + context.GetText() + "' already exists in superclass '" + _checkClass.ClassName + "'! Check line ";

			// Pārbauda, vai atribūta vārds atkārtojas klasē starp asociācijām
			foreach (var ae in _checkClass.AssociationEnds)
			{
				if (ae.RoleName == context.GetText())
				{
					Errors.Add(message + ae.Line + "!");
					return false;
				}
			}

			// Pārbauda, vai atribūta vārds atkārtojas klasē starp metodēm
			foreach (var m in _checkClass.Methods)
			{
				if (m.Name == context.GetText() && m.Protection == "public")
				{
					Errors.Add(message + m.Line + "!");
					return false;
				}
			}

			// Pārbauda, vai atribūta vārds atkārtojas virsklasē starp citiem atribūtiem
			foreach (var v in _checkClass.Attributes)
			{
				if (v.Name == context.GetText() && v.Protection == "public")
				{
					// Pārbauda, vai sakrītošajiem atribūtiem ir vienāds datu tips
					if (v.Type != _attribute.Type) { Errors.Add("At line " + context.Start.Line + ": attribute '" + context.GetText() + "' , that exists in superclass '" + _checkClass.ClassName + "' , does not have the same datatype! Check line " + v.Line + "!"); }
					return false;
				}
			}

			return true;
		}
	}
}