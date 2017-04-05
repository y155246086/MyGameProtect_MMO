::server 编译过程
@echo on
del /q .\output\cpp\*
protoc --cpp_out=.\output\cpp -I=.\input\  .\input\ice_cProtol.proto
protoc --cpp_out=.\output\cpp -I=.\input\  .\input\ice_sProtol.proto

copy .\output\cpp\* ..\..\..\server\common\ICEprotocol\
pause