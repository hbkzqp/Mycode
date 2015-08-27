pushd %~dp0
csc /reference:System.Threading.Tasks.Dataflow.dll;Microsoft.CSharp.dll;System.Data.dll;mscorlib.dll Echo.cs
pause
 
