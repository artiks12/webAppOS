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

        /// <summary>
        /// Ģeneratora testēšanas funkcija
        /// </summary>
        public static void Test(string _namespace, string outFile)
        {
            // Ģenerē klases "BaseObject" kodu
            using (StreamWriter sw = new StreamWriter(outFile + "BaseObject.cs"))
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
                string filename = outFile + _class.ClassName + ".cs";
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

            generateConstructorBase(sw, _class, ref IsMade); // Konstruktora ģenerēšana

            generateAttributes(sw, _class, ref IsMade); // Īpašību ģenerēšana
            generateAssociations(sw, _class, ref IsMade); // Asociāciju ģenerēsana
            generateMethods(sw, _class, ref IsMade); // Metožu ģenerēšana

            sw.WriteLine("    }");
        }

        /// <summary>
        /// Metode, kas ģenerē konstruktorus ģenerējamajām klasēm
        /// </summary>
        public static void generateConstructorBase(StreamWriter sw, Class _class, ref bool IsMade)
        {
            sw.WriteLine("        public " + _class.ClassName + " ( IWebMemory wm , IRemoteWebCalls wc ) : base( wm , wc )");
            sw.WriteLine("        {");
            sw.WriteLine("            _constructor_" + _class.ClassName + "();");
            sw.WriteLine("            _object = _wm.FindClassByName( \"" + _class.ClassName + "\" ).CreateObject();");
            sw.WriteLine("        }");

            sw.WriteLine("\n        public " + _class.ClassName + " ( IWebMemory wm , IRemoteWebCalls wc , long rObject ) : base( wm , wc , rObject)");
            sw.WriteLine("        {");
            sw.WriteLine("            _constructor_" + _class.ClassName + "();");
            sw.WriteLine("            _object = new( rObject, wm );");
            sw.WriteLine("        }");
        }

        /// <summary>
        /// Metode, kas ģenerē īpašības, ja tādas ir un ja tādas ir jāģenerē
        /// </summary>
        public static void generateAttributes(StreamWriter sw, Class _class, ref bool IsMade)
        {
            // Pārbauda, vai klasē ir atribūti
            if (_class.Attributes.Count != 0)
            {
                if (IsMade == true) { sw.WriteLine(""); }
                else { IsMade = true; }

                foreach (var a in _class.Attributes)
                {
                    if (a.generate == true) 
                    {
                        // Ģenerē īpašības "galvu"
                        sw.Write("\n        " + a.Protection + " " + a.Type + " " + a.Name + " \n");

                        // Ģenerē īpašības "ķermeni"
                        sw.Write("        {\n");
                        sw.WriteLine("            get { " + a.GetValue + "; }");
                        sw.WriteLine("            set { _object[\"" + a.Name + "\"] = Convert.ToString( value ); }");
                        sw.Write("        }\n");
                    }
                }
            }
        }

        /// <summary>
        /// Metode, kas ģenerē asociācijas, ja tādas ir
        /// </summary>
        public static void generateAssociations(StreamWriter sw, Class _class, ref bool IsMade)
        {
            // Pārbauda, vai klasē ir asociācijas
            if (_class.AssociationEnds.Count != 0)
            {
                if (IsMade == true) { sw.WriteLine(""); }
                else { IsMade = true; }

                foreach (var a in _class.AssociationEnds)
                {
                    // Ģenerē asociācijas "galvu"
                    sw.WriteLine("\n        public List<" + a.Class.ClassName + "> " + a.RoleName);

                    // Ģenerē metodes "ķermeni"
                    sw.WriteLine("        {");
                    generateAssociationFunctions(sw, compiler.Associations[(int)a.ID], a); // funkciju "get" un "set" ģenerēšana
                    sw.WriteLine("        }\n");
                }
            }
        }

        /// <summary>
        /// Metode, kas ģenerē asociācijām "get" un "set" funkcijas
        /// </summary>
        public static void generateAssociationFunctions(StreamWriter sw, Association _association, AssociationEnd a)
        {
            string sourceClass;
            string targetClass = a.Class.ClassName;
            string targetName = a.RoleName;

            if (a.IsSource == true) { sourceClass = _association.Target.Class.ClassName; }
            else { sourceClass = _association.Source.Class.ClassName; }

            sw.WriteLine("            get");
            sw.WriteLine("            {");
            sw.WriteLine("                var c = _wm.FindClassByName( \"" + sourceClass + "\" );");
            sw.WriteLine("                var a = c.FindTargetAssociationEndByName( \"" + targetName + "\" );");
            sw.WriteLine("                var list = _object.LinkedObjects(a);");
            sw.WriteLine("                List<" + targetClass + "> result = new();");
            sw.WriteLine("                foreach (var l in list)");
            sw.WriteLine("                {");
            sw.WriteLine("                    result.Add( new " + targetClass + "( _wm , _wc , l.GetReference ));");
            sw.WriteLine("                }");
            sw.WriteLine("                return result;");
            sw.WriteLine("            }");

            sw.WriteLine("            set");
            sw.WriteLine("            {");
            sw.WriteLine("                var c = _wm.FindClassByName( \"" + sourceClass + "\" );");
            sw.WriteLine("                var a = c.FindTargetAssociationEndByName( \"" + targetName + "\" );");
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
        /// Metode, kas ģenerē metodes, ja tādas ir un ja tādas ir jāģenerē
        /// </summary>
        public static void generateMethods(StreamWriter sw, Class _class, ref bool IsMade)
        {
            // Pārbauda, vai klasē ir metodes
            if (_class.Methods.Count != 0)
            {
                if (IsMade == true) { sw.WriteLine(""); }
                else { IsMade = true; }

                foreach (var m in _class.Methods)
                {
                    if (m.generate == true) 
                    {
                        // Ģenerē metodes "galvu"
                        sw.Write("\n        " + m.Protection + " " + m.Type + " " + m.Name + " ");

                        generateArguments(sw, m); // Argumentu ģenerēšana

                        // Ģenerē metodes "ķermeni"
                        sw.WriteLine("        {");
                        sw.WriteLine("            string arguments = JsonSerializer.Serialize( new { " + argumentList(m.Arguments) + " } );");
                        sw.WriteLine("            string result = _wc.WebCall( _wm.GetTDAKernel() , _object.GetReference , \"" + m.Name + "\" , arguments );");
                        sw.WriteLine("            var json = JsonDocument.Parse(result);");
                        sw.WriteLine("            JsonElement errorMessage;");
                        sw.WriteLine("            if (json.RootElement.TryGetAttribute(\"error\", out errorMessage) == true)");
                        sw.WriteLine("            {");
                        sw.WriteLine("                throw new Exception(errorMessage.GetString());");
                        sw.WriteLine("            }");

                        // Neģenerējam, ja metodes tips ir void.
                        if (m.Type != "void")
                        {
                            sw.WriteLine("            else");
                            sw.WriteLine("            {");
                            sw.WriteLine("                var r = json.RootElement.GetAttribute(\"result\");");
                            sw.WriteLine("                " + m.ReturnValue);
                            sw.WriteLine("            }");
                        }

                        sw.WriteLine("        }");
                    }
                }
            }
        }

        /// <summary>
        /// Metode, kas ģenerē argumentus
        /// </summary>
        public static void generateArguments(StreamWriter sw, Method m)
        {
            sw.Write("(");
            int count = 1;
            foreach (var arg in m.Arguments)
            {
                if (count == 1) { sw.Write(" " + arg.Type + " " + arg.Name + ""); }
                else { sw.Write(" , " + arg.Type + " " + arg.Name + ""); }
                count++;
            }
            sw.Write(" )\n");
        }

        /// <summary>
        /// Metode, kas iegūst sarakstu ar argumentu vārdiem
        /// </summary>
        public static string argumentList(List<Attribute> _arguments)
        {
            string result = "";
            for (int x = 0; x < _arguments.Count; x++)
            {
                if (x + 1 == _arguments.Count) { result += _arguments[x].Name; }
                else { result += _arguments[x].Name + " , "; }
            }
            return result;
        }
    }
}