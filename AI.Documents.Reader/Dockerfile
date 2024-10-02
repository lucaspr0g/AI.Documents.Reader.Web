FROM mcr.microsoft.com/dotnet/sdk:8.0 AS restore
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["AI.Documents.Reader/AI.Documents.Reader/AI.Documents.Reader.csproj", "AI.Documents.Reader/"]
COPY ["AI.Documents.Reader/AI.Documents.Reader.Client/AI.Documents.Reader.Client.csproj", "AI.Documents.Reader.Client/"]
RUN dotnet restore "AI.Documents.Reader/AI.Documents.Reader.csproj"

# FROM node:alpine AS nodetailwind
# WORKDIR /src
COPY . .
WORKDIR /src/AI.Documents.Reader
# RUN npm run tailwind

FROM restore AS build
ARG BUILD_CONFIGURATION=Release
# WORKDIR /src
# COPY --from=nodetailwind /src .
# WORKDIR /src/AI.Documents.Reader
RUN dotnet build AI.Documents.Reader.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish AI.Documents.Reader.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AI.Documents.Reader.dll"]