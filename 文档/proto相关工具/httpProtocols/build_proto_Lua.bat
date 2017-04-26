rem 切换到.proto协议所在的目录  
cd protobuf
rem 将当前文件夹中的所有协议文件转换为lua文件  
for %%i in (*.proto) do (    
echo %%i
protoc.exe --lua_out=../protolua --plugin=protoc-gen-lua="..\..\Tools\protoc-gen-lua\plugin\protoc-gen-lua.bat" %%i
)  
echo end  
pause 