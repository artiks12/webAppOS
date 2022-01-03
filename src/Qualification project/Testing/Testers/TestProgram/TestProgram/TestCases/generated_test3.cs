using System;
using WebAppOS;
using System.Reflection;
using System.Collections.Generic;
using Test;

namespace TestProgram
{
    partial class Program
    {
        static bool generated_test3() 
        {
            DirWebMemory memory = new();
            RemoteWebCalls calls = new();
            memory.Open("ar:C:/sample");

            _class1 test1 = new(memory, calls);
            _class7 test7 = new(memory, calls);
            _class10 test10 = new(memory, calls);

            var toFind = "_class1";
            var find = memory.FindClassByName(toFind);
            if (find != null)
            {
                Console.WriteLine(toFind);
                Console.WriteLine("    Ingoing associations");
                foreach (var a in find.IngoingAssociationEnds())
                {
                    Console.WriteLine("        " + a.GetReference + " " + a.Name + " " + a.TargetClass.Name);
                }
                Console.WriteLine("    Outgoing associations");
                foreach (var a in find.OutgoingAssociationEnds())
                {
                    Console.WriteLine("        " + a.GetReference + " " + a.Name + " " + a.TargetClass.Name);
                }
                Console.WriteLine("");
            }
            else { Console.WriteLine("Class '" + toFind + "' not found!"); }

            toFind = "_class7";
            find = memory.FindClassByName(toFind);
            if (find != null)
            {
                Console.WriteLine(toFind);
                Console.WriteLine("    Ingoing associations");
                foreach (var a in find.IngoingAssociationEnds())
                {
                    Console.WriteLine("        " + a.GetReference + " " + a.Name + " " + a.TargetClass.Name);
                }
                Console.WriteLine("    Outgoing associations");
                foreach (var a in find.OutgoingAssociationEnds())
                {
                    Console.WriteLine("        " + a.GetReference + " " + a.Name + " " + a.TargetClass.Name);
                }
                Console.WriteLine("");
            }
            else { Console.WriteLine("Class '" + toFind + "' not found!"); }

            toFind = "_class8";
            find = memory.FindClassByName(toFind);
            if (find != null)
            {
                Console.WriteLine(toFind);
                Console.WriteLine("    Ingoing associations");
                foreach (var a in find.IngoingAssociationEnds())
                {
                    Console.WriteLine("        " + a.GetReference + " " + a.Name + " " + a.TargetClass.Name);
                }
                Console.WriteLine("    Outgoing associations");
                foreach (var a in find.OutgoingAssociationEnds())
                {
                    Console.WriteLine("        " + a.GetReference + " " + a.Name + " " + a.TargetClass.Name);
                }
                Console.WriteLine("");
            }
            else { Console.WriteLine("Class '" + toFind + "' not found!"); }

            toFind = "_class9";
            find = memory.FindClassByName(toFind);
            if (find != null)
            {
                Console.WriteLine(toFind);
                Console.WriteLine("    Ingoing associations");
                foreach (var a in find.IngoingAssociationEnds())
                {
                    Console.WriteLine("        " + a.GetReference + " " + a.Name + " " + a.TargetClass.Name);
                }
                Console.WriteLine("    Outgoing associations");
                foreach (var a in find.OutgoingAssociationEnds())
                {
                    Console.WriteLine("        " + a.GetReference + " " + a.Name + " " + a.TargetClass.Name);
                }
                Console.WriteLine("");
            }
            else { Console.WriteLine("Class '" + toFind + "' not found!"); }

            toFind = "_class10";
            find = memory.FindClassByName(toFind);
            if (find != null)
            {
                Console.WriteLine(toFind);
                Console.WriteLine("    Ingoing associations");
                foreach (var a in find.IngoingAssociationEnds())
                {
                    Console.WriteLine("        " + a.GetReference + " " + a.Name + " " + a.TargetClass.Name);
                }
                Console.WriteLine("    Outgoing associations");
                foreach (var a in find.OutgoingAssociationEnds())
                {
                    Console.WriteLine("        " + a.GetReference + " " + a.Name + " " + a.TargetClass.Name);
                }
                Console.WriteLine("");
            }
            else { Console.WriteLine("Class '" + toFind + "' not found!"); }

            toFind = "_class11";
            find = memory.FindClassByName(toFind);
            if (find != null)
            {
                Console.WriteLine(toFind);
                Console.WriteLine("    Ingoing associations");
                foreach (var a in find.IngoingAssociationEnds())
                {
                    Console.WriteLine("        " + a.GetReference + " " + a.Name + " " + a.TargetClass.Name);
                }
                Console.WriteLine("    Outgoing associations");
                foreach (var a in find.OutgoingAssociationEnds())
                {
                    Console.WriteLine("        " + a.GetReference + " " + a.Name + " " + a.TargetClass.Name);
                }
                Console.WriteLine("");
            }
            else { Console.WriteLine("Class '" + toFind + "' not found!"); }

            toFind = "_class12";
            find = memory.FindClassByName(toFind);
            if (find != null)
            {
                Console.WriteLine(toFind);
                Console.WriteLine("    Ingoing associations");
                foreach (var a in find.IngoingAssociationEnds())
                {
                    Console.WriteLine("        " + a.GetReference + " " + a.Name + " " + a.TargetClass.Name);
                }
                Console.WriteLine("    Outgoing associations");
                foreach (var a in find.OutgoingAssociationEnds())
                {
                    Console.WriteLine("        " + a.GetReference + " " + a.Name + " " + a.TargetClass.Name);
                }
                Console.WriteLine("");
            }
            else { Console.WriteLine("Class '" + toFind + "' not found!"); }
            return true;
        }
    }
}
