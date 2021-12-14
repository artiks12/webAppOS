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
        public override object VisitFields([NotNull] FieldsContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitFields(context);
        }

        public override object VisitField([NotNull] FieldContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitField(context);
        }

        public override object VisitFieldDefinition([NotNull] FieldDefinitionContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitFieldDefinition(context);
        }

        public override object VisitFieldProtection([NotNull] FieldProtectionContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitFieldProtection(context);
        }

        public override object VisitFieldDataType([NotNull] FieldDataTypeContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitFieldDataType(context);
        }

        public override object VisitFieldName([NotNull] FieldNameContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitFieldName(context);
        }
    }
}