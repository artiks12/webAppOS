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

            if (memory.FindClassByName("Frame") != null) { Console.WriteLine(memory.FindClassByName("Frame").Name); }
            else { Console.WriteLine("Class 'Frame' not found!"); }
            
            memory.DeleteClass("Frame");

            if (memory.FindClassByName("Frame") != null){ Console.WriteLine(memory.FindClassByName("Frame").Name); }
            else { Console.WriteLine("Class 'Frame' not found!"); }

            return true;
        }
    }
}
