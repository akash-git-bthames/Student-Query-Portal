using Microsoft.EntityFrameworkCore;
using SmartStudentQueryAPI.Data;

using SmartStudentQueryAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext (LocalDB)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=SmartStudentDB.db"));

// Register AIHelper as a typed HTTP client (it will get HttpClient and IConfiguration)
builder.Services.AddHttpClient<AIHelper>();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
