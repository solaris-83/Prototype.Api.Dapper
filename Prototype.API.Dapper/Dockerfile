#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY ["Prototype.API.Dapper/Prototype.API.Dapper.csproj", "Prototype.API.Dapper/"]
RUN dotnet restore "Prototype.API.Dapper/Prototype.API.Dapper.csproj"
COPY . .
WORKDIR "/src/Prototype.API.Dapper"
RUN dotnet build "Prototype.API.Dapper.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Prototype.API.Dapper.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Prototype.API.Dapper.dll"]