FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Mc2Tech.LawSuitsApi/Mc2Tech.LawSuitsApi.csproj", "Mc2Tech.LawSuitsApi/"]
RUN dotnet restore "Mc2Tech.LawSuitsApi/Mc2Tech.LawSuitsApi.csproj"
COPY . .
WORKDIR "/src/Mc2Tech.LawSuitsApi"
RUN dotnet build "Mc2Tech.LawSuitsApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mc2Tech.LawSuitsApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mc2Tech.LawSuitsApi.dll"]