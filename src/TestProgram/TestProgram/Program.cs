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

            Artis temp1 = new(memory, calls);

            temp1.Vecums = 25;
            temp1.Vards = "Raivis Paunins";
            temp1.IrStudents = false;
            temp1.Nauda = 333.33;

            Console.WriteLine(temp1.Vecums + " " + temp1.Vards + " " + temp1.IrStudents + " " + temp1.Nauda);

            Console.WriteLine(temp1.sum1(5, 5));
        }
    }
}
