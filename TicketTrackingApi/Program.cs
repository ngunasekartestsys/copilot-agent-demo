using TicketTrackingApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Ticket Tracking API",
        Version = "v1",
        Description = "A simple IT Ticket / Issue Tracking API built with .NET 8 Minimal API"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// In-memory data store (thread-safe)
var ticketsStore = new System.Collections.Concurrent.ConcurrentDictionary<int, Ticket>(
    new Dictionary<int, Ticket>
    {
        [1] = new Ticket
        {
            Id = 1,
            Title = "Network printer not responding",
            Description = "The printer on floor 2 is not responding to print jobs.",
            Status = TicketStatus.Open,
            Priority = TicketPriority.High,
            Category = "Hardware",
            CreatedBy = "alice@company.com",
            AssignedTo = "it-support@company.com",
            CreatedAt = DateTime.UtcNow.AddDays(-2),
            UpdatedAt = DateTime.UtcNow.AddDays(-2)
        },
        [2] = new Ticket
        {
            Id = 2,
            Title = "VPN access request",
            Description = "New employee requires VPN access for remote work.",
            Status = TicketStatus.InProgress,
            Priority = TicketPriority.Medium,
            Category = "Access Management",
            CreatedBy = "manager@company.com",
            AssignedTo = "it-support@company.com",
            CreatedAt = DateTime.UtcNow.AddDays(-1),
            UpdatedAt = DateTime.UtcNow.AddHours(-3)
        },
        [3] = new Ticket
        {
            Id = 3,
            Title = "Software installation required",
            Description = "Need Adobe Acrobat installed on workstation WS-042.",
            Status = TicketStatus.Resolved,
            Priority = TicketPriority.Low,
            Category = "Software",
            CreatedBy = "bob@company.com",
            AssignedTo = "it-support@company.com",
            CreatedAt = DateTime.UtcNow.AddDays(-5),
            UpdatedAt = DateTime.UtcNow.AddDays(-1)
        }
    });

int nextId = ticketsStore.Count + 1;

// GET /tickets - Retrieve all tickets
app.MapGet("/tickets", () =>
{
    return Results.Ok(ticketsStore.Values.OrderBy(t => t.Id));
})
.WithName("GetAllTickets")
.WithSummary("Get all tickets")
.WithDescription("Returns a list of all IT support tickets.")
.WithOpenApi();

// GET /tickets/{id} - Retrieve a ticket by ID
app.MapGet("/tickets/{id}", (int id) =>
{
    return ticketsStore.TryGetValue(id, out var ticket)
        ? Results.Ok(ticket)
        : Results.NotFound(new { message = $"Ticket with ID {id} not found." });
})
.WithName("GetTicketById")
.WithSummary("Get a ticket by ID")
.WithDescription("Returns a single ticket by its unique identifier.")
.WithOpenApi();

// GET /tickets/status/{status} - Retrieve tickets by status
app.MapGet("/tickets/status/{status}", (TicketStatus status) =>
{
    var filtered = ticketsStore.Values.Where(t => t.Status == status).OrderBy(t => t.Id).ToList();
    return Results.Ok(filtered);
})
.WithName("GetTicketsByStatus")
.WithSummary("Get tickets by status")
.WithDescription("Returns all tickets matching the specified status (Open, InProgress, OnHold, Resolved, Closed).")
.WithOpenApi();

// GET /tickets/priority/{priority} - Retrieve tickets by priority
app.MapGet("/tickets/priority/{priority}", (TicketPriority priority) =>
{
    var filtered = ticketsStore.Values.Where(t => t.Priority == priority).OrderBy(t => t.Id).ToList();
    return Results.Ok(filtered);
})
.WithName("GetTicketsByPriority")
.WithSummary("Get tickets by priority")
.WithDescription("Returns all tickets matching the specified priority (Low, Medium, High, Critical).")
.WithOpenApi();

// POST /tickets - Create a new ticket
app.MapPost("/tickets", (CreateTicketRequest request) =>
{
    var id = Interlocked.Increment(ref nextId);
    var ticket = new Ticket
    {
        Id = id,
        Title = request.Title,
        Description = request.Description,
        Priority = request.Priority,
        Category = request.Category,
        CreatedBy = request.CreatedBy,
        AssignedTo = request.AssignedTo,
        Status = TicketStatus.Open,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    };

    ticketsStore[ticket.Id] = ticket;
    return Results.Created($"/tickets/{ticket.Id}", ticket);
})
.WithName("CreateTicket")
.WithSummary("Create a new ticket")
.WithDescription("Creates a new IT support ticket and returns the created ticket.")
.WithOpenApi();

// PUT /tickets/{id} - Update an existing ticket
app.MapPut("/tickets/{id}", (int id, UpdateTicketRequest request) =>
{
    if (!ticketsStore.TryGetValue(id, out var ticket))
        return Results.NotFound(new { message = $"Ticket with ID {id} not found." });

    if (request.Title is not null) ticket.Title = request.Title;
    if (request.Description is not null) ticket.Description = request.Description;
    if (request.Status.HasValue) ticket.Status = request.Status.Value;
    if (request.Priority.HasValue) ticket.Priority = request.Priority.Value;
    if (request.Category is not null) ticket.Category = request.Category;
    if (request.AssignedTo is not null) ticket.AssignedTo = request.AssignedTo;
    ticket.UpdatedAt = DateTime.UtcNow;

    return Results.Ok(ticket);
})
.WithName("UpdateTicket")
.WithSummary("Update a ticket")
.WithDescription("Updates the fields of an existing ticket by its ID.")
.WithOpenApi();

// DELETE /tickets/{id} - Delete a ticket
app.MapDelete("/tickets/{id}", (int id) =>
{
    return ticketsStore.TryRemove(id, out _)
        ? Results.NoContent()
        : Results.NotFound(new { message = $"Ticket with ID {id} not found." });
})
.WithName("DeleteTicket")
.WithSummary("Delete a ticket")
.WithDescription("Permanently removes a ticket by its ID.")
.WithOpenApi();

app.Run();
