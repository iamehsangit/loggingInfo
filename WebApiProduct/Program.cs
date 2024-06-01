using Serilog;
using Serilog.Events;
using WebApiProduct.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton(new ProductRepository(connectionString));

builder.Host.UseSerilog((ctx,lc) => lc
.MinimumLevel.Debug()
.MinimumLevel.Override("Microsoft",LogEventLevel.Warning)
.Enrich.FromLogContext()
.ReadFrom.Configuration(builder.Configuration)
);

var app = builder.Build();

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
