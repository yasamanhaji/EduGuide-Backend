using Base.Api.Registration;
using EduGuide.Application.Contracts.Repositories;
using EduGuide.Gateway.Registration;
using EduGuide.Gateway.SignalRHub;
using EduGuide.Infrastructure.Persistence.Seed;
using System.Reactive.Linq;

var builder = WebApplication.CreateBuilder(args);



builder.RegisterServices(builder.Configuration);
builder.Services.AddSignalR();

var app = builder.Build();

EduGuideSeed.SeedDatabase(app);
app.UseCustomMiddlewares(builder);
//app.UseRouting();
app.MapHub<ChatHub>("/chat")
        .RequireCors("chat");

app.Run();