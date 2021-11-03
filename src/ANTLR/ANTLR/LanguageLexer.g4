lexer grammar LanguageLexer;


/// Iekavas
CURLYOPEN:			'{';
CURLYCLOSE:			'}';
BRACKETOPEN:		'(';
BRACKETCLOSE:		')';
SQUAREOPEN:			'[';
SQUARECLOSE:		']';

/// Citi simboli
SEMICOLON:			';';
COLON:				':';
QUOTE:				'"';
DOT:				'.';
COMA:				',';
HASH:				'#';
ARROWS:				'<' ('-')* '>' | '<>' ('-')*;

/// Rezervetie vardi
CLASS:				'class';
ASSOCIATION:		'association';
URL:				'URL';
PUBLIC:				'public';
PRIVATE:			'private';
TYPE:				'Integer' | 'String' | 'Boolean' | 'Real';

/// Dazads
NAME:				[_a-zA-Z]+[_a-zA-Z0-9]*;		/// lauku vardi
WS :				[ \t\f\r\n] -> channel(HIDDEN); /// tuksumi
ANYEXCEPTQUOTE:		~[\"]*;							/// Jebkada simbolu virkne, kura neietilpst ".