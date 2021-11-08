parser grammar LanguageParser;	/// Parsera gramatikas nosaukums


options 
{ 
	tokenVocab=LanguageLexer; /// Norādam, ka vēl tiks izmantota gramatika ar nosaukumu LanguageLexer
}

/// Sākuma punkts
code:					( blocks | association | classBody)*;

blocks:					blockType blockBody?;
blockBody:				(webMemoryClass | association);

blockType:				NAME | PROTECTION;

/// webMemoryClass likumi
webMemoryClass:			classHead? classBody?;
classHead:				className superClass;
superClass:				COLON? superClassName*;
classBody:				CURLYOPEN fields* CURLYCLOSE;

className:				NAME | PROTECTION;
superClassName:			NAME | PROTECTION;


/// Lauku (mainīgo un funkciju) likumi
fields:					field? SEMICOLON;
field:					annotation* fieldDefinition;

fieldDefinition:		fieldProtection? fieldDataType? fieldName? methodDefinition?;
methodDefinition:		BRACKETOPEN arguments BRACKETCLOSE;

fieldProtection:		PROTECTION;
fieldDataType:			NAME | PROTECTION;
fieldName:				NAME | PROTECTION;


/// Anotaciju likumi
annotation:				SQUAREOPEN annotationType? BRACKETOPEN? startQuote? annotationDefinition endQuote? BRACKETCLOSE? SQUARECLOSE;
annotationDefinition:	urlAttributes? annotationAttributes;
annotationAttributes:	( annotationSeperator | annotationData )*;
urlAttributes:			protocol? COLON location? COLON;

annotationSeperator:	(COLON | HASH | DOT);
annotationData:			NAME | PROTECTION;
annotationType:			NAME | PROTECTION;
protocol:				NAME | PROTECTION;
location:				NAME | PROTECTION;
startQuote:				QUOTE;
endQuote:				QUOTE;


/// Argumentu likumi
arguments:				(coma | argument)*;
argument:				argumentDataType argumentName?;

argumentDataType:		NAME | PROTECTION;
argumentName:			NAME | PROTECTION;
coma:					COMA;


/// Asociaciju likumi
association:			BRACKETOPEN associationDefinition BRACKETCLOSE;
associationDefinition:	source ARROWS? target;
source:					associationSourceName? COLON? sourceClass?; 
target:					associationTargetName? COLON? targetClass?;


associationType:		NAME | PROTECTION;
associationSourceName:	NAME | PROTECTION;
associationTargetName:	NAME | PROTECTION;
sourceClass:			NAME | PROTECTION;
targetClass:			NAME | PROTECTION;