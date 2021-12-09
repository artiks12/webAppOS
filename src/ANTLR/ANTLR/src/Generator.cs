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
            sw.WriteLine("        protected static IRemoteWebCalls _wc;");
            sw.WriteLine("        public WebObject _object;\n");

            generateBaseConstructor(sw, ref IsMade);

            // Ģenerē "BaseObject" "ķermeni"
            generateCheckClass(sw); // Funkcijas "CheckObject" ģenerēšana
            generateCheckAssociation(sw); // Funkcijas "CheckAssociation" ģenerēšana
            sw.WriteLine("    }");
        }

        /// <summary>
        /// Metode, kas ģenerē konstruktoru bāzes klasei
        /// </summary>
        public static void generateBaseConstructor(StreamWriter sw, ref bool IsMade) 
        {
            if (IsMade == true) { sw.WriteLine(""); }
            else { IsMade = true; }

            sw.WriteLine("        public BaseObject ( IWebMemory wm , IRemoteWebCalls wc )");
            sw.WriteLine("        {");
            sw.WriteLine("            _wm = wm;");
            sw.WriteLine("            _wc = wc;");
            sw.WriteLine("        }\n");

            sw.WriteLine("        public BaseObject ( IWebMemory wm , IRemoteWebCalls wc , long rObject )");
            sw.WriteLine("        {");
            sw.WriteLine("            _wm = wm;");
            sw.WriteLine("            _wc = wc;");
            sw.WriteLine("        }\n");
        }

        /// <summary>
        /// Metode, kas ģenerē konstruktorus ģenerējamajām klasēm
        /// </summary>
        public static void generateConstructor(StreamWriter sw, Class _class, ref bool IsMade)
        {
            if (IsMade == true) { sw.WriteLine(""); }
            else { IsMade = true; }

            string argumentList = "";
            foreach (var v in _class._attributes) 
            {
                if (argumentList == "") { argumentList += "\"" + v.Name + "\" , \"" + v.primitiveType + "\""; }
                else { argumentList += " , \"" + v.Name + "\" , \"" + v.primitiveType + "\""; }
            }

            string associationList = "";
            foreach (var v in _class._associationEnds)
            {
                var association = compiler.Associations[(int)v.ID];

                string sourceName;
                string targetName;
                string targetClass;
                string IsComposition;

                if (v.IsSource == true)
                {
                    sourceName = association.Target.RoleName;
                    targetName = association.Source.RoleName;
                    targetClass = association.Source.Class.ClassName;
                }
                else
                {
                    sourceName = association.Source.RoleName;
                    targetName = association.Target.RoleName;
                    targetClass = association.Target.Class.ClassName;
                }

                if (association.IsComposition == true) { IsComposition = "true"; }
                else { IsComposition = "false"; }

                if (associationList == "") { associationList += "\"" + sourceName + "\" , \"" + targetName + "\" , \"" + targetClass + "\" , \"" + IsComposition + "\""; }
                else { associationList += " , \"" + sourceName + "\" , \"" + targetName + "\" , \"" + targetClass + "\" , \"" + IsComposition + "\""; }
            }

            sw.WriteLine("        public " + _class.ClassName + " ( IWebMemory wm , IRemoteWebCalls wc ) : base( wm , wc )");
            sw.WriteLine("        {");
            sw.WriteLine("            List<string> attributes = new() { " + argumentList + " };");
            sw.WriteLine("            checkClass( attributes , \"" + _class.ClassName + "\" );");
            sw.WriteLine("            List<string> associations = new() { " + associationList + " , \"" + _class.ClassName + "\" };");
            sw.WriteLine("            checkAssociations( associations , \"" + _class.ClassName + "\" );");
            sw.WriteLine("            _object = _wm.FindClassByName( \"" + _class.ClassName + "\" ).CreateObject();");
            sw.WriteLine("        }\n");

            sw.WriteLine("        public " + _class.ClassName + " ( IWebMemory wm, IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject )");
            sw.WriteLine("        {");
            sw.WriteLine("            List<string> attributes = new() { " + argumentList + " };");
            sw.WriteLine("            checkClass( attributes , \"" + _class.ClassName + "\" );");
            sw.WriteLine("            List<string> associations = new() { " + associationList + " , \"" + _class.ClassName + "\" };");
            sw.WriteLine("            checkAssociations( associations , \"" + _class.ClassName + "\" );");
            sw.WriteLine("            _object = new( rObject, wm );");
            sw.WriteLine("        }\n");
        }

        /// <summary>
        /// Metode, kas ģenerē metodi "CheckObject"
        /// </summary>
        public static void generateCheckClass(StreamWriter sw) 
        {
            sw.WriteLine("        protected void checkClass( List<string> attributes , string className )");
            sw.WriteLine("        {");
            sw.WriteLine("            var c = _wm.FindClassByName( className );");
            sw.WriteLine("            if (c == null)");
            sw.WriteLine("            {");
            sw.WriteLine("                c = _wm.CreateClass( className );");
            sw.WriteLine("            }");
            sw.WriteLine("            for(int x=0; x<attributes.Count; x+=2)");
            sw.WriteLine("            {");
            sw.WriteLine("                var a = c.FindAttribute( attributes[x] );");
            sw.WriteLine("                if (a == null)");
            sw.WriteLine("                {");
            sw.WriteLine("                    c.CreateAttribute( attributes[x] , attributes[x+1] );");
            sw.WriteLine("                }");
            sw.WriteLine("            }");
            sw.WriteLine("        }");
        }

        /// <summary>
        /// Metode, kas ģenerē metodi "CheckAssociation"
        /// </summary>
        public static void generateCheckAssociation(StreamWriter sw)
        {
            sw.WriteLine("");
            sw.WriteLine("        protected void checkAssociations( List<string> associations , string className )");
            sw.WriteLine("        {");
            sw.WriteLine("            for (int x = 0; x < associations.Count; x += 4)");
            sw.WriteLine("            {");
            sw.WriteLine("                var cSource = _wm.FindClassByName( className );");
            sw.WriteLine("                var cTarget = _wm.FindClassByName( associations[x+2] );");
            sw.WriteLine("                if (cTarget == null)");
            sw.WriteLine("                {");
            sw.WriteLine("                    cTarget = _wm.CreateClass( associations[x+2] );");
            sw.WriteLine("                }");
            sw.WriteLine("                var a = cSource.FindAssociationEnd( associations[x+1] );");
            sw.WriteLine("                if (a == null)");
            sw.WriteLine("                {");
            sw.WriteLine("                    bool isComposition;");
            sw.WriteLine("                    if (associations[x+3] == \"true\") { isComposition = true; }");
            sw.WriteLine("                    else { isComposition = false; }");
            sw.WriteLine("                    cSource.CreateAssociation( cTarget, associations[x], associations[x + 1], isComposition);");
            sw.WriteLine("                }");
            sw.WriteLine("            }");
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
        public static void generatePropertyGet(StreamWriter sw, Attribute _variable) 
        {
            sw.WriteLine("            get { " + _variable.GetValue + "; }");
        }

        /// <summary>
        /// Metode, kas ģenerē īpašībām "set" funkciju
        /// </summary>
        public static void generatePropertySet(StreamWriter sw, Attribute _variable)
        {
            sw.WriteLine("            set { _object[\"" + _variable.Name + "\"] = Convert.ToString( value ); }");
        }

        /// <summary>
        /// Metode, kas ģenerē īpašības
        /// </summary>
        public static void generateProperties(StreamWriter sw, Class _class, ref bool IsMade) 
        {
            if (_class._attributes.Count != 0)
            {
                foreach (var a in _class._attributes)
                {
                    if (IsMade == true) { sw.WriteLine(""); }
                    else { IsMade = true; }

                    // Ģenerē īpašības "galvu"
                    sw.Write("\n        " + a.Protection + " " + a.Type + " " + a.Name + " \n");

                    // Ģenerē īpašības "ķermeni"
                    sw.Write("        {\n");
                    generatePropertyGet(sw,a); // funkcijas "get" ģenerēšana
                    generatePropertySet(sw,a); // funkcijas "set" ģenerēšana
                    sw.Write("        }\n");
                }
            }
        }

        /// <summary>
        /// Metode, kas ģenerē asociācijām "get" funkciju
        /// </summary>
        public static void generateAssociationGet(StreamWriter sw, Association _association, AssociationEnd a)
        {
            string sourceClass;
            string targetClass;
            string targetName;

            if (a.IsSource == true)
            {
                sourceClass = _association.Target.Class.ClassName;
                targetClass = _association.Source.Class.ClassName;
                targetName = _association.Source.Class.ClassName;
            }
            else
            {
                sourceClass = _association.Source.Class.ClassName;
                targetClass = _association.Target.Class.ClassName;
                targetName = _association.Target.Class.ClassName;
            }

            sw.WriteLine("            get");
            sw.WriteLine("            {");
            sw.WriteLine("                var c = _wm.FindClassByName( \"" + sourceClass + "\" );");
            sw.WriteLine("                var a = c.FindAssociationEnd( \"" + targetName + "\" );");
            sw.WriteLine("                var list = _object.LinkedObjects(a);");
            sw.WriteLine("                List<" + targetClass + "> result = new();");
            sw.WriteLine("                foreach (var l in list)");
            sw.WriteLine("                {");
            sw.WriteLine("                    result.Add( new " + targetClass + "( _wm , _wc , l.GetReference ));");
            sw.WriteLine("                }");
            sw.WriteLine("                return result;");
            sw.WriteLine("            }");
        }

        /// <summary>
        /// Metode, kas ģenerē asociācijām "set" funkciju
        /// </summary>
        public static void generateAssociationSet(StreamWriter sw, Association _association, AssociationEnd a)
        {
            string sourceClass;
            string targetName;

            if (a.IsSource == true)
            {
                sourceClass = _association.Target.Class.ClassName;
                targetName = _association.Source.Class.ClassName;
            }
            else
            {
                sourceClass = _association.Source.Class.ClassName;
                targetName = _association.Target.Class.ClassName;
            }

            sw.WriteLine("            set");
            sw.WriteLine("            {");
            sw.WriteLine("                var c = _wm.FindClassByName( \"" + sourceClass + "\" );");
            sw.WriteLine("                var a = c.FindAssociationEnd( \"" + targetName + "\" );");
            sw.WriteLine("                var list = value;");
            sw.WriteLine("                List<WebObject> result = new();");
            sw.WriteLine("                foreach (var l in list)");
            sw.WriteLine("                {");
            sw.WriteLine("                    result.Add( l._object );");
            sw.WriteLine("                }");
            sw.WriteLine("                _object.LinkObjects(a,result);");
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
                    sw.WriteLine("        public List<" + a.Class.ClassName + "> " + a.RoleName);

                    // Ģenerē metodes "ķermeni"
                    sw.WriteLine("        {");
                    generateAssociationGet(sw, compiler.Associations[(int)a.ID], a); // funkcijas "get" ģenerēšana
                    generateAssociationSet(sw, compiler.Associations[(int)a.ID], a); // funkcijas "set" ģenerēšana
                    sw.Write("        }\n");
                }
            }
        }

        public static string argumentList(List<Attribute> _arguments) 
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
                    sw.Write("\n        " + m.Protection + " " + m.Type + " " + m.Name + " ");

                    generateArguments(sw,m); // Argumentu ģenerēšana

                    // Ģenerē metodes "ķermeni"
                    sw.WriteLine("        {");
                    sw.WriteLine("            string arguments = JsonSerializer.Serialize( new { " + argumentList(m._arguments) + " } );");
                    sw.WriteLine("            string result = _wc.WebCall( _wm.GetTDAKernel() , _object.GetReference , \"" + m.Name + "\" , arguments );");
                    sw.WriteLine("            var json = JsonDocument.Parse(result);");
                    sw.WriteLine("            JsonElement errorMessage;");
                    sw.WriteLine("            if (json.RootElement.TryGetProperty(\"error\", out errorMessage) == true)");
                    sw.WriteLine("            {");
                    sw.WriteLine("                throw new Exception(errorMessage.GetString());");
                    sw.WriteLine("            }");

                    // Neģenerējam, ja ir void.
                    if (m.Type != "void") 
                    {
                        sw.WriteLine("            else");
                        sw.WriteLine("            {");
                        sw.WriteLine("                var r = json.RootElement.GetProperty(\"result\");");
                        sw.WriteLine("                " + m.ReturnValue);
                        sw.WriteLine("            }");
                    }
                    
                    sw.WriteLine("        }");
                }
            }
        }

        /// <summary>
        /// Metode, kas ģenerē klases
        /// </summary>
        public static void generateClass(StreamWriter sw, Class _class) 
        {
            // Ja klasei ir virsklase, tad tā tiek izmantota kā virsklase, citādāk virsklase ir "BaseObject"
            string superClass = _class.SuperClass != null ? _class.SuperClass.ClassName : "BaseObject"; 

            // Ģenerē klases "galvu"
            sw.Write("    class " + _class.ClassName + " : " + superClass + "\n");

            // Ģenerē klases "ķermeni"
            sw.WriteLine("    {");

            generateConstructor(sw,_class,ref IsMade);

            generateProperties(sw,_class,ref IsMade); // Īpašību ģenerēšana
            generateAssociations(sw, _class, ref IsMade); // Asociāciju ģenerēsana
            generateMethods(sw, _class,ref IsMade); // Metožu ģenerēšana

            sw.WriteLine("    }");
        }

        /// <summary>
        /// Ģenerēšanas pamatmetode
        /// </summary>
        public static void generate(string _namespace)
        {
            // Ģenerē klases "BaseObject" kodu
            using (StreamWriter sw = new StreamWriter("Classes/BaseObject.cs"))
            {
                sw.WriteLine("using WebAppOS;");
                sw.WriteLine("using System.Collections.Generic;\n");
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
        }
    }
}