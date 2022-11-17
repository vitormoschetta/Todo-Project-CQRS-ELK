using Microsoft.EntityFrameworkCore;
using Todo.Api.Middlewares;
using Todo.Api.Services;
using Todo.Domain;
using Todo.Domain.Commands.Handlers;
using Todo.Domain.Contracts.Commands;
using Todo.Domain.Contracts.Events;
using Todo.Domain.Contracts.Repositories;
using Todo.Domain.Contracts.Services;
using Todo.Domain.Events.Handlers;
using Todo.Domain.Events.Notifications;
using Todo.Domain.Settings;
using Todo.Infrastructure.Database;
using Todo.Infrastructure.Database.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddSingleton<IMessageService, MessageService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITodoItemCommandHandler, TodoItemCommandHandler>();

builder.Services.AddScoped<IEventHandler<CreatedTodoItemNotification>, TodoItemEventHandler>();
builder.Services.AddScoped<IEventHandler<UpdatedTodoItemNotification>, TodoItemEventHandler>();
builder.Services.AddScoped<IEventHandler<DeletedTodoItemNotification>, TodoItemEventHandler>();
builder.Services.AddScoped<IEventHandler<MarkedAsDoneTodoItemNotification>, TodoItemEventHandler>();

builder.Services.AddScoped<IEventHandler<CreatedTodoItemNotification>, LoggingEventHandler>();
builder.Services.AddScoped<IEventHandler<UpdatedTodoItemNotification>, LoggingEventHandler>();
builder.Services.AddScoped<IEventHandler<DeletedTodoItemNotification>, LoggingEventHandler>();
builder.Services.AddScoped<IEventHandler<MarkedAsDoneTodoItemNotification>, LoggingEventHandler>();

builder.Services.AddScoped<IMediator, Mediator>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Apply Migrations
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (dataContext.Database.IsRelational() && dataContext.Database.GetPendingMigrations().Any())
        dataContext.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
