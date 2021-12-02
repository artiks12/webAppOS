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
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			
			// Sagatavojas asociācijas un galapunktus
			_association = new();
			_source = new();
			_target = new();
			_association.Line = (uint)context.Start.Line;

			VisitChildren(context); // Apstaigājam asociācijas definīciju

			// Saglabājam asociācijas galapunktiem asociācijas ID
			_source.ID = (uint)Associations.Count;
			_target.ID = (uint)Associations.Count;

			// Saglabājam asociācijas galapunktiem faktu, vai tas ir avots
			_source.IsSource = true;
			_target.IsSource = false;

			// Pārbauda, vai visi dati ir ievadīti
			if (_association.SourceName != null && _association.SourceClass != null && _association.TargetName != null && _association.TargetClass != null) 
			{
				uint sourceNameLine = (uint)context.associationDefinition().associationSource().associationSourceName().Start.Line;
				uint targetNameLine = (uint)context.associationDefinition().associationTarget().associationTargetName().Start.Line;

				// Pārbauda, vai asociāciju lomu vārdi ir pareizi
				checkRoleName(_association.SourceName, _association.TargetClass, sourceNameLine);
				checkRoleName(_association.TargetName, _association.SourceClass, targetNameLine);

				// Pievienojam asociāciju galapunktus klasēm
				Classes[_sourceClass]._associationEnds.Add(_source);
				Classes[_targetClass]._associationEnds.Add(_target);

				Associations.Add(_association); // Pievienojam asociāciju sarakstam
			}
			
			return null;
		}

		/// <summary>
		/// Apstaigājam asociācijas definīciju
		/// </summary>
		public override object VisitAssociationDefinition([NotNull] AssociationDefinitionContext context)
        {
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			
			uint line = (uint)context.Start.Line; // Nosaka rindu, kurā ir kļūda, ja tādu atrod.

			// Pārbauda, vai asociācijai ir avote definīcija
			if (context.associationSource() == null) { Errors.Add("At line " + line + ": missing source definition!"); }
			else 
			{
				line = (uint)context.associationSource().Stop.Line;
				VisitAssociationSource(context.associationSource());
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
			else { VisitAssociationTarget(context.associationTarget()); }

			return null;
        }

        /// <summary>
        /// Apstaigājam asociācijas avota definīciju
        /// </summary>
        public override object VisitAssociationSource([NotNull] AssociationSourceContext context)
        {
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			
			uint line = (uint)context.Start.Line; // Nosaka rindu, kurā ir kļūda, ja tādu atrod.

			// Pārbauda, vai avotam ir definēts lomas vārds
			if (context.associationSourceName() == null) { Errors.Add("At line " + line + ": Missing source role name!"); }
			else
			{
				line = (uint)context.associationSourceName().Stop.Line;
				VisitAssociationSourceName(context.associationSourceName());
			}

			// Pārbauda, vai ir ielikts kols
			if (context.COLON() == null) { Errors.Add("At line " + line + ": Syntax error! Missing ':'!"); }
			else { line = (uint)context.COLON().Symbol.Line; }

			// Parbauda, vai avotam ir definēta klase
			if (context.associationSourceClass() == null) { Errors.Add("At line " + line + ": Missing source class!"); }
			else { VisitAssociationSourceClass(context.associationSourceClass()); }
			
			return null;
        }

		/// <summary>
		/// Apstaigājam asociācijas avota lomas vārdu
		/// </summary>
		public override object VisitAssociationSourceName([NotNull] AssociationSourceNameContext context)
        {
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n"); 

			_association.SourceName = context.GetText();
			_target.RoleName = context.GetText();

			return null;
		}

		/// <summary>
		/// Apstaigājam asociācijas avota klasi
		/// </summary>
		public override object VisitAssociationSourceClass([NotNull] AssociationSourceClassContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n"); 
			
			bool found = false;

			foreach (var r in Reserved)
			{
				if (context.GetText() == r)
				{
					Errors.Add("At line " + context.Start.Line + ": A class cannot be named '" + r + "'!");
					_class.ClassName = " ";
					found = true;
					break;
				}
			}

			if (found == false) 
			{
				// Pārbauda, vai eksiste avotklase.
				for (int x = 0; x < Classes.Count; x++)
				{
					if (Classes[x].ClassName == context.GetText())
					{
						_association.SourceClass = Classes[x];
						_target.Class = Classes[x];
						_sourceClass = x;
						found = true;
						break;
					}
				}
				if (found == false) { Errors.Add("At line " + context.Start.Line + ": there is no class '" + context.GetText() + "' to use as source class!"); }
				return VisitChildren(context);
			}

			return null;
		}

		/// <summary>
		/// Apstaigājam asociācijas mērķa definīciju
		/// </summary>
		public override object VisitAssociationTarget([NotNull] AssociationTargetContext context)
		{
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");
			
			uint line = (uint)context.Start.Line; // Nosaka rindu, kurā ir kļūda, ja tādu atrod.

			// Pārbauda, vai mērķim ir definēts lomas vārds
			if (context.associationTargetName() == null) { Errors.Add("At line " + line + ": Missing target role name!"); }
			else
			{
				line = (uint)context.associationTargetName().Stop.Line;
				VisitAssociationTargetName(context.associationTargetName());
			}

			// Pārbauda, vai ir ielikts kols
			if (context.COLON() == null) { Errors.Add("At line " + line + ": Syntax error! Missing ':'!"); }
			else { line = (uint)context.COLON().Symbol.Line; }

			// Pārbauda, vai mērķim ir definēta klase
			if (context.associationTargetClass() == null) { Errors.Add("At line " + line + ": Missing target class!"); }
			else { VisitAssociationTargetClass(context.associationTargetClass()); }

			return null;
		}

		/// <summary>
		/// Apstaigājam asociācijas mērķa lomas vārdu
		/// </summary>
		public override object VisitAssociationTargetName([NotNull] AssociationTargetNameContext context)
        {
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n"); 

			_association.TargetName = context.GetText();
			_source.RoleName = context.GetText();

			return null;
		}

		/// <summary>
		/// Apstaigājam asociācijas mērķa klasi
		/// </summary>
		public override object VisitAssociationTargetClass([NotNull] AssociationTargetClassContext context)
        {
			///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n"); 
			
			bool found = false;

			foreach (var r in Reserved)
			{
				if (context.GetText() == r)
				{
					Errors.Add("At line " + context.Start.Line + ": A class cannot be named '" + r + "'!");
					_class.ClassName = " ";
					found = true;
					break;
				}
			}

			if (found == false) 
			{
				// Pārbauda, vai eksistē mērķklase
				for (int x = 0; x < Classes.Count; x++)
				{
					if (Classes[x].ClassName == context.GetText())
					{
						_association.TargetClass = Classes[x];
						_source.Class = Classes[x];
						_targetClass = x;
						found = true;
						break;
					}
				}
				if (found == false) { Errors.Add("At line " + context.Start.Line + ": there is no class '" + context.GetText() + "' to use as target class!"); }
				return VisitChildren(context);
			}

			return null;
		}

		/// <summary>
		/// Funkcija, kas pārbauda lomas vārda kļūdas
		/// </summary>
		public bool checkRoleName(string rolename, Class _class, uint line) 
		{
			// Pārbauda, vai lomas vārds nesakrīt ar pretējās klases vārdu
			if (rolename == _class.ClassName)
			{
				Errors.Add("At line " + line + ": association source role name cannot be '" + _class.ClassName + "'!");
				return false;
			}
			else 
			{
				// Pārbauda, vai lomas vārds nesakrīt ar rezervētajiem vārdiem
				foreach (var r in Reserved)
				{
					if (r == rolename)
					{
						Errors.Add("At line " + line + ": association source role name cannot be '" + r + "'!");
						return false;
					}
				}

				// Pārbauda, vai klasē ir īpasība, kura vārds sakrīit ar lomas vārdu
				foreach (var v in _class._variables)
				{
					if (v.Name == rolename)
					{
						Errors.Add("At line " + line + ": a field with name '" + v.Name + "' already exists in class '" + _class.ClassName + "'! Check line " + v.Line + "!");
						return false;
					}
				}

				// Pārbauda, vai klasē ir metode, kura vārds sakrīit ar lomas vārdu
				foreach (var m in _class._methods)
				{
					if (m.Name == rolename)
					{
						Errors.Add("At line " + line + ": a field with name '" + m.Name + "' already exists in class '" + _class.ClassName + "'! Check line " + m.Line + "!");
						return false;
					}
				}

				// Pārbauda, vai klasē ir asociācijas galapunkts, kura vārds sakrīit ar lomas vārdu
				foreach (var ae in _class._associationEnds)
				{
					if (ae.RoleName == rolename)
					{
						Errors.Add("At line " + line + ": an association with role name '" + ae.RoleName + "' already exists in class '" + _class.ClassName + "'! Check line " + Associations[(int)ae.ID].Line + "!");
						return false;
					}
				}

				// Pārbauda, vai klasei ir virsklase, kuru pārbaudīt
				if (_class.SuperClass != null)
				{
					// Pārbauda, vai virsklasē ir metode, kura vārds sakrīit ar lomas vārdu
					foreach (var m in _class.SuperClass._methods)
					{
						if (m.Name == rolename)
						{
							Errors.Add("At line " + line + ": a field with name '" + m.Name + "' already exists in super class '" + _class.SuperClass.ClassName + "'! Check line " + m.Line + "!");
							return false;
						}
					}

					// Pārbauda, vai virsklasē ir īpašība, kura vārds sakrīit ar lomas vārdu
					foreach (var v in _class.SuperClass._variables)
					{
						if (v.Name == rolename)
						{
							Errors.Add("At line " + line + ": a field with name '" + v.Name + "' already exists in super class '" + _class.SuperClass.ClassName + "'! Check line " + v.Line + "!");
							return false;
						}
					}

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
		}
	}
}