::client 编译过程
@echo on
del /q .\output\csharp\*
protogen -i:.\input\ice_cProtol.proto -o:.\output\csharp\ice_cProtol.cs
protogen -i:.\input\ice_sProtol.proto -o:.\output\csharp\ice_sProtol.cs

::server 编译过程
@echo on
del /q .\output\cpp\*
protoc --cpp_out=.\output\cpp -I=.\input\  .\input\ice_cProtol.proto
protoc --cpp_out=.\output\cpp -I=.\input\  .\input\ice_sProtol.proto

copy .\output\cpp\* ..\..\..\server\common\ICEprotocol\
pause