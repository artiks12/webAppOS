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
        public override object VisitAssociation([NotNull] AssociationContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitAssociation(context);
        }

        public override object VisitAssociationDefinition([NotNull] AssociationDefinitionContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitAssociationDefinition(context);
        }

        public override object VisitAssociationSource([NotNull] AssociationSourceContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitAssociationSource(context);
        }

        public override object VisitAssociationSourceClass([NotNull] AssociationSourceClassContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitAssociationSourceClass(context);
        }

        public override object VisitAssociationSourceName([NotNull] AssociationSourceNameContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitAssociationSourceName(context);
        }

        public override object VisitAssociationTarget([NotNull] AssociationTargetContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitAssociationTarget(context);
        }

        public override object VisitAssociationTargetClass([NotNull] AssociationTargetClassContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitAssociationTargetClass(context);
        }

        public override object VisitAssociationTargetName([NotNull] AssociationTargetNameContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitAssociationTargetName(context);
        }
    }
}