using GrammarTester;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using GrammarTester.Grammar;
using static GrammarTester.Grammar.LanguageParser;
using Antlr4.Runtime.Tree;

namespace GrammarTester
{
	public partial class Compiler : LanguageParserBaseVisitor<object>
	{
        public override object VisitAnnotation([NotNull] AnnotationContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitAnnotation(context);
        }

        public override object VisitAnnotationContent([NotNull] AnnotationContentContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitAnnotationContent(context);
        }

        public override object VisitAnnotationType([NotNull] AnnotationTypeContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitAnnotationType(context);
        }

        public override object VisitAnnotationBody([NotNull] AnnotationBodyContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitAnnotationBody(context);
        }

        public override object VisitAnnotationDefinition([NotNull] AnnotationDefinitionContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitAnnotationDefinition(context);
        }

        public override object VisitAnnotationValue([NotNull] AnnotationValueContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitAnnotationValue(context);
        }

        public override object VisitAnnotationAttributes([NotNull] AnnotationAttributesContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitAnnotationAttributes(context);
        }

        public override object VisitAnnotationData([NotNull] AnnotationDataContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitAnnotationData(context);
        }

        public override object VisitAnnotationSeperator([NotNull] AnnotationSeperatorContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitAnnotationSeperator(context);
        }

        /// <summary>
        /// Extras
        /// </summary>

        public override object VisitStartQuote([NotNull] StartQuoteContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitStartQuote(context);
        }

        public override object VisitEndQuote([NotNull] EndQuoteContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitEndQuote(context);
        }

        public override object VisitUrlAttributes([NotNull] UrlAttributesContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitUrlAttributes(context);
        }

        public override object VisitProtocol([NotNull] ProtocolContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitProtocol(context);
        }

        public override object VisitLocation([NotNull] LocationContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitLocation(context);
        }
    }
}