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
            memory.Open("ar:D:/sample");

            Artis temp = new(memory, calls);

            temp.Vecums = 21;
            temp.Vards = "Artis Paunins";
            temp.IrStudents = true;
            temp.Nauda = 222.99;

            Console.WriteLine(temp.Vecums + " " + temp.Vards + " " + temp.IrStudents + " " + temp.Nauda);

            Artis temp1 = new(memory, calls);

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
