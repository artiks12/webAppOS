//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\Artis\Documents\GitHub\webAppOS\src\Qualification project\Testing\Testers\GrammarTester\GrammarTester\Grammar\LanguageParser.g4 by ANTLR 4.6.6

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace GrammarTester.Grammar {
using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="LanguageParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public interface ILanguageParserListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.code"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCode([NotNull] LanguageParser.CodeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.code"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCode([NotNull] LanguageParser.CodeContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.blocks"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBlocks([NotNull] LanguageParser.BlocksContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.blocks"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBlocks([NotNull] LanguageParser.BlocksContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.blockBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBlockBody([NotNull] LanguageParser.BlockBodyContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.blockBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBlockBody([NotNull] LanguageParser.BlockBodyContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.blockType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBlockType([NotNull] LanguageParser.BlockTypeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.blockType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBlockType([NotNull] LanguageParser.BlockTypeContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.association"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAssociation([NotNull] LanguageParser.AssociationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.association"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAssociation([NotNull] LanguageParser.AssociationContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.associationDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAssociationDefinition([NotNull] LanguageParser.AssociationDefinitionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.associationDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAssociationDefinition([NotNull] LanguageParser.AssociationDefinitionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.associationSource"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAssociationSource([NotNull] LanguageParser.AssociationSourceContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.associationSource"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAssociationSource([NotNull] LanguageParser.AssociationSourceContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.associationTarget"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAssociationTarget([NotNull] LanguageParser.AssociationTargetContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.associationTarget"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAssociationTarget([NotNull] LanguageParser.AssociationTargetContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.associationSourceName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAssociationSourceName([NotNull] LanguageParser.AssociationSourceNameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.associationSourceName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAssociationSourceName([NotNull] LanguageParser.AssociationSourceNameContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.associationTargetName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAssociationTargetName([NotNull] LanguageParser.AssociationTargetNameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.associationTargetName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAssociationTargetName([NotNull] LanguageParser.AssociationTargetNameContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.associationSourceClass"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAssociationSourceClass([NotNull] LanguageParser.AssociationSourceClassContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.associationSourceClass"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAssociationSourceClass([NotNull] LanguageParser.AssociationSourceClassContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.associationTargetClass"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAssociationTargetClass([NotNull] LanguageParser.AssociationTargetClassContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.associationTargetClass"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAssociationTargetClass([NotNull] LanguageParser.AssociationTargetClassContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.webMemoryClass"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterWebMemoryClass([NotNull] LanguageParser.WebMemoryClassContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.webMemoryClass"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitWebMemoryClass([NotNull] LanguageParser.WebMemoryClassContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.classHead"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterClassHead([NotNull] LanguageParser.ClassHeadContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.classHead"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitClassHead([NotNull] LanguageParser.ClassHeadContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.classBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterClassBody([NotNull] LanguageParser.ClassBodyContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.classBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitClassBody([NotNull] LanguageParser.ClassBodyContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.superClass"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSuperClass([NotNull] LanguageParser.SuperClassContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.superClass"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSuperClass([NotNull] LanguageParser.SuperClassContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.fields"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFields([NotNull] LanguageParser.FieldsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.fields"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFields([NotNull] LanguageParser.FieldsContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.field"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterField([NotNull] LanguageParser.FieldContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.field"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitField([NotNull] LanguageParser.FieldContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.className"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterClassName([NotNull] LanguageParser.ClassNameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.className"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitClassName([NotNull] LanguageParser.ClassNameContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.superClassName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSuperClassName([NotNull] LanguageParser.SuperClassNameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.superClassName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSuperClassName([NotNull] LanguageParser.SuperClassNameContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.annotation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAnnotation([NotNull] LanguageParser.AnnotationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.annotation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAnnotation([NotNull] LanguageParser.AnnotationContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.annotationContent"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAnnotationContent([NotNull] LanguageParser.AnnotationContentContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.annotationContent"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAnnotationContent([NotNull] LanguageParser.AnnotationContentContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.annotationBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAnnotationBody([NotNull] LanguageParser.AnnotationBodyContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.annotationBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAnnotationBody([NotNull] LanguageParser.AnnotationBodyContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.annotationDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAnnotationDefinition([NotNull] LanguageParser.AnnotationDefinitionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.annotationDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAnnotationDefinition([NotNull] LanguageParser.AnnotationDefinitionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.annotationValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAnnotationValue([NotNull] LanguageParser.AnnotationValueContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.annotationValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAnnotationValue([NotNull] LanguageParser.AnnotationValueContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.urlAttributes"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterUrlAttributes([NotNull] LanguageParser.UrlAttributesContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.urlAttributes"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitUrlAttributes([NotNull] LanguageParser.UrlAttributesContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.annotationAttributes"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAnnotationAttributes([NotNull] LanguageParser.AnnotationAttributesContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.annotationAttributes"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAnnotationAttributes([NotNull] LanguageParser.AnnotationAttributesContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.annotationData"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAnnotationData([NotNull] LanguageParser.AnnotationDataContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.annotationData"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAnnotationData([NotNull] LanguageParser.AnnotationDataContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.annotationType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAnnotationType([NotNull] LanguageParser.AnnotationTypeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.annotationType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAnnotationType([NotNull] LanguageParser.AnnotationTypeContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.annotationSeperator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAnnotationSeperator([NotNull] LanguageParser.AnnotationSeperatorContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.annotationSeperator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAnnotationSeperator([NotNull] LanguageParser.AnnotationSeperatorContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.protocol"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProtocol([NotNull] LanguageParser.ProtocolContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.protocol"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProtocol([NotNull] LanguageParser.ProtocolContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.location"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLocation([NotNull] LanguageParser.LocationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.location"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLocation([NotNull] LanguageParser.LocationContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.startQuote"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStartQuote([NotNull] LanguageParser.StartQuoteContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.startQuote"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStartQuote([NotNull] LanguageParser.StartQuoteContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.endQuote"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEndQuote([NotNull] LanguageParser.EndQuoteContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.endQuote"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEndQuote([NotNull] LanguageParser.EndQuoteContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.fieldDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFieldDefinition([NotNull] LanguageParser.FieldDefinitionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.fieldDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFieldDefinition([NotNull] LanguageParser.FieldDefinitionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.attributeDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAttributeDefinition([NotNull] LanguageParser.AttributeDefinitionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.attributeDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAttributeDefinition([NotNull] LanguageParser.AttributeDefinitionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.methodDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMethodDefinition([NotNull] LanguageParser.MethodDefinitionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.methodDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMethodDefinition([NotNull] LanguageParser.MethodDefinitionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.attribute"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAttribute([NotNull] LanguageParser.AttributeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.attribute"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAttribute([NotNull] LanguageParser.AttributeContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.arguments"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterArguments([NotNull] LanguageParser.ArgumentsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.arguments"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitArguments([NotNull] LanguageParser.ArgumentsContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.argument"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterArgument([NotNull] LanguageParser.ArgumentContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.argument"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitArgument([NotNull] LanguageParser.ArgumentContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.fieldProtection"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFieldProtection([NotNull] LanguageParser.FieldProtectionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.fieldProtection"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFieldProtection([NotNull] LanguageParser.FieldProtectionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.fieldDataType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFieldDataType([NotNull] LanguageParser.FieldDataTypeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.fieldDataType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFieldDataType([NotNull] LanguageParser.FieldDataTypeContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.fieldName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFieldName([NotNull] LanguageParser.FieldNameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.fieldName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFieldName([NotNull] LanguageParser.FieldNameContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.argumentDataType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterArgumentDataType([NotNull] LanguageParser.ArgumentDataTypeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.argumentDataType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitArgumentDataType([NotNull] LanguageParser.ArgumentDataTypeContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.argumentName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterArgumentName([NotNull] LanguageParser.ArgumentNameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.argumentName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitArgumentName([NotNull] LanguageParser.ArgumentNameContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="LanguageParser.coma"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterComa([NotNull] LanguageParser.ComaContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="LanguageParser.coma"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitComa([NotNull] LanguageParser.ComaContext context);
}
} // namespace GrammarTester.Grammar
