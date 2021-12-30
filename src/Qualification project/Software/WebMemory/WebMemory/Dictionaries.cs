using System.Collections.Generic;
using System.Linq;

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
            var rClass = _k.resolveIteratorFirst(it);
            while (rClass != 0)
            {
                d.Add(rClass, _m.FindClassByReference(rClass));
                rClass = _k.resolveIteratorNext(it);
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
                d.Add(r, o);
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
            var rClass = _k.resolveIteratorFirst(it);
            while (rClass != 0)
            {
                WebClass c = new(rClass, _m);
                d.Add(rClass, c);
                rClass = _k.resolveIteratorNext(it);
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
                d.Add(r, o);
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
                d.Add(r, a);
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
                d.Add(r, e);
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
                d.Add(r, e);
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
            var it = _k.getIteratorForDirectSubClasses(rClass);
            var r = _k.resolveIteratorFirst(it);
            while (r != 0)
            {
                d.Add(r, _m.FindClassByReference(r));
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
                d.Add(r, _m.FindClassByReference(r));
                r = _k.resolveIteratorNext(it);
            }
            _k.freeIterator(it);
            return d;
        }
    }
}
