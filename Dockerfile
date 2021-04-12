FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["workoutAPI/WorkoutAPI.csproj", "workoutAPI/"]
RUN dotnet restore "workoutAPI\WorkoutAPI.csproj"
COPY . .
WORKDIR "/src/workoutAPI"
RUN dotnet build "WorkoutAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkoutAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkoutAPI.dll"]
