// Testing.cs
/******************************************************
* Kompilatora un starpkoda ģeneratora testēšanas funkcijas.
*  Izmanto testēšanā.
*  
******************************************************/
// Autors:  Artis Pauniņš
// Pabeigts: v1.0 02.01.22

using System; // nodrošina ievad-izvadierīču lietošanu
using Antlr4.Runtime; // Nodrošina ANTLR4.Runtime rīku lietošanu (AntlrInputStrream un CommonTokenStream)
using System.IO; // Nodrosina darbu ar failiem
using ANTLR.Grammar; // Nodrošina darbu ar gramatikas kodu
using static ANTLR.Grammar.LanguageParser; // Nodrošina vienkāršāku konteksta objektu notāciju (var rakstīt, piem., CodeContext nevis LanguageParser.CodeContext)

namespace AntlrCSharp
{
    partial class Program
    {
        /// <summary>
        /// Testēšanas sākumfunkcija
        /// </summary>
        public static void ProgramTesting()
        {
            Console.Write("Specify test type: ");
            string type = Console.ReadLine();
            while (type != "Syntax" && type != "Functionality" && type != "Generator")
            {
                type = Console.ReadLine();
            }

            string filename = "Generator";
            string path = "TestCases/" + type + "/";

            if (type != "Generator") 
            {
                Console.Write("Specify test group: ");
                filename = Console.ReadLine();
                while (filename == "")
                {
                    filename = Console.ReadLine();
                }
                path += filename;
            }

            string _namespace = "Test";
            
            int length = Directory.GetFiles(path+"/Input/").Length;

            for (int count = 1; count <= length; count++) 
            {
                string inFile = path + "/Input/" + filename + ".i" + count;
                string outFile = path + "/Output/" + filename + ".o" + count;

                if (type == "Generator") { outFile = path + "/Output/i" + count + "/"; }

                Console.WriteLine("testing: " + inFile);

                // Iegūstam koda saturu no faila
                string text = "";
                using (StreamReader sr = new(inFile))
                {
                    text = sr.ReadToEnd();
                }

                if (filename == "Namespaces")
                {
                    _namespace = text;
                }

                // Sagatavojam lekseri un parseri
                AntlrInputStream input = new(text);
                LanguageLexer lexer = new(input);
                CommonTokenStream commonTokenStream = new(lexer);
                LanguageParser parser = new(commonTokenStream);

                // Sagatavojam kodu kompilēšanai
                CodeContext codeContext = parser.code();
                compiler = new Compiler();

                compiler.Test(codeContext, _namespace, type, outFile); // Kompilējam kodu

                Console.WriteLine("\n");
            }
        }

        /// <summary>
        /// Ģeneratora testēšanas funkcija
        /// </summary>
        public static void GeneratorTesting(string _namespace, string outFile)
        {
            // Ģenerē klases "BaseObject" kodu
            using (StreamWriter sw = new(outFile + "BaseObject.cs"))
            {
                sw.WriteLine("using WebAppOS;");
                sw.WriteLine("using System.Collections.Generic;\n");
                sw.WriteLine("namespace " + _namespace);
                sw.WriteLine("{");
                generateBaseObject(sw);
                sw.Write('}');
            }

            // Ģenerē kodu visām klasēm.
            foreach (var _class in compiler.Classes)
            {
                string filename = outFile + _class.ClassName + ".cs";
                using (StreamWriter sw = new(filename))
                {
                    sw.WriteLine("using WebAppOS;");
                    sw.WriteLine("using System.Text.Json;");
                    sw.WriteLine("using System;");
                    sw.WriteLine("using System.Collections.Generic;\n");
                    sw.WriteLine("namespace " + _namespace);
                    sw.WriteLine("{");
                    generateClass(sw, _class);
                    sw.Write('}');
                }
            }
        }
    }

    public partial class Compiler : LanguageParserBaseVisitor<object> 
    {
        /// <summary>
		/// Kompilatora testēšanas funkcija
		/// </summary>
		public void Test(LanguageParser.CodeContext context, string _namespace, string type, string outFile)
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

            if (_namespace == "Test")
            {
                // Apstaigā kodu
                VisitChildren(context);
            }

            // Pārbauda, vai kodā nav kļūdu. Ja nav, tad ģenerējam starpkodu
            if (Errors.Count != 0)
            {
                Console.WriteLine("");
                Console.WriteLine("Compilation unsuccessful! See errors in file " + outFile + "!");

                using (StreamWriter sw = new(outFile))
                {
                    foreach (var error in Errors)
                    {
                        sw.WriteLine(error);
                    }
                }
            }
            else
            {
                if (type == "Generator")
                {
                    Program.GeneratorTesting(_namespace, outFile);
                }
                else
                {
                    using (StreamWriter sw = new(outFile))
                    {
                        foreach (var error in Errors)
                        {
                            sw.WriteLine(error);
                        }
                    }
                }

                Console.WriteLine("");
                Console.WriteLine("Compilation successful!");
            }
        }
    }
}