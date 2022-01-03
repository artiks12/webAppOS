using System;
using WebAppOS;
using System.Reflection;
using System.Collections.Generic;
using Test;

namespace TestProgram
{
    partial class Program
    {
        static bool generated_test1() 
        {
            test1_class0();
            test1_class1();
            test1_class2();
            test1_class3();
            test1_class4();
            test1_class5();
            test1_class6();
            test1_class7();
            test1_class8();
            test1_class9();
            test1_class10();
            test1_class11();
            test1_class12();

            return true;
        }
        static void test1_class0()
        {
            DirWebMemory memory = new();
            RemoteWebCalls calls = new();
            memory.Open("ar:C:/sample");

            Console.WriteLine("test1_class0");
            checkExistance(memory);
            Console.WriteLine("");
        }

        static void test1_class1() 
        {
            DirWebMemory memory = new();
            RemoteWebCalls calls = new();
            memory.Open("ar:C:/sample");

            Console.WriteLine("test1_class1");
            _class1 test = new(memory, calls);
            checkExistance(memory);
            Console.WriteLine("");
        }

        static void test1_class2()
        {
            DirWebMemory memory = new();
            RemoteWebCalls calls = new();
            memory.Open("ar:C:/sample");

            Console.WriteLine("test1_class2");
            _class2 test = new(memory, calls);
            checkExistance(memory);
            Console.WriteLine("");
        }

        static void test1_class3()
        {
            DirWebMemory memory = new();
            RemoteWebCalls calls = new();
            memory.Open("ar:C:/sample");

            Console.WriteLine("test1_class3");
            _class3 test = new(memory, calls);
            checkExistance(memory);
            Console.WriteLine("");
        }

        static void test1_class4()
        {
            DirWebMemory memory = new();
            RemoteWebCalls calls = new();
            memory.Open("ar:C:/sample");

            Console.WriteLine("test1_class4");
            _class4 test = new(memory, calls);
            checkExistance(memory);
            Console.WriteLine("");
        }

        static void test1_class5()
        {
            DirWebMemory memory = new();
            RemoteWebCalls calls = new();
            memory.Open("ar:C:/sample");

            Console.WriteLine("test1_class5");
            _class5 test = new(memory, calls);
            checkExistance(memory);
            Console.WriteLine("");
        }

        static void test1_class6()
        {
            DirWebMemory memory = new();
            RemoteWebCalls calls = new();
            memory.Open("ar:C:/sample");

            Console.WriteLine("test1_class6");
            _class6 test = new(memory, calls);
            checkExistance(memory);
            Console.WriteLine("");
        }

        static void test1_class7()
        {
            DirWebMemory memory = new();
            RemoteWebCalls calls = new();
            memory.Open("ar:C:/sample");

            Console.WriteLine("test1_class7");
            _class7 test = new(memory, calls);
            checkExistance(memory);
            Console.WriteLine("");
        }

        static void test1_class8()
        {
            DirWebMemory memory = new();
            RemoteWebCalls calls = new();
            memory.Open("ar:C:/sample");

            Console.WriteLine("test1_class8");
            _class8 test = new(memory, calls);
            checkExistance(memory);
            Console.WriteLine("");
        }

        static void test1_class9()
        {
            DirWebMemory memory = new();
            RemoteWebCalls calls = new();
            memory.Open("ar:C:/sample");

            Console.WriteLine("test1_class9");
            _class9 test = new(memory, calls);
            checkExistance(memory);
            Console.WriteLine("");
        }

        static void test1_class10()
        {
            DirWebMemory memory = new();
            RemoteWebCalls calls = new();
            memory.Open("ar:C:/sample");

            Console.WriteLine("test1_class10");
            _class10 test = new(memory, calls);
            checkExistance(memory);
            Console.WriteLine("");
        }

        static void test1_class11()
        {
            DirWebMemory memory = new();
            RemoteWebCalls calls = new();
            memory.Open("ar:C:/sample");

            Console.WriteLine("test1_class11");
            _class11 test = new(memory, calls);
            checkExistance(memory);
            Console.WriteLine("");
        }

        static void test1_class12()
        {
            DirWebMemory memory = new();
            RemoteWebCalls calls = new();
            memory.Open("ar:C:/sample");

            Console.WriteLine("test1_class12");
            _class12 test = new(memory, calls);
            checkExistance(memory);
            Console.WriteLine("");
        }

        static void checkExistance(IWebMemory memory) 
        {
            for (int x = 1; x <= 12; x++) 
            {
                var toFind = "_class" + Convert.ToString(x);
                var find = memory.FindClassByName(toFind);
                if (find != null) { Console.WriteLine(toFind); }
                else { Console.WriteLine("Class '" + toFind + "' not found!"); }
            }
        }
    }
}
