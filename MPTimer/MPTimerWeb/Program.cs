using MPSourceControl;
using MPTimerAgent;
using MPTimerWeb.Hubs;
using MPTimerWeb.Services;
using MPTimerWorkspaceEvent;
using MPTimerWorkTask;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "local",
        builder =>
        {
            builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials();
        });
});
builder.Services.AddSignalR();
builder.Services.AddHostedService<DisconnectAgentsService>();
WorkTaskServices.ConfigureServices(builder.Services);
WorkspaceEventServices.ConfigureServices(builder.Services);
AgentServices.ConfigureServices(builder.Services);
SourceControlServices.ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("local");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<AgentHub>("/Agent");
});

app.MapFallbackToFile("index.html"); ;

app.Run();
