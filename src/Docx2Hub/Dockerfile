#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0.5-focal-amd64 AS base
RUN apt-get update && apt-get install openjdk-8-jre -y && apt-get install git -y
WORKDIR /opt
RUN git clone https://github.com/transpect/docx2hub-frontend --recursive ./docx2hub && chmod +x ./docx2hub/docx2hub.sh
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0.5-focal-amd64 AS build
WORKDIR /src
COPY ["Docx2HubSvc.csproj", "./"]
RUN dotnet restore "Docx2HubSvc.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "Docx2HubSvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Docx2HubSvc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Docx2HubSvc.dll"]
