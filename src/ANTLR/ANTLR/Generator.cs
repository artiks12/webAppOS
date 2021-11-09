using System;
using ANTLR;
using Antlr4.Runtime;
using System.IO;
using static ANTLR.LanguageParser;
using Antlr4.Runtime.Tree;

namespace AntlrCSharp
{
    partial class Program
    {
        public static void generateBaseObject(StreamWriter sw)
        {
            sw.WriteLine("    public class BaseObject");
            sw.WriteLine("    {");

            generateCheckObject(sw);
            generateCheckAssociation(sw);

            sw.WriteLine("    }");
        }

        public static void generateConstructor(StreamWriter sw, string className, ref bool IsMade) 
        {
            if (IsMade == true) { sw.WriteLine(""); }
            else { IsMade = true; }

            sw.WriteLine("        public "+ className +" ( IWebMemory wm )");
            sw.WriteLine("        {");
            sw.WriteLine("            _wm = wm;");
            sw.WriteLine("            _object = null;");
            sw.WriteLine("        }\n");

            sw.WriteLine("        public " + className + " ( IWebMemory wm, long rObject )");
            sw.WriteLine("        {");
            sw.WriteLine("            _wm = wm;");
            sw.WriteLine("            _object = new( rObject, wm );");
            sw.WriteLine("        }");
        }

        public static void generateCheckObject(StreamWriter sw) 
        {
            sw.WriteLine("        public static void checkObject( string attributeName, string dataType, string className )");
            sw.WriteLine("        {");
            sw.WriteLine("            var c = _wm.findClassByName( className );");
            sw.WriteLine("            if (c == null)");
            sw.WriteLine("            {");
            sw.WriteLine("                c = _wm.createClass( className );");
            sw.WriteLine("            }");
            sw.WriteLine("            var a = c.findAttribute( attributeName );");
            sw.WriteLine("            if (a == null)");
            sw.WriteLine("            {");
            sw.WriteLine("                a = _wm.createAttribute( attributeName , dataType );");
            sw.WriteLine("            }");
            sw.WriteLine("            if (_object == null)");
            sw.WriteLine("            {");
            sw.WriteLine("                _object = c.createObject();");
            sw.WriteLine("            }");
            sw.WriteLine("        }");
        }

        public static void generateCheckAssociation(StreamWriter sw)
        {
            sw.WriteLine("");
            sw.WriteLine("        public static WebAssociationEnd checkAssociation( string associationNameSource, string associationNameTarget, string sourceClass, string targetClass )");
            sw.WriteLine("        {");
            sw.WriteLine("            var cSource = _wm.findClassByName( sourceClass );");
            sw.WriteLine("            if (cSource == null)");
            sw.WriteLine("            {");
            sw.WriteLine("                cSource = _wm.createClass( sourceClass );");
            sw.WriteLine("            }");
            sw.WriteLine("            var cTarget = _wm.findClassByName( targetClass );");
            sw.WriteLine("            if (cTarget == null)");
            sw.WriteLine("            {");
            sw.WriteLine("                cTarget = _wm.createClass( targetClass );");
            sw.WriteLine("            }");
            sw.WriteLine("            var a = c.findAssociationEnd( associationNameTarget );");
            sw.WriteLine("            if (a == null)");
            sw.WriteLine("            {");
            sw.WriteLine("                a = _wm.createAssociationEnd( cSource , cTarget , associationNameSource , associationNameTarget );");
            sw.WriteLine("            }");
            sw.WriteLine("            return a;");
            sw.WriteLine("        }");
        }

        public static void generateArguments(StreamWriter sw, Method m) 
        {
            sw.Write("(");
            int count = 1;
            foreach (var arg in m._arguments)
            {
                if (count == 1) { sw.Write(" " + arg.Type + " " + arg.Name + " "); }
                else { sw.Write(", " + arg.Type + " " + arg.Name + " "); }
                count++;
            }
            sw.Write(" )\n");
        }

        public static void generatePropertyGet(StreamWriter sw, Variable _variable, Class _class) 
        {
            sw.WriteLine("            get");
            sw.WriteLine("            {");
            sw.WriteLine("                BaseObject.checkObject( " + "\"" + _variable.Name + "\" , \"" + _variable.primitiveType + "\" , \"" + _class.ClassName + "\" );");
            sw.WriteLine("                return _object[\"" + _variable.Name + "\"];");
            sw.WriteLine("            }");
        }

        public static void generatePropertySet(StreamWriter sw, Variable _variable, Class _class)
        {
            sw.WriteLine("            set");
            sw.WriteLine("            {");
            sw.WriteLine("                BaseObject.checkObject( " + "\"" + _variable.Name + "\" , \"" + _variable.primitiveType + "\" , \"" + _class.ClassName + "\" );");
            sw.WriteLine("                _object[\"" + _variable.Name + "\"] = Convert.ToString( value );");
            sw.WriteLine("            }");
        }

        public static void generateProperties(StreamWriter sw, Class _class, ref bool IsMade) 
        {
            if (_class._variables.Count != 0)
            {
                foreach (var f in _class._variables)
                {
                    if (IsMade == true) { sw.WriteLine(""); }
                    else { IsMade = true; }

                    if (f.Protection != null) { sw.Write("\n        " + f.Protection); }
                    else { sw.Write("        public"); }

                    sw.Write(" " + f.Type + " " + f.Name + " \n");
                    sw.WriteLine("        {");

                    generatePropertyGet(sw,f,_class);
                    generatePropertySet(sw,f,_class);

                    sw.Write("        }\n");
                }
            }
        }

        public static void generateAssociationGet(StreamWriter sw, Association _association, Class _class)
        {
            sw.WriteLine("            get");
            sw.WriteLine("            {");
            sw.WriteLine("                var a = BaseObject.checkAssociation( " + "\"" + _association.SourceName + "\" , \"" + _association.TargetName + "\" , \"" + _class.ClassName + "\" , \"" + _association.TargetClass + "\" , \"" + _association.IsComposition + "\" );");
            sw.WriteLine("                var list = _object.LinkedObjects(a);");
            sw.WriteLine("                List<" + _association.TargetClass + "> result = _object.LinkedObjects(a);");
            sw.WriteLine("                foreach (var l in list)");
            sw.WriteLine("                {");
            sw.WriteLine("                    result.add( new " + _association.TargetClass + "( _vm , l.getReference() ));");
            sw.WriteLine("                }");
            sw.WriteLine("                return result;");
            sw.WriteLine("            }");
        }

        public static void generateAssociationSet(StreamWriter sw, Association _association, Class _class)
        {
            sw.WriteLine("            set");
            sw.WriteLine("            {");
            sw.WriteLine("                var a = BaseObject.checkAssociation( " + "\"" + _association.SourceName + "\" , \"" + _association.TargetName + "\" , \"" + _class.ClassName + "\" , \"" + _association.TargetClass + "\" , \"" + _association.IsComposition + "\");");
            sw.WriteLine("                var list = value");
            sw.WriteLine("                List<WebObject> result = new();");
            sw.WriteLine("                foreach (var l in list)");
            sw.WriteLine("                {");
            sw.WriteLine("                    result.add( l._object );");
            sw.WriteLine("                }");
            sw.WriteLine("            }");
        }

        public static void generateAssociations(StreamWriter sw, Class _class, ref bool IsMade) 
        {
            if (_class._associationEnds.Count != 0)
            {
                foreach (var a in _class._associationEnds)
                {
                    if (IsMade == true) { sw.WriteLine(""); }
                    else { IsMade = true; }

                    sw.WriteLine("        public List<" + a.ClassName + "> " + a.RoleName);
                    sw.WriteLine("        {");

                    generateAssociationGet(sw, visitor.Associations[(int)a.ID], _class);
                    generateAssociationSet(sw, visitor.Associations[(int)a.ID], _class);

                    sw.Write("        }\n");
                }
            }
        }

        public static void generateMethods(StreamWriter sw, Class _class, ref bool IsMade)
        {
            if (_class._methods.Count != 0)
            {
                foreach (var m in _class._methods)
                {
                    if (IsMade == true) { sw.WriteLine(""); }
                    else { IsMade = true; }

                    if (m.Protection != null) { sw.Write("\n        " + m.Protection); }
                    else { sw.Write("        public"); }

                    sw.Write(" " + m.Type + " " + m.Name + " ");

                    generateArguments(sw,m);

                    sw.WriteLine("        {");
                    sw.WriteLine("            return " + m.ReturnValue + ";");
                    sw.WriteLine("        }");
                }
            }
        }

        public static void generateClass(StreamWriter sw, Class _class) 
        {
            bool IsMade = false;

            sw.Write("    class " + _class.ClassName);

            if (_class.SuperClass != null)
            {
                sw.Write(" : " + _class.SuperClass.ClassName);
            }
            sw.WriteLine("");

            sw.WriteLine("    {");

            sw.WriteLine("        public IWebMemory _wm;");
            sw.WriteLine("        public WebObject _object;\n");

            generateConstructor(sw, _class.ClassName, ref IsMade);

            generateProperties(sw,_class,ref IsMade);
            generateAssociations(sw, _class, ref IsMade);
            generateMethods(sw, _class,ref IsMade);

            if (IsMade == false) { sw.WriteLine("        "); }
            sw.WriteLine("    }");
        }

        public static bool generate(Compiler visitor, string _namespace)
        {
            if (visitor.Errors.Count != 0)
            {
                foreach (var error in visitor.Errors)
                {
                    Console.WriteLine(error);
                }
                return false;
            }
            if (!checkNamespace(_namespace)) 
            {
                Console.WriteLine("'" + _namespace + "' is in incorrect format!");
                return false; 
            }
            using (StreamWriter sw = new StreamWriter("Classes/BaseObject.cs"))
            {
                sw.WriteLine("using WebAppOS;\n");
                sw.WriteLine("namespace " + _namespace);
                sw.WriteLine("{");
                generateBaseObject(sw);
                sw.Write('}');
            }
            foreach (var _class in visitor.Classes)
            {
                string filename = "Classes/" + _class.ClassName + ".cs";
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    sw.WriteLine("using WebAppOS;\n");
                    sw.WriteLine("namespace " + _namespace);
                    sw.WriteLine("{");
                    generateClass(sw, _class);
                    sw.Write('}');
                }
            }
            return true;
        }
    }
}
