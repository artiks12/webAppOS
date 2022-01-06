// Program.cs
/******************************************************
* Kompilatora un starpkoda ģeneratora sākumprogramma.
*  satur galveno funkciju, kurā iegūst faila vārdu,
*  vārdtelpas vārdu, sagatavo ANTLR rīkus.
******************************************************/
// Autors:  Artis Pauniņš
// Pabeigts: v1.0 06.01.22

using System; // nodrošina ievad-izvadierīču lietošanu
using Antlr4.Runtime; // Nodrošina ANTLR4.Runtime rīku lietošanu (AntlrInputStrream un CommonTokenStream)
using System.IO; // Nodrosina darbu ar failiem
using ANTLR.Grammar; // Nodrošina darbu ar gramatikas kodu
using static ANTLR.Grammar.LanguageParser; // Nodrošina vienkāršāku konteksta objektu notāciju (var rakstīt, piem., CodeContext nevis LanguageParser.CodeContext)

namespace AntlrCSharp
{
    partial class Program
    {
        private static Compiler compiler; // Kompilatora objekts

        /// <summary>
        /// Programmas galvenā funkcija
        /// </summary>
        /// <param name="args"></param>
        public static void Main() 
        {
            // Iegūstam faila nosaukumu
            Console.Write("Specify full filename: ");
            string filename = Console.ReadLine();

            // Iegūstam vārdtelpas nosaukumu
            Console.Write("Specify namespace: ");
            string _namespace = Console.ReadLine();

            if (filename == "") { filename = "Test.waoscs"; }
            if (_namespace == "") { _namespace = "Test"; }

            // Iegūstam koda saturu no faila
            string text;
            using (StreamReader sr = new(filename))
            {
                text = sr.ReadToEnd();
            }

            // Sagatavojam lekseri un parseri
            AntlrInputStream input = new(text);
            LanguageLexer lexer = new(input);
            CommonTokenStream commonTokenStream = new(lexer);
            LanguageParser parser = new(commonTokenStream);

            // Sagatavojam kodu kompilēšanai
            CodeContext codeContext = parser.code();
            compiler = new Compiler();

            compiler.Compile(codeContext, _namespace); // Kompilējam kodu (skat. BaseCompiler.cs)
        }
    }
}