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
        /// <summary>
        /// Apstaigā laukus
        /// </summary>
        public override object VisitFields([NotNull] FieldsContext context)
        {
            ///		Console.WriteLine(context.GetType() + "\n" + context.GetText() + "\n\n");

            uint line = (uint)context.Start.Line; // Nosaka rindu, kurā ir kļūda, ja tādu atrod.
            
            // Pārbauda, vai lauks ir definēts
            if (context.field() != null) 
            {
                line = (uint)context.field().Stop.Line;
                VisitField(context.field());
            }

            // Pārbauda, vai lauks beidzas ar semikolu
            if (context.SEMICOLON() == null) { Errors.Add("At line " + line + ": Syntax error! Missing ';'!"); }
            
            return null;
        }

        /// <summary>
        /// Apstaigā lauku
        /// </summary>
        public override object VisitField([NotNull] FieldContext context)
        {
            // Ja laukam ir vismaz viena anoptācija, vai ir iekavas, tad tā ir metode. Citādi, tā ir īpasiba
            // Pārbauda, vai laukam ir anotācijas
            if (context.annotation().Length != 0)  { VisitMethod(context); }
            else 
            {
                // Pārbauda, vai laukam ir iekavas
                if (context.fieldDefinition().methodDefinition() != null) { VisitMethod(context); }
                else { VisitVariable(context.fieldDefinition().variableDefinition()); }
            }
            
            return null;
        }
    }
}