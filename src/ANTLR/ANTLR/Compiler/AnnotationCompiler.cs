using ANTLR;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using ANTLR.Grammar;
using static ANTLR.Grammar.LanguageParser;
using Antlr4.Runtime.Tree;

namespace AntlrCSharp
{
	public partial class Compiler : LanguageParserBaseVisitor<object>
	{
        private bool _urlFound; // Vai metodei jau ir URL atribūts
        private bool _isUrl; // Vai anotācija, kura tiek skatīta ir URL anotācija

        Annotation _annotation; // Pagaidu anotācija
        uint _annotationLine; // Pagaidu anotacijas rinda kodā

        /// <summary>
        /// Apstaigā anotāciju
        /// </summary>
        public override object VisitAnnotation([NotNull] AnnotationContext context)
        {
            _isUrl = false;

            uint line = (uint)context.Start.Line; // Nosaka rindu, kurā ir kļūda, ja tādu atrod.
            _annotationLine = line;

            // Pārbauda, vai ir atveroša kvadrātiekava
            if (context.SQUAREOPEN() == null) { Errors.Add("At line " + line + ": Syntax error! Missing '['!"); }
            else { line = (uint)context.SQUAREOPEN().Symbol.Line; }

            // Parbauda, vai ir anotācijas tips
            if (context.annotationType() == null) { Errors.Add("At line " + line + ": Missing annotation type!"); }
            else
            {
                VisitAnnotationType(context.annotationType());
                line = (uint)context.annotationType().Stop.Line;
            }

            // Pārbauda, vai ir atverošā apaļā iekava
            if (context.BRACKETOPEN() == null) { Errors.Add("At line " + line + ": Syntax error! Missing '('!"); }
            else { line = (uint)context.BRACKETOPEN().Symbol.Line; }

            // Pārbauda, vai ir sākumpēdiņas
            if (context.startQuote() == null) { Errors.Add("At line " + line + ": Syntax error! Missing '\"'!"); }
            else { line = (uint)context.startQuote().Stop.Line; }

            // Pārbauda, vai ir anotācijas definīcija
            if (context.annotationDefinition() == null) { Errors.Add("At line " + line + ": Missing annotation definition!"); }
            else
            {
                VisitAnnotationDefinition(context.annotationDefinition());
                line = (uint)context.annotationDefinition().Stop.Line;
            }

            // Pārbauda, vai ir beigu pēdiņa
            if (context.endQuote() == null) { Errors.Add("At line " + line + ": Syntax error! Missing '\"'!"); }
            else { line = (uint)context.endQuote().Stop.Line; }

            // Pārbauda, vai ir aizverošā apaļā iekava
            if (context.BRACKETCLOSE() == null) { Errors.Add("At line " + line + ": Syntax error! Missing ')'!"); }
            else { line = (uint)context.BRACKETCLOSE().Symbol.Line; }

            // Pārbauda, vai ir aizverošā kvadrātiekava
            if (context.SQUARECLOSE() == null) { Errors.Add("At line " + line + ": Syntax error! Missing ']'!"); }

            return null;
        }

        /// <summary>
        /// Apstaigā anotācijas tipu
        /// </summary>
        public override object VisitAnnotationType([NotNull] AnnotationTypeContext context)
        {
            // Pārbauda, vai tips ir URL
            if (context.GetText() == "URL")
            {
                _isUrl = true;

                // Pārbauda, vai metodei jau ir izveidota url anotācija
                if ( _urlFound == true ) { Errors.Add("At line " + context.Start.Line + ": a definition for URL for method '" + _method.Name + "' is already given! Check line " + _method.URL.Line + "!"); }
                else 
                { 
                    _method.URL = new();
                    _method.URL.Line = (uint)context.Start.Line;
                }
            }
            else 
            {
                // Sagatavojam anotāciju, kas nav URL
                _annotation = new();
                _annotation.Line = _annotationLine;
                foreach (var a in AnnotationTypes) 
                {
                    if (context.GetText() == a) 
                    {
                        _annotation.Type = a;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Apstaigā anotācijas definīciju
        /// </summary>
        public override object VisitAnnotationDefinition([NotNull] AnnotationDefinitionContext context)
        {
            // Pārbauda, vai pašreizējā anotācija ir URL
            if (_isUrl == true)
            {
                // Pārbauda, vai metodei jau ir definēts URL
                if (_urlFound == false) 
                {
                    // Pārbauda, vai ir doti URL būtiskie attribūti
                    if (context.urlAttributes() == null) { Errors.Add("At line " + context.Start.Line + ": URL attributes for method '" + _method.Name + "' are not given!"); }
                    else
                    {
                        VisitChildren(context);
                        _urlFound = true;
                    }
                }
            }
            else 
            {
                _annotation.Definition = context.GetText();
            }

            return null;
        }

        /// <summary>
        /// Apstaigā URL attribūtus
        /// </summary>
        public override object VisitUrlAttributes([NotNull] UrlAttributesContext context)
        {
            uint line = (uint)context.Start.Line; // Nosaka rindu, kurā ir kļūda, ja tādu atrod.

            // Pārbaudam, vai ir dots valodas protokols
            if (context.protocol() == null) { Errors.Add("At line " + line + ": URL protocol not given!"); }
            else 
            {
                bool found = false;
                // Pārbaudam, vai ir dots pareizs protokols
                foreach (var p in URLProtocols) 
                {
                    if (p == context.protocol().GetText()) 
                    {
                        found = true;
                        _method.URL.Protocol = p;
                        break;
                    }
                }
                if (found == false) { Errors.Add("At line " + context.protocol().Start.Line + ": unsupported URL protocol was given!"); }
                line = (uint)(context.COLON())[1].Symbol.Line;
            }

            // Pārbaudam, vai ir dota lokācija
            if (context.location() == null) { Errors.Add("At line " + line + ": URL location not given!"); }
            else
            {
                bool found = false;
                // Pārbaudam, vai ir dots pareiza lokācija
                foreach (var l in URLlocations)
                {
                    if (l == context.location().GetText())
                    {
                        found = true;
                        _method.URL.Location = l;
                        break;
                    }
                }
                if (found == false) { Errors.Add("At line " + context.location().Start.Line + ": unsupported URL location was given!"); }
            }

            return null;
        }

        /// <summary>
        /// Apstaigā anotācijas attribūtus
        /// </summary>
        public override object VisitAnnotationAttributes([NotNull] AnnotationAttributesContext context)
        {
            _method.URL.MethodPath = context.GetText();
            
            return null;
        }
    }
}