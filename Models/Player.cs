namespace MP_WORDLE_SERVER.Models
{
    internal class Player(string username, bool ishost = false, bool inGame = true)
    {
        public String Username { get; } = username;
        public bool IsHost { get; } = ishost;
        public bool InGame { get; set; } = inGame;
        public Game? CurrentGame { get; set; } = null;
    }
}