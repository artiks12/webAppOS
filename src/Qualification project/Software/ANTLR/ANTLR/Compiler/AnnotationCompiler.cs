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

            // Pārbauda, vai anotācijai ir saturs
            if (context.annotationContent() == null) { Errors.Add("At line " + line + ": Missing annotation content!"); }
            else { VisitAnnotationContent(context.annotationContent()); }

            // Metodes anotāciju sarakstam pievienojam anotāciju, ja netika skatīta URL anotācija
            if (_isUrl == false) { _method.Annotations.Add(_annotation); }
            return null;
        }

        /// <summary>
        /// Apstaigā anotācijas saturu
        /// </summary>
        public override object VisitAnnotationContent([NotNull] AnnotationContentContext context)
        {
            var start = (AnnotationContext)context.Parent;
            uint line = (uint)start.Start.Line; // Nosaka rindu, kurā ir kļūda, ja tādu atrod.

            // Parbauda, vai ir anotācijas tips
            if (context.annotationType() == null) 
            { 
                Errors.Add("At line " + line + ": Missing annotation type!");
                _annotation = new();
                _annotation.Line = _annotationLine;
            }
            else
            {
                VisitAnnotationType(context.annotationType());
                line = (uint)context.annotationType().Stop.Line;
            }

            // Pārbauda, vai anotācijas saturam ir ķermenis
            if (context.annotationBody() == null) { Errors.Add("At line " + line + ": Missing annotation body!"); }
            else { VisitAnnotationBody(context.annotationBody()); }

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
                if (_urlFound == true) { Errors.Add("At line " + context.Start.Line + ": a definition for URL for method '" + _method.Name + "' is already given! Check line " + _method.URL.Line + "!"); }
                else
                {
                    _method.URL = new();
                    _method.URL.Line = _annotationLine;
                }
            }
            else
            {
                // Sagatavojam anotāciju, kas nav URL
                _annotation = new();
                _annotation.Line = _annotationLine;

                bool found = false; // Nosaka, vai ir atrasts anotācijas tips

                // Pārbaudam, vai padotais anotācijas tips ir atbalstīts
                foreach (var a in AnnotationTypes)
                {
                    if (context.GetText() == a)
                    {
                        _annotation.Type = a;
                        found = true;
                        break;
                    }
                }

                // Ja anotācijas tips nav atbalstīts, tad saglabā kļūdu
                if (found == false) { Errors.Add("At line " + context.Start.Line + ": annotation type '" + context.GetText() + "' is not supported!"); }
            }
            return null;
        }

        /// <summary>
        /// Apstaigā anotācijas ķermeni
        /// </summary>
        public override object VisitAnnotationBody([NotNull] AnnotationBodyContext context)
        {
            uint line = (uint)context.Start.Line; // Nosaka rindu, kurā ir kļūda, ja tādu atrod.

            // Pārbauda, vai anotācijas ķermenī ir definīcija
            if (context.annotationDefinition() == null) { Errors.Add("At line " + line + ": Missing annotation definition!"); }
            else { VisitAnnotationDefinition(context.annotationDefinition()); }
            
            return null;
        }

        /// <summary>
        /// Apstaigā anotācijas definīciju
        /// </summary>
        public override object VisitAnnotationDefinition([NotNull] AnnotationDefinitionContext context)
        {
            uint line = (uint)context.Start.Line;

            // Pārbauda, vai ir sākumpēdiņas
            if (context.startQuote() == null) { Errors.Add("At line " + line + ": Syntax error! Missing '\"'!"); }
            else { line = (uint)context.startQuote().Stop.Line; }

            // Pārbauda, vai ir anotācijas vērtība
            if (context.annotationValue() == null) { Errors.Add("At line " + line + ": Missing annotation value!"); }
            else
            {
                VisitAnnotationValue(context.annotationValue());
                line = (uint)context.annotationValue().Stop.Line;
            }

            // Pārbauda, vai ir beigu pēdiņas
            if (context.endQuote() == null) { Errors.Add("At line " + line + ": Syntax error! Missing '\"'!"); }

            return null;
        }

        /// <summary>
        /// Apstaigā anotācijas vērtību
        /// </summary>
        public override object VisitAnnotationValue([NotNull] AnnotationValueContext context)
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
                        VisitUrlAttributes(context.urlAttributes());
                        _method.URL.MethodPath = context.annotationAttributes().GetText();
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
            uint line;
            var start = (AnnotationDefinitionContext)context.Parent.Parent; // Rindas fiksēšanu sāk no pēdiņām vai apaļajām iekavām
            if (start.startQuote() != null) { line = (uint)start.Start.Line; }
            else { line = (uint)context.Start.Line; } // Nosaka rindu, kurā ir kļūda, ja tādu atrod.

            // Pārbaudam, vai ir dots valodas protokols
            if (context.protocol() == null) 
            { 
                Errors.Add("At line " + line + ": URL protocol not given!");
                line = (uint)(context.COLON())[0].Symbol.Line;
            }
            else 
            {
                _method.URL.Protocol = context.protocol().GetText();
                
                bool found = false;
                // Pārbaudam, vai ir dots pareizs protokols
                foreach (var p in URLProtocols) 
                {
                    if (p == context.protocol().GetText()) 
                    {
                        found = true;
                        break;
                    }
                }
                if (found == false) { Errors.Add("At line " + context.protocol().Start.Line + ": unsupported URL protocol was given!"); }
                line = (uint)(context.COLON())[0].Symbol.Line;
            }

            // Pārbaudam, vai ir dota lokācija
            if (context.location() == null) { Errors.Add("At line " + line + ": URL location not given!"); }
            else
            {
                _method.URL.Location = context.location().GetText();

                bool found = false;
                // Pārbaudam, vai ir dots pareiza lokācija
                foreach (var l in URLlocations)
                {
                    if (l == context.location().GetText())
                    {
                        found = true;
                        break;
                    }
                }
                if (found == false) { Errors.Add("At line " + context.location().Start.Line + ": unsupported URL location was given!"); }
            }

            return null;
        }
    }
}