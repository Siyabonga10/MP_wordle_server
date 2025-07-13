using MP_WORDLE_SERVER.Models;

namespace MP_WORDLE_SERVER.Services
{
    internal static class GameService
    {
        readonly private static Random RNG;
        readonly private static List<Game> AllGames = [];
        static GameService()
        {
            RNG = new Random();
        }

        public static int NewGame()
        {
            int gameID = RNG.Next(10000, 99999);
            while (true)
            {
                var matchingGame = AllGames.Find(game => game.GameID == gameID);
                if (matchingGame == null)
                    break;
            }

            var newGame = new Game(gameID);
            AllGames.Add(newGame);
            return gameID;
        }

        public static bool AddPlayerToGame(int gameID, Player player)
        {
            var targetGame = AllGames.Find(game => game.GameID == gameID);
            if (targetGame == null)
                return false;

            // Check if the player is already in the game
            if (targetGame.ContainsPlayer(player))
                return true; // Maybe I should return something different but the player is in there so idk

            targetGame.Players.Add(player);
            return true;
        }
    }
}