using MP_WORDLE_SERVER.MP_Player;
namespace MP_WORDLE_SERVER.MP_Game
{
    public class Game(int gameID)
    {
        readonly private List<Player> Players = [];
        public int GameId { get; } = gameID;
        public void AddPlayer(string newPlayer)
        {
            Players.Add(new Player(newPlayer));
        }
        public void RemovePlayer(string targetPlayer)
        {
            foreach (Player player in Players)
            {
                if (player.Username == targetPlayer)
                    Players.Remove(player);
            }
        }

        public string GenerateRandomWord()
        {
            return "random"; // replace with a querry into a database holding random words
        }
    }
}