
.\A2.exe test abc xyz
pause

echo N | comp HawkesBayNZ-jpg-check.bmp HawkesBayNZ-jpg-check.bmp.copy
pause

echo N | comp HawkesBayNZ-jpg-check.bmp HawkesBayNZ-jpg-seqcopy.bmp
echo N | comp HawkesBayNZ-jpg-check.bmp HawkesBayNZ-jpg-parcopy.bmp
echo N | comp HawkesBayNZ-jpg-check.bmp HawkesBayNZ-jpg-seqhisto.bmp
echo N | comp HawkesBayNZ-jpg-check.bmp HawkesBayNZ-jpg-seqgrow.bmp
echo N | comp HawkesBayNZ-jpg-check.bmp HawkesBayNZ-jpg-seqcolour.bmp
pause

HawkesBayNZ-EQ.jpg
HawkesBayNZ-EQ-jpg-seqcolour.bmp
pause

rem echo N | comp HawkesBayNZ-bmp-check.bmp HawkesBayNZ-jpg-check.bmp
rem echo N | comp HawkesBayNZ-bmp-check.bmp HawkesBayNZ-bmp-seqcopy.bmp
rem echo N | comp HawkesBayNZ-bmp-check.bmp HawkesBayNZ-bmp-parcopy.bmp
rem echo ...
rem pause
