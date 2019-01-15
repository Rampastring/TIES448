
grammar Haskellmm;

prog:	(f_defi NEWLINE*)* ;

expr:	'(' expr ')'            # parenExp
	|   expr ('*'|'/') expr     # multExp
    |	expr ('+'|'-') expr     # addExp
    |   F_NAME                  # fRefVar                            
    |	INT                     # intVar
    ;

f_defi: F_NAME '=' (expr | ite_defi);

ite_defi: IF expr THEN expr ELSE expr;

IF : 'if' ;
THEN : 'then' ;
ELSE : 'else' ;
NEWLINE : [\r\n]+ ;
INT     : [0-9]+ ;
F_NAME : [a-z][a-zA-Z0-9']* ;