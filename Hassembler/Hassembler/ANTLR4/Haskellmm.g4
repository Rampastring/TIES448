
grammar Haskellmm;

prog:	(f_defi NEWLINE*)* ;

expr:	'(' expr ')'                                # parenExp
    |   R_VAR+                                      # refVar                            
    |   ite_defi                                    # iteExp
	|   expr ('*'|'/') expr                         # multExp
    |	expr ('+'|'-') expr                         # addExp
    |   expr ('<'|'<='|'=='|'!='|'>='|'>'|) expr    # compExp 
    |	INT                                         # intVar
    |   BOOL                                        # boolVar
    ;

f_defi: R_VAR+ '=' expr;

ite_defi: IF expr THEN expr ELSE expr;

IF : 'if' ;
THEN : 'then' ;
ELSE : 'else' ;
NEWLINE : [\r\n]+ ;
INT     : [0-9]+ ;
BOOL    : ('True' | 'False') ;
CHAR : ['][a-zA-Z]['] ;
R_VAR : [a-z][a-zA-Z0-9']* ;
WS : [ \n\t\r]+ -> skip ;