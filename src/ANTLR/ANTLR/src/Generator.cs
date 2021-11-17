using System;
using ANTLR;
using Antlr4.Runtime;
using System.IO;
using ANTLR.Grammar;
using static ANTLR.Grammar.LanguageParser;
using Antlr4.Runtime.Tree;
using System.Collections.Generic;

namespace AntlrCSharp
{
    partial class Program
    {
        static bool IsMade = false; // Vai klasē kaut kas ir ģenerēts. Nepieciesams ģenerēšanas stilam.

        /// <summary>
        /// Metode, kas ģenerē klasi "BaseObject"
        /// </summary>
        public static void generateBaseObject(StreamWriter sw)
        {
            // Ģenerē "BaseObject" "galvu"
            sw.WriteLine("    public class BaseObject");
            sw.WriteLine("    {");

            // Mainīgo generēšana
            sw.WriteLine("        protected static IWebMemory _wm;");
            sw.WriteLine("        protected static IWebCalls _wc;");
            sw.WriteLine("        protected WebObject _object;\n");

            generateConstructor(sw, "BaseObject", ref IsMade);

            // Ģenerē "BaseObject" "ķermeni"
            generateCheckObject(sw); // Funkcijas "CheckObject" ģenerēšana
            generateCheckAssociation(sw); // Funkcijas "CheckAssociation" ģenerēšana
            sw.WriteLine("    }");
        }

        /// <summary>
        /// Metode, kas ģenerē konstruktorus
        /// </summary>
        public static void generateConstructor(StreamWriter sw, string className, ref bool IsMade) 
        {
            if (IsMade == true) { sw.WriteLine(""); }
            else { IsMade = true; }

            sw.WriteLine("        public "+ className +" ( IWebMemory wm , IWebCalls wc )");
            sw.WriteLine("        {");
            sw.WriteLine("            _wm = wm;");
            sw.WriteLine("            _wc = wc;");
            sw.WriteLine("            _object = null;");
            sw.WriteLine("        }\n");

            sw.WriteLine("        public " + className + " ( IWebMemory wm , IWebCalls wc , long rObject )");
            sw.WriteLine("        {");
            sw.WriteLine("            _wc = wc;");
            sw.WriteLine("            _object = new( rObject, wm );");
            sw.WriteLine("        }\n");
        }

        /// <summary>
        /// Metode, kas ģenerē metodi "CheckObject"
        /// </summary>
        public static void generateCheckObject(StreamWriter sw) 
        {
            sw.WriteLine("        protected void checkObject( string attributeName, string dataType, string className )");
            sw.WriteLine("        {");
            sw.WriteLine("            var c = _wm.FindClassByName( className );");
            sw.WriteLine("            if (c == null)");
            sw.WriteLine("            {");
            sw.WriteLine("                c = _wm.CreateClass( className );");
            sw.WriteLine("            }");
            sw.WriteLine("            var a = c.FindAttribute( attributeName );");
            sw.WriteLine("            if (a == null)");
            sw.WriteLine("            {");
            sw.WriteLine("                a = c.CreateAttribute( attributeName , dataType );");
            sw.WriteLine("            }");
            sw.WriteLine("            if (_object == null)");
            sw.WriteLine("            {");
            sw.WriteLine("                _object = c.CreateObject();");
            sw.WriteLine("            }");
            sw.WriteLine("        }");
        }

        /// <summary>
        /// Metode, kas ģenerē metodi "CheckAssociation"
        /// </summary>
        public static void generateCheckAssociation(StreamWriter sw)
        {
            sw.WriteLine("");
            sw.WriteLine("        protected WebAssociationEnd checkAssociation( string associationNameSource, string associationNameTarget, string sourceClass, string targetClass, bool IsComposition )");
            sw.WriteLine("        {");
            sw.WriteLine("            var cSource = _wm.FindClassByName( sourceClass );");
            sw.WriteLine("            if (cSource == null)");
            sw.WriteLine("            {");
            sw.WriteLine("                cSource = _wm.CreateClass( sourceClass );");
            sw.WriteLine("            }");
            sw.WriteLine("            var cTarget = _wm.FindClassByName( targetClass );");
            sw.WriteLine("            if (cTarget == null)");
            sw.WriteLine("            {");
            sw.WriteLine("                cTarget = _wm.CreateClass( targetClass );");
            sw.WriteLine("            }");
            sw.WriteLine("            var a = cSource.FindAssociationEnd( associationNameTarget );");
            sw.WriteLine("            if (a == null)");
            sw.WriteLine("            {");
            sw.WriteLine("                a = cSource.CreateAssociationEnd( cSource , cTarget , associationNameSource , associationNameTarget , IsComposition );");
            sw.WriteLine("            }");
            sw.WriteLine("            return a;");
            sw.WriteLine("        }");
        }

        /// <summary>
        /// Metode, kas ģenerē argumentus
        /// </summary>
        public static void generateArguments(StreamWriter sw, Method m) 
        {
            sw.Write("(");
            int count = 1;
            foreach (var arg in m._arguments)
            {
                if (count == 1) { sw.Write(" " + arg.Type + " " + arg.Name + ""); }
                else { sw.Write(" , " + arg.Type + " " + arg.Name + ""); }
                count++;
            }
            sw.Write(" )\n");
        }

        /// <summary>
        /// Metode, kas ģenerē īpašībām "get" funkciju
        /// </summary>
        public static void generatePropertyGet(StreamWriter sw, Variable _variable, Class _class) 
        {
            string conversion = "";

            switch (_variable.Type) 
            {
                case "int":
                    conversion = "Convert.ToInt32( _object[\"" + _variable.Name + "\"] )";
                    break;
                case "string":
                    conversion = "_object[\"" + _variable.Name + "\"]";
                    break;
                case "bool":
                    conversion = "Convert.ToBoolean( _object[\"" + _variable.Name + "\"] )";
                    break;
                case "double":
                    conversion = "Convert.ToDouble( _object[\"" + _variable.Name + "\"] )";
                    break;
            }

            sw.WriteLine("            get");
            sw.WriteLine("            {");
            sw.WriteLine("                checkObject( " + "\"" + _variable.Name + "\" , \"" + _variable.primitiveType + "\" , \"" + _class.ClassName + "\" );");
            sw.WriteLine("                return " + conversion + ";");
            sw.WriteLine("            }");
        }

        /// <summary>
        /// Metode, kas ģenerē īpašībām "set" funkciju
        /// </summary>
        public static void generatePropertySet(StreamWriter sw, Variable _variable, Class _class)
        {
            sw.WriteLine("            set");
            sw.WriteLine("            {");
            sw.WriteLine("                checkObject( " + "\"" + _variable.Name + "\" , \"" + _variable.primitiveType + "\" , \"" + _class.ClassName + "\" );");
            sw.WriteLine("                _object[\"" + _variable.Name + "\"] = Convert.ToString( value );");
            sw.WriteLine("            }");
        }

        /// <summary>
        /// Metode, kas ģenerē īpašības
        /// </summary>
        public static void generateProperties(StreamWriter sw, Class _class, ref bool IsMade) 
        {
            if (_class._variables.Count != 0)
            {
                foreach (var f in _class._variables)
                {
                    if (IsMade == true) { sw.WriteLine(""); }
                    else { IsMade = true; }

                    // Ģenerē īpašības "galvu"
                    if (f.Protection != null) { sw.Write("\n        " + f.Protection); }
                    else { sw.Write("        public"); }

                    sw.Write(" " + f.Type + " " + f.Name + " \n");

                    // Ģenerē īpašības "ķermeni"
                    sw.WriteLine("        {");
                    generatePropertyGet(sw,f,_class); // funkcijas "get" ģenerēšana
                    generatePropertySet(sw,f,_class); // funkcijas "set" ģenerēšana
                    sw.Write("        }\n");
                }
            }
        }

        /// <summary>
        /// Metode, kas ģenerē asociācijām "get" funkciju
        /// </summary>
        public static void generateAssociationGet(StreamWriter sw, Association _association, AssociationEnd a)
        {
            string sourceName;
            string targetName;
            string sourceClass;
            string targetClass;
            string IsComposition;

            if (a.IsSource == true)
            {
                sourceName = _association.SourceName;
                targetName = _association.TargetName;
                sourceClass = _association.SourceClass;
                targetClass = _association.TargetClass;
            }
            else
            {
                sourceName = _association.TargetName;
                targetName = _association.SourceName;
                sourceClass = _association.TargetClass;
                targetClass = _association.SourceClass;
            }

            if (_association.IsComposition == true) { IsComposition = "true"; }
            else { IsComposition = "false"; }

            sw.WriteLine("            get");
            sw.WriteLine("            {");
            sw.WriteLine("                var a = checkAssociation( " + "\"" + sourceName + "\" , \"" + targetName + "\" , \"" + sourceClass + "\" , \"" + targetClass + "\" , " + IsComposition + ");");
            sw.WriteLine("                var list = _object.LinkedObjects(a);");
            sw.WriteLine("                List<" + targetClass + "> result = new();");
            sw.WriteLine("                foreach (var l in list)");
            sw.WriteLine("                {");
            sw.WriteLine("                    result.Add( new " + targetClass + "( _wm , _wc , l.GetReference() ));");
            sw.WriteLine("                }");
            sw.WriteLine("                return result;");
            sw.WriteLine("            }");
        }

        /// <summary>
        /// Metode, kas ģenerē asociācijām "set" funkciju
        /// </summary>
        public static void generateAssociationSet(StreamWriter sw, Association _association, AssociationEnd a)
        {
            string sourceName;
            string targetName;
            string sourceClass;
            string targetClass;
            string IsComposition;

            if (a.IsSource == true) 
            {
                sourceName = _association.SourceName;
                targetName = _association.TargetName;
                sourceClass = _association.SourceClass;
                targetClass = _association.TargetClass;
            }
            else
            {
                sourceName = _association.TargetName;
                targetName = _association.SourceName;
                sourceClass = _association.TargetClass;
                targetClass = _association.SourceClass;
            }

            if (_association.IsComposition == true) { IsComposition = "true"; }
            else { IsComposition = "false"; }

            sw.WriteLine("            set");
            sw.WriteLine("            {");
            sw.WriteLine("                var a = checkAssociation( " + "\"" + sourceName + "\" , \"" + targetName + "\" , \"" + sourceClass + "\" , \"" + targetClass + "\" , " + IsComposition + ");");
            sw.WriteLine("                var list = value;");
            sw.WriteLine("                List<WebObject> result = new();");
            sw.WriteLine("                foreach (var l in list)");
            sw.WriteLine("                {");
            sw.WriteLine("                    result.Add( l._object );");
            sw.WriteLine("                }");
            sw.WriteLine("            }");
        }

        /// <summary>
        /// Metode, kas ģenerē asociācijas
        /// </summary>
        public static void generateAssociations(StreamWriter sw, Class _class, ref bool IsMade) 
        {
            // Pārbauda, vai klasē ir asociācijas
            if (_class._associationEnds.Count != 0)
            {
                foreach (var a in _class._associationEnds)
                {
                    if (IsMade == true) { sw.WriteLine(""); }
                    else { IsMade = true; }

                    // Ģenerē asociācijas "galvu"
                    sw.WriteLine("        public List<" + a.ClassName + "> " + a.RoleName);

                    // Ģenerē metodes "ķermeni"
                    sw.WriteLine("        {");
                    generateAssociationGet(sw, compiler.Associations[(int)a.ID], a); // funkcijas "get" ģenerēšana
                    generateAssociationSet(sw, compiler.Associations[(int)a.ID], a); // funkcijas "set" ģenerēšana
                    sw.Write("        }\n");
                }
            }
        }

        public static string argumentList(List<Variable> _arguments) 
        {
            string result = "";
            for (int x=0; x<_arguments.Count; x++) 
            {
                if (x + 1 == _arguments.Count) { result += _arguments[x].Name; }
                else { result += _arguments[x].Name + " , "; }
            }
            return result;
        }

        /// <summary>
        /// Metode, kas ģenerē metodes
        /// </summary>
        public static void generateMethods(StreamWriter sw, Class _class, ref bool IsMade)
        {
            // Pārbauda, vai klasē ir metodes
            if (_class._methods.Count != 0)
            {
                foreach (var m in _class._methods)
                {
                    if (IsMade == true) { sw.WriteLine(""); }
                    else { IsMade = true; }

                    // Ģenerē metodes "galvu"
                    if (m.Protection != null) { sw.Write("\n        " + m.Protection); }
                    else { sw.Write("        public"); }

                    sw.Write(" " + m.Type + " " + m.Name + " ");

                    generateArguments(sw,m); // Argumentu ģenerēšana

                    // Ģenerē metodes "ķermeni"
                    sw.WriteLine("        {");
                    sw.WriteLine("            string s = JsonSerializer.Serialize( new { " + argumentList(m._arguments) + " } );");
                    sw.WriteLine("            string result = _wc.WebCall( _wm , _object.GetReference() , \"" + m.Name + "\" , s );");
                    sw.WriteLine("            return " + m.ReturnValue + ";");
                    sw.WriteLine("        }");
                }
            }
        }

        /// <summary>
        /// Metode, kas ģenerē klases
        /// </summary>
        public static void generateClass(StreamWriter sw, Class _class) 
        {
            // Ģenerē klases "galvu"
            sw.Write("    class " + _class.ClassName);

            if (_class.SuperClass != null) { sw.Write(" : " + _class.SuperClass.ClassName); }
            else { sw.Write(" : BaseObject"); }
            sw.WriteLine("");

            // Ģenerē klases "ķermeni"
            sw.WriteLine("    {");

            sw.WriteLine("        public " + _class.ClassName + " ( IWebMemory wm , IWebCalls wc ) : base( wm , wc ) { }\n");
            sw.WriteLine("        public " + _class.ClassName + " ( IWebMemory wm, IWebCalls wc , long rObject ) : base( wm , wc , rObject ) { }");

            generateProperties(sw,_class,ref IsMade); // Īpašību ģenerēšana
            generateAssociations(sw, _class, ref IsMade); // Asociāciju ģenerēsana
            generateMethods(sw, _class,ref IsMade); // Metožu ģenerēšana

            if (IsMade == false) { sw.WriteLine("        "); }
            sw.WriteLine("    }");
        }

        /// <summary>
        /// Ģenerēšanas pamatmetode
        /// </summary>
        public static bool generate(string _namespace)
        {
            Console.WriteLine("");

            // Pārbauda, vai kodā nav kļūdu
            if (compiler.Errors.Count != 0)
            {
                foreach (var error in compiler.Errors)
                {
                    Console.WriteLine(error);
                }
                return false;
            }

            // Pārbauda, vai padotā vārdtelpa ir sintaktiski pareizs
            if (!checkNamespace(_namespace)) 
            {
                Console.WriteLine("'" + _namespace + "' is in incorrect format!");
                return false; 
            }

            // Ģenerē klases "BaseObject" kodu
            using (StreamWriter sw = new StreamWriter("Classes/BaseObject.cs"))
            {
                sw.WriteLine("using WebAppOS;\n");
                sw.WriteLine("namespace " + _namespace);
                sw.WriteLine("{");
                generateBaseObject(sw);
                sw.Write('}');
            }

            // Ģenerē kodu visām klasēm.
            foreach (var _class in compiler.Classes)
            {
                string filename = "Classes/" + _class.ClassName + ".cs";
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    sw.WriteLine("using WebAppOS;");
                    sw.WriteLine("using System.Text.Json;");
                    sw.WriteLine("using System;");
                    sw.WriteLine("using System.Collections.Generic;\n");
                    sw.WriteLine("namespace " + _namespace);
                    sw.WriteLine("{");
                    generateClass(sw, _class);
                    sw.Write('}');
                }
            }

            Console.WriteLine("Compilation successful!");

            return true;
        }
    }
}