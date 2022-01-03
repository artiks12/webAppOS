using System;
using WebAppOS;
using System.Reflection;
using System.Collections.Generic;
using Test;

namespace TestProgram
{
    partial class Program
    {
        static bool generated_test4() 
        {
            DirWebMemory memory = new();
            RemoteWebCalls calls = new();
            memory.Open("ar:C:/sample");

            _class4 _Class4 = new(memory, calls);

            Console.WriteLine("_int: " + _Class4._int);
            _Class4._int = 6;
            Console.WriteLine("_int: " + _Class4._int);

            Console.WriteLine("_str: " + _Class4._str);
            _Class4._str = "Hello World!";
            Console.WriteLine("_str: " + _Class4._str);

            Console.WriteLine("_bool: " + _Class4._bool);
            _Class4._bool = true;
            Console.WriteLine("_bool: " + _Class4._bool);

            Console.WriteLine("_real: " + _Class4._real);
            _Class4._real = 6.90886;
            Console.WriteLine("_real: " + _Class4._real);


            _class3 _Class3_1 = new(memory, calls);
            _class3 _Class3_2 = new(memory, calls);

            Console.WriteLine(_Class3_1._str + " : " + _Class3_2._str);
            _Class3_1._str = "string one";
            _Class3_2._str = "String two";
            Console.WriteLine(_Class3_1._str + " : " + _Class3_2._str);

            return true;
        }
    }
}
