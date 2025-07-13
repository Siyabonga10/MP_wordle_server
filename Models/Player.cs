namespace MP_WORDLE_SERVER.Models
{
    public class Player
    {
        public int Id { get; }
        public string Username { get; }
        public bool IsHost { get; }
        public DateTime CreatedAt { get; }
        public Player(string username, bool isHost)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be null or empty", nameof(username));
            Id = new Random().Next(1000, 9999);
            Username = username;
            IsHost = isHost;
            CreatedAt = DateTime.UtcNow;
        }
    }
}