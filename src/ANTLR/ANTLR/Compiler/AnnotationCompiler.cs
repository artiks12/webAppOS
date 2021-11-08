using ANTLR;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using static ANTLR.LanguageParser;
using Antlr4.Runtime.Tree;

namespace AntlrCSharp
{
	public partial class Compiler : LanguageParserBaseVisitor<object>
	{
        private bool _urlFound;
        public override object VisitAnnotation([NotNull] AnnotationContext context)
        {
            _urlFound = false;

            uint line = (uint)context.Start.Line;

            var squareopen = context.SQUAREOPEN();
            var squareclose = context.SQUARECLOSE();
            var bracketopen = context.BRACKETOPEN();
            var bracketclose = context.BRACKETCLOSE();
            var start = context.startQuote();
            var end = context.endQuote();

            var type = context.annotationType();
            var definition = context.annotationDefinition();


            if (squareopen == null) { Errors.Add("At line " + line + ": Syntax error! Missing '['!"); }
            else { line = (uint)squareopen.Symbol.Line; }

            if (type == null) { Errors.Add("At line " + line + ": Missing annotation type!"); }
            else
            {
                VisitAnnotationType(type);
                line = (uint)type.Stop.Line;
            }
            if (bracketopen == null) { Errors.Add("At line " + line + ": Syntax error! Missing '('!"); }
            else { line = (uint)squareopen.Symbol.Line; }

            if (start == null) { Errors.Add("At line " + line + ": Syntax error! Missing '\"'!"); }
            else { line = (uint)start.Stop.Line; }

            if (definition == null) { Errors.Add("At line " + line + ": Missing annotation definition!"); }
            else
            {
                VisitAnnotationDefinition(definition);
                line = (uint)type.Stop.Line;
            }

            if (end == null) { Errors.Add("At line " + line + ": Syntax error! Missing '\"'!"); }
            else { line = (uint)end.Stop.Line; }

            if (bracketclose == null) { Errors.Add("At line " + line + ": Syntax error! Missing ')'!"); }
            else { line = (uint)squareopen.Symbol.Line; }

            if (squareclose == null) { Errors.Add("At line " + line + ": Syntax error! Missing ']'!"); }

            return null;
        }

        public override object VisitAnnotationType([NotNull] AnnotationTypeContext context)
        {
            if (context.GetText() == "URL")
            {
                _urlFound = true;
            }
            else 
            {
                foreach (var a in AnnotationTypes) 
                {

                }
            }
            return null;
        }

        public override object VisitAnnotationDefinition([NotNull] AnnotationDefinitionContext context)
        {
            if (context.urlAttributes() == null)
            {
                if (_urlFound == true) 
                { 
                    Errors.Add("At line " + context.Start.Line + ": URL attributes for method '" + _method.Name + "' are not given!"); 
                }
                else { }
            }
            else 
            {
                if (_urlFound == true)
                {
                    _method._url = new();
                    VisitUrlAttributes(context.urlAttributes());
                }
                else 
                {
                    Errors.Add("At line " + context.urlAttributes().Start.Line + ": URL attributes given for non URL annotation!");
                }
            }
            VisitAnnotationAttributes(context.annotationAttributes());

            return null;
        }
        public override object VisitUrlAttributes([NotNull] UrlAttributesContext context)
        {
            var location = context.location();
            var protocol = context.protocol();

            if (protocol == null) { Errors.Add("At line " + context.Start.Line + ": URL protocol not given!"); }
            else 
            {
                bool found = false;
                foreach (var p in URLProtocols) 
                {
                    if (p == protocol.GetText()) 
                    {
                        found = true;
                        _method._url.Protocol = p;
                        break;
                    }
                }
                if (found == false) { Errors.Add("At line " + protocol.Start.Line + ": unsupported URL protocol was given!"); }
            }

            if (location == null) { Errors.Add("At line " + context.Start.Line + ": URL location not given!"); }
            else
            {
                bool found = false;
                foreach (var l in URLlocations)
                {
                    if (l == location.GetText())
                    {
                        found = true;
                        _method._url.Location = l;
                        break;
                    }
                }
                if (found == false) { Errors.Add("At line " + protocol.Start.Line + ": unsupported URL location was given!"); }
            }

            return null;
        }

        public override object VisitAnnotationAttributes([NotNull] AnnotationAttributesContext context)
        {
            if (_urlFound == true)
            {
                _method._url.MethodPath = context.GetText();
            }
            else 
            {
                
            }

            if (context.children != null) 
            {
                if (context.children.Count > 0)
                {
                    bool needSeperator = false;
                    foreach (var c in context.children)
                    {
                        switch (c.GetType().ToString())
                        {
                            case "ANTLR.LanguageParser+AnnotationSeperatorContext":
                                if (needSeperator == false)
                                {
                                    Errors.Add("At line " + ((AnnotationSeperatorContext)c).Start.Line + ": empty string inbetween seperators!");
                                }
                                else { needSeperator = false; }
                                break;
                            case "ANTLR.LanguageParser+AnnotationDataContext":
                                if (needSeperator == false)
                                {
                                    needSeperator = true;
                                }
                                else
                                {
                                    Errors.Add("At line " + ((AnnotationDataContext)c).Start.Line + ": Syntax error! Missing '#', '.' or ':'!");
                                }
                                break;
                        }
                    }
                }
            }
            return null;
        }
    }
}