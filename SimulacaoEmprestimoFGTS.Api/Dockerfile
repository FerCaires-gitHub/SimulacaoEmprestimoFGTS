FROM mcr.microsoft.com/dotnet/sdk:3.1

COPY bin/Release/netcoreapp3.1/publish/ App/
WORKDIR /App

ENV DOTNET_EnableDiagnostics=0
EXPOSE 80/tcp
ENV ASPNETCORE_URLS http://*:80
ENTRYPOINT ["dotnet", "SimulacaoEmprestimoFGTS.Api.dll"]