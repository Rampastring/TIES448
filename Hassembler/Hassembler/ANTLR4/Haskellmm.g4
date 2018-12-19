
grammar Haskellmm;

prog:	(f_defi NEWLINE)* ;

expr:	'(' expr ')'            # parenExp
	|   expr ('*'|'/') expr     # multExp
    |	expr ('+'|'-') expr     # addExp
    |   f_name                  # fRefVar                            
    |	INT                     # intVar
    ;

f_defi: f_name '=' expr;

f_name: NAME_F_LETTER (NAME_C_LETTER)*  ;

NEWLINE : [\r\n]+ ;
INT     : [0-9]+ ;
NAME_F_LETTER : [a-z] ;
NAME_C_LETTER : [a-zA-Z0-9'] ;