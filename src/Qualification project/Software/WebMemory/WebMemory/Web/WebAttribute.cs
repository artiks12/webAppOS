﻿// WebAttribute.cs
/******************************************************
* Satur klases "WebAttribute" atribūtus.
******************************************************/
// Autors:  Artis Pauniņš
// Pabeigts: v1.0 06.01.22

namespace WebAppOS
{
    public class WebAttribute 
    {
        /// <summary>
        /// Mainīgie
        /// </summary>
        private readonly TDAKernel _k; // TDAKernel instance
        private readonly long _r; // Atribūta atsauce

        /// <summary>
        /// Konstruktors
        /// </summary>
        /// <param name="r">Atribūta atsauce</param>
        /// <param name="k">TDAKernel instance</param>
        public WebAttribute(long r, IWebMemory m)
        {
            _r = r;
            _k = m.GetTDAKernel();
        }

        /// <summary>
        /// Atgriež objekta atsauci
        /// </summary>
        /// <returns></returns>
        public long GetReference { get { return _r; } }

        /// <summary>
        /// Atgriež atribūta vārdu.
        /// </summary>
        public string AttributeName
        {
            get { return _k.getAttributeName(_r); }
        }

        /// <summary>
        /// Atgriež atribūta tipu.
        /// </summary>
        public string AttributeType
        {
            get 
            { 
                var type = _k.getAttributeType(_r);
                return _k.getPrimitiveDataTypeName(type);
            }
        }
    }
}
