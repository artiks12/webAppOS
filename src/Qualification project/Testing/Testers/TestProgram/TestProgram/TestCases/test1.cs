using System;
using WebAppOS;
using System.Reflection;
using System.Collections.Generic;
using Test;

namespace TestProgram
{
    partial class Program
    {
        static bool test1() 
        {
            DirWebMemory memory = new();
            RemoteWebCalls calls = new();
            memory.Open("ar:C:/sample");

            var classes = memory.Classes();
            foreach (var c in classes) 
            {
                Console.WriteLine(c.Name);
            }

            if (memory.FindClassByName("CSV") != null) { Console.WriteLine(memory.FindClassByName("CSV").Name); }
            else { Console.WriteLine("Class 'CSV' not found!"); }
            
            Console.WriteLine(memory.DeleteClass("CSV"));
            Console.WriteLine(memory.DeleteClass("CSV"));

            if (memory.FindClassByName("CSV") != null){ Console.WriteLine(memory.FindClassByName("CSV").Name); }
            else { Console.WriteLine("Class 'CSV' not found!"); }

            return true;
        }
    }
}
