xcopy "bin\Debug\netcoreapp3.1\Logistics.Models.dll" "lib\netcoreapp3.1" /y
nuget pack Logistics.Models.1.0.0.nuspec
nuget locals all -clear
pause