using System;
using System.IO;

namespace GrammarTestCaseMaker
{
    class Program
    {
        static string text;
        static int count;
        public static void generateTestCasesInMultipleFiles(string[][] parts , string filename , int elements , int element , string output, string start, string end) 
        {
            if (element == 0) { count = 1; }
            if (element == elements)
            {
                if (filename == "Blocks")
                {
                    using (StreamWriter sw = new StreamWriter("Result/" + filename + "/" + filename + ".i" + count))
                    {
                        sw.WriteLine(start + output + end);
                    }
                    count++;
                }
                else if (output != "") 
                {
                    using (StreamWriter sw = new StreamWriter("Result/" + filename + "/" + filename + ".i" + count))
                    {
                        sw.WriteLine(start + output + end);
                    }
                    count++;
                }
            }
            else 
            {
                for (int x = 0; x < parts[element].Length; x++) 
                {
                    if (parts[element][x] == "") { generateTestCasesInMultipleFiles(parts, filename, elements, element + 1, output, start, end); }
                    else 
                    {
                        if (output == "") 
                        {
                            if (parts[element][x] == ": superClass") { generateTestCasesInMultipleFiles(parts, filename, elements, element + 1, ":", start, end); }
                            generateTestCasesInMultipleFiles(parts, filename, elements, element + 1, parts[element][x], start,end); 
                        }
                        else
                        {
                            if (parts[element][x] == ": superClass") { generateTestCasesInMultipleFiles(parts, filename, elements, element + 1, output + " :", start, end); }
                            generateTestCasesInMultipleFiles(parts, filename, elements, element + 1, output + " " + parts[element][x], start,end); 
                        }
                    }
                }
            }
        }

        public static void generateTestCasesInOneFile(string[][] parts, string filename, int elements, int element, string output, string start, string end)
        {
            if (element == 0) { text = start; }
            if (element == elements)
            {
                if (filename == "Blocks")
                {
                    text += output + "\n";
                }
                else if (output != "")
                {
                    text += output + "\n";
                }
            }
            else
            {
                for (int x = 0; x < parts[element].Length; x++)
                {
                    if (parts[element][x] == "") { generateTestCasesInOneFile(parts, filename, elements, element + 1, output, start, end); }
                    else
                    {
                        if (output == "")
                        {
                            if (parts[element][x] == ": superClass") { generateTestCasesInOneFile(parts, filename, elements, element + 1, ":", start, end); }
                            generateTestCasesInOneFile(parts, filename, elements, element + 1, parts[element][x], start, end);
                        }
                        else
                        {
                            if (parts[element][x] == ": superClass") { generateTestCasesInOneFile(parts, filename, elements, element + 1, output + " :", start, end); }
                            generateTestCasesInOneFile(parts, filename, elements, element + 1, output + " " + parts[element][x], start, end);
                        }
                    }
                }
            }
            if (element == 0) 
            {
                using (StreamWriter sw = new StreamWriter("Result/" + filename + ".in"))
                {
                    sw.Write(text+end);
                }
            }

        }

        static void Main(string[] args)
        {
            /*
            string[][] parts = new string[][] { new string[] { "" , "baseClass" }, new string[] { "" , ": superClass" }, new string[] { "" , "{}" } };
            generateTestCasesInOneFile(parts , "WebClass" , parts.Length , 0, "" , "class ", "");
            generateTestCasesInMultipleFiles(parts, "WebClass", parts.Length, 0, "", "class ", "");

            parts = new string[][] { new string[] { "" , "class" , "association" , "Integer" }, new string[] { "" , "(sourceName:sourceClass<->targetName:targetClass)" , "className : superClass {}" } };
            generateTestCasesInOneFile(parts, "Blocks", parts.Length, 0, "", "", "");
            generateTestCasesInMultipleFiles(parts, "Blocks", parts.Length, 0, "", "", "");

            parts = new string[][] { new string[] { "", "sourceName" }, new string[] { "", ":" }, new string[] { "", "sourceClass" }, new string[] { "", "<->" }, new string[] { "", "targetName" }, new string[] { "", ":" }, new string[] { "", "targetClass" } };
            generateTestCasesInOneFile(parts, "Associations", parts.Length, 0, "", "association(", ")");
            generateTestCasesInMultipleFiles(parts, "Associations", parts.Length, 0, "", "association(", ")");
            */

            string[][] parts = new string[][] { new string[] { "", "public" }, new string[] { "", "Integer" }, new string[] { "", "int" }, new string[] { ";" } };
            //generateTestCasesInOneFile(parts, "Attributes", parts.Length, 0, "", "class\n{\n", "}");
            generateTestCasesInMultipleFiles(parts, "Attributes", parts.Length, 0, "", "class\n{\n", "}");
        }
    }
}
