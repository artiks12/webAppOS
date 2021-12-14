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
        public override object VisitArguments([NotNull] ArgumentsContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitArguments(context);
        }

        public override object VisitComa([NotNull] ComaContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitComa(context);
        }

        public override object VisitArgument([NotNull] ArgumentContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitArgument(context);
        }

        public override object VisitArgumentDataType([NotNull] ArgumentDataTypeContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitArgumentDataType(context);
        }

        public override object VisitArgumentName([NotNull] ArgumentNameContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitArgumentName(context);
        }
    }
}