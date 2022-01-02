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

			// Apstaigājam asociācijas definīciju, ja tāda ir definēta
			if (context.associationDefinition().GetText() == "") { Errors.Add("At line " + context.Start.Line + ": missing association definition!");  }
			else { VisitAssociationDefinition(context.associationDefinition()); } 

			// Pievienojam asociāciju sarakstam, ja ir pareizs vismaz viens galapunkts
			if (_source.RoleName != null || _target.RoleName != null) { Associations.Add(_association); }

			return null;
		}

		/// <summary>
		/// Apstaigājam asociācijas definīciju
		/// </summary>
		public override object VisitAssociationDefinition([NotNull] AssociationDefinitionContext context)
        {
			var start = (AssociationContext)context.Parent; // Rindas fiksēšanu sākam no iekavām.
			uint line = (uint)start.Start.Line; // Nosaka rindu, kurā ir kļūda, ja tādu atrod.

			// Pārbauda, vai asociācijai ir avote definīcija
			if (context.associationSource().GetText() == "") { Errors.Add("At line " + line + ": missing source definition!"); }
			else 
			{
				// Pārbauda, vai ir ielikts kols
				if (context.associationSource().COLON() == null) 
				{
					line = (uint)context.associationSource().associationSourceName().Stop.Line; // Ja kola nav, tad noteikti ir lomas vārds
					Errors.Add("At line " + line + ": Syntax error! Missing ':' at source definition!"); 
				}

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
			if (context.associationTarget().GetText() == "") { Errors.Add("At line " + line + ": missing target definition!"); }
			else 
			{
				// Pārbauda, vai ir ielikts kols
				if (context.associationTarget().COLON() == null) 
				{
					line = (uint)context.associationTarget().associationTargetName().Stop.Line; // Ja kola nav, tad noteikti ir lomas vārds
					Errors.Add("At line " + line + ": Syntax error! Missing ':' at target definition!"); 
				}
			}

			VisitAssociationClasses(context); // Apstaigājam asociācijas klases
			VisitAssociationRoleNames(context); // Apstaigājam asociacijas lomu vārdus

			return null;
        }

		/// <summary>
		/// Apstaigājam asociācijas klases
		/// </summary>
		public object VisitAssociationClasses([NotNull] AssociationDefinitionContext context) 
		{
			uint line; // Nosaka rindu, kurā ir kļūda, ja tādu atrod.

			// Pārbaudam, vai ir definēts avots
			if (context.associationSource().GetText() != "") 
			{
				// Mainam rindu. Ja nav kola, tad noteikti ir lomas vārds
				if (context.associationSource().COLON() != null) { line = (uint)context.associationSource().COLON().Symbol.Line; }
				else { line = (uint)context.associationSource().associationSourceName().Stop.Line; }

				// Parbauda, vai avotam ir definēta klase
				if (context.associationSource().associationSourceClass() == null) { Errors.Add("At line " + line + ": Missing source class!"); }
				else { VisitAssociationSourceClass(context.associationSource().associationSourceClass()); }
			}

			// Parbaudam, vai ir definēts mērķis
			if (context.associationTarget().GetText() != "")
			{
				// Mainam rindu. Ja nav kola, tad noteikti ir lomas vārds
				if (context.associationTarget().COLON() != null) { line = (uint)context.associationTarget().COLON().Symbol.Line; }
				else { line = (uint)context.associationTarget().associationTargetName().Stop.Line; }

				// Parbauda, vai mērķim ir definēta klase
				if (context.associationTarget().associationTargetClass() == null) { Errors.Add("At line " + line + ": Missing target class!"); }
				else { VisitAssociationTargetClass(context.associationTarget().associationTargetClass()); }
			}

			return null;
		}

		/// <summary>
		/// Apstaigājam asociācijas avota klasi
		/// </summary>
		public override object VisitAssociationSourceClass([NotNull] AssociationSourceClassContext context)
		{
			VisitAssociationClass(context.GetText(),context.Start.Line,true);
			return null;
		}

		/// <summary>
		/// Apstaigājam asociācijas mērķa klasi
		/// </summary>
		public override object VisitAssociationTargetClass([NotNull] AssociationTargetClassContext context)
		{
			VisitAssociationClass(context.GetText(),context.Start.Line,false);
			return null;
		}

		/// <summary>
		/// Apstaigājam asociācijas klasi
		/// </summary>
		public object VisitAssociationClass(string className, int line, bool isSource)
		{
			// Pārbauda, vai klases vārds nesakrīt ar rezervētajiem vārdiem
			foreach (var r in Reserved)
			{
				if (className == r)
				{
					Errors.Add("At line " + line + ": A class cannot be named '" + r + "'!");
					return null;
				}
			}

			bool found = false; // Nosaka, vai klase ir atrasta

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

			// Ja klase nav atrasta, tad saglabā kļūdu
			if (found == false) 
			{
				// Sagatavo kļūdas paziņojumu
				string message;
				if (isSource == true) { message = "At line " + line + ": there is no class '" + className + "' to use as source class!"; }
				else { message = "At line " + line + ": there is no class '" + className + "' to use as target class!"; }
				Errors.Add(message); 
			}

			return null;
		}

		/// <summary>
		/// Apstaigājam asociācijas lomu vārdus
		/// </summary>
		public object VisitAssociationRoleNames([NotNull] AssociationDefinitionContext context)
		{
			var start = (AssociationContext)context.Parent; // Rindas fiksēšanu sākam no iekavām.
			uint line = (uint)start.Start.Line; // Nosaka rindu, kurā ir kļūda, ja tādu atrod.

			// Pārbaudam, vai ir definēts avots
			if (context.associationSource().GetText() != "")
			{
				// Parbauda, vai avotam ir definēta klase
				if (context.associationSource().associationSourceName() == null) { Errors.Add("At line " + line + ": Missing source name!"); }
				else { VisitAssociationSourceName(context.associationSource().associationSourceName()); }
			}

			// Pārbaudam, vai ir definēts mērķis
			if (context.associationTarget().GetText() != "")
			{
				// Mainam rindu. Vispirms pārbaudam, vai ir bultas un ja nav, tad pārbaudam, vai ir avots.
				if (context.ARROWS() != null) { line = (uint)context.ARROWS().Symbol.Line; }
				else if (context.associationSource().GetText() != "") { line = (uint)context.associationSource().Stop.Line; }

				// Parbauda, vai avotam ir definēta klase
				if (context.associationTarget().associationTargetName() == null) { Errors.Add("At line " + line + ": Missing target name!"); }
				else { VisitAssociationTargetName(context.associationTarget().associationTargetName()); }
			}

			return null;
		}

		/// <summary>
		/// Apstaigājam asociācijas avota lomas vārdu
		/// </summary>
		public override object VisitAssociationSourceName([NotNull] AssociationSourceNameContext context)
		{
			VisitAssociationRoleName(context.GetText(), _target.Class, (uint)context.Start.Line, true);
			return null;
		}

		/// <summary>
		/// Apstaigājam asociācijas mērķa lomas vārdu
		/// </summary>
		public override object VisitAssociationTargetName([NotNull] AssociationTargetNameContext context)
		{
			VisitAssociationRoleName(context.GetText(), _source.Class, (uint)context.Start.Line, false);
			return null;
		}

		/// <summary>
		/// Apstaigājam asociācijas lomas vārdu
		/// </summary>
		public object VisitAssociationRoleName(string roleName, Class associationClass, uint line, bool IsSource) 
		{
			string direction = IsSource ? "source" : "target";

			// Pārbauda, vai lomas vārds nesakrīt ar rezervētajiem vārdiem
			foreach (var r in Reserved)
			{
				if (r == roleName)
				{
					Errors.Add("At line " + line + ": association " + direction + " role name cannot be '" + r + "'!");
					return null;
				}
			}

			// Pārbauda, vai lomas vārds nesākas ar "_constructor_"
			if (roleName.StartsWith("_constructor_")) { Errors.Add("At line " + line + ": Association " + direction + " role name cannot start with '_constructor_'!"); }

			// Parbauda, vai ir defineta klase, kurā tiks likts skatāmais lomas vārds
			if (associationClass != null)
			{
				// Pārbauda, vai lomas vārds nesakrīt ar pretējās klases vārdu
				if (roleName == associationClass.ClassName)
				{
					Errors.Add("At line " + line + ": association " + direction + " role name cannot be '" + roleName + "'!");
					return null;
				}

				// Pārbauda, vai lomas vārds jau ir sastopams pretējā klasē kā cits mainīgais vai asociācijas galapunkts.
				if (checkRoleNameInClass(roleName, associationClass, line, "class") == true)
				{
					// Pārbauda, vai lomas vārds jau ir sastopams pretējās klases virsklasēs kā cits mainīgais vai asociācijas galapunkts.
					var sc = associationClass.SuperClass;
					while (sc != null)
					{
						if (checkRoleNameInSuperClass(roleName, sc, line) == false) { return null; }
						sc = sc.SuperClass;
					}

					// Pārbauda, vai lomas vārds jau ir sastopams pretējās klases apakšklasēs kā cits mainīgais vai asociācijas galapunkts.
					if (checkSubClasses(roleName, associationClass, line, "subclass") == true)
					{
						if (IsSource == true)
						{
							_source.RoleName = roleName;
							_source.Line = line;
							_source.ID = (uint)Associations.Count;
							_source.IsSource = true;
							_association.Source = _source;
							if (_targetClass != -1) { Classes[_targetClass].AssociationEnds.Add(_source); }
						}
						else 
						{
							_target.RoleName = roleName;
							_target.Line = line;
							_target.ID = (uint)Associations.Count;
							_target.IsSource = false;
							_association.Target = _target;
							if (_sourceClass != -1) { Classes[_sourceClass].AssociationEnds.Add(_target); }
						}
					}
				}
			}

			return null;
		}

		/// <summary>
		/// Funkcija, kas pārbauda lomas vārda esamību bāzes klasē un apakšklasē
		/// </summary>
		/// <returns>Atgriež true, ja lomas vārds klasē neeksistē, citādi atgriež false</returns>
		public bool checkRoleNameInClass(string rolename, Class checkClass, uint line, string type)
		{
			string message = "At line " + line + ": a field with name '" + rolename + "' already exists in " + type + " '" + checkClass.ClassName + "'! Check line ";

			// Pārbauda, vai klasē ir atribūts, kura vārds sakrīit ar lomas vārdu
			foreach (var v in checkClass.Attributes)
			{
				if (v.Name == rolename)
				{
					Errors.Add(message + v.Line + "!");
					return false;
				}
			}

			// Pārbauda, vai klasē ir metode, kura vārds sakrīit ar lomas vārdu
			foreach (var m in checkClass.Methods)
			{
				if (m.Name == rolename)
				{
					Errors.Add(message + m.Line + "!");
					return false;
				}
			}

			// Pārbauda, vai klasē ir asociācijas galapunkts, kura vārds sakrīit ar lomas vārdu
			foreach (var ae in checkClass.AssociationEnds)
			{
				if (ae.RoleName == rolename)
				{
					Errors.Add("At line " + line + ": an association end with role name '" + ae.RoleName + "' already exists in " + type + " '" + checkClass.ClassName + "'! Check line " + ae.Line + "!");
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Funkcija, kas pārbauda lomas vārda esamību virsklasē
		/// </summary>
		/// <returns>Atgriež true, ja lomas vārds virsklasē neeksistē, citādi atgriež false</returns>
		public bool checkRoleNameInSuperClass(string rolename, Class checkClass, uint line)
		{
			string message = "At line " + line + ": a field with name '" + rolename + "' already exists in superclass '" + checkClass.ClassName + "'! Check line ";

			// Pārbauda, vai klasē ir atribūts, kura vārds sakrīit ar lomas vārdu
			foreach (var v in checkClass.Attributes)
			{
				if (v.Name == rolename && v.Protection == "public")
				{
					Errors.Add(message + v.Line + "!");
					return false;
				}
			}

			// Pārbauda, vai klasē ir metode, kura vārds sakrīit ar lomas vārdu
			foreach (var m in checkClass.Methods)
			{
				if (m.Name == rolename && m.Protection == "public")
				{
					Errors.Add(message + m.Line + "!");
					return false;
				}
			}

			// Pārbauda, vai virsklasē ir asociācijas galapunkts, kura vārds sakrīit ar lomas vārdu
			foreach (var ae in checkClass.AssociationEnds)
			{
				if (ae.RoleName == rolename)
				{
					Errors.Add("At line " + line + ": an association end with role name '" + ae.RoleName + "' already exists in superclass '" + checkClass.ClassName + "'! Check line " + ae.Line + "!");
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Funkcija, kas iziet cauri apakšklasēm un tajās pārbauda lomas vārda esamību
		/// </summary>
		/// <returns>Atgriež true, ja lomas vārds apakšklasēs neeksistē, citādi atgriež false</returns>
		public bool checkSubClasses(string rolename, Class checkClass, uint line, string type)
		{
			foreach (var sub in checkClass.SubClasses) 
			{
				// Pārbauda, vai apakšklasē lomas vārds jau tiek izmantots
				if (checkRoleNameInClass(rolename, sub, line, type) == false) { return false; };
				
				// Ja apakšklasei eksistē apakšklases, tad apakšklasei ši funkcija tiek rekursīvi izsaukta
				if (sub.SubClasses.Count != 0) 
				{
					if (checkSubClasses(rolename, sub, line, type) == false) { return false; };
				}
			}
			
			return true;
		}
	}
}