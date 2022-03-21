FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build-env
WORKDIR /src
COPY . .
WORKDIR /src/Bcp.Test.Services.WebApi
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
WORKDIR /app
COPY --from=build-env /src/Bcp.Test.Services.WebApi/out ./

ENTRYPOINT ["dotnet", "Bcp.Test.Services.WebApi.dll"]