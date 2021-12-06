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

            Raivis temp2 = new(memory, calls);
            Artis temp1 = new(memory, calls);
            


            var artis = memory.FindClassByName("Artis");
            var raivis = memory.FindClassByName("Raivis");

            Console.WriteLine(artis.Name + " " + raivis.Name);

            
            //Console.WriteLine(temp1.target1);
            //Console.WriteLine(temp2.source1);
            Console.WriteLine("Ingoing in Artis");
            foreach (var ia in artis.IngoingAssociationEnds()) 
            {
                Console.WriteLine(ia.Name);
            }
            Console.WriteLine("Outgoing from Artis");
            foreach (var oa in artis.OutgoingAssociationEnds()) 
            {
                Console.WriteLine(oa.Name);
            }
            Console.WriteLine("Ingoing in Raivis");
            foreach (var ia in raivis.IngoingAssociationEnds())
            {
                Console.WriteLine(ia.Name);
            }
            Console.WriteLine("Outgoing from Raivis");
            foreach (var oa in raivis.OutgoingAssociationEnds())
            {
                Console.WriteLine(oa.Name);
            }

            artis.FindAssociationEnd("target1");
            
        }
    }
}
