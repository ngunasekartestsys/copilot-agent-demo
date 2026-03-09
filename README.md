# copilot-agent-demo

This repository contains two .NET 8 Minimal API projects:

1. **WeatherForecastApi** – Returns sample weather forecast data.
2. **TicketTrackingApi** – A full IT Ticket / Issue Tracking API with CRUD operations.

---

## TicketTrackingApi

A .NET 8 Minimal API for IT ticket and issue tracking with in-memory storage, full CRUD operations, and Swagger/OpenAPI documentation.

### Project Structure

```
TicketTrackingApi/
├── Models/
│   ├── Ticket.cs                # Ticket entity model
│   ├── TicketStatus.cs          # Enum: Open, InProgress, OnHold, Resolved, Closed
│   ├── TicketPriority.cs        # Enum: Low, Medium, High, Critical
│   ├── CreateTicketRequest.cs   # DTO for creating a ticket
│   └── UpdateTicketRequest.cs   # DTO for updating a ticket
├── Properties/
│   └── launchSettings.json
├── Program.cs                   # Application entry point & API endpoints
├── TicketTrackingApi.csproj
├── appsettings.json
└── appsettings.Development.json
```

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Getting Started

```bash
cd TicketTrackingApi
dotnet restore
dotnet run
```

The API will start on the URLs configured in `launchSettings.json` (default: `https://localhost:7100` and `http://localhost:5100`).

### API Endpoints

| Method | Route                          | Description                          |
|--------|--------------------------------|--------------------------------------|
| GET    | /tickets                       | Get all tickets                      |
| GET    | /tickets/{id}                  | Get a ticket by ID                   |
| GET    | /tickets/status/{status}       | Get tickets filtered by status       |
| GET    | /tickets/priority/{priority}   | Get tickets filtered by priority     |
| POST   | /tickets                       | Create a new ticket                  |
| PUT    | /tickets/{id}                  | Update an existing ticket            |
| DELETE | /tickets/{id}                  | Delete a ticket                      |

#### Ticket Status Values

`Open` | `InProgress` | `OnHold` | `Resolved` | `Closed`

#### Ticket Priority Values

`Low` | `Medium` | `High` | `Critical`

### Sample Request – Create Ticket

```json
POST /tickets
{
  "title": "Email client not opening",
  "description": "Outlook crashes on startup after latest Windows update.",
  "priority": "High",
  "category": "Software",
  "createdBy": "user@company.com",
  "assignedTo": "it-support@company.com"
}
```

### Sample Response – Get Ticket

```json
{
  "id": 1,
  "title": "Network printer not responding",
  "description": "The printer on floor 2 is not responding to print jobs.",
  "status": "Open",
  "priority": "High",
  "category": "Hardware",
  "createdBy": "alice@company.com",
  "assignedTo": "it-support@company.com",
  "createdAt": "2025-01-01T10:00:00Z",
  "updatedAt": "2025-01-01T10:00:00Z"
}
```

### Swagger UI

When running in Development mode, Swagger UI is available at:

```
https://localhost:7100/swagger
```

---

## WeatherForecastApi

A .NET 8 Minimal API that returns sample weather forecast data.

### Project Structure

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

### Getting Started

```bash
cd WeatherForecastApi
dotnet restore
dotnet run
```

The API will start on the URLs configured in `launchSettings.json` (default: `https://localhost:7245` and `http://localhost:5231`).

### API Endpoints

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

### Swagger UI

When running in Development mode, Swagger UI is available at:

```
https://localhost:7245/swagger
```