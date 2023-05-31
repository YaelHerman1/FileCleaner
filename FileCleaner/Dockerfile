FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FileCleaner.csproj", "./"]
RUN dotnet restore "FileCleaner.csproj"
COPY . .
RUN dotnet build "FileCleaner.csproj" -c Release -o /app/build

FROM base AS final
WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "FileCleaner.dll"]

##See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#WORKDIR /app
#COPY FileCleaner.csproj FileCleaner.csproj
#RUN dotnet restore FileCleaner.csproj
#COPY . .
#RUN dotnet build --no-restore FileCleaner.csproj
#
##
##COPY ["FileCleaner.csproj", "FileCleaner/"]
##RUN dotnet restore "FileCleaner/FileCleaner.csproj"
##COPY . .
##RUN dotnet build "FileCleaner/FileCleaner.csproj" -c Release -o /app/build
##
##FROM build AS publish
##RUN dotnet publish "FileCleaner.csproj" -c Release -o /app/publish
#
#FROM base AS final
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "FileCleaner.dll"]