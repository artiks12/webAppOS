using System;
using WebAppOS;
using System.Reflection;
using System.Collections.Generic;
using Test;

namespace TestProgram
{
    partial class Program
    {
        static bool WebMemory_test1() 
        {
            DirWebMemory memory = new();
            RemoteWebCalls calls = new();
            memory.Open("ar:C:/sample");

            var classes = memory.Classes();
            foreach (var c in classes) 
            {
                Console.WriteLine(c.Name);
            }

            Console.WriteLine("");
            var temp1 = memory.FindClassByName("ProjectCreatedEvent");
            if (temp1 != null) { Console.WriteLine(temp1.GetReference + " " + temp1.Name); }
            else { Console.WriteLine("Class 'ProjectCreatedEvent' not found!"); }

            Console.WriteLine("");
            var temp2 = memory.FindClassByName("_class1");
            if (temp2 != null) { Console.WriteLine(temp2.GetReference + " " + temp2.Name); }
            else { Console.WriteLine("Class '_class1' not found!"); }

            Console.WriteLine("");
            temp2 = memory.CreateClass("_class1");
            if (temp2 != null) { Console.WriteLine(temp2.GetReference + " " + temp2.Name); }
            else { Console.WriteLine("Class '_class1' not found!"); }

            Console.WriteLine("");
            Console.WriteLine(memory.DeleteClass("_class1"));
            var temp3 = memory.FindClassByName("_class1");
            if (temp3 != null) { Console.WriteLine(temp3.GetReference + " " + temp3.Name); }
            else { Console.WriteLine("Class '_class1' not found!"); }

            return true;
        }
    }
}
