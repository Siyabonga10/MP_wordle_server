namespace MP_WORDLE_SERVER.Models
{
    internal class Game(int gameID)
    {
        public List<Player> Players { get; } = [];
        public int GameID { get; } = gameID;

        public bool ContainsPlayer(Player querryPlayer)
        {
            var targetPlayer = Players.Find(player => querryPlayer.Username == player.Username);
            return targetPlayer != null;
        }
    }
}