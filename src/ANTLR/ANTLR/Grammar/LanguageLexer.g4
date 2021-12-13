lexer grammar LanguageLexer;

/// Iekavas
CURLYOPEN:			'{';
CURLYCLOSE:			'}';
BRACKETOPEN:		'(';
BRACKETCLOSE:		')';
SQUAREOPEN:			'[';
SQUARECLOSE:		']';

/// Citi simboli
SEMICOLON:				';';
COLON:					':';
QUOTE:					'"';
DOT:					'.';
COMA:					',';
HASH:					'#';
ARROWS:					'<' ('-')+ '>' | '<>' ('-')+;

/// Atslēgvārdi
PROTECTION:				'public' | 'private';
BLOCKTYPE:				'class' | 'association';
DATATYPE:				'Integer' | 'String' | 'Boolean' | 'Real' | 'Void';

/// Dažādi
IDENTIFIER:				[_a-zA-Z]+[_a-zA-Z0-9]*; /// Identifikatori
WS:						[ \t\f\r\n] -> channel(HIDDEN); /// Tukšumi
ANYTHING:				~[ \t\f\r\n"EOF;:.#{}()',[\]<->]+; /// Anotāciju datiem veidota patvaļīga simbolu virkne