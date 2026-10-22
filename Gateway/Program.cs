using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Đọc bảng chỉ đường
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

builder.Services.AddCors(o => o.AddPolicy("DevCors", p =>
    p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

app.UseCors("DevCors");
app.UseWebSockets(); // bắt buộc để Ocelot chuyển tiếp WebSocket
await app.UseOcelot();

app.Run();