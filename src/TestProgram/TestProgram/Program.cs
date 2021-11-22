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
            WebCalls calls = new();
            memory.Open("ar:C:/sample");

            BaseObject temp = new(memory, calls);

            Artis temp1 = new(memory, calls);

            foreach (var c in memory.Classes())
            {
                Console.WriteLine(c.Name);
            }

            temp1.Vecums = 25;
            temp1.Vards = "Raivis Paunins";
            temp1.IrStudents = false;
            temp1.Nauda = 333.33;

            Console.WriteLine(temp1.Vecums + " " + temp1.Vards + " " + temp1.IrStudents + " " + temp1.Nauda);

            memory.DeleteClass("Artis");

            foreach (var c in memory.Classes()) 
            {
                Console.WriteLine(c.Name);
            }
        }
    }
}
