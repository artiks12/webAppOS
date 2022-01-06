// KernelWebMemory.cs
/******************************************************
* Satur interfeisa IWebMemory funkciju definīcijas.
* Izmantos WebAppOS sistēmā
******************************************************/
// Autors:  Artis Pauniņš
// Pabeigts: v1.0 06.01.22

using System.Collections.Generic; // Nodrošina darbu ar iebūvētajām datu struktūrām
using System.Linq; // Nodrošina LINQ funkcijas

namespace WebAppOS
{
    public class KernelWebMemory : IWebMemory
    {
        private TDAKernel _k; // TDAKernel instance

        public KernelWebMemory(TDAKernel k) 
        {
            _k = k;
        }

        /// <summary>
        /// Iegūst TDAKernel instanci
        /// </summary>
        public TDAKernel GetTDAKernel() { return _k; }

        /// <summary>
        /// Atgriež sarakstu ar klasēm
        /// </summary>
        public IEnumerable<WebClass> Classes()
        {
            var d = Dictionaries.D_GetClasses(_k, this);
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
            WebClass o = new(r, this);
            return o;
        }

        /// <summary>
        /// Izdzēš klasi
        /// </summary>
        /// <param name="name">klases vārds</param>
        public int DeleteClass(string name)
        {
            var c = FindClassByName(name);
            if (c != null) { _k.deleteClass(c.GetReference); }
            return 0;
        }
    }
}