using System;
using System.Collections.Generic;
using static ANTLR.LanguageParser;
using System.Linq;

namespace AntlrCSharp
{
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
    }
    public class URL 
    {
        /// <summary>
        /// Asociācijas avota vārds
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Asociācijas avota vārds
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Asociācijas avota vārds
        /// </summary>
        public string MethodPath { get; set; }
    }
    public class Association 
    {
        /// <summary>
        /// Asociācijas avota vārds
        /// </summary>
        public string SourceName { get; set; }

        /// <summary>
        /// Asociācijas avota klase
        /// </summary>
        public string SourceClass { get; set; }

        /// <summary>
        /// Asociācijas mērķa vārds
        /// </summary>
        public string TargetName { get; set; }

        /// <summary>
        /// Asociācijas mērķa klase
        /// </summary>
        public string TargetClass { get; set; }

        /// <summary>
        /// Rinda, kurā metode tiek definēts failā
        /// </summary>
        public uint Line { get; set; }
    }
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
                        return "int";
                    case "String":
                        return "string";
                    case "Boolean":
                        return "bool";
                    case "Real":
                        return "double";
                }
                return null;
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
        public URL _url { get; set; }

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
                        return "0";
                    case "String":
                        return "null";
                    case "Boolean":
                        return "false";
                    case "Real":
                        return "0";
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
        /// Saraksts ar mainīgajiem
        /// </summary>
        public List<Association> _associations;

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
        public List<Class> _superClasses { get; set; }

        /// <summary>
        /// Klases vārds
        /// </summary>
        public string className { get; set; }

        public uint Line { get; set; }

        public Class()
        {
            _variables = new();
            _methods = new();
            _superClasses = new();
            _associations = new();
        }
    }
}
