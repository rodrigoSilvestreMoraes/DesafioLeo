FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /build
EXPOSE 5000
 

COPY . /build
RUN dotnet publish API/Test.DesafioLeo.csproj -c RELEASE --self-contained  -r linux-x64 -o src/out

# build runtime image
FROM mcr.microsoft.com/dotnet/runtime:5.0
RUN sed -i 's/CipherString = DEFAULT@SECLEVEL=2/CipherString = DEFAULT@SECLEVEL=1/' /etc/ssl/openssl.cnf
WORKDIR /build
COPY --from=build /build/src/out ./

ENV TZ=Brazil/East
ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "Test.DesafioLeo.dll"]