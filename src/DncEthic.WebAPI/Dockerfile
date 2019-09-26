FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["src/DncEthic.WebAPI/DncEthic.WebAPI.csproj", "src/DncEthic.WebAPI/"]
RUN dotnet restore "src/DncEthic.WebAPI/DncEthic.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/DncEthic.WebAPI"
RUN dotnet build "DncEthic.WebAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DncEthic.WebAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DncEthic.WebAPI.dll"]