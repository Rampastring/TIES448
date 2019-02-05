# TIES448

Haskell compiler implemented for the University of Jyväskylä course TIES448 Kääntäjätekniikka (Compiler technology)


Kuvaus
===========
Lähdekieli: Rajattu Haskell
Kohdekieli: WebAssembly

Lähdekielenä toimii rajattu Haskell.

Ominaisuudet 5.2.2019:
- Aritmeettiset operaatiot (+,-,*,/)
- Sulkulausekkeet
- Tyypit: 
  * positiiviset kokonaisluvut
  * totuusarvot (True, False)
- Funktiot/binding
  * parametreilla ja ilman
- Valintalauseet (if then else)
- Rekursio

Esimerkki lähdekielisestä koodista:

f = 1+2*3
g = f + 1
h a = if a < 10 then h (a+1) else a 


Tunnetut puutteet
===========
- ANTLR:n anteeksiantavasta luonteesta johtuen lausekkeet, joissa on +/-, joista pitäisi tulla syntaksivirhe, menevät läpi. Esimerkiksi f = 1++2 tuottaa f == 1 ja f = -2-2 tuottaa f == -4
- rivinvaihdon ympärillä esiintyvät välilyönnit sekoittavat parsimisen
