FROM mariadb:latest as mariadb
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR redbrowback

EXPOSE 80
EXPOSE 5024
EXPOSE 3306

#Copiar archivos de proyecto
COPY ./*.csproj ./

#Copiar todo lo demás
COPY . .

#ejecutar los procesos
RUN mysql -u root -p < /DBFiles/init.sql
RUN dotnet publish -c Release -o out

#Construccion de la imagen
FROM mariadb:lastest
FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /redbrowback
COPY --from=build /redbrowback/out .

ENTRYPOINT ["dotnet", "RedBrowBack.dll"]




