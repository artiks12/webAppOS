// MethodCompiler.cs
/******************************************************
* Satur klases metožu kompilesanas funkcijas.
* Tās iekļauj aizsardzības, datu tipa un vārda kompilēsanu,
* pārbaudi un glabāšanau, ka arī argumentu un anotāciju esamību.
******************************************************/
// Autors:  Artis Pauniņš
// Pabeigts: v1.0 06.01.22

using Antlr4.Runtime.Misc; // Nodrošina to, ka visās "Visit" funkcijas padotie konteksti nav ar vērtību 'null'
using ANTLR.Grammar; // Nodrošina darbu ar gramatikas kodu
using static ANTLR.Grammar.LanguageParser; // Nodrošina vienkāršāku konteksta objektu notāciju (var rakstīt, piem., CodeContext nevis LanguageParser.CodeContext)

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

			AttributeDefinitionContext methodBody = null;
			MethodDefinitionContext argumentBody = null;
			var temp = context.fieldDefinition();
			if (temp != null) 
			{
				methodBody = temp.attributeDefinition();
				argumentBody = temp.methodDefinition();
			}

			uint line = (uint)context.Start.Line; ; // Nosaka rindu, kurā ir kļūda, ja tādu atrod.

			// Mainam rindu
			if (methodBody != null) { line = (uint)methodBody.Stop.Line; }
			else if (context.annotation().Length != 0) { line = (uint)context.annotation()[context.annotation().Length - 1].Stop.Line; }

			// Pārbauda, vai metodei ir definēti argumenti
			if (argumentBody != null) { VisitMethodDefinition(argumentBody); /* Apstaiga argumentus (skat. ArgumentCompiler.cs) */ }
			else { Errors.Add("At line " + line + ": Missing arguemnt definition for method!"); }

			// Pārbaudam, vai metodei ir definēts ķermenis
			if (methodBody != null)
			{
				line = (uint)methodBody.Start.Line;
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

					// Pārbauda, vai metodei ir vārds
					if (methodBody.attribute().fieldName() != null) { VisitMethodName(methodBody.attribute().fieldName()); }
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
					VisitAnnotation(a); // Apstaiga argumentus (skat. AnotationCompiler.cs)
				}
				if (_urlFound == false) { Errors.Add("At line " + context.Start.Line + ": Missing method URL!"); }
			}
			else { Errors.Add("At line " + context.Start.Line + ": Missing method URL!"); }

			// Ja metodei ir vārds vai metodes vārds neatkārtojas, tad klasē saglabā metodi
			if (_method.Name != null) 
			{
				_method.Line = (uint)context.fieldDefinition().attributeDefinition().attribute().fieldName().Start.Line;
				_class.Methods.Add(_method);
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

			// Pārbauda, vai metodei ir pareizs datu tips
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

			// Pārbauda, vai metodes vārds sākas ar "_constructor_"
			if (context.GetText().StartsWith("_constructor_")) { Errors.Add("At line " + context.Start.Line + ": A method cannot start with '_constructor_'!"); }

			// Pārbauda, vai metode ir sastopama virsklasē
			var sc = _class.SuperClass;
			while (sc != null)
			{
				if (checkMethodNameInSuperClass(context, sc) == false) { return null; }
				sc = sc.SuperClass;

			}

			// Pārbauda, vai metode ir sastopama klasē
			if (checkMethodNameInClass(context, _class) == true) { _method.Name = context.GetText(); }

			return null;
		}

		/// <summary>
		/// Pārbauda metodes vārda esamību klasē
		/// </summary>
		/// <param name="_checkClass">Klase, kuru parbauda</param>
		/// <returns>Atgriež true, ja metodes vārds klasē neeksistē, citādi atgriež false</returns>
		public bool checkMethodNameInClass([NotNull] FieldNameContext context, Class _checkClass) 
		{
			string message = "At line " + context.Start.Line + ": A field with name '" + context.GetText() + "' already exists in class '" + _checkClass.ClassName + "'! Check line ";

			// Pārbauda, vai metodes vārds atkārtojas klasē starp atribūtiem
			foreach (var v in _checkClass.Attributes)
			{
				if (v.Name == context.GetText())
				{
					Errors.Add(message + v.Line + "!");
					return false;
				}
			}

			// Pārbauda, vai metodes vārds atkārtojas klasē starp citām metodēm
			foreach (var m in _checkClass.Methods)
			{
				if (m.Name == context.GetText())
				{
					Errors.Add(message + m.Line + "!");
					return false;
				}
			}
			
			return true;
		}

		/// <summary>
		/// Pārbauda metodes vārda esamību virsklasē
		/// </summary>
		/// <param name="_checkClass">Virsklase, kuru parbauda</param>
		/// <returns>Atgriež true, ja metodes vārds virsklasē neeksistē, citādi atgriež false</returns>
		public bool checkMethodNameInSuperClass([NotNull] FieldNameContext context, Class _checkClass)
		{
			string message = "At line " + context.Start.Line + ": A field with name '" + context.GetText() + "' already exists in superclass '" + _checkClass.ClassName + "'! Check line ";

			// Pārbauda, vai metodes vārds atkārtojas klasē starp asociācijām
			foreach (var ae in _checkClass.AssociationEnds)
			{
				if (ae.RoleName == context.GetText())
				{
					Errors.Add(message + ae.Line + "!");
					return false;
				}
			}

			// Pārbauda, vai metodes vārds atkārtojas klasē starp atribūtiem
			foreach (var v in _checkClass.Attributes)
			{
				if (v.Name == context.GetText() && v.Protection == "public" && v.generate == true)
				{
					Errors.Add(message + v.Line + "!");
					return false;
				}
			}

			// Pārbauda, vai metode ir sastopama virsklasē
			foreach (var m in _checkClass.Methods)
			{
				// Pārbauda, vai metožu vārdi sakrīt
				if (m.Name == context.GetText() && m.Protection == "public" && m.generate == true)
				{
					bool error = true;

					// Pārbauda, vai metodēm sakrīt datu tipi
					if (m.Type != _method.Type)
					{
						Errors.Add("At line " + context.Start.Line + ": Method '" + context.GetText() + "' , that exists in superclass '" + _checkClass.ClassName + "' , does not have the same datatype! Check line " + m.Line + "!");
						error = false;
					}

					// Pārbauda, vai metožu argumentu skaits sakrīt
					if (m.Arguments.Count == _method.Arguments.Count)
					{
						// Pārbauda, vai metožu argumentu datu tipi un vārdi sakrīt
						for (int x = 0; x < m.Arguments.Count; x++)
						{
							// Pārliecinās, ka abiem argumentiem ir datu tipi
							if (m.Arguments[x].Type != null && _method.Arguments[x].Type != null)
							{
								// Pārbauda, vai argumentu datu tipi sakrīt
								if (m.Arguments[x].Type != _method.Arguments[x].Type)
								{
									Errors.Add("At line " + _method.Arguments[x].Line + ": Argument No. " + (x + 1) + ", does not have the same datatype as in '" + _checkClass.ClassName + "'! Check line " + m.Arguments[x].Line + "!");
									error = false;
								}
							}
						}
					}
					else
					{
						Errors.Add("At line " + context.Start.Line + ": Method '" + context.GetText() + "' , that exists in superclass '" + _checkClass.ClassName + "' does not have equal amount of arguments! Check line " + m.Line + "!");
						error = false;
					}

					// Ja netika atrastas atšķirības, tad metodi apakšklasē vēl varēs saglabāt, bet tiek norādīts, ka to nevajadzēs ģenerēt apakšklasē
					if (error == true) { _method.generate = false; }
					return error;
				}
			}

			return true;
		}
	}
}