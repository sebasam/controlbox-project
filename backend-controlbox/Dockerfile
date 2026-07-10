FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["BookReview.Api/BookReview.Api.csproj", "BookReview.Api/"]
COPY ["BookReview.Application/BookReview.Application.csproj", "BookReview.Application/"]
COPY ["BookReview.Domain/BookReview.Domain.csproj", "BookReview.Domain/"]
COPY ["BookReview.Infrastructure/BookReview.Infrastructure.csproj", "BookReview.Infrastructure/"]

RUN dotnet restore "BookReview.Api/BookReview.Api.csproj"

COPY . .
WORKDIR "/src/BookReview.Api"
RUN dotnet publish "BookReview.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "BookReview.Api.dll"]