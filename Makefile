all: dominate.exe

dominate.exe: src/*.cs
	dmcs -out:dominate.exe -debug src/*.cs
