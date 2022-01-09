// BaseCompiler.cs
/******************************************************
* Satur kompilatora pamatlietas.
*  Tas iekļauj sarakstus ar klasēm, asociācijām un kļūdām, kā arī
*  sarakstus ar rezervētajiem vārdiem, metožu anotāciju tipiem,
*  URL protokoliem un lokācijām.
*  Ir iekļautas arī koda bloku apstaigāšanas funkcijas
******************************************************/
// Autors:  Artis Pauniņš
// Pabeigts: v1.0 06.01.22

using Antlr4.Runtime.Misc; // Nodrošina to, ka visās "Visit" funkcijas padotie konteksti nav ar vērtību 'null'
using System; // nodrošina ievad-izvadierīču lietošanu
using System.Collections.Generic; // Nodrošina darbu ar iebūvētajām datu struktūrām
using ANTLR.Grammar; // Nodrošina darbu ar gramatikas kodu
using static ANTLR.Grammar.LanguageParser; // Nodrošina vienkāršāku konteksta objektu notāciju (var rakstīt, piem., CodeContext nevis LanguageParser.CodeContext)
using Antlr4.Runtime.Tree; // Nodrošina funkcijas "VisitErrorMode" izmantošanu

namespace AntlrCSharp
{
	public partial class Compiler : LanguageParserBaseVisitor<object>
	{
		public List<Class> Classes; // Saraksts ar klasēm.
		public List<Association> Associations; // Saraksts ar asociācijām.
		public List<string> Errors; // Saraksts ar kļūdām.
		public List<string> Reserved = new() { "class", "association", "Void" , "Integer", "String", "Boolean", "Real", "private", "public", "BaseObject" , "URL" , "_constructor" , "_wm" , "_wc" , "_object" }; // Saraksts ar rezervētajiem vārdiem.

		private List<string> AnnotationTypes = new() { "path" }; // Saraksts ar metožu anotāciju tipiem
		private List<string> URLProtocols = new() { "staticJava", "dotnet" , "python3" }; // Saraksts ar URL Valodu protokoliem
		private List<string> URLlocations = new() { "local", "http" , "ftp" }; // Saraksts ar URL lokāciju vērtībām

		/// <summary>
		/// Metode, kas pārbauda, vai vārdtelpa ir sintaktiski pareiza
		/// </summary>
		/// <param name="_namespace">vārdtelpas vārds</param>
		private static bool checkNamespace(string _namespace)
		{
			// Pārbauda, vai pirmais simbols ir burts vai apakšsvītra
			if (_namespace[0] == '_' || (_namespace[0] >= 'a' && _namespace[0] <= 'z') || (_namespace[0] >= 'A' && _namespace[0] <= 'Z'))
			{
				// Pārbauda vai visi pārejie simboli ir burti, cipari vai apakšsvītras
				for (int x = 1; x < _namespace.Length; x++)
				{
					if (!(_namespace[x] == '_' || (_namespace[x] >= '0' && _namespace[x] <= '9') || (_namespace[x] >= 'a' && _namespace[x] <= 'z') || (_namespace[x] >= 'A' && _namespace[x] <= 'Z')))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>
		/// Kļūdainie mezgli (apstrādā visas pārējās kļūdas)
		/// </summary>
		public override object VisitErrorNode([NotNull] IErrorNode node)
		{
			Errors.Add("At line " + node.Symbol.Line + ": Unexpected '" + node.Symbol.Text + "'!");
			return base.VisitErrorNode(node);
		}

		/// <summary>
		/// Apstaigajam blokus
		/// </summary>
		public override object VisitBlocks([NotNull] BlocksContext context)
		{
			uint line = (uint)context.Start.Line; // Nosaka rindu, kurā ir kļūda, ja tādu atrod.
			string type; // Bloka tips
			string body; // Bloka ķermenis

			// Pārbauda, vai blokam ir "galva" jeb tips.
			if (context.blockType() == null)
			{
				// Ja blokam nav tipa, tad skatās, kāds atslēgvārds trūkst
				if (context.blockBody().webMemoryClass() != null) { Errors.Add("At line " + line + ": Missing keyword 'class'!"); }
				else { Errors.Add("At line " + line + ": Missing keyword 'association'!"); }
				type = "";
			}
			else 
			{
				// Ja blokam ir tips, tad skatās, vai tas ir pareizs
				if (VisitBlockType(context.blockType()) == null) 
				{ 
					type = "";
					// Ja tips nav pareizs, tad skatās, kādam ir jābūt tipam atkarībā no ķermeņa, ja tāds ir dots
					if (context.blockBody() == null) { Errors.Add("At line " + context.Start.Line + ": '" + context.blockType().GetText() + "' is not a block type! Use 'class' or 'association' instead!"); }
					else 
					{
						if (context.blockBody().webMemoryClass() != null) { Errors.Add("At line " + context.Start.Line + ": '" + context.blockType().GetText() + "' is not a block type! Use 'class' instead!"); }
						else { Errors.Add("At line " + context.Start.Line + ": '" + context.blockType().GetText() + "' is not a block type! Use 'association' instead!"); }
					}	
				}
				else { type = context.blockType().GetText(); }
				
				line = (uint)context.blockType().Stop.Line;
			}

			// Pārbauda, vai blokam ir ķermenis
			if (context.blockBody() == null) 
			{
				// Ja blokam nav ķermeņa, tad kļūdu saglabā tikai tad, ja padotais tips ir pareizs
				if (type != "") { Errors.Add("At line " + line + ": Missing " + context.blockType().GetText() + " body!"); }
			}
			else 
			{
				// Ja blokam ir ķermenis, tad pirms tas tiek apstaigāts, pārbauda vai sakrīt tips un ķermenis, ja padotais tips ir pareizs
				if (type != "") 
				{
					if (context.blockBody().webMemoryClass() != null) { body = "class"; }
					else { body = "association"; }

					if (type.ToString() != body)
					{
						Errors.Add("At line " + line + ": " + type + " definition is given as " + body + " definition!");
					}
				}

				VisitBlockBody(context.blockBody()); // Apstaigājam bloka ķermeni (skat. AssociationCompiler.cs un ClassCompiler.cs)
			}

			return null;
		}

		/// <summary>
		/// Apstaigā bloka tipu
		/// </summary>
        public override object VisitBlockType([NotNull] BlockTypeContext context)
        {
			return context.BLOCKTYPE(); 
        }

		/// <summary>
		/// Kompilēšanas pamtfunkcija
		/// </summary>
		/// <param name="context">jaunkods</param>
		/// <param name="_namespace">vārdtelpa</param>
		public void Compile(CodeContext context, string _namespace) 
		{
			// Sākam kompilēšanu ar sarakstu iestatīsanu
			Classes = new();
			Associations = new();
			Errors = new();

			// Pārbauda, vai padotā vārdtelpa ir sintaktiski pareizs
			if (!checkNamespace(_namespace))
			{
				Errors.Add("Namespace '" + _namespace + "' is in incorrect format!");
			}

			// Apstaigā kodu
			VisitChildren(context); // Starpkoda ģenerēsana (funkcija "VisitBlocks")

			// Pārbauda, vai kodā nav kļūdu. Ja nav, tad ģenerējam starpkodu, citādi izdrukājam kļūdas.
			if (Errors.Count != 0)
			{
				Console.WriteLine("");
				Console.WriteLine("Compilation unsuccessful! Encountered errors:");
				foreach (var error in Errors)
				{
					Console.WriteLine(error);
				}
			}
			else 
			{
				Program.generate(_namespace); // Starpkoda ģenerēsana (skat. Generator.cs)

				Console.WriteLine("");
				Console.WriteLine("Compilation successful!");
			}
		}
	}
}