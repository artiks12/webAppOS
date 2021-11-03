﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\Artis\source\repos\ANTLR\ANTLR\LanguageLexer.g4 by ANTLR 4.6.6

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace ANTLR {
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
		ARROWS=13, CLASS=14, ASSOCIATION=15, URL=16, PUBLIC=17, PRIVATE=18, TYPE=19, 
		NAME=20, WS=21, ANYEXCEPTQUOTE=22;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"CURLYOPEN", "CURLYCLOSE", "BRACKETOPEN", "BRACKETCLOSE", "SQUAREOPEN", 
		"SQUARECLOSE", "SEMICOLON", "COLON", "QUOTE", "DOT", "COMA", "HASH", "ARROWS", 
		"CLASS", "ASSOCIATION", "URL", "PUBLIC", "PRIVATE", "TYPE", "NAME", "WS", 
		"ANYEXCEPTQUOTE"
	};


	public LanguageLexer(ICharStream input)
		: base(input)
	{
		_interp = new LexerATNSimulator(this,_ATN);
	}

	private static readonly string[] _LiteralNames = {
		null, "'{'", "'}'", "'('", "')'", "'['", "']'", "';'", "':'", "'\"'", 
		"'.'", "','", "'#'", null, "'class'", "'association'", "'URL'", "'public'", 
		"'private'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "CURLYOPEN", "CURLYCLOSE", "BRACKETOPEN", "BRACKETCLOSE", "SQUAREOPEN", 
		"SQUARECLOSE", "SEMICOLON", "COLON", "QUOTE", "DOT", "COMA", "HASH", "ARROWS", 
		"CLASS", "ASSOCIATION", "URL", "PUBLIC", "PRIVATE", "TYPE", "NAME", "WS", 
		"ANYEXCEPTQUOTE"
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
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2\x18\xAE\b\x1\x4"+
		"\x2\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b"+
		"\x4\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4"+
		"\x10\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15"+
		"\t\x15\x4\x16\t\x16\x4\x17\t\x17\x3\x2\x3\x2\x3\x3\x3\x3\x3\x4\x3\x4\x3"+
		"\x5\x3\x5\x3\x6\x3\x6\x3\a\x3\a\x3\b\x3\b\x3\t\x3\t\x3\n\x3\n\x3\v\x3"+
		"\v\x3\f\x3\f\x3\r\x3\r\x3\xE\x3\xE\a\xEJ\n\xE\f\xE\xE\xEM\v\xE\x3\xE\x3"+
		"\xE\x3\xE\x3\xE\x3\xE\a\xET\n\xE\f\xE\xE\xEW\v\xE\x5\xEY\n\xE\x3\xF\x3"+
		"\xF\x3\xF\x3\xF\x3\xF\x3\xF\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10"+
		"\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x11\x3\x11\x3\x11\x3\x11"+
		"\x3\x12\x3\x12\x3\x12\x3\x12\x3\x12\x3\x12\x3\x12\x3\x13\x3\x13\x3\x13"+
		"\x3\x13\x3\x13\x3\x13\x3\x13\x3\x13\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14"+
		"\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14"+
		"\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14\x5\x14"+
		"\x98\n\x14\x3\x15\x6\x15\x9B\n\x15\r\x15\xE\x15\x9C\x3\x15\a\x15\xA0\n"+
		"\x15\f\x15\xE\x15\xA3\v\x15\x3\x16\x3\x16\x3\x16\x3\x16\x3\x17\a\x17\xAA"+
		"\n\x17\f\x17\xE\x17\xAD\v\x17\x2\x2\x2\x18\x3\x2\x3\x5\x2\x4\a\x2\x5\t"+
		"\x2\x6\v\x2\a\r\x2\b\xF\x2\t\x11\x2\n\x13\x2\v\x15\x2\f\x17\x2\r\x19\x2"+
		"\xE\x1B\x2\xF\x1D\x2\x10\x1F\x2\x11!\x2\x12#\x2\x13%\x2\x14\'\x2\x15)"+
		"\x2\x16+\x2\x17-\x2\x18\x3\x2\x6\x5\x2\x43\\\x61\x61\x63|\x6\x2\x32;\x43"+
		"\\\x61\x61\x63|\x5\x2\v\f\xE\xF\"\"\x3\x2$$\xB6\x2\x3\x3\x2\x2\x2\x2\x5"+
		"\x3\x2\x2\x2\x2\a\x3\x2\x2\x2\x2\t\x3\x2\x2\x2\x2\v\x3\x2\x2\x2\x2\r\x3"+
		"\x2\x2\x2\x2\xF\x3\x2\x2\x2\x2\x11\x3\x2\x2\x2\x2\x13\x3\x2\x2\x2\x2\x15"+
		"\x3\x2\x2\x2\x2\x17\x3\x2\x2\x2\x2\x19\x3\x2\x2\x2\x2\x1B\x3\x2\x2\x2"+
		"\x2\x1D\x3\x2\x2\x2\x2\x1F\x3\x2\x2\x2\x2!\x3\x2\x2\x2\x2#\x3\x2\x2\x2"+
		"\x2%\x3\x2\x2\x2\x2\'\x3\x2\x2\x2\x2)\x3\x2\x2\x2\x2+\x3\x2\x2\x2\x2-"+
		"\x3\x2\x2\x2\x3/\x3\x2\x2\x2\x5\x31\x3\x2\x2\x2\a\x33\x3\x2\x2\x2\t\x35"+
		"\x3\x2\x2\x2\v\x37\x3\x2\x2\x2\r\x39\x3\x2\x2\x2\xF;\x3\x2\x2\x2\x11="+
		"\x3\x2\x2\x2\x13?\x3\x2\x2\x2\x15\x41\x3\x2\x2\x2\x17\x43\x3\x2\x2\x2"+
		"\x19\x45\x3\x2\x2\x2\x1BX\x3\x2\x2\x2\x1DZ\x3\x2\x2\x2\x1F`\x3\x2\x2\x2"+
		"!l\x3\x2\x2\x2#p\x3\x2\x2\x2%w\x3\x2\x2\x2\'\x97\x3\x2\x2\x2)\x9A\x3\x2"+
		"\x2\x2+\xA4\x3\x2\x2\x2-\xAB\x3\x2\x2\x2/\x30\a}\x2\x2\x30\x4\x3\x2\x2"+
		"\x2\x31\x32\a\x7F\x2\x2\x32\x6\x3\x2\x2\x2\x33\x34\a*\x2\x2\x34\b\x3\x2"+
		"\x2\x2\x35\x36\a+\x2\x2\x36\n\x3\x2\x2\x2\x37\x38\a]\x2\x2\x38\f\x3\x2"+
		"\x2\x2\x39:\a_\x2\x2:\xE\x3\x2\x2\x2;<\a=\x2\x2<\x10\x3\x2\x2\x2=>\a<"+
		"\x2\x2>\x12\x3\x2\x2\x2?@\a$\x2\x2@\x14\x3\x2\x2\x2\x41\x42\a\x30\x2\x2"+
		"\x42\x16\x3\x2\x2\x2\x43\x44\a.\x2\x2\x44\x18\x3\x2\x2\x2\x45\x46\a%\x2"+
		"\x2\x46\x1A\x3\x2\x2\x2GK\a>\x2\x2HJ\a/\x2\x2IH\x3\x2\x2\x2JM\x3\x2\x2"+
		"\x2KI\x3\x2\x2\x2KL\x3\x2\x2\x2LN\x3\x2\x2\x2MK\x3\x2\x2\x2NY\a@\x2\x2"+
		"OP\a>\x2\x2PQ\a@\x2\x2QU\x3\x2\x2\x2RT\a/\x2\x2SR\x3\x2\x2\x2TW\x3\x2"+
		"\x2\x2US\x3\x2\x2\x2UV\x3\x2\x2\x2VY\x3\x2\x2\x2WU\x3\x2\x2\x2XG\x3\x2"+
		"\x2\x2XO\x3\x2\x2\x2Y\x1C\x3\x2\x2\x2Z[\a\x65\x2\x2[\\\an\x2\x2\\]\a\x63"+
		"\x2\x2]^\au\x2\x2^_\au\x2\x2_\x1E\x3\x2\x2\x2`\x61\a\x63\x2\x2\x61\x62"+
		"\au\x2\x2\x62\x63\au\x2\x2\x63\x64\aq\x2\x2\x64\x65\a\x65\x2\x2\x65\x66"+
		"\ak\x2\x2\x66g\a\x63\x2\x2gh\av\x2\x2hi\ak\x2\x2ij\aq\x2\x2jk\ap\x2\x2"+
		"k \x3\x2\x2\x2lm\aW\x2\x2mn\aT\x2\x2no\aN\x2\x2o\"\x3\x2\x2\x2pq\ar\x2"+
		"\x2qr\aw\x2\x2rs\a\x64\x2\x2st\an\x2\x2tu\ak\x2\x2uv\a\x65\x2\x2v$\x3"+
		"\x2\x2\x2wx\ar\x2\x2xy\at\x2\x2yz\ak\x2\x2z{\ax\x2\x2{|\a\x63\x2\x2|}"+
		"\av\x2\x2}~\ag\x2\x2~&\x3\x2\x2\x2\x7F\x80\aK\x2\x2\x80\x81\ap\x2\x2\x81"+
		"\x82\av\x2\x2\x82\x83\ag\x2\x2\x83\x84\ai\x2\x2\x84\x85\ag\x2\x2\x85\x98"+
		"\at\x2\x2\x86\x87\aU\x2\x2\x87\x88\av\x2\x2\x88\x89\at\x2\x2\x89\x8A\a"+
		"k\x2\x2\x8A\x8B\ap\x2\x2\x8B\x98\ai\x2\x2\x8C\x8D\a\x44\x2\x2\x8D\x8E"+
		"\aq\x2\x2\x8E\x8F\aq\x2\x2\x8F\x90\an\x2\x2\x90\x91\ag\x2\x2\x91\x92\a"+
		"\x63\x2\x2\x92\x98\ap\x2\x2\x93\x94\aT\x2\x2\x94\x95\ag\x2\x2\x95\x96"+
		"\a\x63\x2\x2\x96\x98\an\x2\x2\x97\x7F\x3\x2\x2\x2\x97\x86\x3\x2\x2\x2"+
		"\x97\x8C\x3\x2\x2\x2\x97\x93\x3\x2\x2\x2\x98(\x3\x2\x2\x2\x99\x9B\t\x2"+
		"\x2\x2\x9A\x99\x3\x2\x2\x2\x9B\x9C\x3\x2\x2\x2\x9C\x9A\x3\x2\x2\x2\x9C"+
		"\x9D\x3\x2\x2\x2\x9D\xA1\x3\x2\x2\x2\x9E\xA0\t\x3\x2\x2\x9F\x9E\x3\x2"+
		"\x2\x2\xA0\xA3\x3\x2\x2\x2\xA1\x9F\x3\x2\x2\x2\xA1\xA2\x3\x2\x2\x2\xA2"+
		"*\x3\x2\x2\x2\xA3\xA1\x3\x2\x2\x2\xA4\xA5\t\x4\x2\x2\xA5\xA6\x3\x2\x2"+
		"\x2\xA6\xA7\b\x16\x2\x2\xA7,\x3\x2\x2\x2\xA8\xAA\n\x5\x2\x2\xA9\xA8\x3"+
		"\x2\x2\x2\xAA\xAD\x3\x2\x2\x2\xAB\xA9\x3\x2\x2\x2\xAB\xAC\x3\x2\x2\x2"+
		"\xAC.\x3\x2\x2\x2\xAD\xAB\x3\x2\x2\x2\n\x2KUX\x97\x9C\xA1\xAB\x3\x2\x3"+
		"\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace ANTLR
