@ECHO OFF

REM Warning: Strip the version from the netlicensing-context.xsd file name:
REM xsd.exe uses input xsd file name as the output file name.

REM Xsd.exe ignores the schemaLocation attribute when it appears in the the
REM <import> element. Instead, for Xsd.exe any imported files are specified
REM as additional command-line arguments.
REM http://msdn.microsoft.com/en-us/library/ew6ts9yw(v=vs.100).aspx
REM Besides, URI formats are not supported.
IF NOT EXIST xmldsig-core-schema.xsd (
  ECHO "Please download http://www.w3.org/TR/xmldsig-core/xmldsig-core-schema.xsd to the current directory."
  EXIT /B 1
)

REM Using hack http://stackoverflow.com/questions/906093/xsd-exe-output-filename
"C:\Program Files (x86)\Microsoft Visual Studio 11.0\VC\vcvarsall.bat" x86 && xsd xmldsig-core-schema.xsd .\netlicensing-context.xsd /classes /language:cs
