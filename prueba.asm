;Archivo: prueba.cpp
;Fecha: 20/10/2022 09:57:15 a. m.
#make_COM#
include emu8086.inc
ORG 1000h
MOV AX, 3
PUSH AX
MOV AX, 5
PUSH AX
POP AX
POP BX
ADD AX, BX
PUSH AX
MOV AX, 8
PUSH AX
POP AX
POP BX
MUL BX
PUSH AX
MOV AX, 10
PUSH AX
MOV AX, 4
PUSH AX
POP AX
POP BX
SUB AX, BX
PUSH AX
MOV AX, 2
PUSH AX
POP AX
POP BX
DIV BX
PUSH AX
POP AX
POP BX
SUB AX, BX
PUSH AX
POP AX
RET
END
