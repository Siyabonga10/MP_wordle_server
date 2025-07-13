var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.Use(async (context, next) =>
{
    // TODO: Swap this whole thing out for JWT
    if (!context.Request.Cookies.ContainsKey("Username"))
    {
        var playerID = Random.Shared.Next(100000, 999999);
        context.Response.Cookies.Append("Username", "User" + playerID);
    }
    await next();
});

app.Run();
