using System;
using WebAppOS;
using System.Reflection;
using System.Collections.Generic;
using Test;

namespace TestProgram
{
    partial class Program
    {
        static bool test3() 
        {
            DirWebMemory memory = new();
            RemoteWebCalls calls = new();
            memory.Open("ar:C:/sample");

            _class6 test6 = new(memory, calls);
            _class7 test7 = new(memory, calls);

            var toFind = "_class7";
            var find = memory.FindClassByName(toFind);
            if (find != null)
            {
                Console.WriteLine(toFind);
                foreach (var a in find.Attributes())
                {
                    Console.WriteLine("    " + a.GetReference + " " + a.AttributeType + " " + a.AttributeName);
                }
            }
            else { Console.WriteLine("Class '" + toFind + "' not found!"); }

            toFind = "_class1";
            find = memory.FindClassByName(toFind);
            if (find != null)
            {
                Console.WriteLine(toFind);
                foreach (var a in find.Attributes())
                {
                    Console.WriteLine("    " + a.GetReference + " " + a.AttributeType + " " + a.AttributeName);
                }
            }
            else { Console.WriteLine("Class '" + toFind + "' not found!"); }

            toFind = "_class2";
            find = memory.FindClassByName(toFind);
            if (find != null) 
            { 
                Console.WriteLine(toFind);
                foreach (var a in find.Attributes()) 
                {
                    Console.WriteLine("    " + a.GetReference + " " + a.AttributeType + " " + a.AttributeName);
                }
            }
            else { Console.WriteLine("Class '" + toFind + "' not found!"); }

            toFind = "_class3";
            find = memory.FindClassByName(toFind);
            if (find != null)
            {
                Console.WriteLine(toFind);
                foreach (var a in find.Attributes())
                {
                    Console.WriteLine("    " + a.GetReference + " " + a.AttributeType + " " + a.AttributeName);
                }
            }
            else { Console.WriteLine("Class '" + toFind + "' not found!"); }

            toFind = "_class6";
            find = memory.FindClassByName(toFind);
            if (find != null)
            {
                Console.WriteLine(toFind);
                foreach (var a in find.Attributes())
                {
                    Console.WriteLine("    " + a.GetReference + " " + a.AttributeType + " " + a.AttributeName);
                }
            }
            else { Console.WriteLine("Class '" + toFind + "' not found!"); }

            return true;
        }
    }
}
