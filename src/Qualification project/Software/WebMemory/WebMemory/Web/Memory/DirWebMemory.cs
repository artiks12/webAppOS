using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAppOS
{
    public class DirWebMemory : IWebMemory
    {
        private TDAKernel _k; // TDAKernel instance

        /// <summary>
        /// Izveido TDAKernel instanci
        /// </summary>
        /// <param name="DirName"></param>
        public void Open(string DirName) 
        {
            try
            {
                _k = new();
                _k.open(DirName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }

        /// <summary>
        /// Iegūst TDAKernel instanci
        /// </summary?
        public TDAKernel GetTDAKernel() { return _k; }
        
        /// <summary>
        /// Atgriež sarakstu ar klasēm
        /// </summary>
        public IEnumerable<WebClass> Classes()
        {
            var d = Dictionaries.D_GetClasses(_k,this);
            IEnumerable<WebClass> query = from i in d select i.Value;
            return query;
        }

        /// <summary>
        /// Atrod klasi pēc vārda
        /// </summary>
        /// <param name="r">Klases vārds</param>
        public WebClass FindClassByName(string name)
        {
            return FindClassByReference(_k.findClass(name));
        }

        /// <summary>
        /// Atrod klasi pēc atsauces
        /// </summary>
        /// <param name="r">Klases atsauce</param>
        public WebClass FindClassByReference(long r)
        {
            var d = Dictionaries.D_GetClasses(_k, this);
            if (d.ContainsKey(r)) { return d[r]; }
            else { return null; }
        }

        /// <summary>
        /// Izveido jaunu klasi
        /// </summary>
        /// <param name="name">Klases vārds</param>
        public WebClass CreateClass(string name) 
        {
            var r = _k.createClass(name);
            WebClass o = new(r,this);
            return o;
        }

        /// <summary>
        /// Izdzēš klasi
        /// </summary>
        /// <param name="name">klases vārds</param>
        public void DeleteClass(string name)
        {
            var c = FindClassByName(name);
            if(c != null) { _k.deleteClass(c.GetReference); }
        }
    }
}