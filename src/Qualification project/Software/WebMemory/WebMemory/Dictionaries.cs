// Dictionaries.cs
/******************************************************
* Satur vārdnīcu iegūsanas funkcijas.
* Visām funkcijam ir jāpadod TDAKernel un WebMemory, bet
* ir funkcijas, kuram vel vajag atsauces.
******************************************************/
// Autors:  Artis Pauniņš
// Pabeigts: v1.0 06.01.22

using System.Collections.Generic; // Nodrošina darbu ar iebūvētajām datu struktūrām

namespace WebAppOS
{
    class Dictionaries
    {
        /// <summary>
        /// Iegūst objekta klases
        /// </summary>
        public static Dictionary<long, WebClass> D_GetObjectClasses(long rObject, TDAKernel _k, IWebMemory _m)
        {
            Dictionary<long, WebClass> d = new();
            var it = _k.getIteratorForDirectObjectClasses(rObject);
            var r = _k.resolveIteratorFirst(it);
            while (r != 0)
            {
                if (!d.ContainsKey(r)) { d.Add(r, _m.FindClassByReference(r)); }
                r = _k.resolveIteratorNext(it);
            }
            _k.freeIterator(it);
            return d;
        }

        /// <summary>
        /// Iegūst objekta saistītos objektus
        /// </summary>
        public static Dictionary<long, WebObject> D_GetLinkedObjects(long rObject, long rAssociationEnd, TDAKernel _k, IWebMemory _m)
        {
            Dictionary<long, WebObject> d = new();
            var it = _k.getIteratorForLinkedObjects(rObject, rAssociationEnd);
            var r = _k.resolveIteratorFirst(it);
            while (r != 0)
            {
                WebObject o = new(r, _m);
                if (!d.ContainsKey(r)) { d.Add(r, o); }
                r = _k.resolveIteratorNext(it);
            }
            _k.freeIterator(it);
            return d;
        }

        /// <summary>
        /// Iegūst klases
        /// </summary>
        public static Dictionary<long, WebClass> D_GetClasses(TDAKernel _k, IWebMemory _m)
        {
            Dictionary<long, WebClass> d = new();
            var it = _k.getIteratorForClasses();
            var r = _k.resolveIteratorFirst(it);
            while (r != 0)
            {
                WebClass c = new(r, _m);
                if (!d.ContainsKey(r)) { d.Add(r, c); }
                r = _k.resolveIteratorNext(it);
            }
            _k.freeIterator(it);
            return d;
        }

        /// <summary>
        /// Iegūst klases objektus
        /// <summary>
        public static Dictionary<long, WebObject> D_GetClassObjects(long rClass, TDAKernel _k, IWebMemory _m)
        {
            Dictionary<long, WebObject> d = new();
            var it = _k.getIteratorForAllClassObjects(rClass);
            var r = _k.resolveIteratorFirst(it);
            while (r != 0)
            {
                WebObject o = new(r, _m);
                if (!d.ContainsKey(r)) { d.Add(r, o); }
                r = _k.resolveIteratorNext(it);
            }
            _k.freeIterator(it);
            return d;
        }

        /// <summary>
        /// Iegūst klases atribūtus
        /// <summary>
        public static Dictionary<long, WebAttribute> D_GetClassAttributes(long rClass, TDAKernel _k, IWebMemory _m)
        {
            Dictionary<long, WebAttribute> d = new();
            var it = _k.getIteratorForAllAttributes(rClass);
            var r = _k.resolveIteratorFirst(it);
            while (r != 0)
            {
                WebAttribute a = new(r, _m);
                if (!d.ContainsKey(r)) { d.Add(r, a); }
                r = _k.resolveIteratorNext(it);
            }
            _k.freeIterator(it);
            return d;
        }

        /// <summary>
        /// Iegūst klases Ieejošos asociāciju galapunktus
        /// <summary>
        public static Dictionary<long, WebAssociationEnd> D_GetClassIngoingAssociationEnds(long rClass, TDAKernel _k, IWebMemory _m)
        {
            Dictionary<long, WebAssociationEnd> d = new();
            var it = _k.getIteratorForAllIngoingAssociationEnds(rClass);
            var r = _k.resolveIteratorFirst(it);
            while (r != 0)
            {
                WebAssociationEnd e = new(r, _m);
                if (!d.ContainsKey(r)) { d.Add(r, e); }
                r = _k.resolveIteratorNext(it);
            }
            _k.freeIterator(it);
            return d;
        }

        /// <summary>
        /// Iegūst klases Izejošos asociāciju galapunktus
        /// <summary>
        public static Dictionary<long, WebAssociationEnd> D_GetClassOutgoingAssociationEnds(long rClass, TDAKernel _k, IWebMemory _m)
        {
            Dictionary<long, WebAssociationEnd> d = new();
            var it = _k.getIteratorForAllOutgoingAssociationEnds(rClass);
            var r = _k.resolveIteratorFirst(it);
            while (r != 0)
            {
                WebAssociationEnd e = new(r, _m);
                if (!d.ContainsKey(r)) { d.Add(r, e); }
                r = _k.resolveIteratorNext(it);
            }
            _k.freeIterator(it);
            return d;
        }

        /// <summary>
        /// Iegūst klases virsklases
        /// <summary>
        public static Dictionary<long, WebClass> D_GetSuperClasses(long rClass, TDAKernel _k, IWebMemory _m)
        {
            Dictionary<long, WebClass> d = new();
            var it = _k.getIteratorForDirectSuperClasses(rClass);
            var r = _k.resolveIteratorFirst(it);
            while (r != 0)
            {
                if (!d.ContainsKey(r)) { d.Add(r, _m.FindClassByReference(r)); }
                r = _k.resolveIteratorNext(it);
            }
            _k.freeIterator(it);
            return d;
        }

        /// <summary>
        /// Iegūst klases apakšklases
        /// <summary>
        public static Dictionary<long, WebClass> D_GetSubClasses(long rClass, TDAKernel _k, IWebMemory _m)
        {
            Dictionary<long, WebClass> d = new();
            var it = _k.getIteratorForDirectSubClasses(rClass);
            var r = _k.resolveIteratorFirst(it);
            while (r != 0)
            {
                if (!d.ContainsKey(r)) { d.Add(r, _m.FindClassByReference(r)); }
                r = _k.resolveIteratorNext(it);
            }
            _k.freeIterator(it);
            return d;
        }
    }
}
