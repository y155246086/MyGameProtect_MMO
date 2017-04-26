rem 切换到.proto协议所在的目录  
cd protobuf
rem 将当前文件夹中的所有协议文件转换为lua文件  
for %%i in (*.proto) do (    
echo %%i  
start ../../Tools/ProtoGen/protogen.exe  -i:%%i  -o:../Protocols/%%~niProtocols.cs -p:lightFramework=true
  
)  
echo end  
pause 