using CretX.Components;
using CretX.Extensions;
using CretX.Middleware;
using CretX.Services;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Core
builder.Services.AddHttpClient();
builder.Services.AddControllers();

// Validators
builder.Services.AddValidatorsFromAssembly(typeof(IAssemblyMarker).Assembly);

// Services
builder.Services.AddSingleton<INetworkService, NetworkService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// CretX
builder.AddCretX();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapControllers();
app.UseCretX();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapReverseProxy();
app.Run();

internal interface IAssemblyMarker;
