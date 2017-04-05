::client 编译过程
@echo on
del /q .\output\csharp\*
protogen -i:.\input\ice_cProtol.proto -o:.\output\csharp\ice_cProtol.cs
protogen -i:.\input\ice_sProtol.proto -o:.\output\csharp\ice_sProtol.cs
pause