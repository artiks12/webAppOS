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


/// Dazads
PROTECTION:				'public' | 'private';
NAME:					[_a-zA-Z]+[_a-zA-Z0-9]*;		/// lauku vardi
WS :					[ \t\f\r\n] -> channel(HIDDEN); /// tuksumi
ANYTHING:				~[ \t\f\r\n"EOF;:.#{}()',[\]<->]+;