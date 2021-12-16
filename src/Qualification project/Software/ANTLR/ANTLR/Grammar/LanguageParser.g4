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

associationSourceName:		IDENTIFIER | PROTECTION | DATATYPE | BLOCKTYPE;
associationTargetName:		IDENTIFIER | PROTECTION | DATATYPE | BLOCKTYPE;
associationSourceClass:		IDENTIFIER | PROTECTION | DATATYPE | BLOCKTYPE;
associationTargetClass:		IDENTIFIER | PROTECTION | DATATYPE | BLOCKTYPE;


/// webMemoryClass shēma
webMemoryClass:				(classHead classBody? | classHead? classBody);

classHead:					(className superClass? | className? superClass);
classBody:					CURLYOPEN fields* CURLYCLOSE;

superClass:					COLON superClassName?;
fields:						(field? SEMICOLON | field SEMICOLON?);

field:						(annotation* fieldDefinition | annotation+ fieldDefinition?);

className:					IDENTIFIER;
superClassName:				IDENTIFIER;


/// Anotacijas shēma
annotation:					SQUAREOPEN annotationContent? SQUARECLOSE;

annotationContent:			(annotationType? annotationBody | annotationType annotationBody? );

annotationBody:				BRACKETOPEN annotationDefinition? BRACKETCLOSE;			

annotationDefinition:		(startQuote annotationValue? endQuote | startQuote? annotationValue endQuote? );

annotationValue:			urlAttributes? annotationAttributes;

urlAttributes:				protocol? COLON location? COLON;
annotationAttributes:		(annotationData | annotationSeperator)+;

annotationData:				ANYTHING | IDENTIFIER | PROTECTION | BLOCKTYPE | DATATYPE;
annotationType:				IDENTIFIER | PROTECTION | BLOCKTYPE | DATATYPE;
annotationSeperator:		COLON | SEMICOLON | COMA | DOT | HASH;
protocol:					IDENTIFIER | PROTECTION | BLOCKTYPE | DATATYPE;
location:					IDENTIFIER | PROTECTION | BLOCKTYPE | DATATYPE;
startQuote:					QUOTE;
endQuote:					QUOTE;


/// Lauka definīcijas shēma
fieldDefinition:			(attributeDefinition methodDefinition? | attributeDefinition? methodDefinition);

attributeDefinition:		(fieldProtection? attribute | fieldProtection attribute?);
methodDefinition:			BRACKETOPEN arguments BRACKETCLOSE;

attribute:					(fieldDataType? fieldName | fieldDataType fieldName?);
arguments:					(coma | argument)*;

argument:					(argumentDataType? argumentName | argumentDataType argumentName?);

fieldProtection:			PROTECTION;
fieldDataType:				DATATYPE | BLOCKTYPE;
fieldName:					IDENTIFIER;
argumentDataType:			DATATYPE | BLOCKTYPE | PROTECTION;
argumentName:				IDENTIFIER;
coma:						COMA;