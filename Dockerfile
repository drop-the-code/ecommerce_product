FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ecommerce_product.csproj", "./"]
RUN dotnet restore "ecommerce_product.csproj"
COPY . .
RUN dotnet build "ecommerce_product.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ecommerce_product.csproj" -c Release -o /app/publish

FROM base AS final
FROM build AS final_2
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ecommerce_product.dll"]