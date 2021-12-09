﻿namespace WebAppOS
{
    public partial class WebClass 
    {
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
        /// Izdzēš objektu
        /// </summary>
        /// <param name="name">objekta atsauce</param>
        public void DeleteObject(long r)
        {
            _k.deleteObject(r);
        }

        /// <summary>
        /// Atrod attribūtu pēc vārda
        /// </summary>
        /// <param name="name">attribūta vārds</param>
        /// <returns></returns>
        public WebAttribute FindAttributeByName(string name)
        {
            return FindAttributeByRefernece(_k.findAttribute(_r, name));
        }

        /// <summary>
        /// Atrod attribūtu pēc atsauces
        /// </summary>
        /// <param name="name">attribūta atsauce</param>
        /// <returns></returns>
        public WebAttribute FindAttributeByRefernece(long r)
        {
            var d = Dictionaries.D_GetClassAttributes(_r, _k, _m);
            if (d.ContainsKey(r)) { return d[r]; }
            else { return null; }
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
        /// Izdzēš atribūtu
        /// </summary>
        /// <param name="name">atribūta vārds</param>
        public void DeleteAttribute(string name)
        {
            var a = FindAttributeByName(name);
            if (a != null) { _k.deleteClass(a.GetReference); }
        }

        /// <summary>
        /// Atrod asociācijas galapunktu pēc vārda
        /// </summary>
        /// <param name="targetRoleName">Associācijas galapunkta lomas vārds</param>
        /// <returns></returns>
        public WebAssociationEnd FindAssociationEndByName(string targetRoleName)
        {
            return FindAssociationEndByReference(_k.findAssociationEnd(_r, targetRoleName));
        }

        /// <summary>
        /// Atrod associācijas galapunktu pēc atsauces
        /// </summary>
        /// <param name="r">Associācijas galapunkta atsauce</param>
        /// <returns></returns>
        public WebAssociationEnd FindAssociationEndByReference(long r) 
        {
            var dIngoingEnd = Dictionaries.D_GetClassIngoingAssociationEnds(_r, _k, _m);
            var dOutgoingEnd = Dictionaries.D_GetClassOutgoingAssociationEnds(_r, _k, _m);

            if (dIngoingEnd.ContainsKey(r) != false) { return dIngoingEnd[r]; }
            else if (dOutgoingEnd.ContainsKey(r) != false) { return dOutgoingEnd[r]; }
            else { return null; }
        }

        /// <summary>
        /// Izveido asociācijas galapunktu
        /// </summary>
        /// <param name="targetClass">Mērķa klase</param>
        /// <param name="sourceRoleName">Avota lomas vārds</param>
        /// <param name="targetRoleName">Mērķa lomas vards</param>
        /// <param name="IsComposition">Vai asociācija ir kompozīcija</param>
        /// <returns></returns>
        public WebAssociationEnd CreateAssociation(WebClass targetClass, string sourceRoleName, string targetRoleName, bool IsComposition) 
        {
            var r = _k.createAssociation(_r,targetClass.GetReference,sourceRoleName,targetRoleName,IsComposition);
            WebAssociationEnd a = new(r, _m);
            return a;
        }

        public void DeleteAssociation(string targetRoleName)
        {
            var a = FindAssociationEndByName(targetRoleName);
            if (a != null) { _k.deleteAssociation(a.GetReference); }
        }
    }
}