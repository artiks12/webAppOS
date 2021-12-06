using System.Collections.Generic;
using System.Linq;

namespace WebAppOS
{
    public class WebClass 
    {
        /// <summary>
        /// Mainīgie
        /// </summary>
        private readonly TDAKernel _k; // TDAKernel instance
        private readonly IWebMemory _m; // WebMemory instance
        private readonly long _r; // Klases atsauce

        /// <summary>
        /// Konstruktors
        /// </summary>
        /// <param name="r">Klases atsauce</param>
        /// <param name="k">TDAKernel instance</param>
        /// <param name="m">WebMemory instance</param>
        public WebClass(long r, IWebMemory m) 
        {
            _r = r;
            _k = m.GetTDAKernel();
            _m = m;
        }

        /// <summary>
        /// Atgriež klases atsauci
        /// </summary>
        /// <returns></returns>
        public long GetReference() { return _r; }

        /// <summary>
        /// Klases vārds
        /// </summary>
        public string Name { get { return _k.GetClassName(_r); } }

        /// <summary>
        /// Pārbauda, vai klase c ir apakšklase
        /// </summary>
        /// <param name="c">apakšklase</param>
        public bool IsSubClassOf(WebClass c) { return _k.isDirectSubClass(_r,c._r); }

        /// <summary>
        /// Izveido objektu klasei
        /// </summary>
        public WebObject CreateObject() 
        {
            var or = _k.createObject(_r);
            var oi = new WebObject(or, _m);
            return oi;
        }

        /// <summary>
        ///  Saraksts ar objektiem
        /// </summary>
        public IEnumerable<WebObject> Objects() 
        {
            var d = Dictionaries.D_GetClassObjects(_r,_k,_m);
            IEnumerable<WebObject> query = from i in d select i.Value;
            return query;
        }

        /// <summary>
        ///  Saraksts ar virsklasēm
        /// </summary>
        public IEnumerable<WebClass> Supe_res()
        {
            var d = Dictionaries.D_GetSuperClasses(_r,_k,_m);
            IEnumerable<WebClass> query = from i in d select i.Value;
            return query;
        }

        /// <summary>
        ///  Saraksts ar apakšklasēm
        /// </summary>
        public IEnumerable<WebClass> SubClasses()
        {
            var d = Dictionaries.D_GetSubClasses(_r,_k,_m);
            IEnumerable<WebClass> query = from i in d select i.Value;
            return query;
        }

        /// <summary>
        ///  Saraksts ar atribūtiem
        /// </summary>
        public IEnumerable<WebAttribute> Attributes()
        {
            var d = Dictionaries.D_GetClassAttributes(_r,_k,_m);
            IEnumerable<WebAttribute> query = from i in d select i.Value;
            return query;
        }

        /// <summary>
        /// Atrod attribūtu
        /// </summary>
        /// <param name="name">attribūta vārds</param>
        /// <returns></returns>
        public WebAttribute FindAttribute(string name) 
        {
            var d = Dictionaries.D_GetClassAttributes(_r, _k, _m);
            long r = _k.findAttribute(_r, name);
            if (r == 0) { return null; }
            return d[r];
        }

        /// <summary>
        /// Izveidot attribūtu
        /// </summary>
        /// <param name="name">attribūta vārds</param>
        /// <param name="type">attribūta datu tips</param>
        /// <returns></returns>
        public WebAttribute CreateAttribute(string name, string type)
        {
            var t = _k.findPrimitiveDataType(type);
            var r = _k.createAttribute(_r,name,t);
            WebAttribute a = new(r, _m);
            return a;
        }

        /// <summary>
        ///  Saraksts ar ieejošo associāciju galapunktiem
        /// </summary>
        public IEnumerable<WebAssociationEnd> IngoingAssociationEnds()
        {
            var d = Dictionaries.D_GetClassIngoingAssociationEnds(_r,_k,_m);
            IEnumerable<WebAssociationEnd> query = from i in d select i.Value;
            return query;
        }

        /// <summary>
        ///  Saraksts ar izejošo associāciju galapunktiem
        /// </summary>
        public IEnumerable<WebAssociationEnd> OutgoingAssociationEnds()
        {
            var d = Dictionaries.D_GetClassOutgoingAssociationEnds(_r, _k, _m);
            IEnumerable<WebAssociationEnd> query = from i in d select i.Value;
            return query;
        }

        /// <summary>
        /// Atrod associācijas galapunktu
        /// </summary>
        /// <param name="roleName">Associācijas galapunkta lomas vārds</param>
        /// <returns></returns>
        public WebAssociationEnd FindAssociationEnd(string targetRoleName) 
        {
            var dIngoingEnd = Dictionaries.D_GetClassIngoingAssociationEnds(_r, _k, _m);
            var dOutgoingEnd = Dictionaries.D_GetClassOutgoingAssociationEnds(_r, _k, _m);

            var r = _k.findAssociationEnd(_r, targetRoleName);

            if (r == 0) { return null; }
            else 
            {
                if (dIngoingEnd.ContainsKey(r) != false) { return dIngoingEnd[r]; }
                else { return dOutgoingEnd[r]; }
            }
        }

        /// <summary>
        /// Izveido asociācijas galapunktu
        /// </summary>
        /// <param name="sourceClass">Avota klase</param>
        /// <param name="targetClass">Mērķa klase</param>
        /// <param name="sourceRoleName">Avota lomas vārds</param>
        /// <param name="targetRoleName">Mērķa lomas vards</param>
        /// <param name="IsComposition">Vai asociācija ir kompozīcija</param>
        /// <returns></returns>
        public WebAssociationEnd CreateAssociationEnd(WebClass sourceClass, WebClass targetClass, string sourceRoleName, string targetRoleName, bool IsComposition) 
        {
            var r = _k.createAssociation(sourceClass.GetReference(),targetClass.GetReference(),sourceRoleName,targetRoleName,IsComposition);
            WebAssociationEnd a = new(r, _m);
            return a;
        }
    }
}