;Archivo: prueba.cpp
;Fecha: 10/11/2022 09:17:31 a. m.
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
PRINT ' Ingresa un numero: '
CALL SCAN_NUM
MOV a, CX
RET
define_print_num
define_print_num_uns
define_scan_num
