;Archivo: prueba.cpp
;Fecha: 07/11/2022 04:45:25 p. m.
#make_COM#
include emu8086.inc
ORG 100h
;Variables: 
	 area DW 0
	 radio DW 0
	 pi DW 0
	 resultado DW 0
	 a DW 0
	 d DW 0
	 altura DW 0
	 x DW 0
	 y DW 0
	 i DW 0
	 j DW 0
InicioFor0:
MOV AX, 0
PUSH AX
POP AX
MOV i, AX
MOV AX, 3
PUSH AX
POP BX
POP AX
CMP AX, BX
JGE 
POP AX
MOV AX, 3
PUSH AX
POP BX
POP AX
CMP AX, BX
JGE 
POP AX
MOV AX, 3
PUSH AX
POP BX
POP AX
CMP AX, BX
JGE 
POP AX
MOV AX, 3
PUSH AX
POP BX
POP AX
CMP AX, BX
JGE 
POP AX
FinFor0:
InicioFor1:
MOV AX, 3
PUSH AX
POP AX
MOV i, AX
MOV AX, 0
PUSH AX
POP BX
POP AX
CMP AX, BX
JLE 
POP AX
MOV AX, 0
PUSH AX
POP BX
POP AX
CMP AX, BX
JLE 
POP AX
MOV AX, 0
PUSH AX
POP BX
POP AX
CMP AX, BX
JLE 
POP AX
MOV AX, 0
PUSH AX
POP BX
POP AX
CMP AX, BX
JLE 
POP AX
FinFor1:
InicioFor2:
MOV AX, 0
PUSH AX
POP AX
MOV i, AX
MOV AX, 20
PUSH AX
POP BX
POP AX
CMP AX, BX
JGE 
MOV AX, 5
PUSH AX
POP AX
MOV AX, 20
PUSH AX
POP BX
POP AX
CMP AX, BX
JGE 
MOV AX, 5
PUSH AX
POP AX
MOV AX, 20
PUSH AX
POP BX
POP AX
CMP AX, BX
JGE 
MOV AX, 5
PUSH AX
POP AX
MOV AX, 20
PUSH AX
POP BX
POP AX
CMP AX, BX
JGE 
MOV AX, 5
PUSH AX
POP AX
MOV AX, 20
PUSH AX
POP BX
POP AX
CMP AX, BX
JGE 
MOV AX, 5
PUSH AX
POP AX
FinFor2:
InicioFor3:
MOV AX, 1
PUSH AX
POP AX
MOV i, AX
MOV AX, 6
PUSH AX
POP BX
POP AX
CMP AX, BX
JGE 
MOV AX, 2
PUSH AX
POP AX
MOV AX, 6
PUSH AX
POP BX
POP AX
CMP AX, BX
JGE 
MOV AX, 2
PUSH AX
POP AX
MOV AX, 6
PUSH AX
POP BX
POP AX
CMP AX, BX
JGE 
MOV AX, 2
PUSH AX
POP AX
MOV AX, 6
PUSH AX
POP BX
POP AX
CMP AX, BX
JGE 
MOV AX, 2
PUSH AX
POP AX
FinFor3:
MOV AX, 4
PUSH AX
POP AX
RET
