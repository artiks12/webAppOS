﻿using GrammarTester;
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
		public override object VisitMethodDefinition([NotNull] MethodDefinitionContext context)
		{
			contexts.Add(new(context.GetType().Name, context.GetText()));
			return base.VisitMethodDefinition(context);
		}
	}
}