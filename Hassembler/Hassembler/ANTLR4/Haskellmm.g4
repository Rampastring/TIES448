
grammar Haskellmm;

prog:	(f_defi NEWLINE*)* ;

expr:	'(' expr ')'            # parenExp
    |   F_NAME                  # fRefVar                            
    |   ite_defi                # iteExp
	|   expr ('*'|'/') expr     # multExp
    |	expr ('+'|'-') expr     # addExp
    |	INT                     # intVar
    ;

f_defi: F_NAME '=' expr;

ite_defi: IF expr THEN expr ELSE expr;

IF : 'if' ;
THEN : 'then' ;
ELSE : 'else' ;
NEWLINE : [\r\n]+ ;
INT     : [0-9]+ ;
F_NAME : [a-z][a-zA-Z0-9']* ;
WS: [ \n\t\r]+ -> skip;