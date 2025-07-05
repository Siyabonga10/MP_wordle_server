using MP_WORDLE_SERVER.MP_Game;

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
    if (context.Items["Username"] != null)
    {
        var playerID = Random.Shared.Next(100000, 999999);
        context.Items["Username"] = "User" + playerID;
    }
    await next();
});
app.MapGet("/NewGame", (HttpContext context) =>
{
    int gameID = GameManager.CreateNewGame();
    GameManager.GetGame(gameID)?.AddPlayer((string?)context.Items["Username"], true);
    return new { gameID };
});

app.MapGet("/Username", (HttpContext context) => {
    return (string?)context.Items["Username"];
});

app.Run();
