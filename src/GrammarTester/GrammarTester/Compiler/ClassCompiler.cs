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
        public override object VisitWebMemoryClass([NotNull] WebMemoryClassContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitWebMemoryClass(context);
        }

        public override object VisitClassHead([NotNull] ClassHeadContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitClassHead(context);
        }

        public override object VisitClassName([NotNull] ClassNameContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitClassName(context);
        }

        public override object VisitSuperClass([NotNull] SuperClassContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitSuperClass(context);
        }

        public override object VisitSuperClassName([NotNull] SuperClassNameContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitSuperClassName(context);
        }

        public override object VisitClassBody([NotNull] ClassBodyContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitClassBody(context);
        }
    }
}