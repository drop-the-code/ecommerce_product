#!/bin/sh

set -e
   
dotnet tool install --global dotnet-ef
export PATH=$PATH:/root/.dotnet/tools
export DOTNET_ROOT=$(dirname $(realpath $(which dotnet)))

#until dotnet ef migrations add EcommerceProduct; do
#>&2 echo "SQL Server is starting up"
#sleep 1
#done

until  dotnet ef database update EcommerceProduct; do
>&2 echo "SQL Server is starting up"
sleep 1
done

>&2 echo "SQL Server is up - executing command"