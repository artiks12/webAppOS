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
        public List<Context> contexts = new();

        public override object VisitErrorNode([NotNull] IErrorNode node)
        {
            contexts.Add(new(node.GetType().Name, node.GetText()));
            return base.VisitErrorNode(node);
        }

        public override object VisitCode([NotNull] CodeContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitCode(context);
        }

        public override object VisitBlocks([NotNull] BlocksContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitBlocks(context);
        }

        public override object VisitBlockType([NotNull] BlockTypeContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitBlockType(context);
        }

        public override object VisitBlockBody([NotNull] BlockBodyContext context)
        {
            contexts.Add(new(context.GetType().Name, context.GetText()));
            return base.VisitBlockBody(context);
        }
    }
}