using MP_WORDLE_SERVER.Models;

namespace MP_WORDLE_SERVER.Services
{
    internal static class GameService
    {

        readonly private static List<Game> AllGames = [];

        public static Game CreateGame(string hostUsername)
        {
            int gameId = new Random().Next(1000, 9999);
            while (true)
            {
                var matchingGame = AllGames.Find(game => game.Id == gameId);
                if (matchingGame == null)
                    break;
                gameId = new Random().Next(1000, 9999);
            }

            var newGame = new Game(gameId);
            AllGames.Add(newGame);
            AddPlayerToGame(gameId, hostUsername, isHost: true);
            return newGame;
        }

        public static bool AddPlayerToGame(int gameId, string username, bool isHost)
        {
            var targetGame = AllGames.Find(game => game.Id == gameId);
            if (targetGame == null)
                return false;

            if (!CanJoinGame(gameId)) return false;

            // Check if the player is already in the game
            if (targetGame.HasPlayer(username))
                return true; // Maybe I should return something different but the player is in there so idk

            targetGame.AddPlayer(username, isHost: isHost);
            return true;
        }

        public static Game? FindGame(int gameId)
        {
            return AllGames.Find(game => game.Id == gameId);
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