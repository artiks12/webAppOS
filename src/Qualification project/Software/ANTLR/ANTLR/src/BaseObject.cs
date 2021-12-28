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
        /// <summary>
        /// Metode, kas ģenerē klasi "BaseObject"
        /// </summary>
        public static void generateBaseObject(StreamWriter sw)
        {
            // Ģenerē "BaseObject" "galvu"
            sw.WriteLine("    public class BaseObject");
            sw.WriteLine("    {");

            // Ģenerē "BaseObject" "ķermeni"

            // Mainīgo generēšana
            sw.WriteLine("        protected static IWebMemory _wm;");
            sw.WriteLine("        protected static IRemoteWebCalls _wc;");
            sw.WriteLine("        public WebObject _object;\n");

            generateBaseConstructor(sw, ref IsMade); // Konstruktora ģenerēšana

            generateCheckClass(sw); // Funkcijas "checkObject" ģenerēšana
            generateCheckAttribute(sw); // Funkcijas "checkClass" ģenerēšana
            generateCheckAssociationEnd(sw); // Funkcijas "CheckAssociationEnd" ģenerēšana

            // Klases konstruktora funkciju ģenerēšana
            foreach (var c in compiler.Classes) 
            {
                generateConstructors(sw,c);
            }
            sw.WriteLine("    }");
        }

        /// <summary>
        /// Metode, kas ģenerē konstruktoru bāzes klasei
        /// </summary>
        public static void generateBaseConstructor(StreamWriter sw, ref bool IsMade) 
        {
            if (IsMade == true) { sw.WriteLine(""); }
            else { IsMade = true; }

            sw.WriteLine("        protected BaseObject ( IWebMemory wm , IRemoteWebCalls wc )");
            sw.WriteLine("        {");
            sw.WriteLine("            _wm = wm;");
            sw.WriteLine("            _wc = wc;");
            sw.WriteLine("        }\n");

            sw.WriteLine("        protected BaseObject ( IWebMemory wm , IRemoteWebCalls wc , long rObject )");
            sw.WriteLine("        {");
            sw.WriteLine("            _wm = wm;");
            sw.WriteLine("            _wc = wc;");
            sw.WriteLine("        }\n");

            sw.WriteLine("        protected BaseObject ( IWebMemory wm )");
            sw.WriteLine("        {");
            sw.WriteLine("            _wm = wm;");
            sw.WriteLine("        }\n");
        }

        /// <summary>
        /// Metode, kas ģenerē metodi "checkClass"
        /// </summary>
        public static void generateCheckClass(StreamWriter sw) 
        {
            sw.WriteLine("        protected bool checkClass( List<string> attributes , string className )");
            sw.WriteLine("        {");
            sw.WriteLine("            var c = _wm.FindClassByName( className );");
            sw.WriteLine("            if (c == null)");
            sw.WriteLine("            {");
            sw.WriteLine("                c = _wm.CreateClass( className );");
            sw.WriteLine("            }");
            sw.WriteLine("            else { return true; }");
            sw.WriteLine("            for(int x=0; x<attributes.Count; x+=2)");
            sw.WriteLine("            {");
            sw.WriteLine("                checkAttribute( attributes[x] , attributes[x+1] , c );");
            sw.WriteLine("            }");
            sw.WriteLine("            return false;");
            sw.WriteLine("        }");
        }

        /// <summary>
        /// Metode, kas ģenerē metodi "checkAttribute"
        /// </summary>
        public static void generateCheckAttribute(StreamWriter sw) 
        {
            sw.WriteLine("");
            sw.WriteLine("        protected void checkAttribute( string name , string type , WebClass c )");
            sw.WriteLine("        {");
            sw.WriteLine("            var a = c.FindAttributeByName( name );");
            sw.WriteLine("            if (a == null)");
            sw.WriteLine("            {");
            sw.WriteLine("                c.CreateAttribute( name , type );");
            sw.WriteLine("            }");
            sw.WriteLine("        }");
        }


        /// <summary>
        /// Metode, kas ģenerē metodi "checkAssociationEnd"
        /// </summary>
        public static void generateCheckAssociationEnd(StreamWriter sw)
        {
            sw.WriteLine("");
            sw.WriteLine("        protected void checkAssociationEnd( string sourceName , string targetName , string sourceClass , string targetClass , string Composition )");
            sw.WriteLine("        {");
            sw.WriteLine("            var cSource = _wm.FindClassByName( sourceClass );");
            sw.WriteLine("            var cTarget = _wm.FindClassByName( targetClass );");
            sw.WriteLine("            var a = cSource.FindTargetAssociationEndByName( targetName );");
            sw.WriteLine("            if (a == null)");
            sw.WriteLine("            {");
            sw.WriteLine("                bool isComposition;");
            sw.WriteLine("                if (Composition == \"true\") { isComposition = true; }");
            sw.WriteLine("                else { isComposition = false; }");
            sw.WriteLine("                cSource.CreateAssociation( cTarget, sourceName, targetName, isComposition);");
            sw.WriteLine("            }");
            sw.WriteLine("        }");
        }

        /// <summary>
        /// Metode, kas ģenerē konstruktora funkcijas ģenerējamajām klasēm
        /// </summary>
        public static void generateConstructors(StreamWriter sw, Class _class)
        {
            // Iegūstam attribūtu un asociāciju sarakstus
            string attributes = attributeList(_class);
            string associations = associationList(_class);

            // Iegūstam sarakstu ar klasēm, ar kurām ir asociācija
            List<Class> associationClasses = new();
            foreach (var ac in _class.AssociationEnds)
            {
                if (!associationClasses.Contains(ac.Class) && ac.Class.ClassName != _class.ClassName)
                {
                    var sc = _class.SuperClass;
                    bool found = false;
                    while (sc != null) 
                    {
                        if (ac.Class.ClassName == sc.ClassName) 
                        { 
                            found = true; 
                            break; 
                        }
                        sc = sc.SuperClass;
                    }
                    if (found == false) { associationClasses.Add(ac.Class); }
                }
            }

            /// Ģenerējam konstruktora "galvu"
            sw.WriteLine("\n        protected void _constructor_" + _class.ClassName + "()");
            sw.WriteLine("        {");

            // Ģenerējam konstruktora ķermeni
            sw.WriteLine("            List<string> attributes = new() { " + attributes + " };");
            sw.WriteLine("            var o = checkClass( attributes , \"" + _class.ClassName + "\" );");

            // Tālākais kods netiek ģenerēts, ja klasei nav virsklases vai asociācijas klašu
            if (_class.SuperClass != null || associations != "")
            {
                sw.WriteLine("            if(o == false)");
                sw.WriteLine("            {");
            }

            // Virsklases pārbaude
            if (_class.SuperClass != null)
            {
                sw.WriteLine("               // SuperClass Check");
                sw.WriteLine("               _constructor_" + _class.SuperClass.ClassName + "();");
                sw.WriteLine("               var c = _wm.FindClassByName( \"" + _class.ClassName + "\");");
                sw.WriteLine("               c.CreateGeneralization( \"" + _class.SuperClass.ClassName + "\");");
                if (associations != "") { sw.WriteLine(""); }
            }

            // Asociācijas klašu pārbaude
            if (associations != "")
            {
                sw.WriteLine("               // Association classes Check");
                sw.WriteLine("               List<string> associations = new() { " + associations + " };");
                foreach (var ac in associationClasses)
                {
                    sw.WriteLine("               _constructor_" + ac.ClassName + "();");
                }
                sw.WriteLine("               for(int x=0; x<associations.Count; x+=4)");
                sw.WriteLine("               {");
                sw.WriteLine("                   checkAssociationEnd( associations[x] , associations[x+1] , \"" + _class.ClassName + "\" , associations[x+2] ,  associations[x+3] );");
                sw.WriteLine("               }");
            }
            if (_class.SuperClass != null || associations != "")
            {
                sw.WriteLine("            }");
            }
            sw.WriteLine("        }");
        }

        /// <summary>
        /// Iegūst attribūtu saraksta elementus
        /// </summary>
        public static string attributeList(Class _class)
        {
            string result = "";
            foreach (var v in _class.Attributes)
            {
                if (result == "") { result += "\"" + v.Name + "\" , \"" + v.primitiveType + "\""; }
                else { result += " , \"" + v.Name + "\" , \"" + v.primitiveType + "\""; }
            }
            return result;
        }

        /// <summary>
        /// Iegūst asociāciju saraksta elementus
        /// </summary>
        public static string associationList(Class _class)
        {
            string result = "";
            foreach (var v in _class.AssociationEnds)
            {
                var association = compiler.Associations[(int)v.ID];

                string targetName = v.RoleName;
                string targetClass = v.Class.ClassName;
                string sourceName;
                string IsComposition;

                // Noskaidrojam, ko liekam kā avota lomas vārdu
                if (v.IsSource == true) {  sourceName = association.Target.RoleName; }
                else { sourceName = association.Source.RoleName; }

                // Pārveidojam kompozīcijas patiesumvērtību kā simbolu virkni
                if (association.IsComposition == true) { IsComposition = "true"; }
                else { IsComposition = "false"; }

                if (result == "") { result += "\"" + sourceName + "\" , \"" + targetName + "\" , \"" + targetClass + "\" , \"" + IsComposition + "\""; }
                else { result += " , \"" + sourceName + "\" , \"" + targetName + "\" , \"" + targetClass + "\" , \"" + IsComposition + "\""; }
            }
            return result;
        }
    }
}