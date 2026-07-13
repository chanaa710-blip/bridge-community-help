# שלב הבנייה
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# העתקת קבצי הפרויקט ובנייה
COPY . .
RUN dotnet restore "Bridge.sln"
RUN dotnet publish "Bridge.Api/Bridge.Api.csproj" -c Release -o /app/publish

# שלב הריצה
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# הגדרת פורט קבוע ש-Render מצפה לו
ENV ASPNETCORE_URLS=http://+:10000

ENTRYPOINT ["dotnet", "Bridge.Api.dll"]