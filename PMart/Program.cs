using Microsoft.EntityFrameworkCore;
using PMart.Data;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure the database context to use SQLite with a connection string from the configuration.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlite(connectionString));

// Configure Swagger/OpenAPI for API documentation.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	// Use the developer exception page and enable Swagger in the development environment.
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI();
}
else
{
	// Configure error handling for production environment.
	app.UseExceptionHandler("/Home/Error");
	// Use HSTS (HTTP Strict Transport Security) in production.
	app.UseHsts();
}

// Redirect HTTP requests to HTTPS.
app.UseHttpsRedirection();

// Serve static files.
app.UseStaticFiles();

// Enable routing middleware.
app.UseRouting();

// Enable authorization middleware.
app.UseAuthorization();

// Map controller routes.
app.MapControllers();

// Register custom middleware
app.UseMiddleware<StatusCodeTrackingMiddleware>();

// Add Prometheus metrics middleware
app.UseHttpMetrics();
app.MapMetrics();

// Run the application.
app.Run();
