$rg = 'helloaks-rg'
$location = 'australiaeast'
$loc = 'aue'
$registry = "helloaks$loc"

az acr build -r $registry https://github.com/DanielLarsenNZ/rdostr.git -f src/Rdostr/Rdostr.Configuration/Dockerfile -t rdostr/configuration:latest

az acr build -r $registry https://github.com/DanielLarsenNZ/rdostr.git -f src/Rdostr/Rdostr.Stations/Dockerfile -t rdostr/stations:latest
