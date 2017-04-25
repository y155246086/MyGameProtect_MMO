del .\output\*.* /q
del ..\..\..\Assets\Resources\Configs\*.csv  /q

.\Python27\python.exe extract_excel_2003.py ../data/excel

if exist ..\client\Assets\download\output (
	del /q  ..\client\Assets\download\output
)

copy .\output\*.* ..\..\..\Assets\Resources\Configs
pause