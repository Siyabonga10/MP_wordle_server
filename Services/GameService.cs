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

        public static Game CreateGame(string hostUsername)
        {
            int gameId = RNG.Next(10000, 99999);
            while (true)
            {
                var matchingGame = AllGames.Find(game => game.Id == gameId);
                if (matchingGame == null)
                    break;
            }

            var newGame = new Game(gameId);
            AllGames.Add(newGame);
            AddPlayerToGame(gameId, hostUsername, isHost: true);
            return newGame;
        }

        public static bool AddPlayerToGame(int gameID, string username, bool isHost)
        {
            var targetGame = AllGames.Find(game => game.Id == gameID);
            if (targetGame == null)
                return false;

            // Check if the player is already in the game
            if (targetGame.HasPlayer(username))
                return true; // Maybe I should return something different but the player is in there so idk

            targetGame.AddPlayer(username, isHost: true);
            return true;
        }

        public static Game? FindGame(int gameID)
        {
            return AllGames.Find(game => game.Id == gameID);
        }

        private static bool CanJoinGame(int gameId)
        {
            var game = AllGames.Find(game => game.Id == gameId);
            if (game == null) return false;
            if (game.Players.Count >= 4) return false;
            if (game.Status != GameStatus.WaitingForPlayers) return false;

            return true;
        }
    }
}