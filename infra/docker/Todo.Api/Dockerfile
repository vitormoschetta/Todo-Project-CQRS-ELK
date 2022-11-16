ARG  DOTNET_VERSION=6.0
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build

COPY src/ /app/
RUN dotnet publish /app/Todo.Api/Todo.Api.csproj -c Release -o /public

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION}-alpine
WORKDIR /public
COPY --from=build /public .

# Instala Postgres client, útil para o scrip 'wait_for_bootstrapping.sh'
RUN apk update && apk add postgresql-client && rm -rf /var/lib/apt/lists/*

# 'wait_for_bootstrapping' é um arquivo de scrip que segura a linha de comando até que o banco de dados esteja disponivel, 
# e só então passa para o proximo comando: /usr/bin/dotnet
ENTRYPOINT ["/usr/bin/dotnet", "/public/Todo.Api.dll"]

