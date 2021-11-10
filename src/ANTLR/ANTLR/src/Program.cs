using System;
using ANTLR;
using Antlr4.Runtime;
using System.IO;
using Antlr4.Runtime.Tree;
using ANTLR.Grammar;

namespace AntlrCSharp
{
    partial class Program
    {
        private static Compiler compiler; // Kompilatora objekts

        /// <summary>
        /// Metode, kas pārbauda, vai vārdtelpa ir sintaktiski pareiza
        /// </summary>
        private static bool checkNamespace(string _namespace) 
        {
            // Pārbauda, vai pirmais simbols ir burts vai apakšsvītra
            if (_namespace[0] == '_' || (_namespace[0] >= 'a' && (int)_namespace[0] <= 'z') || (_namespace[0] >= 'A' && (int)_namespace[0] <= 'Z'))
            {
                // Pārbauda vai visi pārejie simboli ir burti, cipari vai apakšsvītras
                for (int x = 1; x < _namespace.Length; x++)
                {
                    if (!(_namespace[x] == '_' || (_namespace[x] >= '0' && (int)_namespace[x] <= '9') || (_namespace[x] >= 'a' && (int)_namespace[x] <= 'z') || (_namespace[x] >= 'A' && (int)_namespace[x] <= 'Z')))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Palaišana pa tiešo
        /// </summary>
        private static void Main(string []args) 
        {
            try
            {
                // Iegūstam faila nosaukumu
                Console.Write("Specify full filename: ");
                string filename = Console.ReadLine();

                // Iegūstam vārdtelpas nosaukumu
                Console.Write("Specify filename: ");
                string _namespace = Console.ReadLine();

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
                LanguageParser.CodeContext codeContext = parser.code();
                compiler = new Compiler();

                compiler.Compile(codeContext); // Kompilējam kodu
                generate(_namespace); // Ģenerējam kodu

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
    }
}
