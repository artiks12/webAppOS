parser grammar LanguageParser;	/// Parsera gramatikas nosaukums


options 
{ 
	tokenVocab=LanguageLexer; /// Norādam, ka vēl tiks izmantota gramatika ar nosaukumu LanguageLexer
}

/// Sākuma punkts
code:						blocks*;

blocks:						(blockType? blockBody | blockType blockBody?);
blockBody:					webMemoryClass | association;

blockType:					BLOCKTYPE;


/// webMemoryClass likumi
webMemoryClass:				(classHead classBody? | classHead? classBody);

classHead:					(className superClass? | className? superClass);
superClass:					COLON superClassName?;
classBody:					CURLYOPEN fields* CURLYCLOSE;

className:					NAME | PROTECTION | DATATYPE;
superClassName:				NAME | PROTECTION | DATATYPE;


/// Lauku (mainīgo un funkciju) likumi
fields:						(field? SEMICOLON | field SEMICOLON?);
field:						(annotation* fieldDefinition | annotation+ fieldDefinition?);

fieldDefinition:			(variableDefinition methodDefinition? | variableDefinition? methodDefinition);
variableDefinition:			(fieldProtection? variable | fieldProtection variable?);
variable:					(fieldDataType? fieldName | fieldDataType fieldName?);
methodDefinition:			BRACKETOPEN arguments BRACKETCLOSE;

fieldProtection:			PROTECTION;
fieldDataType:				DATATYPE;
fieldName:					NAME | BLOCKTYPE;


/// Anotaciju likumi
annotation:					SQUAREOPEN annotationType? BRACKETOPEN? startQuote? annotationDefinition endQuote? BRACKETCLOSE? SQUARECLOSE;
annotationDefinition:		urlAttributes? annotationAttributes;
annotationAttributes:		(annotationData | COLON | SEMICOLON | COMA | DOT | HASH)*;

urlAttributes:				protocol? COLON location? COLON;

annotationData:				ANYTHING | NAME | PROTECTION | BLOCKTYPE | DATATYPE;
annotationType:				NAME | PROTECTION | BLOCKTYPE | DATATYPE;
protocol:					NAME | PROTECTION | BLOCKTYPE | DATATYPE;
location:					NAME | PROTECTION | BLOCKTYPE | DATATYPE;
startQuote:					QUOTE;
endQuote:					QUOTE;


/// Argumentu likumi
arguments:					(coma | argument)*;
argument:					(argumentDataType? argumentName | argumentDataType argumentName?);

argumentDataType:			DATATYPE;
argumentName:				NAME | BLOCKTYPE | PROTECTION;
coma:						COMA;


/// Asociaciju likumi
association:				BRACKETOPEN associationDefinition BRACKETCLOSE;
associationDefinition:		associationSource ARROWS? associationTarget;
associationSource:			associationSourceName? COLON? associationSourceClass?; 
associationTarget:			associationTargetName? COLON? associationTargetClass?;


associationSourceName:		NAME | PROTECTION | DATATYPE | BLOCKTYPE;
associationTargetName:		NAME | PROTECTION | DATATYPE | BLOCKTYPE;
associationSourceClass:		NAME | PROTECTION | DATATYPE | BLOCKTYPE;
associationTargetClass:		NAME | PROTECTION | DATATYPE | BLOCKTYPE;