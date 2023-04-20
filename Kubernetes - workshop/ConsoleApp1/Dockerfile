FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
COPY . ./src
WORKDIR /src
RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as base
COPY --from=build  /publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "DotNet.Docker.dll"]

