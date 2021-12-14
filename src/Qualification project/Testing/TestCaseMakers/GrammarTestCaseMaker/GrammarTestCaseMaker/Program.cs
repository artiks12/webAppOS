using System;
using System.IO;

namespace GrammarTestCaseMaker
{
    class Program
    {
        static int count;
        public static void generateTestCases(string[][] parts , string filename , int elements , int element , string output, string start, string end) 
        {
            if (element == 0) { count = 1; }
            if (element == elements)
            {
                if (output != "") 
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
                    if (parts[element][x] == "") { generateTestCases(parts, filename, elements, element + 1, output, start, end); }
                    else 
                    {
                        if (output == "") { generateTestCases(parts, filename, elements, element + 1, parts[element][x], start,end); }
                        else { generateTestCases(parts, filename, elements, element + 1, output + " " + parts[element][x], start,end); }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            string[][] parts = new string[][] { new string[] { "" , "baseClass" }, new string[] { "" , ": superClass" }, new string[] { "" , "{}" } };
            generateTestCases(parts , "WebClass" , parts.Length , 0, "" , "class ", "");

            parts = new string[][] { new string[] { "", "class" , "association" , "Integer" , "public" }, new string[] { "", "(sourceName:sourceClass<->targetName:targetClass)" , "className : superClass {}" } };
            generateTestCases(parts, "Blocks", parts.Length, 0, "", "", "");
        }
    }
}
