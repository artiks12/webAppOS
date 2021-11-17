parser grammar LanguageParser;	/// Parsera gramatikas nosaukums

options 
{ 
	tokenVocab=LanguageLexer; /// Norādam, ka vēl tiks izmantota gramatika ar nosaukumu LanguageLexer
}

/// Sākuma shēma
code:						blocks*;

blocks:						(blockType? blockBody | blockType blockBody?);
blockBody:					webMemoryClass | association;

blockType:					BLOCKTYPE | PROTECTION | DATATYPE;


/// Asociacijas shēma
association:				BRACKETOPEN associationDefinition BRACKETCLOSE;

associationDefinition:		associationSource ARROWS? associationTarget;

associationSource:			associationSourceName? COLON? associationSourceClass?; 
associationTarget:			associationTargetName? COLON? associationTargetClass?;

associationSourceName:		NAME | PROTECTION | DATATYPE | BLOCKTYPE;
associationTargetName:		NAME | PROTECTION | DATATYPE | BLOCKTYPE;
associationSourceClass:		NAME | PROTECTION | DATATYPE | BLOCKTYPE;
associationTargetClass:		NAME | PROTECTION | DATATYPE | BLOCKTYPE;


/// webMemoryClass shēma
webMemoryClass:				(classHead classBody? | classHead? classBody);

classHead:					(className superClass? | className? superClass);
classBody:					CURLYOPEN fields* CURLYCLOSE;

superClass:					COLON superClassName?;
fields:						(field? SEMICOLON | field SEMICOLON?);

field:						(annotation* fieldDefinition | annotation+ fieldDefinition?);

className:					NAME;
superClassName:				NAME;


/// Anotacijas shēma
annotation:					SQUAREOPEN annotationContent? SQUARECLOSE;

annotationContent:			(annotationType? annotationBody | annotationType annotationBody? );

annotationBody:				BRACKETOPEN annotationDefinition BRACKETCLOSE;			

annotationDefinition:		startQuote? annotationValue? endQuote?;

annotationValue:			urlAttributes? annotationAttributes;

urlAttributes:				protocol? COLON location? COLON;
annotationAttributes:		(annotationData | annotationSeperator)+;

annotationData:				ANYTHING | NAME | PROTECTION | BLOCKTYPE | DATATYPE;
annotationType:				NAME | PROTECTION | BLOCKTYPE | DATATYPE;
annotationSeperator:		COLON | SEMICOLON | COMA | DOT | HASH;
protocol:					NAME | PROTECTION | BLOCKTYPE | DATATYPE;
location:					NAME | PROTECTION | BLOCKTYPE | DATATYPE;
startQuote:					QUOTE;
endQuote:					QUOTE;


/// Lauka definīcijas shēma
fieldDefinition:			(variableDefinition methodDefinition? | variableDefinition? methodDefinition);

variableDefinition:			(fieldProtection? variable | fieldProtection variable?);
methodDefinition:			BRACKETOPEN arguments BRACKETCLOSE;

variable:					(fieldDataType? fieldName | fieldDataType fieldName?);
arguments:					(coma | argument)*;

argument:					(argumentDataType? argumentName | argumentDataType argumentName?);

fieldProtection:			PROTECTION;
fieldDataType:				DATATYPE | BLOCKTYPE;
fieldName:					NAME;
argumentDataType:			DATATYPE | BLOCKTYPE | PROTECTION;
argumentName:				NAME;
coma:						COMA;