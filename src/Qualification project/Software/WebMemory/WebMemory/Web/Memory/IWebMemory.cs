// IWebMemory.cs
/******************************************************
* Satur interfeisa IWebMemory aprakstu.
******************************************************/
// Autors:  Artis Pauniņš
// Pabeigts: v1.0 06.01.22

using System.Collections.Generic; // Nodrošina darbu ar iebūvētajām datu struktūrām

namespace WebAppOS
{
    public interface IWebMemory
    {
        /// <summary>
        /// Iegūst TDAKernel instanci
        /// </summary>
        public TDAKernel GetTDAKernel();

        /// <summary>
        /// Saraksts ar klasēm
        /// </summary>
        public IEnumerable<WebClass> Classes();
        
        /// <summary>
        /// Atrod klasi pēc vārda
        /// </summary>
        /// <param name="name">klases vārds</param>
        public WebClass FindClassByName(string name);

        /// <summary>
        /// Atrod klasi pēc atsauces (reference)
        /// </summary>
        /// <param name="r">klases atsauce (reference)</param>
        public WebClass FindClassByReference(long r);

        /// <summary>
        /// Izveido jaunu klasi
        /// </summary>
        /// <param name="name">Klases vārds</param>
        public WebClass CreateClass(string name);

        /// <summary>
        /// Izdzēš klasi
        /// </summary>
        /// <param name="name">klases vārds</param>
        public int DeleteClass(string name);
    }
}