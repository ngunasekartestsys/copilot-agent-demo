# copilot-agent-demo

A .NET 8 Minimal API that returns sample weather forecast data.

## Project Structure

```
WeatherForecastApi/
├── Models/
│   └── WeatherForecast.cs   # Weather forecast model
├── Properties/
│   └── launchSettings.json
├── Program.cs               # Application entry point & API endpoints
├── WeatherForecastApi.csproj
├── appsettings.json
└── appsettings.Development.json
```

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Getting Started

```bash
cd WeatherForecastApi
dotnet restore
dotnet run
```

The API will start on the URLs configured in `launchSettings.json` (default: `https://localhost:7245` and `http://localhost:5231`).

## API Endpoints

| Method | Route               | Description                        |
|--------|---------------------|------------------------------------|
| GET    | /weatherforecast    | Returns 5 days of weather forecast |

### Sample Response

```json
[
  {
    "date": "2025-01-01",
    "temperatureC": 22,
    "temperatureF": 71,
    "summary": "Warm"
  }
]
```

## Swagger UI

When running in Development mode, Swagger UI is available at:

```
https://localhost:7245/swagger
```