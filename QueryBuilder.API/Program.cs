using QueryBuilder.API.Hubs;
using QueryBuilder.Application;
using QueryBuilder.Infrastucture;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddSignalR();
builder.Services.AddMediatR(config => 
{
    config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
});

var app = builder.Build();

app.MapHub<ReportsSharingHub>("reports-sharing");
app.MapHub<ReportsDataHub>("reports-data");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
