using System.Collections.Generic;

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
        public void DeleteClass(string name);
    }
}