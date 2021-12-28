using System;
using WebAppOS;
using System.Reflection;
using System.Collections.Generic;
using Test;

namespace TestProgram
{
    partial class Program
    {
        static bool test6() 
        {
            DirWebMemory memory = new();
            RemoteWebCalls calls = new();
            memory.Open("ar:C:/sample");

            _class6 _item6_1 = new(memory, calls);
            _class6 _item6_2 = new(memory, calls);
            _class6 _item6_3 = new(memory, calls);
            _class6 _item6_4 = new(memory, calls);
            
            _item6_1._int = 10;
            _item6_2._int = 24;
            _item6_3._int = 56;
            _item6_4._int = 99;

            _class2 _item2_1 = new(memory, calls);

            List<_class6> list = _item2_1.target1;
            Console.WriteLine(list.Count);

            list.Add(_item6_1);
            list.Add(_item6_3);
            list.Add(_item6_2);

            _item2_1.target1 = list;

            Console.WriteLine(_item2_1.target1.Count);
            foreach (var a in _item2_1.target1) 
            {
                Console.WriteLine(a._int);
            }

            _class4 _item4_1 = new(memory, calls);
            list = _item4_1.target1;
            Console.WriteLine(_item4_1.target1.Count);

            list.Add(_item6_4);
            list.Add(_item6_2);

            _item4_1.target1 = list;
            Console.WriteLine(_item4_1.target1.Count);
            foreach (var a in _item4_1.target1)
            {
                Console.WriteLine(a._int);
            }

            _class2 _item2_2 = new(memory, calls);

            list = _item2_2.target1;
            Console.WriteLine(list.Count);

            list.Add(_item6_3);

            _item2_2.target1 = list;

            Console.WriteLine(_item2_2.target1.Count);
            foreach (var a in _item2_2.target1)
            {
                Console.WriteLine(a._int);
            }
            return true;
        }
    }
}
