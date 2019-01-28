// Generated from /home/jose/Programming/TIES448/Hassembler/Hassembler/ANTLR4/Haskellmm.g4 by ANTLR 4.7.1
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class HaskellmmParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.7.1", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, IF=14, THEN=15, ELSE=16, NEWLINE=17, 
		INT=18, BOOL=19, CHAR=20, R_VAR=21, WS=22;
	public static final int
		RULE_prog = 0, RULE_expr = 1, RULE_f_defi = 2, RULE_ite_defi = 3;
	public static final String[] ruleNames = {
		"prog", "expr", "f_defi", "ite_defi"
	};

	private static final String[] _LITERAL_NAMES = {
		null, "'('", "')'", "'*'", "'/'", "'+'", "'-'", "'<'", "'<='", "'=='", 
		"'!='", "'>='", "'>'", "'='", "'if'", "'then'", "'else'"
	};
	private static final String[] _SYMBOLIC_NAMES = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, "IF", "THEN", "ELSE", "NEWLINE", "INT", "BOOL", "CHAR", "R_VAR", 
		"WS"
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

	@Override
	public String getGrammarFileName() { return "Haskellmm.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public HaskellmmParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}
	public static class ProgContext extends ParserRuleContext {
		public List<F_defiContext> f_defi() {
			return getRuleContexts(F_defiContext.class);
		}
		public F_defiContext f_defi(int i) {
			return getRuleContext(F_defiContext.class,i);
		}
		public List<TerminalNode> NEWLINE() { return getTokens(HaskellmmParser.NEWLINE); }
		public TerminalNode NEWLINE(int i) {
			return getToken(HaskellmmParser.NEWLINE, i);
		}
		public ProgContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_prog; }
	}

	public final ProgContext prog() throws RecognitionException {
		ProgContext _localctx = new ProgContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_prog);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(17);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==R_VAR) {
				{
				{
				setState(8);
				f_defi();
				setState(12);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==NEWLINE) {
					{
					{
					setState(9);
					match(NEWLINE);
					}
					}
					setState(14);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				}
				}
				setState(19);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ExprContext extends ParserRuleContext {
		public ExprContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expr; }
	 
		public ExprContext() { }
		public void copyFrom(ExprContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class RefVarContext extends ExprContext {
		public List<TerminalNode> R_VAR() { return getTokens(HaskellmmParser.R_VAR); }
		public TerminalNode R_VAR(int i) {
			return getToken(HaskellmmParser.R_VAR, i);
		}
		public RefVarContext(ExprContext ctx) { copyFrom(ctx); }
	}
	public static class BoolVarContext extends ExprContext {
		public TerminalNode BOOL() { return getToken(HaskellmmParser.BOOL, 0); }
		public BoolVarContext(ExprContext ctx) { copyFrom(ctx); }
	}
	public static class IntVarContext extends ExprContext {
		public TerminalNode INT() { return getToken(HaskellmmParser.INT, 0); }
		public IntVarContext(ExprContext ctx) { copyFrom(ctx); }
	}
	public static class MultExpContext extends ExprContext {
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public MultExpContext(ExprContext ctx) { copyFrom(ctx); }
	}
	public static class CompExpContext extends ExprContext {
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public CompExpContext(ExprContext ctx) { copyFrom(ctx); }
	}
	public static class ParenExpContext extends ExprContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public ParenExpContext(ExprContext ctx) { copyFrom(ctx); }
	}
	public static class AddExpContext extends ExprContext {
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public AddExpContext(ExprContext ctx) { copyFrom(ctx); }
	}
	public static class IteExpContext extends ExprContext {
		public Ite_defiContext ite_defi() {
			return getRuleContext(Ite_defiContext.class,0);
		}
		public IteExpContext(ExprContext ctx) { copyFrom(ctx); }
	}

	public final ExprContext expr() throws RecognitionException {
		return expr(0);
	}

	private ExprContext expr(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		ExprContext _localctx = new ExprContext(_ctx, _parentState);
		ExprContext _prevctx = _localctx;
		int _startState = 2;
		enterRecursionRule(_localctx, 2, RULE_expr, _p);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(33);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__0:
				{
				_localctx = new ParenExpContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;

				setState(21);
				match(T__0);
				setState(22);
				expr(0);
				setState(23);
				match(T__1);
				}
				break;
			case R_VAR:
				{
				_localctx = new RefVarContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(26); 
				_errHandler.sync(this);
				_alt = 1;
				do {
					switch (_alt) {
					case 1:
						{
						{
						setState(25);
						match(R_VAR);
						}
						}
						break;
					default:
						throw new NoViableAltException(this);
					}
					setState(28); 
					_errHandler.sync(this);
					_alt = getInterpreter().adaptivePredict(_input,2,_ctx);
				} while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER );
				}
				break;
			case IF:
				{
				_localctx = new IteExpContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(30);
				ite_defi();
				}
				break;
			case INT:
				{
				_localctx = new IntVarContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(31);
				match(INT);
				}
				break;
			case BOOL:
				{
				_localctx = new BoolVarContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(32);
				match(BOOL);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			_ctx.stop = _input.LT(-1);
			setState(46);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,5,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(44);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,4,_ctx) ) {
					case 1:
						{
						_localctx = new MultExpContext(new ExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(35);
						if (!(precpred(_ctx, 5))) throw new FailedPredicateException(this, "precpred(_ctx, 5)");
						setState(36);
						_la = _input.LA(1);
						if ( !(_la==T__2 || _la==T__3) ) {
						_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(37);
						expr(6);
						}
						break;
					case 2:
						{
						_localctx = new AddExpContext(new ExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(38);
						if (!(precpred(_ctx, 4))) throw new FailedPredicateException(this, "precpred(_ctx, 4)");
						setState(39);
						_la = _input.LA(1);
						if ( !(_la==T__4 || _la==T__5) ) {
						_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(40);
						expr(5);
						}
						break;
					case 3:
						{
						_localctx = new CompExpContext(new ExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(41);
						if (!(precpred(_ctx, 3))) throw new FailedPredicateException(this, "precpred(_ctx, 3)");
						setState(42);
						_la = _input.LA(1);
						if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__6) | (1L << T__7) | (1L << T__8) | (1L << T__9) | (1L << T__10) | (1L << T__11))) != 0)) ) {
						_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(43);
						expr(4);
						}
						break;
					}
					} 
				}
				setState(48);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,5,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class F_defiContext extends ParserRuleContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public List<TerminalNode> R_VAR() { return getTokens(HaskellmmParser.R_VAR); }
		public TerminalNode R_VAR(int i) {
			return getToken(HaskellmmParser.R_VAR, i);
		}
		public F_defiContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_f_defi; }
	}

	public final F_defiContext f_defi() throws RecognitionException {
		F_defiContext _localctx = new F_defiContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_f_defi);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(50); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(49);
				match(R_VAR);
				}
				}
				setState(52); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( _la==R_VAR );
			setState(54);
			match(T__12);
			setState(55);
			expr(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Ite_defiContext extends ParserRuleContext {
		public TerminalNode IF() { return getToken(HaskellmmParser.IF, 0); }
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public TerminalNode THEN() { return getToken(HaskellmmParser.THEN, 0); }
		public TerminalNode ELSE() { return getToken(HaskellmmParser.ELSE, 0); }
		public Ite_defiContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_ite_defi; }
	}

	public final Ite_defiContext ite_defi() throws RecognitionException {
		Ite_defiContext _localctx = new Ite_defiContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_ite_defi);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(57);
			match(IF);
			setState(58);
			expr(0);
			setState(59);
			match(THEN);
			setState(60);
			expr(0);
			setState(61);
			match(ELSE);
			setState(62);
			expr(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public boolean sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 1:
			return expr_sempred((ExprContext)_localctx, predIndex);
		}
		return true;
	}
	private boolean expr_sempred(ExprContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0:
			return precpred(_ctx, 5);
		case 1:
			return precpred(_ctx, 4);
		case 2:
			return precpred(_ctx, 3);
		}
		return true;
	}

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3\30C\4\2\t\2\4\3\t"+
		"\3\4\4\t\4\4\5\t\5\3\2\3\2\7\2\r\n\2\f\2\16\2\20\13\2\7\2\22\n\2\f\2\16"+
		"\2\25\13\2\3\3\3\3\3\3\3\3\3\3\3\3\6\3\35\n\3\r\3\16\3\36\3\3\3\3\3\3"+
		"\5\3$\n\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\7\3/\n\3\f\3\16\3\62\13"+
		"\3\3\4\6\4\65\n\4\r\4\16\4\66\3\4\3\4\3\4\3\5\3\5\3\5\3\5\3\5\3\5\3\5"+
		"\3\5\2\3\4\6\2\4\6\b\2\5\3\2\5\6\3\2\7\b\3\2\t\16\2I\2\23\3\2\2\2\4#\3"+
		"\2\2\2\6\64\3\2\2\2\b;\3\2\2\2\n\16\5\6\4\2\13\r\7\23\2\2\f\13\3\2\2\2"+
		"\r\20\3\2\2\2\16\f\3\2\2\2\16\17\3\2\2\2\17\22\3\2\2\2\20\16\3\2\2\2\21"+
		"\n\3\2\2\2\22\25\3\2\2\2\23\21\3\2\2\2\23\24\3\2\2\2\24\3\3\2\2\2\25\23"+
		"\3\2\2\2\26\27\b\3\1\2\27\30\7\3\2\2\30\31\5\4\3\2\31\32\7\4\2\2\32$\3"+
		"\2\2\2\33\35\7\27\2\2\34\33\3\2\2\2\35\36\3\2\2\2\36\34\3\2\2\2\36\37"+
		"\3\2\2\2\37$\3\2\2\2 $\5\b\5\2!$\7\24\2\2\"$\7\25\2\2#\26\3\2\2\2#\34"+
		"\3\2\2\2# \3\2\2\2#!\3\2\2\2#\"\3\2\2\2$\60\3\2\2\2%&\f\7\2\2&\'\t\2\2"+
		"\2\'/\5\4\3\b()\f\6\2\2)*\t\3\2\2*/\5\4\3\7+,\f\5\2\2,-\t\4\2\2-/\5\4"+
		"\3\6.%\3\2\2\2.(\3\2\2\2.+\3\2\2\2/\62\3\2\2\2\60.\3\2\2\2\60\61\3\2\2"+
		"\2\61\5\3\2\2\2\62\60\3\2\2\2\63\65\7\27\2\2\64\63\3\2\2\2\65\66\3\2\2"+
		"\2\66\64\3\2\2\2\66\67\3\2\2\2\678\3\2\2\289\7\17\2\29:\5\4\3\2:\7\3\2"+
		"\2\2;<\7\20\2\2<=\5\4\3\2=>\7\21\2\2>?\5\4\3\2?@\7\22\2\2@A\5\4\3\2A\t"+
		"\3\2\2\2\t\16\23\36#.\60\66";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}