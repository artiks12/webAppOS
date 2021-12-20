using System;
using WebAppOS;
using System.Reflection;
using System.Collections.Generic;
using Test;

namespace TestProgram
{
    class Program
    {
        static void Main(string[] args)
        {

            DirWebMemory memory = new();
            RemoteWebCalls calls = new();
            memory.Open("ar:C:/sample");

            test3 test = new(memory,calls);



            Console.WriteLine(memory.FindClassByName("test2").Name);

            
        }
    }
}
