﻿cd $(Get-Location)

docker-compose down
docker-compose build
docker-compose up

pause

