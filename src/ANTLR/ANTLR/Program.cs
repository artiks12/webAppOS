using System;
using ANTLR;
using Antlr4.Runtime;
using System.IO;
using Antlr4.Runtime.Tree;

namespace AntlrCSharp
{
    partial class Program
    {
        private static Compiler visitor;

        private static bool checkNamespace(string _namespace) 
        {
            if (_namespace[0] == '_' || (_namespace[0] >= 'a' && (int)_namespace[0] <= 'z') || (_namespace[0] >= 'A' && (int)_namespace[0] <= 'Z'))
            {
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

        private static void Main(string[] args)
        {
            try
            {
                // Iegūstam koda saturu no faila
                string text;
                using (StreamReader sr = new StreamReader("Test.idl"))
                {
                    text = sr.ReadToEnd();
                }
                string _namespace = "test"; // Vārdtelpas nosaukums

                // Sagatavojam lekseri un parseri
                AntlrInputStream input = new AntlrInputStream(text);
                LanguageLexer lexer = new LanguageLexer(input);
                CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
                LanguageParser parser = new LanguageParser(commonTokenStream);                

                // Sagatavojam kodu kompilēšanai
                LanguageParser.CodeContext codeContext = parser.code();
                visitor = new Compiler();

                visitor.Compile(codeContext); // Kompilējam kodu
                generate(visitor, _namespace); // Ģenerējam kodu

            }
            catch (Exception ex)
            {                
                Console.WriteLine("Error: " + ex);                
            }
            /*
                // args[0] - filename   args[1] - namespace
                try
                {
                    // Iegūstam koda saturu no faila
                    string text;
                    using (StreamReader sr = new StreamReader(args[0]))
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
                    Compiler visitor = new Compiler();

                    visitor.Compile(codeContext); // Kompilējam kodu
                    generate(visitor, _namespace); // Ģenerējam kodu
                }
                catch (Exception ex)
                {                
                    Console.WriteLine("Error: " + ex);                
                }
            */
        }
    }

    
}
