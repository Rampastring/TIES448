//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.7.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from Haskellmm.g4 by ANTLR 4.7.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7.1")]
[System.CLSCompliant(false)]
public partial class HaskellmmParser : Parser {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, NEWLINE=8, INT=9, 
		NAME_F_LETTER=10, NAME_C_LETTER=11;
	public const int
		RULE_prog = 0, RULE_expr = 1, RULE_f_defi = 2, RULE_f_name = 3;
	public static readonly string[] ruleNames = {
		"prog", "expr", "f_defi", "f_name"
	};

	private static readonly string[] _LiteralNames = {
		null, "'('", "')'", "'*'", "'/'", "'+'", "'-'", "'='"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, "NEWLINE", "INT", "NAME_F_LETTER", 
		"NAME_C_LETTER"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "Haskellmm.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string SerializedAtn { get { return new string(_serializedATN); } }

	static HaskellmmParser() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}

		public HaskellmmParser(ITokenStream input) : this(input, Console.Out, Console.Error) { }

		public HaskellmmParser(ITokenStream input, TextWriter output, TextWriter errorOutput)
		: base(input, output, errorOutput)
	{
		Interpreter = new ParserATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}
	public partial class ProgContext : ParserRuleContext {
		public F_defiContext[] f_defi() {
			return GetRuleContexts<F_defiContext>();
		}
		public F_defiContext f_defi(int i) {
			return GetRuleContext<F_defiContext>(i);
		}
		public ITerminalNode[] NEWLINE() { return GetTokens(HaskellmmParser.NEWLINE); }
		public ITerminalNode NEWLINE(int i) {
			return GetToken(HaskellmmParser.NEWLINE, i);
		}
		public ProgContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_prog; } }
		public override void EnterRule(IParseTreeListener listener) {
			IHaskellmmListener typedListener = listener as IHaskellmmListener;
			if (typedListener != null) typedListener.EnterProg(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IHaskellmmListener typedListener = listener as IHaskellmmListener;
			if (typedListener != null) typedListener.ExitProg(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHaskellmmVisitor<TResult> typedVisitor = visitor as IHaskellmmVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitProg(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ProgContext prog() {
		ProgContext _localctx = new ProgContext(Context, State);
		EnterRule(_localctx, 0, RULE_prog);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 13;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			while (_la==NAME_F_LETTER) {
				{
				{
				State = 8; f_defi();
				State = 9; Match(NEWLINE);
				}
				}
				State = 15;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ExprContext : ParserRuleContext {
		public ExprContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_expr; } }
	 
		public ExprContext() { }
		public virtual void CopyFrom(ExprContext context) {
			base.CopyFrom(context);
		}
	}
	public partial class IntVarContext : ExprContext {
		public ITerminalNode INT() { return GetToken(HaskellmmParser.INT, 0); }
		public IntVarContext(ExprContext context) { CopyFrom(context); }
		public override void EnterRule(IParseTreeListener listener) {
			IHaskellmmListener typedListener = listener as IHaskellmmListener;
			if (typedListener != null) typedListener.EnterIntVar(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IHaskellmmListener typedListener = listener as IHaskellmmListener;
			if (typedListener != null) typedListener.ExitIntVar(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHaskellmmVisitor<TResult> typedVisitor = visitor as IHaskellmmVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitIntVar(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class MultExpContext : ExprContext {
		public ExprContext[] expr() {
			return GetRuleContexts<ExprContext>();
		}
		public ExprContext expr(int i) {
			return GetRuleContext<ExprContext>(i);
		}
		public MultExpContext(ExprContext context) { CopyFrom(context); }
		public override void EnterRule(IParseTreeListener listener) {
			IHaskellmmListener typedListener = listener as IHaskellmmListener;
			if (typedListener != null) typedListener.EnterMultExp(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IHaskellmmListener typedListener = listener as IHaskellmmListener;
			if (typedListener != null) typedListener.ExitMultExp(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHaskellmmVisitor<TResult> typedVisitor = visitor as IHaskellmmVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitMultExp(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class ParenExpContext : ExprContext {
		public ExprContext expr() {
			return GetRuleContext<ExprContext>(0);
		}
		public ParenExpContext(ExprContext context) { CopyFrom(context); }
		public override void EnterRule(IParseTreeListener listener) {
			IHaskellmmListener typedListener = listener as IHaskellmmListener;
			if (typedListener != null) typedListener.EnterParenExp(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IHaskellmmListener typedListener = listener as IHaskellmmListener;
			if (typedListener != null) typedListener.ExitParenExp(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHaskellmmVisitor<TResult> typedVisitor = visitor as IHaskellmmVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitParenExp(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class AddExpContext : ExprContext {
		public ExprContext[] expr() {
			return GetRuleContexts<ExprContext>();
		}
		public ExprContext expr(int i) {
			return GetRuleContext<ExprContext>(i);
		}
		public AddExpContext(ExprContext context) { CopyFrom(context); }
		public override void EnterRule(IParseTreeListener listener) {
			IHaskellmmListener typedListener = listener as IHaskellmmListener;
			if (typedListener != null) typedListener.EnterAddExp(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IHaskellmmListener typedListener = listener as IHaskellmmListener;
			if (typedListener != null) typedListener.ExitAddExp(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHaskellmmVisitor<TResult> typedVisitor = visitor as IHaskellmmVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitAddExp(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class FRefVarContext : ExprContext {
		public F_nameContext f_name() {
			return GetRuleContext<F_nameContext>(0);
		}
		public FRefVarContext(ExprContext context) { CopyFrom(context); }
		public override void EnterRule(IParseTreeListener listener) {
			IHaskellmmListener typedListener = listener as IHaskellmmListener;
			if (typedListener != null) typedListener.EnterFRefVar(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IHaskellmmListener typedListener = listener as IHaskellmmListener;
			if (typedListener != null) typedListener.ExitFRefVar(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHaskellmmVisitor<TResult> typedVisitor = visitor as IHaskellmmVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitFRefVar(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ExprContext expr() {
		return expr(0);
	}

	private ExprContext expr(int _p) {
		ParserRuleContext _parentctx = Context;
		int _parentState = State;
		ExprContext _localctx = new ExprContext(Context, _parentState);
		ExprContext _prevctx = _localctx;
		int _startState = 2;
		EnterRecursionRule(_localctx, 2, RULE_expr, _p);
		int _la;
		try {
			int _alt;
			EnterOuterAlt(_localctx, 1);
			{
			State = 23;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case T__0:
				{
				_localctx = new ParenExpContext(_localctx);
				Context = _localctx;
				_prevctx = _localctx;

				State = 17; Match(T__0);
				State = 18; expr(0);
				State = 19; Match(T__1);
				}
				break;
			case NAME_F_LETTER:
				{
				_localctx = new FRefVarContext(_localctx);
				Context = _localctx;
				_prevctx = _localctx;
				State = 21; f_name();
				}
				break;
			case INT:
				{
				_localctx = new IntVarContext(_localctx);
				Context = _localctx;
				_prevctx = _localctx;
				State = 22; Match(INT);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			Context.Stop = TokenStream.LT(-1);
			State = 33;
			ErrorHandler.Sync(this);
			_alt = Interpreter.AdaptivePredict(TokenStream,3,Context);
			while ( _alt!=2 && _alt!=global::Antlr4.Runtime.Atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( ParseListeners!=null )
						TriggerExitRuleEvent();
					_prevctx = _localctx;
					{
					State = 31;
					ErrorHandler.Sync(this);
					switch ( Interpreter.AdaptivePredict(TokenStream,2,Context) ) {
					case 1:
						{
						_localctx = new MultExpContext(new ExprContext(_parentctx, _parentState));
						PushNewRecursionContext(_localctx, _startState, RULE_expr);
						State = 25;
						if (!(Precpred(Context, 4))) throw new FailedPredicateException(this, "Precpred(Context, 4)");
						State = 26;
						_la = TokenStream.LA(1);
						if ( !(_la==T__2 || _la==T__3) ) {
						ErrorHandler.RecoverInline(this);
						}
						else {
							ErrorHandler.ReportMatch(this);
						    Consume();
						}
						State = 27; expr(5);
						}
						break;
					case 2:
						{
						_localctx = new AddExpContext(new ExprContext(_parentctx, _parentState));
						PushNewRecursionContext(_localctx, _startState, RULE_expr);
						State = 28;
						if (!(Precpred(Context, 3))) throw new FailedPredicateException(this, "Precpred(Context, 3)");
						State = 29;
						_la = TokenStream.LA(1);
						if ( !(_la==T__4 || _la==T__5) ) {
						ErrorHandler.RecoverInline(this);
						}
						else {
							ErrorHandler.ReportMatch(this);
						    Consume();
						}
						State = 30; expr(4);
						}
						break;
					}
					} 
				}
				State = 35;
				ErrorHandler.Sync(this);
				_alt = Interpreter.AdaptivePredict(TokenStream,3,Context);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			UnrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public partial class F_defiContext : ParserRuleContext {
		public F_nameContext f_name() {
			return GetRuleContext<F_nameContext>(0);
		}
		public ExprContext expr() {
			return GetRuleContext<ExprContext>(0);
		}
		public F_defiContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_f_defi; } }
		public override void EnterRule(IParseTreeListener listener) {
			IHaskellmmListener typedListener = listener as IHaskellmmListener;
			if (typedListener != null) typedListener.EnterF_defi(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IHaskellmmListener typedListener = listener as IHaskellmmListener;
			if (typedListener != null) typedListener.ExitF_defi(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHaskellmmVisitor<TResult> typedVisitor = visitor as IHaskellmmVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitF_defi(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public F_defiContext f_defi() {
		F_defiContext _localctx = new F_defiContext(Context, State);
		EnterRule(_localctx, 4, RULE_f_defi);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 36; f_name();
			State = 37; Match(T__6);
			State = 38; expr(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class F_nameContext : ParserRuleContext {
		public ITerminalNode NAME_F_LETTER() { return GetToken(HaskellmmParser.NAME_F_LETTER, 0); }
		public ITerminalNode[] NAME_C_LETTER() { return GetTokens(HaskellmmParser.NAME_C_LETTER); }
		public ITerminalNode NAME_C_LETTER(int i) {
			return GetToken(HaskellmmParser.NAME_C_LETTER, i);
		}
		public F_nameContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_f_name; } }
		public override void EnterRule(IParseTreeListener listener) {
			IHaskellmmListener typedListener = listener as IHaskellmmListener;
			if (typedListener != null) typedListener.EnterF_name(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IHaskellmmListener typedListener = listener as IHaskellmmListener;
			if (typedListener != null) typedListener.ExitF_name(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IHaskellmmVisitor<TResult> typedVisitor = visitor as IHaskellmmVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitF_name(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public F_nameContext f_name() {
		F_nameContext _localctx = new F_nameContext(Context, State);
		EnterRule(_localctx, 6, RULE_f_name);
		try {
			int _alt;
			EnterOuterAlt(_localctx, 1);
			{
			State = 40; Match(NAME_F_LETTER);
			State = 44;
			ErrorHandler.Sync(this);
			_alt = Interpreter.AdaptivePredict(TokenStream,4,Context);
			while ( _alt!=2 && _alt!=global::Antlr4.Runtime.Atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					State = 41; Match(NAME_C_LETTER);
					}
					} 
				}
				State = 46;
				ErrorHandler.Sync(this);
				_alt = Interpreter.AdaptivePredict(TokenStream,4,Context);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public override bool Sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 1: return expr_sempred((ExprContext)_localctx, predIndex);
		}
		return true;
	}
	private bool expr_sempred(ExprContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0: return Precpred(Context, 4);
		case 1: return Precpred(Context, 3);
		}
		return true;
	}

	private static char[] _serializedATN = {
		'\x3', '\x608B', '\xA72A', '\x8133', '\xB9ED', '\x417C', '\x3BE7', '\x7786', 
		'\x5964', '\x3', '\r', '\x32', '\x4', '\x2', '\t', '\x2', '\x4', '\x3', 
		'\t', '\x3', '\x4', '\x4', '\t', '\x4', '\x4', '\x5', '\t', '\x5', '\x3', 
		'\x2', '\x3', '\x2', '\x3', '\x2', '\a', '\x2', '\xE', '\n', '\x2', '\f', 
		'\x2', '\xE', '\x2', '\x11', '\v', '\x2', '\x3', '\x3', '\x3', '\x3', 
		'\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', 
		'\x5', '\x3', '\x1A', '\n', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', 
		'\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\a', '\x3', '\"', '\n', 
		'\x3', '\f', '\x3', '\xE', '\x3', '%', '\v', '\x3', '\x3', '\x4', '\x3', 
		'\x4', '\x3', '\x4', '\x3', '\x4', '\x3', '\x5', '\x3', '\x5', '\a', '\x5', 
		'-', '\n', '\x5', '\f', '\x5', '\xE', '\x5', '\x30', '\v', '\x5', '\x3', 
		'\x5', '\x2', '\x3', '\x4', '\x6', '\x2', '\x4', '\x6', '\b', '\x2', '\x4', 
		'\x3', '\x2', '\x5', '\x6', '\x3', '\x2', '\a', '\b', '\x2', '\x33', '\x2', 
		'\xF', '\x3', '\x2', '\x2', '\x2', '\x4', '\x19', '\x3', '\x2', '\x2', 
		'\x2', '\x6', '&', '\x3', '\x2', '\x2', '\x2', '\b', '*', '\x3', '\x2', 
		'\x2', '\x2', '\n', '\v', '\x5', '\x6', '\x4', '\x2', '\v', '\f', '\a', 
		'\n', '\x2', '\x2', '\f', '\xE', '\x3', '\x2', '\x2', '\x2', '\r', '\n', 
		'\x3', '\x2', '\x2', '\x2', '\xE', '\x11', '\x3', '\x2', '\x2', '\x2', 
		'\xF', '\r', '\x3', '\x2', '\x2', '\x2', '\xF', '\x10', '\x3', '\x2', 
		'\x2', '\x2', '\x10', '\x3', '\x3', '\x2', '\x2', '\x2', '\x11', '\xF', 
		'\x3', '\x2', '\x2', '\x2', '\x12', '\x13', '\b', '\x3', '\x1', '\x2', 
		'\x13', '\x14', '\a', '\x3', '\x2', '\x2', '\x14', '\x15', '\x5', '\x4', 
		'\x3', '\x2', '\x15', '\x16', '\a', '\x4', '\x2', '\x2', '\x16', '\x1A', 
		'\x3', '\x2', '\x2', '\x2', '\x17', '\x1A', '\x5', '\b', '\x5', '\x2', 
		'\x18', '\x1A', '\a', '\v', '\x2', '\x2', '\x19', '\x12', '\x3', '\x2', 
		'\x2', '\x2', '\x19', '\x17', '\x3', '\x2', '\x2', '\x2', '\x19', '\x18', 
		'\x3', '\x2', '\x2', '\x2', '\x1A', '#', '\x3', '\x2', '\x2', '\x2', '\x1B', 
		'\x1C', '\f', '\x6', '\x2', '\x2', '\x1C', '\x1D', '\t', '\x2', '\x2', 
		'\x2', '\x1D', '\"', '\x5', '\x4', '\x3', '\a', '\x1E', '\x1F', '\f', 
		'\x5', '\x2', '\x2', '\x1F', ' ', '\t', '\x3', '\x2', '\x2', ' ', '\"', 
		'\x5', '\x4', '\x3', '\x6', '!', '\x1B', '\x3', '\x2', '\x2', '\x2', '!', 
		'\x1E', '\x3', '\x2', '\x2', '\x2', '\"', '%', '\x3', '\x2', '\x2', '\x2', 
		'#', '!', '\x3', '\x2', '\x2', '\x2', '#', '$', '\x3', '\x2', '\x2', '\x2', 
		'$', '\x5', '\x3', '\x2', '\x2', '\x2', '%', '#', '\x3', '\x2', '\x2', 
		'\x2', '&', '\'', '\x5', '\b', '\x5', '\x2', '\'', '(', '\a', '\t', '\x2', 
		'\x2', '(', ')', '\x5', '\x4', '\x3', '\x2', ')', '\a', '\x3', '\x2', 
		'\x2', '\x2', '*', '.', '\a', '\f', '\x2', '\x2', '+', '-', '\a', '\r', 
		'\x2', '\x2', ',', '+', '\x3', '\x2', '\x2', '\x2', '-', '\x30', '\x3', 
		'\x2', '\x2', '\x2', '.', ',', '\x3', '\x2', '\x2', '\x2', '.', '/', '\x3', 
		'\x2', '\x2', '\x2', '/', '\t', '\x3', '\x2', '\x2', '\x2', '\x30', '.', 
		'\x3', '\x2', '\x2', '\x2', '\a', '\xF', '\x19', '!', '#', '.',
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
