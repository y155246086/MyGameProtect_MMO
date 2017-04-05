del .\output\*.* /q
del ..\..\..\Assets\Resources\Configs\*.csv  /q

.\Python27\python.exe extract_excel.py ../data/excel

if exist ..\client\Assets\download\output (
	del /q  ..\client\Assets\download\output
)

copy .\output\*.* ..\..\..\Assets\_SLG\Resources\Configs
pause