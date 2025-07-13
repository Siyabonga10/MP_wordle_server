namespace MP_WORDLE_SERVER.Models
{
    public enum GameStatus
    {
        WaitingForPlayers,
        OnGoing,
        Complete
    }
    public class Game
    {
        private readonly List<Player> _players = [];
        public int Id { get; }
        public IReadOnlyList<Player> Players => _players.AsReadOnly();
        public DateTime CreatedAt { get; }
        public GameStatus Status { get; private set; } = GameStatus.WaitingForPlayers;
        private static readonly int MAX_PLAYERS = 4;

        public Game(int id)
        {
            Id = id;
            CreatedAt = DateTime.UtcNow;
        }
        public bool AddPlayer(Player newPlayer)
        {
            if (_players.Any(player => player.Username == newPlayer.Username)) return false;

            _players.Add(newPlayer);
            return true;
        }

        public bool AddPlayer(string username, bool isHost)
        {
            if (_players.Any(player => player.Username == username)) return false;

            _players.Add(new Player(username, isHost));
            return true;
        }   

        public bool HasPlayer(Player targetPlayer)
        {
            return _players.Any(player => player.Username == targetPlayer.Username);
        }

        public bool HasPlayer(string username)
        {
            return _players.Any(player => player.Username == username);
        }
    }
}