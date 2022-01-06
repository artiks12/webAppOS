// FieldCompiler.cs
/******************************************************
* Satur klases lauku kompilēšanas pamatfunkcijas.
* Tās iekļauj semikola parbaudi un arī lauka veida noteikšanu
* (vai lauks ir metode vai atribūts?)
******************************************************/
// Autors:  Artis Pauniņš
// Pabeigts: v1.0 06.01.22

using Antlr4.Runtime.Misc; // Nodrošina to, ka visās "Visit" funkcijas padotie konteksti nav ar vērtību 'null'
using ANTLR.Grammar; // Nodrošina darbu ar gramatikas kodu
using static ANTLR.Grammar.LanguageParser; // Nodrošina vienkāršāku konteksta objektu notāciju (var rakstīt, piem., CodeContext nevis LanguageParser.CodeContext)

namespace AntlrCSharp
{
    public partial class Compiler : LanguageParserBaseVisitor<object>
    {
        /// <summary>
        /// Apstaigā laukus
        /// </summary>
        public override object VisitFields([NotNull] FieldsContext context)
        {
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
            // Ja laukam ir vismaz viena anotācija, vai ir iekavas, tad tā ir metode. Citādi, tā ir īpasiba
            // Pārbauda, vai laukam ir anotācijas
            if (context.annotation().Length != 0)  { VisitMethod(context); /* Apstaigā metodi (skat MethodCompiler.cs) */ }
            else 
            {
                // Pārbauda, vai laukam ir iekavas
                if (context.fieldDefinition().methodDefinition() != null) { VisitMethod(context); /* Apstaigā metodi (skat MethodCompiler.cs) */ }
                else { VisitAttribute(context.fieldDefinition().attributeDefinition()); /* Apstaigā atribūtu (skat AtributeCompiler.cs) */ }
            }
            
            return null;
        }
    }
}