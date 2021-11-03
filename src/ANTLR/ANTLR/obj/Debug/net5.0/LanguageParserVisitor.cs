﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\Artis\source\repos\ANTLR\ANTLR\LanguageParser.g4 by ANTLR 4.6.6

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace ANTLR {
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
	/// Visit a parse tree produced by <see cref="LanguageParser.webMemoryClass"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWebMemoryClass([NotNull] LanguageParser.WebMemoryClassContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.superClass"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSuperClass([NotNull] LanguageParser.SuperClassContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.classBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitClassBody([NotNull] LanguageParser.ClassBodyContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.classType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitClassType([NotNull] LanguageParser.ClassTypeContext context);

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
	/// Visit a parse tree produced by <see cref="LanguageParser.method"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMethod([NotNull] LanguageParser.MethodContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.url"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUrl([NotNull] LanguageParser.UrlContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.urlDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUrlDefinition([NotNull] LanguageParser.UrlDefinitionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.urlType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUrlType([NotNull] LanguageParser.UrlTypeContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.protocolName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProtocolName([NotNull] LanguageParser.ProtocolNameContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.location"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLocation([NotNull] LanguageParser.LocationContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.methodPath"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMethodPath([NotNull] LanguageParser.MethodPathContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.methodAnnotation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMethodAnnotation([NotNull] LanguageParser.MethodAnnotationContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.annotationType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAnnotationType([NotNull] LanguageParser.AnnotationTypeContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.annotationDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAnnotationDefinition([NotNull] LanguageParser.AnnotationDefinitionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.methodDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMethodDefinition([NotNull] LanguageParser.MethodDefinitionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.methodProtection"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMethodProtection([NotNull] LanguageParser.MethodProtectionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.methodDataType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMethodDataType([NotNull] LanguageParser.MethodDataTypeContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.methodName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMethodName([NotNull] LanguageParser.MethodNameContext context);

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
	/// Visit a parse tree produced by <see cref="LanguageParser.variable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariable([NotNull] LanguageParser.VariableContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.variableProtection"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariableProtection([NotNull] LanguageParser.VariableProtectionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.variableDataType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariableDataType([NotNull] LanguageParser.VariableDataTypeContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.variableName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariableName([NotNull] LanguageParser.VariableNameContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.associations"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssociations([NotNull] LanguageParser.AssociationsContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.associationType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssociationType([NotNull] LanguageParser.AssociationTypeContext context);

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
	/// Visit a parse tree produced by <see cref="LanguageParser.sourceClass"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSourceClass([NotNull] LanguageParser.SourceClassContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="LanguageParser.targetClass"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTargetClass([NotNull] LanguageParser.TargetClassContext context);
}
} // namespace ANTLR
