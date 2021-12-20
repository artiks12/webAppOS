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

            test._int1 = 5;
            test._int2 = 6;

            Console.WriteLine(test._int1 + "+" + test._int2 + "=" + (test._int1+test._int2));

            
        }
    }
}
