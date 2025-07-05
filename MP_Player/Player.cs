namespace MP_WORDLE_SERVER.MP_Player
{
    public class Player(string username, bool isHost)
    {
        public bool IsHost { get; } = isHost;
        public String Username { get; } = username;
    }
}