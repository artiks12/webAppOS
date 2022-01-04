using System;
using WebAppOS;
using System.Reflection;
using System.Collections.Generic;
using Test;

namespace TestProgram
{
    partial class Program
    {
        static bool WebMemory_test3() 
        {
            DirWebMemory memory = new();
            RemoteWebCalls calls = new();
            memory.Open("ar:C:/sample");

            Console.WriteLine("");
            var c = memory.FindClassByName("Planet");
            WebObject o1 = null;
            WebObject o2 = null;
            if (c != null) 
            { 
                Console.WriteLine(c.GetReference + " " + c.Name);
                foreach (var l in c.Objects()) 
                {
                    if (l.GetReference == 12973) { o1 = l; }
                    else { o2 = l; }
                    Console.WriteLine("    Object:" + l.GetReference);
                    foreach (var t in l.Classes()) { Console.WriteLine("        Class:" + t.GetReference + " " + t.Name); }
                    foreach (var t in c.OutgoingAssociationEnds()) 
                    { 
                        Console.WriteLine("        OutgoingAssociationEnd:" + t.GetReference + " " + t.Name + " " + t.SourceClass.Name + " " + t.TargetClass.Name + " " + t.IsComposition);
                        foreach (var s in l.LinkedObjects(t.Name)) { Console.WriteLine("            LinkedObject:" + s.GetReference); }
                    }
                    foreach (var t in c.Attributes()) 
                    { 
                        Console.WriteLine("        Attribute:" + t.GetReference + " " + t.AttributeType + " " + t.AttributeName + " " + l[t.AttributeName]); 
                    }
                }            
            }
            else { Console.WriteLine("Class 'ProjectCreatedEvent' not found!"); }

            o2.LinkObject("star", o1);
            o1.LinkObject("star", o2);

            if (c != null)
            {
                Console.WriteLine(c.GetReference + " " + c.Name);
                foreach (var l in c.Objects())
                {
                    Console.WriteLine("    Object:" + l.GetReference);
                    foreach (var t in l.Classes()) { Console.WriteLine("        Class:" + t.GetReference + " " + t.Name); }
                    foreach (var t in c.OutgoingAssociationEnds())
                    {
                        Console.WriteLine("        OutgoingAssociationEnd:" + l.GetReference + " " + t.Name + " " + t.SourceClass.Name + " " + t.TargetClass.Name + " " + t.IsComposition);
                        foreach (var s in l.LinkedObjects(t.Name)) { Console.WriteLine("            LinkedObject:" + s.GetReference); }
                    }
                    foreach (var t in c.Attributes())
                    {
                        Console.WriteLine("        Attribute:" + t.GetReference + " " + t.AttributeType + " " + t.AttributeName + " " + l[t.AttributeName]);
                    }
                }
            }
            else { Console.WriteLine("Class 'ProjectCreatedEvent' not found!"); }

            o1.UnlinkObject("star", o2);

            if (c != null)
            {
                Console.WriteLine(c.GetReference + " " + c.Name);
                foreach (var l in c.Objects())
                {
                    Console.WriteLine("    Object:" + l.GetReference);
                    foreach (var t in l.Classes()) { Console.WriteLine("        Class:" + t.GetReference + " " + t.Name); }
                    foreach (var t in c.OutgoingAssociationEnds())
                    {
                        Console.WriteLine("        OutgoingAssociationEnd:" + l.GetReference + " " + t.Name + " " + t.SourceClass.Name + " " + t.TargetClass.Name + " " + t.IsComposition);
                        foreach (var s in l.LinkedObjects(t.Name)) { Console.WriteLine("            LinkedObject:" + s.GetReference); }
                    }
                    foreach (var t in c.Attributes())
                    {
                        Console.WriteLine("        Attribute:" + t.GetReference + " " + t.AttributeType + " " + t.AttributeName + " " + l[t.AttributeName]);
                    }
                }
            }
            else { Console.WriteLine("Class 'ProjectCreatedEvent' not found!"); }

            return true;
        }
    }
}
