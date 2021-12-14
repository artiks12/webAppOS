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
		Association _association; // Pagaidu asociācija
		AssociationEnd _source; // Pagaidu asociācijas galapunkts avotklasei
		AssociationEnd _target; // Pagaidu asociācijas galapunkts mērķklasei

		int _sourceClass = -1; // Avotklases ID
		int _targetClass = -1; // Mērķklases ID

		/// <summary>
		/// Apstaigājam asociāciju
		/// </summary>
		public override object VisitAssociation([NotNull] AssociationContext context)
		{
			// Sagatavojas asociācijas un galapunktus
			_association = new();
			_source = new();
			_target = new();
			_association.Line = (uint)context.Start.Line;

			VisitAssociationDefinition(context.associationDefinition()); // Apstaigājam asociācijas definīciju

			// Saglabājam asociācijas galapunktiem asociācijas ID
			_source.ID = (uint)Associations.Count;
			_target.ID = (uint)Associations.Count;

			// Saglabājam asociācijas galapunktiem faktu, vai tas ir avots
			_source.IsSource = true;
			_target.IsSource = false;

			// Pievienojam asociāciju galapunktus asociācijai
			_association.Source = _source;
			_association.Target = _target;

			// Pievienojam asociāciju galapunktus klasēm
			if (_sourceClass != -1) { Classes[_sourceClass]._associationEnds.Add(_target); }
			if (_targetClass != -1) { Classes[_targetClass]._associationEnds.Add(_source); }

			Associations.Add(_association); // Pievienojam asociāciju sarakstam

			return null;
		}

		/// <summary>
		/// Apstaigājam asociācijas definīciju
		/// </summary>
		public override object VisitAssociationDefinition([NotNull] AssociationDefinitionContext context)
        {
			uint line = (uint)context.Start.Line; // Nosaka rindu, kurā ir kļūda, ja tādu atrod.

			// Pārbauda, vai asociācijai ir avote definīcija
			if (context.associationSource() == null) { Errors.Add("At line " + line + ": missing source definition!"); }
			else 
			{
				// Mainam rindu
				if (context.associationSource().associationSourceName() != null) { line = (uint)context.associationSource().associationSourceName().Stop.Line; }
				else if (context.associationSource().associationSourceClass() != null) { line = (uint)context.associationSource().associationSourceClass().Start.Line; }

				// Pārbauda, vai ir ielikts kols
				if (context.associationSource().COLON() == null) { Errors.Add("At line " + line + ": Syntax error! Missing ':'!"); }
				line = (uint)context.associationSource().Stop.Line; 
			}

			// Pārbauda, vai asociācijai ir bultas, kas nosaka kompozīciju
			if (context.ARROWS() == null) { Errors.Add("At line " + line + ": missing arrows for association definition!"); }
			else
			{
				if (context.ARROWS().GetText()[1] == '>') { _association.IsComposition = true; }
				else if (context.ARROWS().GetText()[1] == '-') { _association.IsComposition = false; }
				line = (uint)context.ARROWS().Symbol.Line;
			}

			// Pārbauda, vai asociācijai ir definēts mērķis
			if (context.associationTarget() == null) { Errors.Add("At line " + line + ": missing target definition!"); }
			else 
			{
				// Mainam rindu
				if (context.associationTarget().associationTargetName() != null) { line = (uint)context.associationTarget().associationTargetName().Stop.Line; }
				else if (context.associationTarget().associationTargetClass() != null) { line = (uint)context.associationTarget().associationTargetClass().Start.Line; }

				// Pārbauda, vai ir ielikts kols
				if (context.associationTarget().COLON() == null) { Errors.Add("At line " + line + ": Syntax error! Missing ':'!"); }
			}

			if (VisitAssociationClasses(context) == 2) 
			{
				VisitAssociationRoleNames(context);
			}

			return null;
        }

		/// <summary>
		/// Apstaigājam asociācijas klases
		/// </summary>
		public int VisitAssociationClasses([NotNull] AssociationDefinitionContext context) 
		{
			uint line = (uint)context.Start.Line; // Nosaka rindu, kurā ir kļūda, ja tādu atrod.
			int result = 0;

			if (context.associationSource() != null) 
			{
				// Mainam rindu
				if (context.associationSource().COLON() != null) { line = (uint)context.associationSource().COLON().Symbol.Line; }
				else if (context.associationSource().associationSourceName() != null) { line = (uint)context.associationSource().associationSourceName().Stop.Line; }

				// Parbauda, vai avotam ir definēta klase
				if (context.associationSource().associationSourceClass() == null) { Errors.Add("At line " + line + ": Missing source class!"); }
				else 
				{ 
					var s = VisitAssociationSourceClass(context.associationSource().associationSourceClass());
					if (s != null) { result++; }
				}
			}
			if (context.associationTarget() != null)
			{
				// Mainam rindu
				if (context.associationTarget().COLON() != null) { line = (uint)context.associationTarget().COLON().Symbol.Line; }
				else if (context.associationTarget().associationTargetName() != null) { line = (uint)context.associationTarget().associationTargetName().Stop.Line; }

				// Parbauda, vai avotam ir definēta klase
				if (context.associationTarget().associationTargetClass() == null) { Errors.Add("At line " + line + ": Missing target class!"); }
				else 
				{ 
					var t = VisitAssociationTargetClass(context.associationTarget().associationTargetClass());
					if (t != null) { result++; }
				}
			}

			return result;
		}

		/// <summary>
		/// Apstaigājam asociācijas lomu vārdus
		/// </summary>
		public object VisitAssociationRoleNames([NotNull] AssociationDefinitionContext context)
		{
			uint line = (uint)context.Start.Line; // Nosaka rindu, kurā ir kļūda, ja tādu atrod.

			if (context.associationSource() != null)
			{
				// Parbauda, vai avotam ir definēta klase
				if (context.associationSource().associationSourceName() == null) { Errors.Add("At line " + line + ": Missing source name!"); }
				else { VisitAssociationSourceName(context.associationSource().associationSourceName()); }
			}
			if (context.associationTarget() != null)
			{
				// Parbauda, vai avotam ir definēta klase
				if (context.associationTarget().associationTargetName() == null) { Errors.Add("At line " + line + ": Missing target name!"); }
				else { VisitAssociationTargetName(context.associationTarget().associationTargetName()); }
			}

			return null;
		}

		/// <summary>
		/// Apstaigājam asociācijas klasi
		/// </summary>
		/// <param name="className"></param>
		/// <param name="line"></param>
		/// <param name="isSource"></param>
		public object VisitAssociationClass(string className, int line, bool isSource) 
		{
			foreach (var r in Reserved)
			{
				if (className == r)
				{
					Errors.Add("At line " + line + ": A class cannot be named '" + r + "'!");
					return null;
				}
			}

			bool found = false;

			// Pārbauda, vai eksiste avotklase.
			for (int x = 0; x < Classes.Count; x++)
			{
				if (Classes[x].ClassName == className)
				{
					if (isSource == true)
					{
						_source.Class = Classes[x];
						_sourceClass = x;
					}
					else 
					{
						_target.Class = Classes[x];
						_targetClass = x;
					}
					found = true;
					break;
				}
			}

			string message;
			if (isSource == true) { message = "At line " + line + ": there is no class '" + className + "' to use as source class!"; }
            else { message = "At line " + line + ": there is no class '" + className + "' to use as target class!"; }

			if (found == false) { Errors.Add(message); return null; }

			return new();
		}

		/// <summary>
		/// Apstaigājam asociācijas avota klasi
		/// </summary>
		public override object VisitAssociationSourceClass([NotNull] AssociationSourceClassContext context)
		{
			var s = VisitAssociationClass(context.GetText(),context.Start.Line,true);
			if (s == null) { return null; }
			return new();
		}

		/// <summary>
		/// Apstaigājam asociācijas mērķa klasi
		/// </summary>
		public override object VisitAssociationTargetClass([NotNull] AssociationTargetClassContext context)
		{
			var t = VisitAssociationClass(context.GetText(), context.Start.Line, false);
			if (t == null) { return null; }
			return new();
		}

		/// <summary>
		/// Funkcija, kas pārbauda lomas vārda kļūdas
		/// </summary>
		public bool checkRoleName(string rolename, Class _class, uint line, bool isSuperClass)
		{
			string message;

			if (isSuperClass == false) { message = "At line " + line + ": a field with name '" + rolename + "' already exists in class " + _class.ClassName + "! Check line "; }
			else { message = "At line " + line + ": a field with name '" + rolename + "' already exists in superclass " + _class.ClassName + "! Check line "; }

			// Pārbauda, vai klasē ir atribūts, kura vārds sakrīit ar lomas vārdu
			foreach (var v in _class._attributes)
			{
				if (v.Name == rolename)
				{
					Errors.Add(message + v.Line + "!");
					return false;
				}
			}

			// Pārbauda, vai klasē ir metode, kura vārds sakrīit ar lomas vārdu
			foreach (var m in _class._methods)
			{
				if (m.Name == rolename)
				{
					Errors.Add(message + m.Line + "!");
					return false;
				}
			}

			if (isSuperClass == false)
			{
				// Pārbauda, vai klasē ir asociācijas galapunkts, kura vārds sakrīit ar lomas vārdu
				foreach (var ae in _class._associationEnds)
				{
					if (ae.RoleName == rolename)
					{
						Errors.Add("At line " + line + ": an association with role name '" + ae.RoleName + "' already exists in class '" + _class.ClassName + "'! Check line " + Associations[(int)ae.ID].Line + "!");
						return false;
					}
				}
			}
			else
			{
				// Pārbauda, vai virsklasē ir asociācijas galapunkts, kura vārds sakrīit ar lomas vārdu
				foreach (var ae in _class.SuperClass._associationEnds)
				{
					if (ae.RoleName == rolename)
					{
						Errors.Add("At line " + line + ": an association with role name '" + ae.RoleName + "' already exists in super class '" + _class.SuperClass.ClassName + "'! Check line " + Associations[(int)ae.ID].Line + "!");
						return false;
					}
				}
			}

			return true;
		}

		/// <summary>
		/// Apstaigājam asociācijas avota lomas vārdu
		/// </summary>
		public override object VisitAssociationSourceName([NotNull] AssociationSourceNameContext context)
		{
			_source.RoleName = context.GetText();

			// Pārbauda, vai lomas vārds nesakrīt ar pretējās klases vārdu
			if (_source.RoleName == _target.Class.ClassName)
			{
				Errors.Add("At line " + context.Start.Line + ": association source role name cannot be '" + context.GetText() + "'!");
				return null;
			}
			// Pārbauda, vai lomas vārds nesakrīt ar rezervētajiem vārdiem
			foreach (var r in Reserved)
			{
				if (r == _source.RoleName)
				{
					Errors.Add("At line " + context.Start.Line + ": association source role name cannot be '" + r + "'!");
					return null;
				}
			}

			if (checkRoleName(_source.RoleName, _target.Class, (uint)context.Start.Line, false) == true)
			{
				// Pārbauda, vai klasei ir virsklase
				if (_target.Class.SuperClass != null)
				{
					checkRoleName(_source.RoleName, _target.Class.SuperClass, (uint)context.Start.Line, true);
				}
			}

			return null;
		}

		/// <summary>
		/// Apstaigājam asociācijas mērķa lomas vārdu
		/// </summary>
		public override object VisitAssociationTargetName([NotNull] AssociationTargetNameContext context)
        {
			_target.RoleName = context.GetText();

			// Pārbauda, vai lomas vārds nesakrīt ar pretējās klases vārdu
			if (_target.RoleName == _source.Class.ClassName)
			{
				Errors.Add("At line " + context.Start.Line + ": association source role name cannot be '" + context.GetText() + "'!");
				return null;
			}
			// Pārbauda, vai lomas vārds nesakrīt ar rezervētajiem vārdiem
			foreach (var r in Reserved)
			{
				if (r == _target.RoleName)
				{
					Errors.Add("At line " + context.Start.Line + ": association source role name cannot be '" + r + "'!");
					return null;
				}
			}

			if (checkRoleName(_target.RoleName, _source.Class, (uint)context.Start.Line, false) == true)
			{
				// Pārbauda, vai klasei ir virsklase
				if (_source.Class.SuperClass != null)
				{
					checkRoleName(_target.RoleName, _source.Class.SuperClass, (uint)context.Start.Line, true);
				}
			}

			return null;
		}
	}
}