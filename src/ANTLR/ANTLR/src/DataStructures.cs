using System;
using System.Collections.Generic;
using ANTLR.Grammar;
using static ANTLR.Grammar.LanguageParser;
using System.Linq;

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
    }

    /// <summary>
    /// Klase, kas glabā informāciju par asociācijām
    /// </summary>
    public class Association 
    {
        /// <summary>
        /// Asociācijas avota vārds
        /// </summary>
        public string SourceName { get; set; }

        /// <summary>
        /// Asociācijas avota klase
        /// </summary>
        public Class SourceClass { get; set; }

        /// <summary>
        /// Asociācijas mērķa vārds
        /// </summary>
        public string TargetName { get; set; }

        /// <summary>
        /// Asociācijas mērķa klase
        /// </summary>
        public Class TargetClass { get; set; }

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
                        return _type;
                }
            }
            set { _type = value; }
        }

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
    }

    /// <summary>
    /// Klase, kas glabā informāciju par mainīgajiem
    /// </summary>
    public class Variable : Field
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
                        return "return Convert.ToInt32( _object[\"" + Name + "\"] )";
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
        /// <summary>
        /// Saraksts ar metodes argumentiem
        /// </summary>
        public List<Variable> _arguments;

        /// <summary>
        /// Saraksts ar anotācijām
        /// </summary>
        public List<Annotation> _annotations;

        /// <summary>
        /// Metodes URL
        /// </summary>
        public URL URL { get; set; }

        public Method()
        {
            _arguments = new();
        }

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
        /// <summary>
        /// Saraksts ar metodes argumentiem
        /// </summary>
        public List<AssociationEnd> _associationEnds;

        /// <summary>
        /// Saraksts ar mainīgajiem
        /// </summary>
        public List<Variable> _variables;

        /// <summary>
        /// Saraksts ar metodēm
        /// </summary>
        public List<Method> _methods;

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

        /// <summary>
        /// Vai šī klase ir virsklase
        /// </summary>
        public bool isSuperClass { get; set; }
        
        public Class()
        {
            _variables = new();
            _methods = new();
            _associationEnds = new();
        }
    }
}