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
		RULE_prog = 0, RULE_referenceExp = 1, RULE_param = 2, RULE_expr = 3, RULE_f_defi = 4, 
		RULE_ite_defi = 5;
	public static final String[] ruleNames = {
		"prog", "referenceExp", "param", "expr", "f_defi", "ite_defi"
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
			setState(21);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==R_VAR) {
				{
				{
				setState(12);
				f_defi();
				setState(16);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==NEWLINE) {
					{
					{
					setState(13);
					match(NEWLINE);
					}
					}
					setState(18);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				}
				}
				setState(23);
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

	public static class ReferenceExpContext extends ParserRuleContext {
		public TerminalNode R_VAR() { return getToken(HaskellmmParser.R_VAR, 0); }
		public List<ParamContext> param() {
			return getRuleContexts(ParamContext.class);
		}
		public ParamContext param(int i) {
			return getRuleContext(ParamContext.class,i);
		}
		public ReferenceExpContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_referenceExp; }
	}

	public final ReferenceExpContext referenceExp() throws RecognitionException {
		ReferenceExpContext _localctx = new ReferenceExpContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_referenceExp);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(24);
			match(R_VAR);
			setState(28);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,2,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(25);
					param();
					}
					} 
				}
				setState(30);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,2,_ctx);
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

	public static class ParamContext extends ParserRuleContext {
		public ParamContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_param; }
	 
		public ParamContext() { }
		public void copyFrom(ParamContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class RefVarContext extends ParamContext {
		public TerminalNode R_VAR() { return getToken(HaskellmmParser.R_VAR, 0); }
		public RefVarContext(ParamContext ctx) { copyFrom(ctx); }
	}
	public static class ExpContext extends ParamContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public ExpContext(ParamContext ctx) { copyFrom(ctx); }
	}

	public final ParamContext param() throws RecognitionException {
		ParamContext _localctx = new ParamContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_param);
		try {
			setState(33);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,3,_ctx) ) {
			case 1:
				_localctx = new RefVarContext(_localctx);
				enterOuterAlt(_localctx, 1);
				{
				setState(31);
				match(R_VAR);
				}
				break;
			case 2:
				_localctx = new ExpContext(_localctx);
				enterOuterAlt(_localctx, 2);
				{
				setState(32);
				expr(0);
				}
				break;
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
	public static class RefExpContext extends ExprContext {
		public ReferenceExpContext referenceExp() {
			return getRuleContext(ReferenceExpContext.class,0);
		}
		public RefExpContext(ExprContext ctx) { copyFrom(ctx); }
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
		int _startState = 6;
		enterRecursionRule(_localctx, 6, RULE_expr, _p);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(44);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__0:
				{
				_localctx = new ParenExpContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;

				setState(36);
				match(T__0);
				setState(37);
				expr(0);
				setState(38);
				match(T__1);
				}
				break;
			case R_VAR:
				{
				_localctx = new RefExpContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(40);
				referenceExp();
				}
				break;
			case IF:
				{
				_localctx = new IteExpContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(41);
				ite_defi();
				}
				break;
			case INT:
				{
				_localctx = new IntVarContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(42);
				match(INT);
				}
				break;
			case BOOL:
				{
				_localctx = new BoolVarContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(43);
				match(BOOL);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			_ctx.stop = _input.LT(-1);
			setState(57);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,6,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(55);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,5,_ctx) ) {
					case 1:
						{
						_localctx = new MultExpContext(new ExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(46);
						if (!(precpred(_ctx, 5))) throw new FailedPredicateException(this, "precpred(_ctx, 5)");
						setState(47);
						_la = _input.LA(1);
						if ( !(_la==T__2 || _la==T__3) ) {
						_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(48);
						expr(6);
						}
						break;
					case 2:
						{
						_localctx = new AddExpContext(new ExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(49);
						if (!(precpred(_ctx, 4))) throw new FailedPredicateException(this, "precpred(_ctx, 4)");
						setState(50);
						_la = _input.LA(1);
						if ( !(_la==T__4 || _la==T__5) ) {
						_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(51);
						expr(5);
						}
						break;
					case 3:
						{
						_localctx = new CompExpContext(new ExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(52);
						if (!(precpred(_ctx, 3))) throw new FailedPredicateException(this, "precpred(_ctx, 3)");
						setState(53);
						_la = _input.LA(1);
						if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__6) | (1L << T__7) | (1L << T__8) | (1L << T__9) | (1L << T__10) | (1L << T__11))) != 0)) ) {
						_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(54);
						expr(4);
						}
						break;
					}
					} 
				}
				setState(59);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,6,_ctx);
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
		enterRule(_localctx, 8, RULE_f_defi);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(61); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(60);
				match(R_VAR);
				}
				}
				setState(63); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( _la==R_VAR );
			setState(65);
			match(T__12);
			setState(66);
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
		enterRule(_localctx, 10, RULE_ite_defi);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(68);
			match(IF);
			setState(69);
			expr(0);
			setState(70);
			match(THEN);
			setState(71);
			expr(0);
			setState(72);
			match(ELSE);
			setState(73);
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
		case 3:
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
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3\30N\4\2\t\2\4\3\t"+
		"\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\3\2\3\2\7\2\21\n\2\f\2\16\2\24\13\2"+
		"\7\2\26\n\2\f\2\16\2\31\13\2\3\3\3\3\7\3\35\n\3\f\3\16\3 \13\3\3\4\3\4"+
		"\5\4$\n\4\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\5\5/\n\5\3\5\3\5\3\5\3\5"+
		"\3\5\3\5\3\5\3\5\3\5\7\5:\n\5\f\5\16\5=\13\5\3\6\6\6@\n\6\r\6\16\6A\3"+
		"\6\3\6\3\6\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\2\3\b\b\2\4\6\b\n\f\2\5\3\2"+
		"\5\6\3\2\7\b\3\2\t\16\2S\2\27\3\2\2\2\4\32\3\2\2\2\6#\3\2\2\2\b.\3\2\2"+
		"\2\n?\3\2\2\2\fF\3\2\2\2\16\22\5\n\6\2\17\21\7\23\2\2\20\17\3\2\2\2\21"+
		"\24\3\2\2\2\22\20\3\2\2\2\22\23\3\2\2\2\23\26\3\2\2\2\24\22\3\2\2\2\25"+
		"\16\3\2\2\2\26\31\3\2\2\2\27\25\3\2\2\2\27\30\3\2\2\2\30\3\3\2\2\2\31"+
		"\27\3\2\2\2\32\36\7\27\2\2\33\35\5\6\4\2\34\33\3\2\2\2\35 \3\2\2\2\36"+
		"\34\3\2\2\2\36\37\3\2\2\2\37\5\3\2\2\2 \36\3\2\2\2!$\7\27\2\2\"$\5\b\5"+
		"\2#!\3\2\2\2#\"\3\2\2\2$\7\3\2\2\2%&\b\5\1\2&\'\7\3\2\2\'(\5\b\5\2()\7"+
		"\4\2\2)/\3\2\2\2*/\5\4\3\2+/\5\f\7\2,/\7\24\2\2-/\7\25\2\2.%\3\2\2\2."+
		"*\3\2\2\2.+\3\2\2\2.,\3\2\2\2.-\3\2\2\2/;\3\2\2\2\60\61\f\7\2\2\61\62"+
		"\t\2\2\2\62:\5\b\5\b\63\64\f\6\2\2\64\65\t\3\2\2\65:\5\b\5\7\66\67\f\5"+
		"\2\2\678\t\4\2\28:\5\b\5\69\60\3\2\2\29\63\3\2\2\29\66\3\2\2\2:=\3\2\2"+
		"\2;9\3\2\2\2;<\3\2\2\2<\t\3\2\2\2=;\3\2\2\2>@\7\27\2\2?>\3\2\2\2@A\3\2"+
		"\2\2A?\3\2\2\2AB\3\2\2\2BC\3\2\2\2CD\7\17\2\2DE\5\b\5\2E\13\3\2\2\2FG"+
		"\7\20\2\2GH\5\b\5\2HI\7\21\2\2IJ\5\b\5\2JK\7\22\2\2KL\5\b\5\2L\r\3\2\2"+
		"\2\n\22\27\36#.9;A";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}