module FunRef where


f :: a -> b -> c
f a b = g b a

g :: b -> a -> c
g b a = a + b 


testFun = f 5 4