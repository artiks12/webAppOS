//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\Artis\Documents\GitHub\webAppOS\src\Qualification project\Testing\Testers\GrammarTester\GrammarTester\Grammar\LanguageLexer.g4 by ANTLR 4.6.6

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace GrammarTester.Grammar {
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public partial class LanguageLexer : Lexer {
	public const int
		CURLYOPEN=1, CURLYCLOSE=2, BRACKETOPEN=3, BRACKETCLOSE=4, SQUAREOPEN=5, 
		SQUARECLOSE=6, SEMICOLON=7, COLON=8, QUOTE=9, DOT=10, COMA=11, HASH=12, 
		ARROWS=13, PROTECTION=14, BLOCKTYPE=15, DATATYPE=16, IDENTIFIER=17, WS=18, 
		ANYTHING=19;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"CURLYOPEN", "CURLYCLOSE", "BRACKETOPEN", "BRACKETCLOSE", "SQUAREOPEN", 
		"SQUARECLOSE", "SEMICOLON", "COLON", "QUOTE", "DOT", "COMA", "HASH", "ARROWS", 
		"PROTECTION", "BLOCKTYPE", "DATATYPE", "IDENTIFIER", "WS", "ANYTHING"
	};


	public LanguageLexer(ICharStream input)
		: base(input)
	{
		_interp = new LexerATNSimulator(this,_ATN);
	}

	private static readonly string[] _LiteralNames = {
		null, "'{'", "'}'", "'('", "')'", "'['", "']'", "';'", "':'", "'\"'", 
		"'.'", "','", "'#'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "CURLYOPEN", "CURLYCLOSE", "BRACKETOPEN", "BRACKETCLOSE", "SQUAREOPEN", 
		"SQUARECLOSE", "SEMICOLON", "COLON", "QUOTE", "DOT", "COMA", "HASH", "ARROWS", 
		"PROTECTION", "BLOCKTYPE", "DATATYPE", "IDENTIFIER", "WS", "ANYTHING"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[System.Obsolete("Use Vocabulary instead.")]
	public static readonly string[] tokenNames = GenerateTokenNames(DefaultVocabulary, _SymbolicNames.Length);

	private static string[] GenerateTokenNames(IVocabulary vocabulary, int length) {
		string[] tokenNames = new string[length];
		for (int i = 0; i < tokenNames.Length; i++) {
			tokenNames[i] = vocabulary.GetLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = vocabulary.GetSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}

		return tokenNames;
	}

	[System.Obsolete("Use IRecognizer.Vocabulary instead.")]
	public override string[] TokenNames
	{
		get
		{
			return tokenNames;
		}
	}

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "LanguageLexer.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	public static readonly string _serializedATN =
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2\x15\xA5\b\x1\x4"+
		"\x2\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b"+
		"\x4\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4"+
		"\x10\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x3\x2\x3"+
		"\x2\x3\x3\x3\x3\x3\x4\x3\x4\x3\x5\x3\x5\x3\x6\x3\x6\x3\a\x3\a\x3\b\x3"+
		"\b\x3\t\x3\t\x3\n\x3\n\x3\v\x3\v\x3\f\x3\f\x3\r\x3\r\x3\xE\x3\xE\x6\xE"+
		"\x44\n\xE\r\xE\xE\xE\x45\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x6\xEM\n\xE\r\xE"+
		"\xE\xEN\x5\xEQ\n\xE\x3\xF\x3\xF\x3\xF\x3\xF\x3\xF\x3\xF\x3\xF\x3\xF\x3"+
		"\xF\x3\xF\x3\xF\x3\xF\x3\xF\x5\xF`\n\xF\x3\x10\x3\x10\x3\x10\x3\x10\x3"+
		"\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3"+
		"\x10\x3\x10\x5\x10r\n\x10\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3"+
		"\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3"+
		"\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3"+
		"\x11\x3\x11\x5\x11\x90\n\x11\x3\x12\x6\x12\x93\n\x12\r\x12\xE\x12\x94"+
		"\x3\x12\a\x12\x98\n\x12\f\x12\xE\x12\x9B\v\x12\x3\x13\x3\x13\x3\x13\x3"+
		"\x13\x3\x14\x6\x14\xA2\n\x14\r\x14\xE\x14\xA3\x2\x2\x2\x15\x3\x2\x3\x5"+
		"\x2\x4\a\x2\x5\t\x2\x6\v\x2\a\r\x2\b\xF\x2\t\x11\x2\n\x13\x2\v\x15\x2"+
		"\f\x17\x2\r\x19\x2\xE\x1B\x2\xF\x1D\x2\x10\x1F\x2\x11!\x2\x12#\x2\x13"+
		"%\x2\x14\'\x2\x15\x3\x2\x6\x5\x2\x43\\\x61\x61\x63|\x6\x2\x32;\x43\\\x61"+
		"\x61\x63|\x5\x2\v\f\xE\xF\"\"\x10\x2\v\f\xE\xF\"\"$%)+..\x30\x30<@GHQ"+
		"Q]]__}}\x7F\x7F\xB0\x2\x3\x3\x2\x2\x2\x2\x5\x3\x2\x2\x2\x2\a\x3\x2\x2"+
		"\x2\x2\t\x3\x2\x2\x2\x2\v\x3\x2\x2\x2\x2\r\x3\x2\x2\x2\x2\xF\x3\x2\x2"+
		"\x2\x2\x11\x3\x2\x2\x2\x2\x13\x3\x2\x2\x2\x2\x15\x3\x2\x2\x2\x2\x17\x3"+
		"\x2\x2\x2\x2\x19\x3\x2\x2\x2\x2\x1B\x3\x2\x2\x2\x2\x1D\x3\x2\x2\x2\x2"+
		"\x1F\x3\x2\x2\x2\x2!\x3\x2\x2\x2\x2#\x3\x2\x2\x2\x2%\x3\x2\x2\x2\x2\'"+
		"\x3\x2\x2\x2\x3)\x3\x2\x2\x2\x5+\x3\x2\x2\x2\a-\x3\x2\x2\x2\t/\x3\x2\x2"+
		"\x2\v\x31\x3\x2\x2\x2\r\x33\x3\x2\x2\x2\xF\x35\x3\x2\x2\x2\x11\x37\x3"+
		"\x2\x2\x2\x13\x39\x3\x2\x2\x2\x15;\x3\x2\x2\x2\x17=\x3\x2\x2\x2\x19?\x3"+
		"\x2\x2\x2\x1BP\x3\x2\x2\x2\x1D_\x3\x2\x2\x2\x1Fq\x3\x2\x2\x2!\x8F\x3\x2"+
		"\x2\x2#\x92\x3\x2\x2\x2%\x9C\x3\x2\x2\x2\'\xA1\x3\x2\x2\x2)*\a}\x2\x2"+
		"*\x4\x3\x2\x2\x2+,\a\x7F\x2\x2,\x6\x3\x2\x2\x2-.\a*\x2\x2.\b\x3\x2\x2"+
		"\x2/\x30\a+\x2\x2\x30\n\x3\x2\x2\x2\x31\x32\a]\x2\x2\x32\f\x3\x2\x2\x2"+
		"\x33\x34\a_\x2\x2\x34\xE\x3\x2\x2\x2\x35\x36\a=\x2\x2\x36\x10\x3\x2\x2"+
		"\x2\x37\x38\a<\x2\x2\x38\x12\x3\x2\x2\x2\x39:\a$\x2\x2:\x14\x3\x2\x2\x2"+
		";<\a\x30\x2\x2<\x16\x3\x2\x2\x2=>\a.\x2\x2>\x18\x3\x2\x2\x2?@\a%\x2\x2"+
		"@\x1A\x3\x2\x2\x2\x41\x43\a>\x2\x2\x42\x44\a/\x2\x2\x43\x42\x3\x2\x2\x2"+
		"\x44\x45\x3\x2\x2\x2\x45\x43\x3\x2\x2\x2\x45\x46\x3\x2\x2\x2\x46G\x3\x2"+
		"\x2\x2GQ\a@\x2\x2HI\a>\x2\x2IJ\a@\x2\x2JL\x3\x2\x2\x2KM\a/\x2\x2LK\x3"+
		"\x2\x2\x2MN\x3\x2\x2\x2NL\x3\x2\x2\x2NO\x3\x2\x2\x2OQ\x3\x2\x2\x2P\x41"+
		"\x3\x2\x2\x2PH\x3\x2\x2\x2Q\x1C\x3\x2\x2\x2RS\ar\x2\x2ST\aw\x2\x2TU\a"+
		"\x64\x2\x2UV\an\x2\x2VW\ak\x2\x2W`\a\x65\x2\x2XY\ar\x2\x2YZ\at\x2\x2Z"+
		"[\ak\x2\x2[\\\ax\x2\x2\\]\a\x63\x2\x2]^\av\x2\x2^`\ag\x2\x2_R\x3\x2\x2"+
		"\x2_X\x3\x2\x2\x2`\x1E\x3\x2\x2\x2\x61\x62\a\x65\x2\x2\x62\x63\an\x2\x2"+
		"\x63\x64\a\x63\x2\x2\x64\x65\au\x2\x2\x65r\au\x2\x2\x66g\a\x63\x2\x2g"+
		"h\au\x2\x2hi\au\x2\x2ij\aq\x2\x2jk\a\x65\x2\x2kl\ak\x2\x2lm\a\x63\x2\x2"+
		"mn\av\x2\x2no\ak\x2\x2op\aq\x2\x2pr\ap\x2\x2q\x61\x3\x2\x2\x2q\x66\x3"+
		"\x2\x2\x2r \x3\x2\x2\x2st\aK\x2\x2tu\ap\x2\x2uv\av\x2\x2vw\ag\x2\x2wx"+
		"\ai\x2\x2xy\ag\x2\x2y\x90\at\x2\x2z{\aU\x2\x2{|\av\x2\x2|}\at\x2\x2}~"+
		"\ak\x2\x2~\x7F\ap\x2\x2\x7F\x90\ai\x2\x2\x80\x81\a\x44\x2\x2\x81\x82\a"+
		"q\x2\x2\x82\x83\aq\x2\x2\x83\x84\an\x2\x2\x84\x85\ag\x2\x2\x85\x86\a\x63"+
		"\x2\x2\x86\x90\ap\x2\x2\x87\x88\aT\x2\x2\x88\x89\ag\x2\x2\x89\x8A\a\x63"+
		"\x2\x2\x8A\x90\an\x2\x2\x8B\x8C\aX\x2\x2\x8C\x8D\aq\x2\x2\x8D\x8E\ak\x2"+
		"\x2\x8E\x90\a\x66\x2\x2\x8Fs\x3\x2\x2\x2\x8Fz\x3\x2\x2\x2\x8F\x80\x3\x2"+
		"\x2\x2\x8F\x87\x3\x2\x2\x2\x8F\x8B\x3\x2\x2\x2\x90\"\x3\x2\x2\x2\x91\x93"+
		"\t\x2\x2\x2\x92\x91\x3\x2\x2\x2\x93\x94\x3\x2\x2\x2\x94\x92\x3\x2\x2\x2"+
		"\x94\x95\x3\x2\x2\x2\x95\x99\x3\x2\x2\x2\x96\x98\t\x3\x2\x2\x97\x96\x3"+
		"\x2\x2\x2\x98\x9B\x3\x2\x2\x2\x99\x97\x3\x2\x2\x2\x99\x9A\x3\x2\x2\x2"+
		"\x9A$\x3\x2\x2\x2\x9B\x99\x3\x2\x2\x2\x9C\x9D\t\x4\x2\x2\x9D\x9E\x3\x2"+
		"\x2\x2\x9E\x9F\b\x13\x2\x2\x9F&\x3\x2\x2\x2\xA0\xA2\n\x5\x2\x2\xA1\xA0"+
		"\x3\x2\x2\x2\xA2\xA3\x3\x2\x2\x2\xA3\xA1\x3\x2\x2\x2\xA3\xA4\x3\x2\x2"+
		"\x2\xA4(\x3\x2\x2\x2\f\x2\x45NP_q\x8F\x94\x99\xA3\x3\x2\x3\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace GrammarTester.Grammar
