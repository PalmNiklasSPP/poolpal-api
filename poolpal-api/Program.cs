using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using poolpal_api.Database;
using poolpal_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(b =>
    {
        b.WithOrigins("http://localhost:5173") // Change this to specify allowed origins
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
        options.SerializerSettings.Formatting = Formatting.Indented;
        options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
        options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGenNewtonsoftSupport(); 
var connectionString = builder.Configuration.GetConnectionString("PoolTournamentDb");
builder.Services.AddDbContext<PoolTournamentContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAuthentication(IISDefaults.AuthenticationScheme).AddNegotiate();
builder.Services.AddMemoryCache();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.AddPolicy("RequireWindowsAuth", policy =>
    {
        policy.AddAuthenticationSchemes(IISDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
    });
});
builder.Services.AddScoped<ITournamentService, TournamentService>();
builder.Services.AddScoped<IGroupGenerationService, GroupGenerationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting(); // Add this if not already present
app.UseCors(); // CORS middleware


app.UseAuthentication(); // If using authentication, place it after CORS but before Authorization
app.UseAuthorization();

app.MapControllers(); // or your specific routing
app.Run();

