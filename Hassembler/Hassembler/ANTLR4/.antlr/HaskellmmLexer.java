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
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, IF=8, THEN=9, 
		ELSE=10, NEWLINE=11, INT=12, BOOL=13, CHAR=14, F_NAME=15, R_VAR=16, WS=17;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	public static final String[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "IF", "THEN", 
		"ELSE", "NEWLINE", "INT", "BOOL", "CHAR", "F_NAME", "R_VAR", "WS"
	};

	private static final String[] _LITERAL_NAMES = {
		null, "'('", "')'", "'*'", "'/'", "'+'", "'-'", "'='", "'if'", "'then'", 
		"'else'"
	};
	private static final String[] _SYMBOLIC_NAMES = {
		null, null, null, null, null, null, null, null, "IF", "THEN", "ELSE", 
		"NEWLINE", "INT", "BOOL", "CHAR", "F_NAME", "R_VAR", "WS"
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
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2\23n\b\1\4\2\t\2\4"+
		"\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13\t"+
		"\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\3\2\3\2\3\3\3\3\3\4\3\4\3\5\3\5\3\6\3\6\3\7\3\7\3\b\3\b\3\t\3\t\3\t\3"+
		"\n\3\n\3\n\3\n\3\n\3\13\3\13\3\13\3\13\3\13\3\f\6\fB\n\f\r\f\16\fC\3\r"+
		"\6\rG\n\r\r\r\16\rH\3\16\3\16\3\16\3\16\3\16\3\16\3\16\3\16\3\16\5\16"+
		"T\n\16\3\17\3\17\3\17\3\17\3\20\3\20\7\20\\\n\20\f\20\16\20_\13\20\3\21"+
		"\3\21\7\21c\n\21\f\21\16\21f\13\21\3\22\6\22i\n\22\r\22\16\22j\3\22\3"+
		"\22\2\2\23\3\3\5\4\7\5\t\6\13\7\r\b\17\t\21\n\23\13\25\f\27\r\31\16\33"+
		"\17\35\20\37\21!\22#\23\3\2\t\4\2\f\f\17\17\3\2\62;\3\2))\4\2C\\c|\3\2"+
		"c|\6\2))\62;C\\c|\5\2\13\f\17\17\"\"\2s\2\3\3\2\2\2\2\5\3\2\2\2\2\7\3"+
		"\2\2\2\2\t\3\2\2\2\2\13\3\2\2\2\2\r\3\2\2\2\2\17\3\2\2\2\2\21\3\2\2\2"+
		"\2\23\3\2\2\2\2\25\3\2\2\2\2\27\3\2\2\2\2\31\3\2\2\2\2\33\3\2\2\2\2\35"+
		"\3\2\2\2\2\37\3\2\2\2\2!\3\2\2\2\2#\3\2\2\2\3%\3\2\2\2\5\'\3\2\2\2\7)"+
		"\3\2\2\2\t+\3\2\2\2\13-\3\2\2\2\r/\3\2\2\2\17\61\3\2\2\2\21\63\3\2\2\2"+
		"\23\66\3\2\2\2\25;\3\2\2\2\27A\3\2\2\2\31F\3\2\2\2\33S\3\2\2\2\35U\3\2"+
		"\2\2\37Y\3\2\2\2!`\3\2\2\2#h\3\2\2\2%&\7*\2\2&\4\3\2\2\2\'(\7+\2\2(\6"+
		"\3\2\2\2)*\7,\2\2*\b\3\2\2\2+,\7\61\2\2,\n\3\2\2\2-.\7-\2\2.\f\3\2\2\2"+
		"/\60\7/\2\2\60\16\3\2\2\2\61\62\7?\2\2\62\20\3\2\2\2\63\64\7k\2\2\64\65"+
		"\7h\2\2\65\22\3\2\2\2\66\67\7v\2\2\678\7j\2\289\7g\2\29:\7p\2\2:\24\3"+
		"\2\2\2;<\7g\2\2<=\7n\2\2=>\7u\2\2>?\7g\2\2?\26\3\2\2\2@B\t\2\2\2A@\3\2"+
		"\2\2BC\3\2\2\2CA\3\2\2\2CD\3\2\2\2D\30\3\2\2\2EG\t\3\2\2FE\3\2\2\2GH\3"+
		"\2\2\2HF\3\2\2\2HI\3\2\2\2I\32\3\2\2\2JK\7V\2\2KL\7t\2\2LM\7w\2\2MT\7"+
		"g\2\2NO\7H\2\2OP\7c\2\2PQ\7n\2\2QR\7u\2\2RT\7g\2\2SJ\3\2\2\2SN\3\2\2\2"+
		"T\34\3\2\2\2UV\t\4\2\2VW\t\5\2\2WX\t\4\2\2X\36\3\2\2\2Y]\t\6\2\2Z\\\t"+
		"\7\2\2[Z\3\2\2\2\\_\3\2\2\2][\3\2\2\2]^\3\2\2\2^ \3\2\2\2_]\3\2\2\2`d"+
		"\t\6\2\2ac\t\7\2\2ba\3\2\2\2cf\3\2\2\2db\3\2\2\2de\3\2\2\2e\"\3\2\2\2"+
		"fd\3\2\2\2gi\t\b\2\2hg\3\2\2\2ij\3\2\2\2jh\3\2\2\2jk\3\2\2\2kl\3\2\2\2"+
		"lm\b\22\2\2m$\3\2\2\2\t\2CHS]dj\3\b\2\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}