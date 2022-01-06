﻿// WebObject.cs
/******************************************************
* Satur klases "WebObject" atribūtus un funkcijas.
******************************************************/
// Autors:  Artis Pauniņš
// Pabeigts: v1.0 06.01.22

using System.Collections.Generic; // Nodrošina darbu ar iebūvētajām datu struktūrām
using System.Linq; // Nodrošina LINQ funkcijas

namespace WebAppOS
{
    public class WebObject
    {
        /// <summary>
        /// Mainīgie
        /// </summary>
        private readonly TDAKernel _k; // TDAKernel instance
        private readonly IWebMemory _m; // WebMemory instance
        private readonly long _r; // Objekta atsauce

        /// <summary>
        /// Konstruktors
        /// </summary>
        /// <param name="r">Objekta atsauce</param>
        /// <param name="k">TDAKernel instance</param>
        /// <param name="m">WebMemory instance</param>
        public WebObject(long r, IWebMemory m)
        {
            _r = r;
            _k = m.GetTDAKernel();
            _m = m;
        }

        /// <summary>
        /// Iegūst, uzstāda atribūta vērtību.
        /// </summary>
        /// <param name="name">atribūta vārds</param>
        /// <returns></returns>
        public string this[string name]
        {
            get 
            {
                var classes = Classes();
                foreach (var c in classes) 
                {
                    var result = c.FindAttributeByName(name);
                    if (result != null) { return _k.getAttributeValue(_r,result.GetReference); }
                }
                return null;
            }
            set 
            {
                var classes = Classes();
                foreach (var c in classes)
                {
                    var result = c.FindAttributeByName(name);
                    if (result != null) { _k.setAttributeValue(_r,result.GetReference, value); return; }
                }
            }
        }

        /// <summary>
        /// Sasaista objektus, izmantojot Asociācijas galapunkta lomas vārdu.
        /// </summary>
        /// <param name="roleName">Asociācijas galapunkta lomas vārds</param>
        /// <param name="oTarget">Mērķa klase</param>
        public void LinkObject(string roleName, WebObject oTarget) 
        {
            var c = getAssociationClassByRoleName(roleName);
            if (c != null) 
            {
                var a = _k.findAssociationEnd(c.GetReference, roleName);
                LinkObject(new WebAssociationEnd(a, _m), oTarget);
            }
        }

        /// <summary>
        /// Sasaista objektus, izmantojot Asociācijas galapunktu.
        /// </summary>
        /// <param name="a">Asociācijas galapunkts</param>
        /// <param name="oTarget">Mērķa klase</param>
        public void LinkObject(WebAssociationEnd a, WebObject oTarget) { _k.createLink(_r, oTarget._r, a.GetReference); }

        /// <summary>
        /// Izdzēš objektu saiti, izmantojot Asociācijas galapunkta lomas vārdu.
        /// </summary>
        /// <param name="roleName">Asociācijas galapunkta lomas vārds</param>
        /// <param name="oTarget">Mērķa klase</param>
        public void UnlinkObject(string roleName, WebObject oTarget) 
        {
            var c = getAssociationClassByRoleName(roleName);
            if (c != null)
            {
                var a = _k.findAssociationEnd(c.GetReference, roleName);
                UnlinkObject(new WebAssociationEnd(a, _m), oTarget);
            }
        }

        /// <summary>
        /// Izdzēš objektu saiti, izmantojot Asociācijas galapunktu.
        /// </summary>
        /// <param name="a">Asociācijas galapunkts</param>
        /// <param name="oTarget">Mērķa klase</param>
        public void UnlinkObject(WebAssociationEnd a, WebObject oTarget) { _k.deleteLink(_r, oTarget.GetReference, a.GetReference); }

        /// <summary>
        /// Izmet vecos linkus (dObject) ar deleteLink funkciju un tad pielikt klāt elementus no oList
        /// </summary>
        /// <param name="a"></param>
        /// <param name="oList"></param>
        public void LinkObjects(WebAssociationEnd a, IEnumerable<WebObject> oList) 
        {
            List<WebObject> links = LinkedObjects(a).ToList();
            
            foreach (var o in links) { UnlinkObject(a, o); }
            
            foreach (var o in oList) { LinkObject(a, o); }
        }

        /// <summary>
        ///  Saraksts ar saistītajiem objektiem, izmantojot Asociācijas galapunkta lomas vārdu.
        /// </summary>
        /// <param name="roleName">Asociācijas galapunkta lomas vārds</param>
        public IEnumerable<WebObject> LinkedObjects(string roleName)
        {
            var c = getAssociationClassByRoleName(roleName);
            var a = _k.findAssociationEnd(c.GetReference,roleName);
            return LinkedObjects(new WebAssociationEnd(a,_m));
        }

        /// <summary>
        ///  Saraksts ar saistītajiem objektiem, izmantojot Asociācijas galapunktu.
        /// </summary>
        /// <param name="a">Asociācijas galapunkts</param>
        public IEnumerable<WebObject> LinkedObjects(WebAssociationEnd a)
        {
            var d = Dictionaries.D_GetLinkedObjects(_r, a.GetReference, _k, _m);
            IEnumerable<WebObject> query = from i in d select i.Value;
            return query;
        }

        /// <summary>
        /// Atgriež sarakstu ar klasēm, kurām pieder dotais objekts.
        /// </summary>
        public IEnumerable<WebClass> Classes()
        {
            var d = Dictionaries.D_GetObjectClasses(_r,_k,_m);
            IEnumerable<WebClass> query = from i in d select i.Value;
            return query;
        }

        /// <summary>
        /// Atgriež objekta atsauci
        /// </summary>
        /// <returns></returns>
        public long GetReference { get { return _r; } }

        /// <summary>
        /// Atrod asociācijas avota klasi pēc lomas vārda
        /// </summary>
        /// <param name="targetRoleName">Asociācijas mērķa lomas vārds</param>
        public WebClass getAssociationClassByRoleName(string targetRoleName) 
        {
            var classes = Classes();
            foreach (var c in classes)
            {
                var a = _k.findAssociationEnd(c.GetReference, targetRoleName);
                if (a != 0) { return c; }
            }
            return null;
        }
    }
}