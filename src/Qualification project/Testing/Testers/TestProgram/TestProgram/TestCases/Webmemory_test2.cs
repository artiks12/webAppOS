using System;
using WebAppOS;
using System.Reflection;
using System.Collections.Generic;
using Test;

namespace TestProgram
{
    partial class Program
    {
        static bool WebMemory_test2() 
        {
            DirWebMemory memory = new();
            RemoteWebCalls calls = new();
            memory.Open("ar:C:/sample");

            Console.WriteLine("");
            var c = memory.FindClassByName("Planet");
            if (c != null) 
            { 
                Console.WriteLine(c.GetReference + " " + c.Name);
                foreach (var l in c.Objects()) { Console.WriteLine("    Object:" + l.GetReference); }
                foreach (var l in c.SuperClasses()) { Console.WriteLine("    SuperClass:" + l.GetReference + " " + l.Name); }
                foreach (var l in c.SubClasses()) { Console.WriteLine("    SubClass:" + l.GetReference + " " + l.Name); }
                foreach (var l in c.IngoingAssociationEnds()) { Console.WriteLine("    IngoingAssociationEnd:" + l.GetReference + " " + l.Name + " " + l.SourceClass.Name + " " + l.TargetClass.Name + " " + l.IsComposition); }
                foreach (var l in c.OutgoingAssociationEnds()) { Console.WriteLine("    OutgoingAssociationEnd:" + l.GetReference + " " + l.Name + " " + l.SourceClass.Name + " " + l.TargetClass.Name + " " + l.IsComposition); }
                foreach (var l in c.Attributes()) { Console.WriteLine("    Attribute:" + l.GetReference + " " + l.AttributeType + " " + l.AttributeName); }
            }
            else { Console.WriteLine("Class 'ProjectCreatedEvent' not found!"); }

            c.CreateObject();
            c.DeleteObject(12973);
            if (c != null)
            {
                Console.WriteLine(c.GetReference + " " + c.Name);
                foreach (var l in c.Objects()) { Console.WriteLine("    Object:" + l.GetReference); }
            }
            
            var temp = memory.CreateClass("_class1");
            c.CreateAssociation(temp,"source123","target456",true);
            c.DeleteAssociation("rc1");
            if (c != null)
            {
                Console.WriteLine(c.GetReference + " " + c.Name);
                foreach (var l in c.IngoingAssociationEnds()) { Console.WriteLine("    IngoingAssociationEnd:" + l.GetReference + " " + l.Name + " " + l.SourceClass.Name + " " + l.TargetClass.Name + " " + l.IsComposition); }
                foreach (var l in c.OutgoingAssociationEnds()) { Console.WriteLine("    OutgoingAssociationEnd:" + l.GetReference + " " + l.Name + " " + l.SourceClass.Name + " " + l.TargetClass.Name + " " + l.IsComposition); }
            }
            
            var l1 = c.FindTargetAssociationEndByName("star");
            Console.WriteLine("Found: " + l1.GetReference + " " + l1.Name + " " + l1.SourceClass.Name + " " + l1.TargetClass.Name + " " + l1.IsComposition);

            c.CreateAttribute("_int","Integer");
            c.DeleteAttribute("name");
            if (c != null)
            {
                Console.WriteLine(c.GetReference + " " + c.Name);
                foreach (var l in c.Attributes()) { Console.WriteLine("    Attribute:" + l.GetReference + " " + l.AttributeType + " " + l.AttributeName); }
            }
            var l2 = c.FindAttributeByName("state");
            Console.WriteLine("Found: " + l2.AttributeName + " " + l2.AttributeType);

            c.CreateGeneralization("Frame");
            c.DeleteGeneralization("GalaxyComponent");
            if (c != null)
            {
                Console.WriteLine(c.GetReference + " " + c.Name);
                foreach (var l in c.SuperClasses()) { Console.WriteLine("    SuperClass:" + l.GetReference + " " + l.Name); }
            }

            Console.WriteLine(c.IsSubClassOf(memory.FindClassByName("GoogleMaps")));
            Console.WriteLine(c.IsSubClassOf(temp));

            return true;
        }
    }
}
