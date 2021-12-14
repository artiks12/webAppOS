using System;
using ANTLR;
using Antlr4.Runtime;
using System.IO;
using Antlr4.Runtime.Tree;
using ANTLR.Grammar;
using System.Text.Json;
using static ANTLR.Grammar.LanguageParser;

namespace AntlrCSharp
{
    partial class Program
    {
        private static Compiler compiler; // Kompilatora objekts

        /*
        /// <summary>
        /// Programmas galvenā funkcija
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string []args) 
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
            using (StreamReader sr = new StreamReader(filename))
            {
                text = sr.ReadToEnd();
            }

            // Sagatavojam lekseri un parseri
            AntlrInputStream input = new AntlrInputStream(text);
            LanguageLexer lexer = new LanguageLexer(input);
            CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
            LanguageParser parser = new LanguageParser(commonTokenStream);

            // Sagatavojam kodu kompilēšanai
            CodeContext codeContext = parser.code();
            compiler = new Compiler();

            compiler.Compile(codeContext, _namespace); // Kompilējam kodu
        }
        */

        /// <summary>
        /// Testēšanai
        /// </summary>
        public static void Main(string[] args)
        {
            // Iegūstam faila nosaukumu
            Console.Write("Specify test group: ");
            string filename = Console.ReadLine();

            while (filename == "") 
            {
                filename = Console.ReadLine();
            }

            string _namespace = "Test";

            for (int count = 1; count < 7; count++) 
            {
                string path = "TestCases/" + filename + "/" + filename + ".i" + count;

                Console.WriteLine("testing: " + path);

                // Iegūstam koda saturu no faila
                string text = "";
                using (StreamReader sr = new StreamReader(path))
                {
                    text = sr.ReadToEnd();
                }

                // Sagatavojam lekseri un parseri
                AntlrInputStream input = new AntlrInputStream(text);
                LanguageLexer lexer = new LanguageLexer(input);
                CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
                LanguageParser parser = new LanguageParser(commonTokenStream);

                // Sagatavojam kodu kompilēšanai
                CodeContext codeContext = parser.code();
                compiler = new Compiler();

                compiler.Compile(codeContext, _namespace); // Kompilējam kodu

                Console.WriteLine("\n");
            }
        }
    }
}