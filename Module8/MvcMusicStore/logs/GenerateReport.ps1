Set-ExecutionPolicy RemoteSigned
$Path = Get-Location
$SummaryLogFile = Get-ChildItem -Path $Path -Filter '*summary*' -Include "*.log" -Name
$ErrorLogFile = Get-ChildItem -Path $Path -Filter '*error*' -Include "*.log" -Name
.\LogParser.exe -i "TEXTLINE" file:totalReportScript.sql?file=$SummaryLogFile -o "CSV" -filemode 1 -headers ON
.\LogParser.exe -i "TEXTLINE" file:errorsReportScript.sql?file=$ErrorLogFile -o "CSV" -filemode 0 -headers ON