$rg = 'helloaks-rg'
$location = 'australiaeast'
$loc = 'aue'
$registry = "helloaks$loc"

#az acr build -r $registry -t rdostr/configuration:latest src/Rdostr

az acr build -r $registry https://github.com/DanielLarsenNZ/rdostr.git -f src/Rdostr -t rdostr/configuration:latest

#az acr build -r $registry https://github.com/DanielLarsenNZ/rdostr.git -f src/Rdostr/Rdostr.Stations/Dockerfile -t rdostr/stations:latest
