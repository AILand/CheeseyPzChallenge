

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS http://*:5000

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["domain/Domain.Data/Domain.Data.csproj","domain/Domain.Data/"]
COPY ["domain/Domain.Entities/Domain.Entities.csproj","domain/Domain.Entities/"]
COPY ["application/Application.Core/Application.Core.csproj","application/Application.Core/"]
COPY ["application/Application.UseCases/Application.UseCases.csproj","application/Application.UseCases/"]
COPY ["infrastructure/CheeseyPz.WebApi/CheeseyPz.WebApi.csproj","infrastructure/CheeseyPz.WebApi/"]

RUN dotnet restore "infrastructure/CheeseyPz.WebApi/CheeseyPz.WebApi.csproj"
COPY . .
WORKDIR "/src/infrastructure/CheeseyPz.WebApi/."
RUN dotnet build "CheeseyPz.WebApi.csproj"   -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CheeseyPz.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app/infrastructure/CheeseyPz.WebApi
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CheeseyPz.WebApi.dll"]



#docker build -f infrastructure\CheeseyPz.WebApi\Dockerfile -t cheese .
#docker run -p 5006:5000 cheese
