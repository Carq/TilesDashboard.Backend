@echo off
SETLOCAL

SET exclude=\"[*]*.Exceptions.*,[*]*.Configuration.*,[*]*.Contract.*,[*]*.StartupConfig.*,[*]*.Storage.*,[*]*.TestUtils.*\"
 
dotnet test /p:CollectCoverage=true  /p:CoverletOutputFormat=cobertura /p:Exclude=%exclude% /p:MergeWith='/path/to/result.json'
reportgenerator -reports:tests/*.UnitTests/coverage.cobertura.xml* -reporttypes:"HTML;HTMLSummary;Cobertura;" -targetdir:%1tests/CodeCoverageReport -historydir:%1tests/CodeCoverageReportHistory -assemblyfilters:"-*.TestUtils;-*.UnitTests" 

pause

ENDLOCAL