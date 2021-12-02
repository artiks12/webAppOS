﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\Artis\Documents\GitHub\webAppOS\src\ANTLR\ANTLR\Grammar\LanguageParser.g4 by ANTLR 4.6.6

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace ANTLR.Grammar {
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="LanguageParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public interface ILanguageParserVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.code"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCode([NotNull] LanguageParser.CodeContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.blocks"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlocks([NotNull] LanguageParser.BlocksContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.blockBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlockBody([NotNull] LanguageParser.BlockBodyContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.blockType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlockType([NotNull] LanguageParser.BlockTypeContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.association"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssociation([NotNull] LanguageParser.AssociationContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.associationDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssociationDefinition([NotNull] LanguageParser.AssociationDefinitionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.associationSource"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssociationSource([NotNull] LanguageParser.AssociationSourceContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.associationTarget"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssociationTarget([NotNull] LanguageParser.AssociationTargetContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.associationSourceName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssociationSourceName([NotNull] LanguageParser.AssociationSourceNameContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.associationTargetName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssociationTargetName([NotNull] LanguageParser.AssociationTargetNameContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.associationSourceClass"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssociationSourceClass([NotNull] LanguageParser.AssociationSourceClassContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.associationTargetClass"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssociationTargetClass([NotNull] LanguageParser.AssociationTargetClassContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.webMemoryClass"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWebMemoryClass([NotNull] LanguageParser.WebMemoryClassContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.classHead"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitClassHead([NotNull] LanguageParser.ClassHeadContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.classBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitClassBody([NotNull] LanguageParser.ClassBodyContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.superClass"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSuperClass([NotNull] LanguageParser.SuperClassContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.fields"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFields([NotNull] LanguageParser.FieldsContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.field"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitField([NotNull] LanguageParser.FieldContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.className"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitClassName([NotNull] LanguageParser.ClassNameContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.superClassName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSuperClassName([NotNull] LanguageParser.SuperClassNameContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.annotation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAnnotation([NotNull] LanguageParser.AnnotationContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.annotationContent"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAnnotationContent([NotNull] LanguageParser.AnnotationContentContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.annotationBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAnnotationBody([NotNull] LanguageParser.AnnotationBodyContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.annotationDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAnnotationDefinition([NotNull] LanguageParser.AnnotationDefinitionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.annotationValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAnnotationValue([NotNull] LanguageParser.AnnotationValueContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.urlAttributes"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUrlAttributes([NotNull] LanguageParser.UrlAttributesContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.annotationAttributes"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAnnotationAttributes([NotNull] LanguageParser.AnnotationAttributesContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.annotationData"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAnnotationData([NotNull] LanguageParser.AnnotationDataContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.annotationType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAnnotationType([NotNull] LanguageParser.AnnotationTypeContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.annotationSeperator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAnnotationSeperator([NotNull] LanguageParser.AnnotationSeperatorContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.protocol"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProtocol([NotNull] LanguageParser.ProtocolContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.location"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLocation([NotNull] LanguageParser.LocationContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.startQuote"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStartQuote([NotNull] LanguageParser.StartQuoteContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.endQuote"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEndQuote([NotNull] LanguageParser.EndQuoteContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.fieldDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFieldDefinition([NotNull] LanguageParser.FieldDefinitionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.variableDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariableDefinition([NotNull] LanguageParser.VariableDefinitionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.methodDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMethodDefinition([NotNull] LanguageParser.MethodDefinitionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.variable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariable([NotNull] LanguageParser.VariableContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.arguments"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArguments([NotNull] LanguageParser.ArgumentsContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.argument"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArgument([NotNull] LanguageParser.ArgumentContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.fieldProtection"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFieldProtection([NotNull] LanguageParser.FieldProtectionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.fieldDataType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFieldDataType([NotNull] LanguageParser.FieldDataTypeContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.fieldName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFieldName([NotNull] LanguageParser.FieldNameContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.argumentDataType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArgumentDataType([NotNull] LanguageParser.ArgumentDataTypeContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.argumentName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArgumentName([NotNull] LanguageParser.ArgumentNameContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.coma"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitComa([NotNull] LanguageParser.ComaContext context);
}
} // namespace ANTLR.Grammar