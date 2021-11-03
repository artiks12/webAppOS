parser grammar LanguageParser;	/// Parsera gramatikas nosaukums


options 
{ 
	tokenVocab=LanguageLexer; /// Norādam, ka vēl tiks izmantota gramatika ar nosaukumu LanguageLexer
}

/// Sākuma punkts
code:					(webMemoryClass | associations)*;


/// webMemoryClass likumi
webMemoryClass:			classType className superClass CURLYOPEN classBody CURLYCLOSE;
superClass:				(COLON superClassName*)?;
classBody:				((variable | method)? SEMICOLON)*;

classType:				CLASS | NAME;
className:				NAME;
superClassName:			NAME;

/// Metodes likumi
method:					url methodAnnotation* methodDefinition;


/// URL likumi
url:					SQUAREOPEN urlType BRACKETOPEN QUOTE urlDefinition QUOTE BRACKETCLOSE SQUARECLOSE;
urlDefinition:			protocolName COLON location COLON methodPath; 

urlType:				URL | NAME;
protocolName:			NAME;
location:				NAME;
methodPath:				NAME;


/// Metodes anotacijas likumi
methodAnnotation:		SQUAREOPEN annotationType BRACKETOPEN QUOTE annotationDefinition QUOTE BRACKETCLOSE SQUARECLOSE;	
annotationType:			NAME;
annotationDefinition:	ANYEXCEPTQUOTE;		/// Any string except ".


/// Metodes definicijas likumi
methodDefinition:		methodProtection? methodDataType methodName BRACKETOPEN arguments* BRACKETCLOSE;

methodProtection:		PUBLIC | PRIVATE;
methodDataType:			TYPE | NAME;
methodName:				NAME;


/// Argumentu likumi
arguments:				(argument (COMA argument)*);
argument:				argumentDataType argumentName;

argumentDataType:		TYPE | NAME;
argumentName:			NAME;

/// Mainigo likumi
variable:				variableProtection? variableDataType variableName;

variableProtection:		PUBLIC | PRIVATE;
variableDataType:		TYPE | NAME;
variableName:			NAME;


/// Asociaciju likumi
associations:			associationType associationSourceName COLON sourceClass ARROWS associationTargetName COLON targetClass SEMICOLON;

associationType:		ASSOCIATION | NAME;
associationSourceName:	NAME;
associationTargetName:	NAME;
sourceClass:			NAME;
targetClass:			NAME;