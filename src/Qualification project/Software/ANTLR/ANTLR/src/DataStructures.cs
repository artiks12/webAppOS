// DataStructures.cs
/******************************************************
* Kompilatora Datu struktūru definīcijas.
******************************************************/
// Autors:  Artis Pauniņš
// Pabeigts: v1.0 06.01.22

using System.Collections.Generic; // Nodrošina darbu ar iebūvētajām datu struktūrām

namespace AntlrCSharp
{
    /// <summary>
    /// Klase, kas glabā informāciju par anotācijām
    /// </summary>
    public class Annotation 
    {
        /// <summary>
        /// Asociācijas avota vārds
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// Asociācijas avota vārds
        /// </summary>
        public string Definition { get; set; }
        
        /// <summary>
        /// Rinda, kurā metode tiek definēts failā
        /// </summary>
        public uint Line { get; set; }
    }

    /// <summary>
    /// Klase, kas glabā informāciju par asociāciju galapunktiem
    /// </summary>
    public class URL 
    {
        /// <summary>
        /// Asociācijas avota vārds
        /// </summary>
        public string Protocol { get; set; }

        /// <summary>
        /// Asociācijas avota vārds
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Asociācijas avota vārds
        /// </summary>
        public string MethodPath { get; set; }

        /// <summary>
        /// Rinda, kurā metode tiek definēts failā
        /// </summary>
        public uint Line { get; set; }
    }

    /// <summary>
    /// Klase, kas glabā informāciju par asociāciju galapunktiem
    /// </summary>
    public class AssociationEnd 
    {
        /// <summary>
        /// Asociācijas galapunkta lomas vārds
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Asociācijas galapunkta klase
        /// </summary>
        public Class Class { get; set; }

        /// <summary>
        /// Asociācijas ID
        /// </summary>
        public uint ID { get; set; }

        /// <summary>
        /// Vai asociācijas galapunkts ir avots
        /// </summary>
        public bool IsSource { get; set; }

        /// <summary>
        /// Rinda, kurā metode tiek definēts failā
        /// </summary>
        public uint Line { get; set; }
    }

    /// <summary>
    /// Klase, kas glabā informāciju par asociācijām
    /// </summary>
    public class Association 
    {
        /// <summary>
        /// Asociācijas avota vārds
        /// </summary>
        public AssociationEnd Source { get; set; }

        /// <summary>
        /// Asociācijas avota klase
        /// </summary>
        public AssociationEnd Target { get; set; }

        /// <summary>
        /// Vai asociācija ir kompozīcija
        /// </summary>
        public bool IsComposition { get; set; }

        /// <summary>
        /// Rinda, kurā metode tiek definēts failā
        /// </summary>
        public uint Line { get; set; }
    }

    /// <summary>
    /// Klase, kas glabā informāciju par laukiem
    /// </summary>
    public class Field 
    {
        protected string _type;
        /// <summary>
        /// Metodes atgriežamais tips
        /// </summary>
        public string Type 
        {
            get 
            {
                switch (_type)
                {
                    case "Integer":
                        return "long";
                    case "String":
                        return "string";
                    case "Boolean":
                        return "bool";
                    case "Real":
                        return "double";
                    case "Void":
                        return "void";
                    default:
                        return null;
                }
            }
            set { _type = value; }
        }

        /// <summary>
        /// Metodes tips RAAPI
        /// </summary>
        public string primitiveType { get { return _type; } }
        
        /// <summary>
        /// Metodes vārds
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Metodes aizsardzības līmenis
        /// </summary>
        public string Protection { get; set; }

        /// <summary>
        /// Rinda, kurā metode tiek definēts failā
        /// </summary>
        public uint Line { get; set; }

        /// <summary>
        /// Vai ir jāģenerē lauks
        /// </summary>
        public bool generate { get; set; }
    }

    /// <summary>
    /// Klase, kas glabā informāciju par atribūtiem
    /// </summary>
    public class Attribute : Field
    {
        /// <summary>
        /// Mainīgā atgriežamā vērtība
        /// </summary>
        public string GetValue
        {
            get
            {
                switch (_type)
                {
                    case "Integer":
                        return "return Convert.ToInt64( _object[\"" + Name + "\"] )";
                    case "String":
                        return "return _object[\"" + Name + "\"]";
                    case "Boolean":
                        return "return Convert.ToBoolean( _object[\"" + Name + "\"] )";
                    case "Real":
                        return "return Convert.ToDouble( _object[\"" + Name + "\"] )";
                }
                return null;
            }
        }
    }

    /// <summary>
    /// Klase, kas glabā informāciju par metodēm.
    /// </summary>
    public class Method : Field
    {
        public Method()
        {
            Arguments = new();
            Annotations = new();
        }

        /// <summary>
        /// Saraksts ar metodes argumentiem
        /// </summary>
        public List<Attribute> Arguments;

        /// <summary>
        /// Saraksts ar anotācijām
        /// </summary>
        public List<Annotation> Annotations;

        /// <summary>
        /// Metodes URL
        /// </summary>
        public URL URL { get; set; }

        /// <summary>
        /// Metodes atgriežamā vērtība
        /// </summary>
        public string ReturnValue 
        {
            get 
            {
                switch (_type)
                {
                    case "Integer":
                        return "return r.GetInt64();";
                    case "String":
                        return "return r.GetString();";
                    case "Boolean":
                        return "return r.GetBoolean();";
                    case "Real":
                        return "return r.GetDouble();";
                }
                return null;
            }
        }
    }

    /// <summary>
    /// Klase, kas glabā informāciju par klasēm
    /// </summary>
    public class Class
    {
        public Class()
        {
            Attributes = new();
            Methods = new();
            AssociationEnds = new();
            SubClasses = new();
        }

        /// <summary>
        /// Saraksts ar metodes argumentiem
        /// </summary>
        public List<AssociationEnd> AssociationEnds;

        /// <summary>
        /// Saraksts ar mainīgajiem
        /// </summary>
        public List<Attribute> Attributes;

        /// <summary>
        /// Saraksts ar metodēm
        /// </summary>
        public List<Method> Methods;

        /// <summary>
        /// Saraksts ar virsklasēm
        /// </summary>
        public List<Class> SubClasses;

        /// <summary>
        /// Saraksts ar virsklasēm
        /// </summary>
        public Class SuperClass { get; set; }

        /// <summary>
        /// Klases vārds
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// Rinda, kurā klase tiek definēts failā
        /// </summary>
        public uint Line { get; set; }
    }
}