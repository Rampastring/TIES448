// Generated from /home/jose/Programming/TIES448/Hassembler/Hassembler/ANTLR4/Haskellmm.g4 by ANTLR 4.7.1
import org.antlr.v4.runtime.Lexer;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenStream;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.misc.*;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class HaskellmmLexer extends Lexer {
	static { RuntimeMetaData.checkVersion("4.7.1", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, C_IND=14, IF=15, THEN=16, ELSE=17, 
		NEWLINE=18, INT=19, BOOL=20, CHAR=21, R_VAR=22, WS=23;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	public static final String[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
		"T__9", "T__10", "T__11", "T__12", "C_IND", "IF", "THEN", "ELSE", "NEWLINE", 
		"INT", "BOOL", "CHAR", "R_VAR", "WS"
	};

	private static final String[] _LITERAL_NAMES = {
		null, "'('", "')'", "'*'", "'/'", "'+'", "'-'", "'<'", "'<='", "'=='", 
		"'!='", "'>='", "'>'", "'='", null, "'if'", "'then'", "'else'"
	};
	private static final String[] _SYMBOLIC_NAMES = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, "C_IND", "IF", "THEN", "ELSE", "NEWLINE", "INT", "BOOL", "CHAR", 
		"R_VAR", "WS"
	};
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}


	public HaskellmmLexer(CharStream input) {
		super(input);
		_interp = new LexerATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@Override
	public String getGrammarFileName() { return "Haskellmm.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public String[] getChannelNames() { return channelNames; }

	@Override
	public String[] getModeNames() { return modeNames; }

	@Override
	public ATN getATN() { return _ATN; }

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2\31\u0090\b\1\4\2"+
		"\t\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4"+
		"\13\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22"+
		"\t\22\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\3\2"+
		"\3\2\3\3\3\3\3\4\3\4\3\5\3\5\3\6\3\6\3\7\3\7\3\b\3\b\3\t\3\t\3\t\3\n\3"+
		"\n\3\n\3\13\3\13\3\13\3\f\3\f\3\f\3\r\3\r\3\16\3\16\3\17\3\17\3\17\3\17"+
		"\7\17T\n\17\f\17\16\17W\13\17\3\17\3\17\3\17\3\17\3\20\3\20\3\20\3\21"+
		"\3\21\3\21\3\21\3\21\3\22\3\22\3\22\3\22\3\22\3\23\6\23k\n\23\r\23\16"+
		"\23l\3\24\6\24p\n\24\r\24\16\24q\3\25\3\25\3\25\3\25\3\25\3\25\3\25\3"+
		"\25\3\25\5\25}\n\25\3\26\3\26\3\26\3\26\3\27\3\27\7\27\u0085\n\27\f\27"+
		"\16\27\u0088\13\27\3\30\6\30\u008b\n\30\r\30\16\30\u008c\3\30\3\30\3U"+
		"\2\31\3\3\5\4\7\5\t\6\13\7\r\b\17\t\21\n\23\13\25\f\27\r\31\16\33\17\35"+
		"\20\37\21!\22#\23%\24\'\25)\26+\27-\30/\31\3\2\t\4\2\f\f\17\17\3\2\62"+
		";\3\2))\4\2C\\c|\3\2c|\6\2))\62;C\\c|\5\2\13\f\17\17\"\"\2\u0095\2\3\3"+
		"\2\2\2\2\5\3\2\2\2\2\7\3\2\2\2\2\t\3\2\2\2\2\13\3\2\2\2\2\r\3\2\2\2\2"+
		"\17\3\2\2\2\2\21\3\2\2\2\2\23\3\2\2\2\2\25\3\2\2\2\2\27\3\2\2\2\2\31\3"+
		"\2\2\2\2\33\3\2\2\2\2\35\3\2\2\2\2\37\3\2\2\2\2!\3\2\2\2\2#\3\2\2\2\2"+
		"%\3\2\2\2\2\'\3\2\2\2\2)\3\2\2\2\2+\3\2\2\2\2-\3\2\2\2\2/\3\2\2\2\3\61"+
		"\3\2\2\2\5\63\3\2\2\2\7\65\3\2\2\2\t\67\3\2\2\2\139\3\2\2\2\r;\3\2\2\2"+
		"\17=\3\2\2\2\21?\3\2\2\2\23B\3\2\2\2\25E\3\2\2\2\27H\3\2\2\2\31K\3\2\2"+
		"\2\33M\3\2\2\2\35O\3\2\2\2\37\\\3\2\2\2!_\3\2\2\2#d\3\2\2\2%j\3\2\2\2"+
		"\'o\3\2\2\2)|\3\2\2\2+~\3\2\2\2-\u0082\3\2\2\2/\u008a\3\2\2\2\61\62\7"+
		"*\2\2\62\4\3\2\2\2\63\64\7+\2\2\64\6\3\2\2\2\65\66\7,\2\2\66\b\3\2\2\2"+
		"\678\7\61\2\28\n\3\2\2\29:\7-\2\2:\f\3\2\2\2;<\7/\2\2<\16\3\2\2\2=>\7"+
		">\2\2>\20\3\2\2\2?@\7>\2\2@A\7?\2\2A\22\3\2\2\2BC\7?\2\2CD\7?\2\2D\24"+
		"\3\2\2\2EF\7#\2\2FG\7?\2\2G\26\3\2\2\2HI\7@\2\2IJ\7?\2\2J\30\3\2\2\2K"+
		"L\7@\2\2L\32\3\2\2\2MN\7?\2\2N\34\3\2\2\2OP\7/\2\2PQ\7/\2\2QU\3\2\2\2"+
		"RT\13\2\2\2SR\3\2\2\2TW\3\2\2\2UV\3\2\2\2US\3\2\2\2VX\3\2\2\2WU\3\2\2"+
		"\2XY\5%\23\2YZ\3\2\2\2Z[\b\17\2\2[\36\3\2\2\2\\]\7k\2\2]^\7h\2\2^ \3\2"+
		"\2\2_`\7v\2\2`a\7j\2\2ab\7g\2\2bc\7p\2\2c\"\3\2\2\2de\7g\2\2ef\7n\2\2"+
		"fg\7u\2\2gh\7g\2\2h$\3\2\2\2ik\t\2\2\2ji\3\2\2\2kl\3\2\2\2lj\3\2\2\2l"+
		"m\3\2\2\2m&\3\2\2\2np\t\3\2\2on\3\2\2\2pq\3\2\2\2qo\3\2\2\2qr\3\2\2\2"+
		"r(\3\2\2\2st\7V\2\2tu\7t\2\2uv\7w\2\2v}\7g\2\2wx\7H\2\2xy\7c\2\2yz\7n"+
		"\2\2z{\7u\2\2{}\7g\2\2|s\3\2\2\2|w\3\2\2\2}*\3\2\2\2~\177\t\4\2\2\177"+
		"\u0080\t\5\2\2\u0080\u0081\t\4\2\2\u0081,\3\2\2\2\u0082\u0086\t\6\2\2"+
		"\u0083\u0085\t\7\2\2\u0084\u0083\3\2\2\2\u0085\u0088\3\2\2\2\u0086\u0084"+
		"\3\2\2\2\u0086\u0087\3\2\2\2\u0087.\3\2\2\2\u0088\u0086\3\2\2\2\u0089"+
		"\u008b\t\b\2\2\u008a\u0089\3\2\2\2\u008b\u008c\3\2\2\2\u008c\u008a\3\2"+
		"\2\2\u008c\u008d\3\2\2\2\u008d\u008e\3\2\2\2\u008e\u008f\b\30\2\2\u008f"+
		"\60\3\2\2\2\t\2Ulq|\u0086\u008c\3\b\2\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}