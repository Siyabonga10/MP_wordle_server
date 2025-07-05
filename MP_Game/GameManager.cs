namespace MP_WORDLE_SERVER.MP_Game
{
    public class GameManager
    {
        readonly private static List<Game> Games = [];
        readonly private static List<int> GameIDs = [];

        public static int CreateNewGame()
        {
            int newGameId;
            do
            {
                newGameId = Random.Shared.Next(1000, 9999);
            } while (GameIDs.Contains(newGameId));

            GameIDs.Add(newGameId);
            Games.Add(new Game(newGameId));
            return newGameId;
        }
    }
}