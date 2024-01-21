# game-lib_base-project

## EF Commands:
### Migration: dotnet ef migrations add Initial -p GameLib.Infrastructure\GameLib.Infrastructure.csproj -s GameLib.API\GameLib.API.csproj -o Data/Migrations
### Update Database:  dotnet ef database update -p GameLib.Infrastructure\GameLib.Infrastructure.csproj -s GameLib.API\GameLib.API.csproj

## Create Publisher Dto example:
```
{
  "name": "EA",
  "established": "2024-01-21",
  "phoneNumbers": [
    {
      "countryCode": "88",
      "number": "55555",
      "extension": "++"
    },
    {
      "countryCode": null,
      "number": "77777",
      "extension": null
    }
  ]
}
```
