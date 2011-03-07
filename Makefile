all: application

application: board
	dmcs dominate.cs

board:
	dmcs board.cs -target:library
