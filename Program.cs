using MP_WORDLE_SERVER.MP_Game;
using MP_WORDLE_SERVER.MP_Player;

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

app.MapGet("/NewGame", (HttpContext context) =>
{
    int gameID = GameManager.CreateNewGame();
    GameManager.GetGame(gameID)?.AddPlayer(context.Request.Cookies["Username"], true);
    return new { gameID };
});

app.MapGet("/Username", (HttpContext context) =>
{
    return context.Request.Cookies["Username"];
});

app.MapGet("/JoinGame", (HttpContext context) =>
{
    var gameID = (string?)context.Request.Query["gameID"];
    var username = context.Request.Cookies["Username"];
    if (gameID != null && username != null)
    {
        if (int.TryParse(gameID, out int gameID_i))
        {
            GameManager.GetGame(gameID_i)?.AddPlayer(username, false);
            Game? game = GameManager.GetGame(gameID_i);
            if (game != null)
            {
                foreach (Player player in game.Players)
                    Console.WriteLine("Player " + player.Username);
            }
            return "Sucess";
        }
    }
    return "Failed";
});

app.Run();
