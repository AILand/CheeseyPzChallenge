#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS http://*:5000

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CheeseyPz.WebApi/CheeseyPz.WebApi.csproj", "CheeseyPz.WebApi/"]
RUN dotnet restore "CheeseyPz.WebApi/CheeseyPz.WebApi.csproj"
COPY . .
WORKDIR "/src/CheeseyPz.WebApi"
RUN dotnet build "CheeseyPz.WebApi.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "CheeseyPz.WebApi.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CheeseyPz.WebApi.dll"]

#
#FROM node:14-alpine AS development
#ENV NODE_ENV development
## Add a work directory
#WORKDIR /app
## Cache and Install dependencies
#COPY package.json .
#COPY yarn.lock .
#RUN yarn install
## Copy app files
#COPY . .
## Expose port
#EXPOSE 3000
## Start the app
#CMD [ "yarn", "start" ]