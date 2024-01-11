#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Query/Post.Query.API/Post.Query.API.csproj", "Services/Query/Post.Query.API/"]
RUN dotnet restore "Services/Query/Post.Query.API/Post.Query.API.csproj"
COPY . .
WORKDIR "/src/Services/Query/Post.Query.API"
RUN dotnet build "Post.Query.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Post.Query.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Post.Query.API.dll"]