# Hassembler project
Compiler for Haskell-like language implemented for the University of Jyväskylä course TIES448 Kääntäjätekniikka (Compiler technology)

Authors
============
Milla Koivuniemi

Rami Pasanen

Jose Saarimaa

Roles
============
Rami: C# expert, Version control, Visitor class design

Jose: ANTLR4/grammar, Target language 

Milla: Testing, Haskell guru


Most of coding was done as group work.


Description
===========
Source language: Simplified Haskell

Host language: C#

Target language: WebAssembly

Properties (as of 5.2.2019):
- Interpreter
- Compiler
- Arithmetic operations (+,-,*,/)
- Line comments (denoted by --)
- Parenthesized expressions
- Types: 
  * positive integers
  * booleans (True, False)
- Functions/binding
  * with and without parameters
- Condition statements (if then else)
- Recursion
- Simple static type checking

Example of source language syntax:

f = 1+2*3

g = f + 1

h a = if a < 10 then h (a+1) else a 

Tool requirements 
=============
- .NET core (v. 2.1 or newer)
- Visual Studio 2017 / Visual Studio code

To compile
============
In project directory, run command:
> dotnet build

If you want to generate an executable, run command:
> dotnet publish -c Release -r [OS]
where [OS] is your environment, e.g. win10-x64

To use 
============
1. In Hassembler/Hassembler directory, run command:
> dotnet run
2. enter the path for source file (at this point, the file is not .hs but a regular .txt file)
 * example-code.txt is an example program
 * you may choose to compile to WebAssembly, if you do, a name for the destination file will be asked
 * see syntax examples in Description

Tests
===========

Unit tests for the interpreter are located in the folder HassemblerTests. 

Unit tests for the Compiler are located in the same folder.

A table of tests (test_plan.pdf) is located in the folder Documents.

Known deficiencies
===========

The interpreter/compiler has the following deficiencies. Tests for these deficiencies exist. 

- Due to ANTLR's capability to flexibly recover from syntax errors, some expressions (with addition or subtraction) that should result in a syntax error and an exception, will be parsed and interpreted. For example, f = 1++2 results in f == 1 and f = -2-2 results in f == -4
- whitespace around line breaks cause parsing errors