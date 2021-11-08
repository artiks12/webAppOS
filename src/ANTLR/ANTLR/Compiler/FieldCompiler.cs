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
        public override object VisitFields([NotNull] FieldsContext context)
        {
            var semi = context.SEMICOLON().GetText();
            var field = context.field();
            
            if (semi != ";") { Errors.Add("At line " + context.Stop.Line + ": Syntax error! Missing ';'!"); }

            if (field != null) 
            {
                return VisitField(field);
            }
            return null;
        }

        public override object VisitField([NotNull] FieldContext context)
        {
            var annotations = context.annotation();
            var method = context.fieldDefinition().methodDefinition();

            if (annotations.Length == 0 && method == null) 
            { 
                VisitVariable(context);
            }
            else 
            { 
                VisitMethod(context);
            }
            return null;
        }


    }

}
